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
using System.IO;
#pragma warning disable 0168
#pragma warning disable 0618
namespace 主控端
{
    public partial class Remote_Manager : Form
    {
        String Ip;                           //被控端IP
        Main_Form MF;                        //主窗体句柄
        TcpClient Client;
        NetworkStream Ns;
        bool IsLis2Rev = true;
        bool _IsListenning2Desktop = false;  //接收桌面流标志位，默认为FLASE，当被控端激活HDC确认后改为 TRUE
        String[] Order_Set;
        Image img;
        public delegate void Pt();
        UdpClient UDP_Client;
        private int number = 0;

        public Remote_Manager(String Ip , Main_Form MF)
        {
            InitializeComponent();
            this.Ip = Ip;
            this.MF = MF;
            this.panel1.AutoSize = true;
            this.Remote_Desktop.SizeMode = PictureBoxSizeMode.AutoSize;
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
                Ns.Write(Encoding.Default.GetBytes("$ActiveHDC||"), 0, Encoding.Default.GetBytes("$ActiveHDC||").Length);
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
            while (this.IsLis2Rev)
            {
                try
                {
                    byte[] bb = new byte[1024];
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
            switch (Order_Set[0])
            {
                case "$ActiveHDC":
                    this.Check_Connect(Order_Set[1]);
                    break;
            }
        }

        /// <summary>
        /// 此方法用于根据客户端返回值
        /// 来判断是否被控端已经激活HDC
        /// </summary>
        /// <param name="Info"></param>
        public void Check_Connect(String Info)
        {
            if (Info == "True")
            {
                this._IsListenning2Desktop = true;
                //设置UDP用于接收数据
                this.Setting_UDP_Trans();
            }
            else
            {
                MessageBox.Show("抱歉，被控端HDC未激活，您不能与被控端进行屏幕查看操作!\n此窗口将会自动关闭!");
                this.Close();
            }
        }

        public void Setting_UDP_Trans()
        {
            try
            {
                int Flag = 0;
                //开启UDP端口
                //UdpClient UDP_Client = new UdpClient(9999);
                if (UDP_Client == null)
                {
                    //MessageBox.Show("FIRST TIME");
                    UDP_Client = new UdpClient(9999);
                }
                IPEndPoint ap = new IPEndPoint(IPAddress.Parse(this.Ip), 9999);

                while (this._IsListenning2Desktop)
                {
                    using (MemoryStream Ms = new MemoryStream())
                    {
                        Flag = 0;
                        try
                        {
                            while (Flag == 0)
                            {
                                byte[] bb = null;
                                bb = UDP_Client.Receive(ref ap);
                                //Ms.Flush();
                                if (Encoding.Default.GetString(bb) == "**End**")
                                {
                                    Flag = 1;
                                    //如果一张图传送完毕，则将完整流转换成Image
                                    Image img = Image.FromStream(Ms);
                                    //将Image打入到PictureBox中
                                    this.img = img;
                                    this.BeginInvoke(new Pt(this.Copy_Image));
                                }
                                else Ms.Write(bb, 0, bb.Length);
                            }
                        }
                        catch (Exception ex)
                        { };
                    }
                }
                if (UDP_Client != null)
                {
                    UDP_Client.Close();
                    //MessageBox.Show("Close UDP");
                    //if (UDP_Client != null) MessageBox.Show("NOT NULL");
                }
            }
            catch (Exception ex) 
            {
                MessageBox.Show("二次错误!  " + ex.ToString() );
            }
        }

        public void Copy_Image()
        {
            this.Remote_Desktop.Image = this.img;
        }

        private void Remote_Manager_Load(object sender, EventArgs e)
        {
            this.Try_to_Conect();
        }

        private void Remote_Manager_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.Client.Connected)
            {
                _IsListenning2Desktop = false;
                Ns.Write(Encoding.Default.GetBytes("$ExitHDC||"), 0, Encoding.Default.GetBytes("$ExitHDC||").Length);
                Ns.Flush();
                System.Threading.Thread.Sleep(10);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                Bitmap bmp = new Bitmap(Remote_Desktop.Width, Remote_Desktop.Height);
                this.Remote_Desktop.DrawToBitmap(bmp, Remote_Desktop.ClientRectangle);
                if (!Directory.Exists("Images")) Directory.CreateDirectory("Images");
                bmp.Save("Images\\" + number.ToString() + ".bmp", System.Drawing.Imaging.ImageFormat.Bmp);
                //bmp.Save("123.bmp", System.Drawing.Imaging.ImageFormat.Bmp);
                number++;
                bmp.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
