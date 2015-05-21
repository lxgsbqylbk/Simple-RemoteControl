using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Net.Sockets;

namespace 主控端
{
    public partial class Message_Manager : Form
    {
        String Ip;
        Main_Form MF;
        TcpClient Client;
        NetworkStream Ns;
        public Message_Manager(String Ip, Main_Form MF)
        {
            InitializeComponent();
            this.Ip = Ip;
            this.MF = MF;
        }
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
        public void Send_Order(NetworkStream Ns)
        {
            if (Ns != null && textBox1.Text!="")
            {
                //列举进程命令原型 ： $GetService||
                //MessageBox.Show(textBox1.Text);
                Ns.Write(Encoding.Default.GetBytes("$RMessage||" + textBox1.Text), 0, Encoding.Default.GetBytes("$RMessage||" + textBox1.Text).Length);
                Ns.Flush();
                ////如果发送请求成功则开启线程负责接收结果
                //Thread thread = new Thread(new ThreadStart(this.Get_Result));
                //thread.Start();
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Try_to_Conect();
        }
    }
}
