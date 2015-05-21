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

namespace 主控端
{
    public partial class Form1 : Form
    {
        NetworkStream Ns;
        string IP;
        TcpClient Client;
        System.Timers.Timer tm;
        UdpClient UDP_Client = new UdpClient();

        public Form1(string ip)
        {
            InitializeComponent();
            this.IP = ip;
        }

        private void SendImage(object sender, EventArgs e)
        {
            Image img = new Bitmap(Screen.AllScreens[0].Bounds.Width, Screen.AllScreens[0].Bounds.Height);
            Graphics g = Graphics.FromImage(img);
            g.CopyFromScreen(new Point(0, 0), new Point(0, 0), Screen.AllScreens[0].Bounds.Size);
            MemoryStream Ms = new MemoryStream();
            img.Save(Ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            int Len = 0;
            byte[] bb = new byte[2048];
            Ms.Position = 0;
            while ((Len = Ms.Read(bb, 0, bb.Length)) > 0)
            {
                this.UDP_Client.Send(bb, Len);
                System.Threading.Thread.Sleep(1);
            }
            //发送结尾符
            this.UDP_Client.Send(Encoding.Default.GetBytes("END"), Encoding.Default.GetBytes("END").Length);
        }
        private void GetImage()
        {
            System.Threading.Thread.Sleep(1000);
            UDP_Client.Connect(this.IP, 6666);
            //MessageBox.Show("1");
            if (this.UDP_Client.Client.Connected)
            {
                //MessageBox.Show("2");
                this.tm = new System.Timers.Timer();
                this.tm.Interval = 200;
                this.tm.Elapsed += this.SendImage;
                this.tm.Start();
                //this.SendImage();
            }
        }
        private void Try_Connect()
        {
            try
            {
                this.Client = new TcpClient();
                this.Client.Connect(this.IP, 6666);
                if (this.Client.Connected)
                {
                    this.Ns = this.Client.GetStream();
                    if (Ns != null)
                    {
                        Ns.Write(Encoding.Default.GetBytes("$Teach||"), 0, Encoding.Default.GetBytes("$Teach||").Length);
                        Ns.Flush();
                        Thread thread = new Thread(new ThreadStart(GetImage));
                        thread.Start();
                    }
                }
            }
            catch (Exception) { MessageBox.Show("connect error!"); }
        }

        private void GetCloseIF()
        {
            try
            {
                TcpListener Lis = new TcpListener(7777);
                Lis.Start();
                Socket lissocket = Lis.AcceptSocket();
                while (true)
                {
                    NetworkStream ns = new NetworkStream(lissocket);
                    byte[] bb = new byte[1024];
                    int len = ns.Read(bb, 0, bb.Length);
                    String str = System.Text.Encoding.Default.GetString(bb, 0, len);
                    //MessageBox.Show("Receive String " + str);
                    if (str == "END")
                    {
                        //MessageBox.Show("Got End Command!");
                        this.tm.Stop();
                        Lis.Stop();
                        this.Close();
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("EEEEEEEEEEEE " + ex.ToString());
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            //this.Hide();
            this.notifyIcon1.ShowBalloonTip(3000, "提醒", "正在发送屏幕演示...", System.Windows.Forms.ToolTipIcon.Info);
            Thread wfc=new Thread(new ThreadStart(GetCloseIF));
            wfc.Start();
            Try_Connect();
        }

        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                tm.Stop();
                this.Close();
            }
            catch (Exception) { MessageBox.Show("退出错误"); }
        }
    }
}
