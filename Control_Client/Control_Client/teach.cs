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

namespace Control_Client
{
    public partial class teach : Form
    {
        private bool IsReceve = true;
        Image img;
        public delegate void Pt();
        private UdpClient UDP_Client;

        public teach()
        {
            InitializeComponent();
        }
        private void Showx()
        {
            this.pictureBox1.Image = this.img;
        }
        private void GetTeach()
        {
            try
            {
                int Flag = 0;
                UDP_Client = new UdpClient(6666);
                IPEndPoint ap = new IPEndPoint(IPAddress.Any, 6666);
                while (IsReceve)
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
                                if (Encoding.Default.GetString(bb) == "END")
                                {
                                    Flag = 1;
                                    Image img = Image.FromStream(Ms);
                                    //img.Save("C:\\123.jpg");
                                    //MessageBox.Show("1111111111111");
                                    this.img = img;
                                    this.BeginInvoke(new Pt(this.Showx));
                                    //this.Show();
                                }
                                else Ms.Write(bb, 0, bb.Length);
                            }
                        }
                        catch (Exception) { }
                    }
                }
                //UDP_Client.Close();
                //MessageBox.Show("UDP CLOSE()");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Teach receive error!"+ex.ToString());
            }
        }
        private void teach_Load(object sender, EventArgs e)
        {
            Thread thread = new Thread(new ThreadStart(this.GetTeach));
            thread.Start();
        }

        private void SendEnd()
        {
            try
            {
                TcpClient TCPClient = new TcpClient();
                TCPClient.Connect(Globle.Host, 7777);
                if (TCPClient.Connected)
                {
                    NetworkStream ns = TCPClient.GetStream();
                    if (ns != null)
                    {
                        ns.Write(Encoding.Default.GetBytes("END"), 0, Encoding.Default.GetBytes("END").Length);
                        ns.Flush();
                        //MessageBox.Show("Send  END done");
                    }
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show("AAAAAAA  " + ex.ToString());
            }
        }
        private void teach_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.IsReceve = false;
            UDP_Client.Close();
            this.SendEnd();
            //System.Threading.Thread.Sleep(5);
        }
    }
}
