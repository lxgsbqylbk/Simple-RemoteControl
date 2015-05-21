using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
#pragma warning disable 0168
#pragma warning disable 0618
namespace 主控端
{
    public partial class Process_Manager : Form
    {
        String Ip;                   //被控端IP地址
        Main_Form MF;                //主窗体句柄
        TcpClient Client;
        NetworkStream Ns;
        bool _IsLis2Result = true;   //是否循环接收结果标志位
        String[] Result_List;
        public delegate void Pt();

        public Process_Manager(String Ip , Main_Form MF)
        {
            InitializeComponent();
            this.Ip = Ip;
            this.MF = MF;
            this.Text = "进程管理 - " + this.Ip; ;
        }

        private void Process_Manager_Load(object sender, EventArgs e)
        {
            this.Try_to_Conect();  //尝试连接被控端
        }

        /// <summary>
        /// 此方法尝试连接被控端
        /// 如果连接上则返回对于控制端的流句柄
        /// </summary>
        public void Try_to_Conect()
        {
            this.Client = new TcpClient();
            this.Client.Connect(this.Ip, Globle.ClientPort);
            //如果连接上了
            if (this.Client.Connected)
            {
                this.Ns = this.Client.GetStream();  //返回流控制句柄
                this.Send_Order(Ns);
            }
        }

        /// <summary>
        /// 此方法负责发送请求列举所有进程命令
        /// </summary>
        public void Send_Order(NetworkStream Ns)
        {
            if (Ns != null)
            {
                //列举进程命令原型 ： $GetProcess||
                Ns.Write(Encoding.Default.GetBytes("$GetProcess||"), 0, Encoding.Default.GetBytes("$GetProcess||").Length);
                Ns.Flush();
                //如果发送请求成功则开启线程负责接收结果
                Thread thread = new Thread(new ThreadStart(this.Get_Result));
                thread.Start();
            }
        }

        /// <summary>
        /// 此方法负责接收被控端反馈的结果
        /// 调用形式 ： [多线程]
        /// </summary>
        public void Get_Result()
        {
            while(this._IsLis2Result)
            {
                byte[] bb = new byte[2048];
                try
                {
                    int Res_Len = this.Ns.Read(bb, 0, bb.Length);
                    //得到结果分割后的数组结构体
                    String[] Result_List = Encoding.Default.GetString(bb, 0, Res_Len).Split(new String[] { "||" }, StringSplitOptions.RemoveEmptyEntries);
                    if (Result_List[0] == "$GetProcess")
                    {
                        //将全局的结果字符串赋值
                        this.Result_List = Result_List;
                        //调用委托来刷窗体
                        this.BeginInvoke(new Pt(this.reFlush_Process));
                    }

                    //如果是杀死进程反馈
                    if (Result_List[0] == "$KillProcess")
                    {
                        switch (Result_List[1])
                        {
                            case "True":
                                //如果NetWorkStream可以写入
                                if (this.Ns.CanWrite)
                                {
                                    //再次发出列进程请求，目标是刷新进程
                                    this.Send_Order(this.Ns);
                                }
                                else
                                {
                                    MessageBox.Show("已经与被控端失去了联系，请重新与被控端建立通信通道......");
                                    this.Close();
                                }
                                break;
                            case "False":
                                MessageBox.Show("杀死进程失败!");
                                break;
                        }
                    }
                }
                catch (Exception ex)
                { };
            }
        }

        /// <summary>
        /// 此方法负责详细拆分结果字符串
        /// 得到进一步数组结构体，
        /// 并且加入到前台显示中
        /// 调用形式 ： [委托]
        /// </summary>
        public void reFlush_Process()
        {
            //如果数组被赋值了
            if (this.Result_List != null)
            {
                //清空原有列表选项
                this.Process_ES.Items.Clear();
                for (int i = 1 ; i < this.Result_List.Length ; i++)
                {
                    //用逗号分割进程详细参数
                    String[] Process_Parmeter = this.Result_List[i].Split(',');
                    this.Process_ES.Items.Add(Process_Parmeter[0], Process_Parmeter[0], 0);
                    this.Process_ES.Items[Process_Parmeter[0]].SubItems.Add(Process_Parmeter[1]);
                    this.Process_ES.Items[Process_Parmeter[0]].SubItems.Add(Process_Parmeter[2]);
                }
                this.Ip_Addr.Text = " " + this.Ip + " - On Setting of Process";
                this.Process_Count.Text = " " + this.Process_ES.Items.Count.ToString();
                this.button2.Enabled = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Kill_Sp_Process();
        }

        /// <summary>
        /// 此命令用于将选中的进程名发送给被控端
        /// </summary>
        public void Kill_Sp_Process()
        {            
            //杀死进程命令 原型 ： $KillProcess||进程名
            String Kill_Process_Order = "$KillProcess||";
            //如果有选中项
            if (this.Process_ES.SelectedItems.Count > 0)
            {
                //尝试拼接杀死进程命令
                Kill_Process_Order += this.Process_ES.SelectedItems[0].Text.Trim();
                //尝试发送杀死进程请求
                this.Ns.Write(Encoding.Default.GetBytes(Kill_Process_Order), 0, Encoding.Default.GetBytes(Kill_Process_Order).Length);
                this.Ns.Flush();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //如果NetWorkStream可以写入
            if (this.Ns.CanWrite)
            {
                //再次发出列进程请求，目标是刷新进程
                this.Send_Order(this.Ns);
                this.button2.Enabled = false;
            }
            else
            {
                MessageBox.Show("已经与被控端失去了联系，请重新与被控端建立通信通道......");
                this.Close();
            }
        }

    }
}