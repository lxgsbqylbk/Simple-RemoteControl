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
    public partial class Command_Manager : Form
    {
        Main_Form MF;
        String Ip_Addr;
        TcpClient Client;
        NetworkStream Ns;
        String[] Order_Set;
        String Welcome_Message;                  //欢迎信息
        String Result_Message;                   //被控端执行命令后的返回信息
        bool IsLis2Result = true;                //循环监听标志位
        public delegate void Pt();

        public Command_Manager(String Ip_Addr ,Main_Form MF)
        {
            InitializeComponent();
            this.Ip_Addr = Ip_Addr;
            this.MF = MF;
            this.Text = "超级终端管理 On - " + this.Ip_Addr;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //清空命令框文本
            this.txt_Order.Text = "";
        }

        /// <summary>
        /// 此方法尝试连接被控端
        /// 如果连接上则返回对于控制端的流句柄
        /// </summary>
        public void Try_to_Conect()
        {
            this.Client = new TcpClient();
            this.Client.Connect(this.Ip_Addr, Globle.ClientPort);
            this.State.Text = "尝试连接被控端DOS控制台......";
            this.State.ForeColor = Color.GreenYellow;
            //如果连接上了
            if (this.Client.Connected)
            {
                this.Ns = this.Client.GetStream();  //返回流控制句柄
                this.Send_Order(Ns);
                //开启线程进行接收结果
                Thread thread = new Thread(new ThreadStart(this.Get_Result));
                thread.Start();
            }
        }

        /// <summary>
        /// 此方法负责发送请求列举所有进程命令
        /// </summary>
        public void Send_Order(NetworkStream Ns)
        {
            //得到目标主机注册表根键命令  原型 ：$$ActiveDos||
            String Get_Register_Root_Order = "$ActiveDos||";
            //尝试发送请求
            Ns.Write(Encoding.Default.GetBytes(Get_Register_Root_Order), 0, Encoding.Default.GetBytes(Get_Register_Root_Order).Length);
            Ns.Flush();
        }

        /// <summary>
        /// 此方法负责接收由被控端返回的结果
        /// </summary>
        public void Get_Result()
        {
            //循环监听
            while (this.IsLis2Result)
            {
                try
                {
                    byte[] bb = new byte[102400];
                    //读取结果
                    int Len = this.Ns.Read(bb, 0, bb.Length);
                    //得到结果字符串，并且根据特定字符拆分成数组结构体
                    String[] Order_Set = Encoding.Default.GetString(bb, 0, Len).Split(new String[] { "||" }, StringSplitOptions.RemoveEmptyEntries);
                    //try
                    //{
                    //    foreach (String s in Order_Set)
                    //    {
                    //        MessageBox.Show("Server " + s);
                    //    }
                    //}
                    //catch (Exception ex)
                    //{
                    //    MessageBox.Show("Unknwn error!");
                    //}
                    this.Order_Set = Order_Set;
                    //调用命令判断函数，进行命令分析
                    this.Order_Catcher(Order_Set);
                }
                catch (Exception ex)
                { };
            }
        }

        /// <summary>
        /// 此方法根据被控端返回信息进行判断
        /// </summary>
        /// <param name="Order_Set"></param>
        public void Order_Catcher(String[] Order_Set)
        {
            try
            {
                switch (Order_Set[0])
                {
                    //此命令头表示被控端返回的激活DOS请求的信息
                    case "$ActiveDos":
                        this.Active_Dos(Order_Set[1]);
                        break;
                    //此命令头表示被控端执行命令后的结果反馈
                    case "$ExecuteCommand":
                            if (Order_Set.Length > 1) this.Result_Message = Order_Set[1];
                            else this.Result_Message = "";
                            this.BeginInvoke(new Pt(this.Order_Infomations));
                            break;
                    default: break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR " + ex.ToString());
            }
        }

        /// <summary>
        /// 此方法判断激活DOS请求发出后的反馈信息
        /// 如果是ERROR则表示错误 
        /// 如果是其他则表示正确
        /// </summary>
        /// <param name="Info"></param>
        public void Active_Dos(String Info)
        {
            if (Info == "Error")
            {
                //如果错误了则通过委托显示在界面上
                this.BeginInvoke(new Pt(this.Error_Notify));
            }
            else
            {
                //如果不是错误则证明是被控端DOS的欢迎信息，调用委托进行刷新
                this.Welcome_Message = Info;
                //MessageBox.Show(this.Welcome_Message);
                this.BeginInvoke(new Pt(this.Welcome_Infomation));
            }
        }

        /// <summary>
        /// 此方法用于将错误信息刷新到界面上
        /// 调用方式 ： [委托]
        /// </summary>
        public void Error_Notify()
        {
            this.State.Text = "被控端系统中 cmd.exe 未找到，导致链接错误...";
            this.State.ForeColor = Color.Red;
        }

        /// <summary>
        /// 此方法用于将DOS欢迎信息显示到界面上
        /// 调用方式 ： [委托]
        /// </summary>
        public void Welcome_Infomation()
        {
            this.State.Text = "被控端系统DOS命令台连接成功!";
            this.State.ForeColor = Color.Green;
            this.Rtc_Infomations.Text += this.Welcome_Message;
            this.button3.Enabled = true;
            this.txt_Order.Enabled = true;
        }

        /// <summary>
        /// 此方法用于将执行命令的返回结果刷新到窗体上
        /// 调用方式 ： [委托]
        /// </summary>
        public void Order_Infomations()
        {

            //MessageBox.Show("Enable Message");

            this.Rtc_Infomations.Text = this.Result_Message;
            this.button3.Enabled = true;
            this.txt_Order.Enabled = true;
            this.txt_Order.Focus();
        }

        private void Command_Manager_Load(object sender, EventArgs e)
        {
            this.Try_to_Conect();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.button3.Enabled = false;
            this.txt_Order.Enabled = false;
            
            //得到用户输入的CMD命令
            String Order = this.txt_Order.Text.Trim();
            String Execute_Command = "$ExecuteCommand||";
            //如果网络流不可写，则说明连接终端
            if (!this.Ns.CanWrite)
            {
                MessageBox.Show("已经与被控端失去联系!");
                this.Close();
            }
            //否则
            else
            {
                //发送执行命令请求 原型： $ExecuteCommand|| [参数1：命令体] 
                Ns.Write(Encoding.Default.GetBytes(Execute_Command + Order), 0, Encoding.Default.GetBytes(Execute_Command + Order).Length);
                Ns.Flush();
                this.txt_Order.Text = "";
            }
        }

    }
}