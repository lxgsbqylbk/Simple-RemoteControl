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
    public partial class Service_Manager : Form
    {
        String Ip;
        Main_Form MF;
        TcpClient Client;
        NetworkStream Ns;
        bool IsLis2Rev = true;
        String[] Order_Set;
        public delegate void Pt();

        public Service_Manager(String Ip , Main_Form MF)
        {
            InitializeComponent();
            this.Ip = Ip;
            this.MF = MF;
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
                //列举进程命令原型 ： $GetService||
                Ns.Write(Encoding.Default.GetBytes("$GetService||"), 0, Encoding.Default.GetBytes("$GetService||").Length);
                Ns.Flush();
                //如果发送请求成功则开启线程负责接收结果
                Thread thread = new Thread(new ThreadStart(this.Get_Result));
                thread.Start();
            }
        }

        /// <summary>
        /// 此方法负责接收被控端命令结果
        /// 调用方式 ： [多线程]
        /// </summary>
        public void Get_Result()
        {
            //循环监听
            while(this.IsLis2Rev)
            {
                try
                {
                    byte[] bb = new byte[1024000];
                    //接受结果
                    int Read_Len = this.Ns.Read(bb, 0, bb.Length);
                    //得到结果字符串，并且分割成结果数组结构体
                    String[] Order_Set = Encoding.Default.GetString(bb, 0, Read_Len).Split(new String[] { "||" } , StringSplitOptions.RemoveEmptyEntries);
                    this.Order_Set = Order_Set;
                    //调用命令判断函数进行命令解析
                    this.Order_Catcher(Order_Set);
                }
                catch (Exception ex)
                { };
            }
        }

        /// <summary>
        /// 此方法用于判断命令头部
        /// 根据不同的命令来调用不同的方法
        /// 进行处理
        /// </summary>
        /// <param name="Order_Set"></param>
        public void Order_Catcher(String[] Order_Set)
        {
            //判断命令头部
            switch(Order_Set[0])
            {
                case "$GetService":
                    this.BeginInvoke(new Pt(this.reFlush_Service));
                    break;
            }
        }

        /// <summary>
        /// 此方法用于将接收到的结果
        /// 分割成数据固定格式
        /// 并且通过委托将数据刷新到窗体当中
        /// 调用方式 ： [委托]
        /// </summary>
        public void reFlush_Service()
        {
            //结果返回格式 ： $GetService||服务名 ， 启动状态 ， 服务描述 {..||..||..}
            for (int i = 1; i < this.Order_Set.Length; i++)
            {
                //详细分割字符串
                String[] Result_List = this.Order_Set[i].Split(',');
                //判断结果头部不为空则刷新
                if(Result_List[0] != "")
                {
                    this.Service_List.Items.Add(Result_List[0], Result_List[0], 0);
                    if (Result_List[1] == "启动中")
                    {
                        this.Service_List.Items[Result_List[0]].SubItems.Add("启动中", Color.Green, SystemColors.Control, new Font("黑体", 8));
                    }
                    else
                    {
                        this.Service_List.Items[Result_List[0]].SubItems.Add("停止中", Color.Red, SystemColors.Control, new Font("黑体", 8));
                    }
                    this.Service_List.Items[Result_List[0]].SubItems.Add(Result_List[2]);
                }
            }
        }

        private void Service_Manager_Load(object sender, EventArgs e)
        {
            this.Try_to_Conect();
        }
    }
}
