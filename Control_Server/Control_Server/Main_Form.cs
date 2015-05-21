using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Threading;
#pragma warning disable 0168
#pragma warning disable 0618
namespace 主控端
{
    public partial class Main_Form : Form
    {
        public delegate void Pt();
        TcpListener Lis;
        Socket socket;
        Type_Client tc = null;
        String Remove_Ip = "";
        //System.Timers.Timer timer1 = new System.Timers.Timer();
        public Main_Form()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 此方法用于监听上线端口
        /// </summary>
        public void Listen_Port()
        {
            Lis = new TcpListener(Globle.Port);
            this.Sys_Icon.ShowBalloonTip(5000, "端口监听成功", "端口 [" + Globle.Port + "]监听成功!\r等待主机上线......", ToolTipIcon.Info);
            while (Globle._IsListen_Port)
            {
                try
                {
                    Lis.Start();  //一直监听
                    this.Program_State.Text = "[" + Globle.Port + "]" + " 端口正在监听.";
                    this.BeginInvoke(new Pt(this.Change_Label_Color_Green));
                    //this.Change_Label_Color_Green();
                }
                catch (Exception ex)
                {
                    this.Program_State.Text = "监听端口" + Globle.Port + "失败....";
                    this.BeginInvoke(new Pt(this.Change_Label_Color_Red));
                }
                try
                {
                    this.socket = Lis.AcceptSocket();  //如果有客户端请求则创建套接字
                    while (Globle._IsResvice_Message)
                    {
                        using (NetworkStream ns = new NetworkStream(this.socket))
                        {
                            try
                            {
                                byte[] bb = new byte[1024];
                                //得到命令
                                int Res_Len = ns.Read(bb, 0, bb.Length);
                                //得到完整命令分割后的数组结构
                                String[] Order_Set = Encoding.Default.GetString(bb, 0, Res_Len).Split(new String[] { "||" }, StringSplitOptions.RemoveEmptyEntries);
                                //调用判断命令函数
                                this.Order_Catcher(Order_Set);
                            }
                            catch (Exception ex) { };
                        }
                    }
                }
                catch (Exception ex)
                {
                    //MessageBox.Show("监听成功后出现错误 : " + ex.Message);
                }
            }
        }

        /// <summary>
        /// 主窗体加载时的事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Main_Form_Load(object sender, EventArgs e)
        {
            //this.timer1.Enabled = false;
            //this.timer1.Interval = 5000;
            //this.timer1.Elapsed += this.timer1_Tick;
            //窗体加载的同时则开启端口监听 - [多线程]
            Thread thread = new Thread(new ThreadStart(this.Listen_Port));
            thread.Start();
        }

        /// <summary>
        /// 此方法用于判断命令结构
        /// 根据不同的命令调用不同的方法进行处理
        /// </summary>
        /// <param name="Order_Set"></param>
        public void Order_Catcher(String[] Order_Set)
        {
            //开始根据命令的第一个参数判断
            switch (Order_Set[0])
            {
                //上线命令
                case "$Online":
                    this.Online(Order_Set);
                    break;
                //下线命令
                case "$OffLine":
                    this.OffLine(Order_Set[1]);
                    break;
            }
        }

        /// <summary>
        /// 此功能负责主机上线的结构体参数添加
        /// 并且显示前台
        /// </summary>
        public void Online(String[] Order_Set)
        {
            Type_Client tc = new Type_Client();
            //加入IP参数
            tc.Ip = ((IPEndPoint)this.socket.RemoteEndPoint).Address.ToString(); 
            //加入其他参数
            tc.Software      = Order_Set[1];
            tc.Computer_Name = Order_Set[2];
            tc.Customer      = Order_Set[3];
            tc.System_Info   = Order_Set[4];
            tc.Cpu           = Order_Set[5];
            tc.Memory        = Order_Set[6];
            tc.Socket        = this.socket;
            if (tc.Software == "专用版本")
            {
                this.tc = tc;
                this.BeginInvoke(new Pt(this.reFlush_OnlineList));
            }

            //向被控端发送上线成功反馈
            using(NetworkStream Ns = new NetworkStream(tc.Socket))
            {
                Ns.Write(Encoding.Default.GetBytes("$Return||#Online_OK"), 0, Encoding.Default.GetBytes("$Return||#Online_OK").Length);
                Ns.Flush();
            }

            //加入上线主机个数
            Globle.Online_Number++;
            //if (this.timer1.Enabled == false)
            //{
            //    //this.timer1.Enabled = true;
            //    this.timer1.Start();
               
            //    //MessageBox.Show("timer start!");
            //    //if (this.timer1.Enabled == false) MessageBox.Show("timer error1");
            //    //this.timer1.Tick += timer1_Tick;
            //}
        }
   
        /// <summary>
        /// 此方法用于被控端下线
        /// 下线成功后循环至泛型集合，
        /// 删除其中所在对应主机
        /// </summary>
        public void OffLine(String Ip)
        {
            //删除泛型集合种的指定计算机
            for (int i = 0; i < Globle.Online_Computer_Attr.Count; i++)
            {
                if (Globle.Online_Computer_Attr[i].Ip == Ip)
                {
                    Globle.Online_Computer_Attr.Remove(Globle.Online_Computer_Attr[i]);
                    break;
                }
            }
            this.Remove_Ip = Ip;
            //删除LISTVIEW中的主机
            this.BeginInvoke(new Pt(this.Remove_ListView_Computer));
        }

        /// <summary>
        /// 删除LISTVIEW中的指定主机
        /// </summary>
        public void Remove_ListView_Computer()
        {
            //循环上线表集合
            for (int i = 0; i < this.Nomarl_Member.Items.Count; i++)
            {
                if (this.Nomarl_Member.Items[i].Text.Trim() == this.Remove_Ip.Trim())
                {
                    //消除该项
                    this.Nomarl_Member.Items[i].Remove();
                    break;
                }
            }
            Globle.Online_Number--;
            this.Customer_Online_Info.Items.Add(this.Remove_Ip, "主机 : " + this.Remove_Ip + " 下线成功 !", 0);
            this.OnLine_Counter.Text = "" + (int.Parse(this.OnLine_Counter.Text) - 1);
            //if (Globle.Online_Number == 0)
            //{
            //    this.timer1.Stop();
            //    //MessageBox.Show("timer stop!");
            //    //System.Threading.Thread.Sleep(50);
            //    //if (this.timer1.Enabled == true) MessageBox.Show("timer error 2");
            //}
        }

        /// <summary>
        /// 此事件在关闭窗体时消除一切运行中线程
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Main_Form_FormClosing(object sender, FormClosingEventArgs e)
        {
            Environment.Exit(0);
        }

        /// <summary>
        /// 此方法用于改变窗体中状态标签颜色 [红色]
        /// </summary>
        public void Change_Label_Color_Red()
        {
            this.Program_State.ForeColor = Color.Red;
        }

        /// <summary>
        /// 此方法用于改变窗体中状态标签颜色 [绿色]
        /// </summary>
        public void Change_Label_Color_Green()
        {
            this.Program_State.ForeColor = Color.Green;
        }

        /// <summary>
        /// 此方法用于向上线主机主列表中添加已经上线的主机信息
        /// 方法调用类型 [委托]
        /// </summary>
        public void reFlush_OnlineList()
        {
            bool Flag = false;  //查询是否重复上线标记

            for (int i = 0; i < Globle.Online_Computer_Attr.Count; i++)
            {
                if (Globle.Online_Computer_Attr[i].Ip == tc.Ip)
                {
                    Flag = true;
                    break;
                }    
            }

            //如果没找到则添加
            if (!Flag)
            {
                //加入上线主机集合中
                Globle.Online_Computer_Attr.Add(tc);
                this.Nomarl_Member.Items.Add(this.tc.Ip, this.tc.Ip, 0);
                this.Nomarl_Member.Items[this.tc.Ip].SubItems.Add(this.tc.Software);
                this.Nomarl_Member.Items[this.tc.Ip].SubItems.Add(this.tc.Computer_Name);
                this.Nomarl_Member.Items[this.tc.Ip].SubItems.Add(this.tc.Customer);
                this.Nomarl_Member.Items[this.tc.Ip].SubItems.Add(this.tc.System_Info);
                this.Nomarl_Member.Items[this.tc.Ip].SubItems.Add(this.tc.Cpu);
                this.Nomarl_Member.Items[this.tc.Ip].SubItems.Add(this.tc.Memory);
                //加入上线主机消息列表
                this.Customer_Online_Info.Items.Add(this.tc.Ip, "主机 : " + this.tc.Ip + "上线了 !", 1);
                if (this.tc.Customer != "")
                {
                    this.Customer_Online_Info.Items[this.tc.Ip].SubItems.Add(this.tc.Customer);
                }
                else
                {
                    this.Customer_Online_Info.Items[this.tc.Ip].SubItems.Add(" [没有备注] ");
                }
                //弹出气球消息
                this.Sys_Icon.ShowBalloonTip(5000, "有主机上线啦!", "主机 : " + tc.Ip + " 上线啦", ToolTipIcon.Info);
                this.OnLine_Counter.Text = Globle.Online_Number.ToString();
            }
        }

        private void Nomarl_Member_DoubleClick(object sender, EventArgs e)
        {
            //如果选中了某项
            if (this.Nomarl_Member.SelectedItems.Count > 0)
            {
               //根据选中的IP对上线主机集合进行查找
                String Ip = this.Nomarl_Member.SelectedItems[0].Text.Trim();
                for (int i = 0; i < Globle.Online_Computer_Attr.Count; i++)
                {
                    //如果找到了
                    if (Globle.Online_Computer_Attr[i].Ip == Ip)
                    {
                        File_Manager FM = new File_Manager(Ip, Globle.Online_Computer_Attr[i].Socket , this);
                        FM.Show();
                    }
                }

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //如果选中了某项
            if (this.Nomarl_Member.SelectedItems.Count > 0)
            {
                //根据选中的IP对上线主机集合进行查找
                String Ip = this.Nomarl_Member.SelectedItems[0].Text.Trim();
                for (int i = 0; i < Globle.Online_Computer_Attr.Count; i++)
                {
                    //如果找到了
                    if (Globle.Online_Computer_Attr[i].Ip == Ip)
                    {
                        File_Manager FM = new File_Manager(Ip, Globle.Online_Computer_Attr[i].Socket, this);
                        FM.Show();
                    }
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //如果选中了某项
            if (this.Nomarl_Member.SelectedItems.Count > 0)
            {
                //根据选中的IP对上线主机集合进行查找
                String Ip = this.Nomarl_Member.SelectedItems[0].Text.Trim();
                for (int i = 0; i < Globle.Online_Computer_Attr.Count; i++)
                {
                    //如果找到了
                    if (Globle.Online_Computer_Attr[i].Ip == Ip)
                    {
                        Process_Manager PM = new Process_Manager(Ip, this);
                        PM.Show();
                    }
                }

            }
        }

        private void Online_Computer_PopMenu_Opening(object sender, CancelEventArgs e)
        {
            //如果选中了某项
            if (this.Nomarl_Member.SelectedItems.Count <= 0)
            {
                this.File_C.Enabled = false;
                this.Process_C.Enabled = false;
                this.Reg_Con.Enabled = false;
                this.Dos_C.Enabled = false;
                this.Service_C.Enabled = false;
            }
            else
            {
                this.File_C.Enabled = true;
                this.Process_C.Enabled = true;
                this.Reg_Con.Enabled = true;
                this.Dos_C.Enabled = true;
                this.Service_C.Enabled = true;
            }
        }

        private void File_C_Click(object sender, EventArgs e)
        {
            //如果选中了某项
            if (this.Nomarl_Member.SelectedItems.Count > 0)
            {
                //根据选中的IP对上线主机集合进行查找
                String Ip = this.Nomarl_Member.SelectedItems[0].Text.Trim();
                for (int i = 0; i < Globle.Online_Computer_Attr.Count; i++)
                {
                    //如果找到了
                    if (Globle.Online_Computer_Attr[i].Ip == Ip)
                    {
                        File_Manager FM = new File_Manager(Ip, Globle.Online_Computer_Attr[i].Socket, this);
                        FM.Show();
                    }
                }
            }
        }

        private void Process_C_Click(object sender, EventArgs e)
        {
            //如果选中了某项
            if (this.Nomarl_Member.SelectedItems.Count > 0)
            {
                //根据选中的IP对上线主机集合进行查找
                String Ip = this.Nomarl_Member.SelectedItems[0].Text.Trim();
                for (int i = 0; i < Globle.Online_Computer_Attr.Count; i++)
                {
                    //如果找到了
                    if (Globle.Online_Computer_Attr[i].Ip == Ip)
                    {
                        Process_Manager PM = new Process_Manager(Ip, this);
                        PM.Show();
                    }
                }

            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
             //如果选中了某项
            if (this.Nomarl_Member.SelectedItems.Count > 0)
            {
                //根据选中的IP对上线主机集合进行查找
                String Ip = this.Nomarl_Member.SelectedItems[0].Text.Trim();
                for (int i = 0; i < Globle.Online_Computer_Attr.Count; i++)
                {
                    //如果找到了
                    if (Globle.Online_Computer_Attr[i].Ip == Ip)
                    {
                        Register_Manager RM = new Register_Manager(Ip, this);
                        RM.Show();
                    }
                }

            }
        }

        private void 注册表管理ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //如果选中了某项
            if (this.Nomarl_Member.SelectedItems.Count > 0)
            {
                //根据选中的IP对上线主机集合进行查找
                String Ip = this.Nomarl_Member.SelectedItems[0].Text.Trim();
                for (int i = 0; i < Globle.Online_Computer_Attr.Count; i++)
                {
                    //如果找到了
                    if (Globle.Online_Computer_Attr[i].Ip == Ip)
                    {
                        Register_Manager RM = new Register_Manager(Ip, this);
                        RM.Show();
                    }
                }

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //如果选中了某项
            if (this.Nomarl_Member.SelectedItems.Count > 0)
            {
                //根据选中的IP对上线主机集合进行查找
                String Ip = this.Nomarl_Member.SelectedItems[0].Text.Trim();
                for (int i = 0; i < Globle.Online_Computer_Attr.Count; i++)
                {
                    //如果找到了
                    if (Globle.Online_Computer_Attr[i].Ip == Ip)
                    {
                        Command_Manager CM = new Command_Manager(Ip, this);
                        CM.Show();
                    }
                }

            }
        }

        private void Dos_C_Click(object sender, EventArgs e)
        {
            //如果选中了某项
            if (this.Nomarl_Member.SelectedItems.Count > 0)
            {
                //根据选中的IP对上线主机集合进行查找
                String Ip = this.Nomarl_Member.SelectedItems[0].Text.Trim();
                for (int i = 0; i < Globle.Online_Computer_Attr.Count; i++)
                {
                    //如果找到了
                    if (Globle.Online_Computer_Attr[i].Ip == Ip)
                    {
                        Command_Manager CM = new Command_Manager(Ip, this);
                        CM.Show();
                    }
                }

            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            //如果选中了某项
            if (this.Nomarl_Member.SelectedItems.Count > 0)
            {
                //根据选中的IP对上线主机集合进行查找
                String Ip = this.Nomarl_Member.SelectedItems[0].Text.Trim();
                for (int i = 0; i < Globle.Online_Computer_Attr.Count; i++)
                {
                    //如果找到了
                    if (Globle.Online_Computer_Attr[i].Ip == Ip)
                    {
                        Service_Manager SM = new Service_Manager(Ip, this);
                        SM.Show();
                    }
                }

            }
        }

        private void Service_C_Click(object sender, EventArgs e)
        {
            //如果选中了某项
            if (this.Nomarl_Member.SelectedItems.Count > 0)
            {
                //根据选中的IP对上线主机集合进行查找
                String Ip = this.Nomarl_Member.SelectedItems[0].Text.Trim();
                for (int i = 0; i < Globle.Online_Computer_Attr.Count; i++)
                {
                    //如果找到了
                    if (Globle.Online_Computer_Attr[i].Ip == Ip)
                    {
                        Service_Manager SM = new Service_Manager(Ip, this);
                        SM.Show();
                    }
                }

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //如果选中了某项
            if (this.Nomarl_Member.SelectedItems.Count > 0)
            {
                //根据选中的IP对上线主机集合进行查找
                String Ip = this.Nomarl_Member.SelectedItems[0].Text.Trim();
                for (int i = 0; i < Globle.Online_Computer_Attr.Count; i++)
                {
                    //如果找到了
                    if (Globle.Online_Computer_Attr[i].Ip == Ip)
                    {
                        Remote_Manager RM = new Remote_Manager(Ip, this);
                        RM.Show();
                    }
                }

            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            //如果选中了某项
            if (this.Nomarl_Member.SelectedItems.Count > 0)
            {
                //根据选中的IP对上线主机集合进行查找
                String Ip = this.Nomarl_Member.SelectedItems[0].Text.Trim();
                for (int i = 0; i < Globle.Online_Computer_Attr.Count; i++)
                {
                    //如果找到了
                    if (Globle.Online_Computer_Attr[i].Ip == Ip)
                    {
                        Message_Manager MM = new Message_Manager(Ip, this);
                        MM.Show();
                    }
                }

            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //MessageBox.Show("In Timer...");
            for (int i = 0; i < Globle.Online_Computer_Attr.Count; i++)
            {
                TcpClient Client = new TcpClient();
                try
                {
                    Client.Connect(Globle.Online_Computer_Attr[i].Ip.Trim(), Globle.ClientPort);
                }
                catch (Exception)
                {
                    this.OffLine(Globle.Online_Computer_Attr[i].Ip.Trim());
                }
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            //如果选中了某项
            if (this.Nomarl_Member.SelectedItems.Count > 0)
            {
                //根据选中的IP对上线主机集合进行查找
                String Ip = this.Nomarl_Member.SelectedItems[0].Text.Trim();
                for (int i = 0; i < Globle.Online_Computer_Attr.Count; i++)
                {
                    //如果找到了
                    if (Globle.Online_Computer_Attr[i].Ip == Ip)
                    {
                        Form1 fm = new Form1(Ip);
                        fm.Show();
                    }
                }

            }
        }
    }
}
