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
    public partial class Register_Manager : Form
    {
        String Ip_Addr;                //被控端IP地址
        Main_Form MF;                  //主窗口句柄
        TcpClient Client;              //与被控端连接的句柄
        NetworkStream Ns;              //控制被控端网络流的句柄
        TreeNode TN;                   //当前树状图选择的节点
        String[] Order_Set;               //用于保存结果字符串
        bool IsLis2Result = true;
        public delegate void Pt();

        public Register_Manager(String Ip_Addr , Main_Form MF)
        {
            InitializeComponent();
            this.Ip_Addr = Ip_Addr;
            this.MF = MF;
            this.Text = "注册表管理 - On : " + this.Ip_Addr;
        }

        /// <summary>
        /// 此方法尝试连接被控端
        /// 如果连接上则返回对于控制端的流句柄
        /// </summary>
        public void Try_to_Conect()
        {
            this.Client = new TcpClient();
            this.Client.Connect(this.Ip_Addr, Globle.ClientPort);
            //如果连接上了
            if (this.Client.Connected)
            {
                this.Ns = this.Client.GetStream();  //返回流控制句柄
                //开启线程进行接收结果
                Thread thread = new Thread(new ThreadStart(this.Get_Result));
                thread.Start();
            }
        }

        /// <summary>
        /// 此方法负责发送请求列举所有进程命令
        /// </summary>
        public void Send_Order(NetworkStream Ns , String Reg_Path)
        {
            //得到目标主机注册表根键命令  原型 ：$GetRegisterRoot|| [路径]
            String Get_Register_Root_Order = "$GetRegisterRoot||" + Reg_Path;
            //尝试发送请求
            Ns.Write(Encoding.Default.GetBytes(Get_Register_Root_Order), 0, Encoding.Default.GetBytes(Get_Register_Root_Order).Length);
            Ns.Flush();
        }

        private void Register_Manager_Load(object sender, EventArgs e)
        {
            this.Try_to_Conect();
        }

        private void Reg_Tree_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            String Root_Name = "";
            TreeNode Root_Node;
            this.TN = e.Node;
            //取得选择节点的名字
            String Sel_Text = this.TN.Text.Trim();
            Root_Node = this.TN;
            while (Root_Node.Parent != null)
            {
                Root_Name = Root_Node.Parent.Text;
                Root_Node = Root_Node.Parent;

            }
            switch (Sel_Text)
            {
                case "HKEY_CLASSES_ROOT":
                //如果是HKEY_CURRENT_CONFIG
                case "HKEY_CURRENT_CONFIG":
                //如果是HKEY_CURRENT_USER
                case "HKEY_CURRENT_USER":
                //如果是HKEY_LOCAL_MACHINE
                case "HKEY_LOCAL_MACHINE":
                //如果是HKEY_USERS
                case "HKEY_USERS":
                    if (this.Ns.CanWrite)
                    {
                        //尝试列举根键
                        this.Send_Order(Ns, Sel_Text + "||******%None%******");
                        //尝试列举子项所有值 原型 ： $GetRegisterRootValues|| [项路径]
                        this.Ns.Write(Encoding.Default.GetBytes("$GetRegisterRootValues||" + Root_Name + "||******%None%******"), 0, Encoding.Default.GetBytes("$GetRegisterRootValues||" + Root_Name + "||******%None%******").Length);
                        this.Ns.Flush();
                    }
                    else
                    {
                        MessageBox.Show("已经与被控端失去了联系，请重新与被控端建立通信通道......");
                        this.Close();
                    }
                    break;
                default :
                    if (this.Ns.CanWrite)
                    {
                        try
                        {
                            Root_Node = this.TN;
                            String Ta = this.TN.Tag.ToString();
                            while (Root_Node.Parent != null)
                            {
                                Ta = Root_Node.Parent.Text + "\\" + Ta;
                                Root_Node = Root_Node.Parent;

                            }
                            String Ok_Ta = Ta.Substring(Ta.IndexOf('\\') + 1, Ta.Length - ((Ta.IndexOf('\\') + 1)));
                            //尝试列举根键
                            this.Send_Order(Ns, Root_Name + "||" + Ok_Ta);
                            //尝试列举子项所有值 原型 ： $GetRegisterRootValues|| [项路径]
                            this.Ns.Write(Encoding.Default.GetBytes("$GetRegisterRootValues||" + Root_Name + "||" + Ok_Ta), 0, Encoding.Default.GetBytes("$GetRegisterRootValues||" + Root_Name + "||" + Ok_Ta).Length);
                            this.Ns.Flush();
                        }
                        catch (Exception ex)
                        { };
                    }
                    else
                    {
                        MessageBox.Show("已经与被控端失去了联系，请重新与被控端建立通信通道......");
                        this.Close();
                    }
                    break;
            }
        }

        /// <summary>
        /// 此方法负责接收由被控端返回的结果
        /// </summary>
        public void Get_Result()
        {
            //循环监听
            while(this.IsLis2Result)
            {
                try
                {
                    byte[] bb = new byte[10240000];
                    //读取结果
                    int Len = this.Ns.Read(bb, 0, bb.Length);
                    //得到结果字符串，并且根据特定字符拆分成数组结构体
                    String[] Order_Set = Encoding.Default.GetString(bb, 0, Len).Split(new String[] { "||" }, StringSplitOptions.RemoveEmptyEntries);
                    this.Order_Set = Order_Set;
                    //调用命令判断函数，进行命令分析
                    this.Order_Catcher(Order_Set);
                }
                catch (Exception ex)
                { };
            }
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
                case "$GetRegisterRoot":
                    this.BeginInvoke(new Pt(this.reFlush_Register));
                    break;
                case "$GetRegisterRootValues":
                    this.BeginInvoke(new Pt(this.reFlush_RegisterValues));
                    break; 
            }
        }

        /// <summary>
        /// 此方法用来把当前接收的数据结构
        /// 刷新到界面上去
        /// 调用方式 ：[委托]
        /// </summary>
        public void reFlush_Register()
        {
            this.TN.Nodes.Clear();
            for (int i = 1; i < this.Order_Set.Length; i++)
            {
                try
                {
                    //添加子节点
                    this.TN.Nodes.Add(this.Order_Set[i], this.Order_Set[i], 1);
                    //添加节点 Tag
                    this.TN.Nodes[this.Order_Set[i]].Tag = this.Order_Set[i] + "\\";
                }
                catch (Exception ex)
                { };
            }
            this.TN.Expand();
        }

        /// <summary>
        /// 此方法用来把当前接收的数据结构
        /// 刷新到界面的ListItem上去
        /// 调用方式 ：[委托]
        /// </summary>
        public void reFlush_RegisterValues()
        {
            //清空LISTVIEW种的项，用来重新添加
            this.listView1.Items.Clear();

            for (int i = 1; i < this.Order_Set.Length; i++)
            {
                String[] Result = this.Order_Set[i].Split(new String[] { "##" }, StringSplitOptions.RemoveEmptyEntries);
                try
                {
                    //添加项
                    this.listView1.Items.Add(Result[0], Result[0], 0); 
                    //添加节点 Tag
                    this.listView1.Items[Result[0]].SubItems.Add("String");
                    this.listView1.Items[Result[0]].SubItems.Add(Result[1]);
                }
                catch (Exception ex)
                { };
            }
        }
    }
}