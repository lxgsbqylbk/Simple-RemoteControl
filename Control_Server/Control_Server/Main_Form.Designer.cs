namespace 主控端
{
    partial class Main_Form
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main_Form));
            this.panel1 = new System.Windows.Forms.Panel();
            this.button8 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.Online_Computer_PopMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.File_C = new System.Windows.Forms.ToolStripMenuItem();
            this.Process_C = new System.Windows.Forms.ToolStripMenuItem();
            this.Reg_Con = new System.Windows.Forms.ToolStripMenuItem();
            this.Dos_C = new System.Windows.Forms.ToolStripMenuItem();
            this.Service_C = new System.Windows.Forms.ToolStripMenuItem();
            this.Remote_C = new System.Windows.Forms.ToolStripMenuItem();
            this.Small_Icon_ImageList = new System.Windows.Forms.ImageList(this.components);
            this.tabControl2 = new System.Windows.Forms.TabControl();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.Customer_Online_Info = new System.Windows.Forms.ListView();
            this.Ip_Info = new System.Windows.Forms.ColumnHeader();
            this.Marks = new System.Windows.Forms.ColumnHeader();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.listView2 = new System.Windows.Forms.ListView();
            this.columnHeader17 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader18 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader19 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader20 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader21 = new System.Windows.Forms.ColumnHeader();
            this.Sys_Icon = new System.Windows.Forms.NotifyIcon(this.components);
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.Program_State = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel3 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel4 = new System.Windows.Forms.ToolStripStatusLabel();
            this.OnLine_Counter = new System.Windows.Forms.ToolStripStatusLabel();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.Nomarl_Member = new System.Windows.Forms.ListView();
            this.IP = new System.Windows.Forms.ColumnHeader();
            this.Software = new System.Windows.Forms.ColumnHeader();
            this.Computer_Name = new System.Windows.Forms.ColumnHeader();
            this.Customer = new System.Windows.Forms.ColumnHeader();
            this.System_Name = new System.Windows.Forms.ColumnHeader();
            this.CPU = new System.Windows.Forms.ColumnHeader();
            this.Memory = new System.Windows.Forms.ColumnHeader();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.panel1.SuspendLayout();
            this.Online_Computer_PopMenu.SuspendLayout();
            this.tabControl2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.button8);
            this.panel1.Controls.Add(this.button4);
            this.panel1.Controls.Add(this.button7);
            this.panel1.Controls.Add(this.button6);
            this.panel1.Controls.Add(this.button5);
            this.panel1.Controls.Add(this.button3);
            this.panel1.Controls.Add(this.button2);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(894, 81);
            this.panel1.TabIndex = 1;
            // 
            // button8
            // 
            this.button8.BackColor = System.Drawing.Color.White;
            this.button8.Image = global::Server.Properties.Resources.Skin_1_Process;
            this.button8.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.button8.Location = new System.Drawing.Point(444, 3);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(63, 75);
            this.button8.TabIndex = 7;
            this.button8.Text = "屏幕演示";
            this.button8.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.button8.UseVisualStyleBackColor = false;
            this.button8.Click += new System.EventHandler(this.button9_Click);
            // 
            // button4
            // 
            this.button4.BackColor = System.Drawing.Color.White;
            this.button4.Image = global::Server.Properties.Resources.Skin_1_Window;
            this.button4.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.button4.Location = new System.Drawing.Point(3, 3);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(63, 75);
            this.button4.TabIndex = 7;
            this.button4.Text = "发送广播";
            this.button4.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.button4.UseVisualStyleBackColor = false;
            this.button4.Click += new System.EventHandler(this.button8_Click);
            // 
            // button7
            // 
            this.button7.BackColor = System.Drawing.Color.White;
            this.button7.Image = ((System.Drawing.Image)(resources.GetObject("button7.Image")));
            this.button7.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.button7.Location = new System.Drawing.Point(318, 3);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(63, 75);
            this.button7.TabIndex = 6;
            this.button7.Text = "服务管理";
            this.button7.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.button7.UseVisualStyleBackColor = false;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // button6
            // 
            this.button6.BackColor = System.Drawing.Color.White;
            this.button6.Image = ((System.Drawing.Image)(resources.GetObject("button6.Image")));
            this.button6.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.button6.Location = new System.Drawing.Point(255, 3);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(63, 75);
            this.button6.TabIndex = 5;
            this.button6.Text = "注册管理";
            this.button6.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.button6.UseVisualStyleBackColor = false;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // button5
            // 
            this.button5.BackColor = System.Drawing.Color.White;
            this.button5.Image = ((System.Drawing.Image)(resources.GetObject("button5.Image")));
            this.button5.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.button5.Location = new System.Drawing.Point(192, 3);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(63, 75);
            this.button5.TabIndex = 4;
            this.button5.Text = "进程管理";
            this.button5.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.button5.UseVisualStyleBackColor = false;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.White;
            this.button3.Image = ((System.Drawing.Image)(resources.GetObject("button3.Image")));
            this.button3.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.button3.Location = new System.Drawing.Point(381, 3);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(63, 75);
            this.button3.TabIndex = 3;
            this.button3.Text = "超级终端";
            this.button3.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.White;
            this.button2.Image = ((System.Drawing.Image)(resources.GetObject("button2.Image")));
            this.button2.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.button2.Location = new System.Drawing.Point(66, 3);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(63, 75);
            this.button2.TabIndex = 2;
            this.button2.Text = "屏幕查看";
            this.button2.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.White;
            this.button1.Image = ((System.Drawing.Image)(resources.GetObject("button1.Image")));
            this.button1.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.button1.Location = new System.Drawing.Point(129, 3);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(63, 75);
            this.button1.TabIndex = 1;
            this.button1.Text = "文件管理";
            this.button1.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Online_Computer_PopMenu
            // 
            this.Online_Computer_PopMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.File_C,
            this.Process_C,
            this.Reg_Con,
            this.Dos_C,
            this.Service_C,
            this.Remote_C});
            this.Online_Computer_PopMenu.Name = "contextMenuStrip1";
            this.Online_Computer_PopMenu.Size = new System.Drawing.Size(188, 256);
            this.Online_Computer_PopMenu.Opening += new System.ComponentModel.CancelEventHandler(this.Online_Computer_PopMenu_Opening);
            // 
            // File_C
            // 
            this.File_C.Image = global::Server.Properties.Resources.File_ler1;
            this.File_C.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.File_C.Name = "File_C";
            this.File_C.Size = new System.Drawing.Size(187, 42);
            this.File_C.Text = "文件管理 (&F)";
            this.File_C.Click += new System.EventHandler(this.File_C_Click);
            // 
            // Process_C
            // 
            this.Process_C.Image = global::Server.Properties.Resources.Process_ler;
            this.Process_C.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.Process_C.Name = "Process_C";
            this.Process_C.Size = new System.Drawing.Size(187, 42);
            this.Process_C.Text = "进程管理 (&P)";
            this.Process_C.Click += new System.EventHandler(this.Process_C_Click);
            // 
            // Reg_Con
            // 
            this.Reg_Con.Image = global::Server.Properties.Resources.Reg_ler1;
            this.Reg_Con.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.Reg_Con.Name = "Reg_Con";
            this.Reg_Con.Size = new System.Drawing.Size(187, 42);
            this.Reg_Con.Text = "注册表管理 (&R)";
            this.Reg_Con.Click += new System.EventHandler(this.注册表管理ToolStripMenuItem_Click);
            // 
            // Dos_C
            // 
            this.Dos_C.Image = global::Server.Properties.Resources.DOS;
            this.Dos_C.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.Dos_C.Name = "Dos_C";
            this.Dos_C.Size = new System.Drawing.Size(187, 42);
            this.Dos_C.Text = "超级终端 (&D)";
            this.Dos_C.Click += new System.EventHandler(this.Dos_C_Click);
            // 
            // Service_C
            // 
            this.Service_C.Image = global::Server.Properties.Resources.Service_ler;
            this.Service_C.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.Service_C.Name = "Service_C";
            this.Service_C.Size = new System.Drawing.Size(187, 42);
            this.Service_C.Text = "系统服务管理 (&S)";
            this.Service_C.Click += new System.EventHandler(this.Service_C_Click);
            // 
            // Remote_C
            // 
            this.Remote_C.Image = global::Server.Properties.Resources.Remote_ler;
            this.Remote_C.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.Remote_C.Name = "Remote_C";
            this.Remote_C.Size = new System.Drawing.Size(187, 42);
            this.Remote_C.Text = "屏幕查看 (&T)";
            this.Remote_C.Click += new System.EventHandler(this.button2_Click);
            // 
            // Small_Icon_ImageList
            // 
            this.Small_Icon_ImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("Small_Icon_ImageList.ImageStream")));
            this.Small_Icon_ImageList.TransparentColor = System.Drawing.Color.Transparent;
            this.Small_Icon_ImageList.Images.SetKeyName(0, "Online.bmp");
            this.Small_Icon_ImageList.Images.SetKeyName(1, "Disk_Root.bmp");
            // 
            // tabControl2
            // 
            this.tabControl2.Controls.Add(this.tabPage3);
            this.tabControl2.Controls.Add(this.tabPage4);
            this.tabControl2.Location = new System.Drawing.Point(3, 358);
            this.tabControl2.Name = "tabControl2";
            this.tabControl2.SelectedIndex = 0;
            this.tabControl2.Size = new System.Drawing.Size(890, 223);
            this.tabControl2.TabIndex = 3;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.Customer_Online_Info);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(882, 197);
            this.tabPage3.TabIndex = 0;
            this.tabPage3.Text = "客户连接信息";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // Customer_Online_Info
            // 
            this.Customer_Online_Info.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Ip_Info,
            this.Marks});
            this.Customer_Online_Info.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Customer_Online_Info.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Customer_Online_Info.ForeColor = System.Drawing.Color.Gray;
            this.Customer_Online_Info.GridLines = true;
            this.Customer_Online_Info.Location = new System.Drawing.Point(3, 3);
            this.Customer_Online_Info.Name = "Customer_Online_Info";
            this.Customer_Online_Info.Size = new System.Drawing.Size(876, 191);
            this.Customer_Online_Info.SmallImageList = this.Small_Icon_ImageList;
            this.Customer_Online_Info.TabIndex = 0;
            this.Customer_Online_Info.UseCompatibleStateImageBehavior = false;
            this.Customer_Online_Info.View = System.Windows.Forms.View.Details;
            // 
            // Ip_Info
            // 
            this.Ip_Info.Text = "被控端IP地址";
            this.Ip_Info.Width = 200;
            // 
            // Marks
            // 
            this.Marks.Text = "备注";
            this.Marks.Width = 400;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.listView2);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(882, 197);
            this.tabPage4.TabIndex = 1;
            this.tabPage4.Text = "文件下载管理";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // listView2
            // 
            this.listView2.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader17,
            this.columnHeader18,
            this.columnHeader19,
            this.columnHeader20,
            this.columnHeader21});
            this.listView2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView2.GridLines = true;
            this.listView2.Location = new System.Drawing.Point(3, 3);
            this.listView2.Name = "listView2";
            this.listView2.Size = new System.Drawing.Size(876, 191);
            this.listView2.TabIndex = 1;
            this.listView2.UseCompatibleStateImageBehavior = false;
            this.listView2.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader17
            // 
            this.columnHeader17.Text = "本地文件名称";
            this.columnHeader17.Width = 100;
            // 
            // columnHeader18
            // 
            this.columnHeader18.Text = "远程文件名称";
            this.columnHeader18.Width = 100;
            // 
            // columnHeader19
            // 
            this.columnHeader19.Text = "文件总长度";
            this.columnHeader19.Width = 100;
            // 
            // columnHeader20
            // 
            this.columnHeader20.Text = "已完成长度";
            this.columnHeader20.Width = 100;
            // 
            // columnHeader21
            // 
            this.columnHeader21.Text = "传输状态";
            this.columnHeader21.Width = 100;
            // 
            // Sys_Icon
            // 
            this.Sys_Icon.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.Sys_Icon.Icon = ((System.Drawing.Icon)(resources.GetObject("Sys_Icon.Icon")));
            this.Sys_Icon.Text = "远程广播控制(主控端)";
            this.Sys_Icon.Visible = true;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.Program_State,
            this.toolStripStatusLabel3,
            this.toolStripStatusLabel4,
            this.OnLine_Counter});
            this.statusStrip1.Location = new System.Drawing.Point(0, 588);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(894, 22);
            this.statusStrip1.TabIndex = 4;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(79, 17);
            this.toolStripStatusLabel1.Text = "当前状态 :    ";
            // 
            // Program_State
            // 
            this.Program_State.Name = "Program_State";
            this.Program_State.Size = new System.Drawing.Size(62, 17);
            this.Program_State.Text = "未就绪......";
            // 
            // toolStripStatusLabel3
            // 
            this.toolStripStatusLabel3.Name = "toolStripStatusLabel3";
            this.toolStripStatusLabel3.Size = new System.Drawing.Size(696, 17);
            this.toolStripStatusLabel3.Text = "                                                                                 " +
                "                                                                                " +
                "           ";
            // 
            // toolStripStatusLabel4
            // 
            this.toolStripStatusLabel4.Name = "toolStripStatusLabel4";
            this.toolStripStatusLabel4.Size = new System.Drawing.Size(103, 17);
            this.toolStripStatusLabel4.Text = "累计上线主机 :    ";
            // 
            // OnLine_Counter
            // 
            this.OnLine_Counter.Name = "OnLine_Counter";
            this.OnLine_Counter.Size = new System.Drawing.Size(15, 17);
            this.OnLine_Counter.Text = "0";
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.Nomarl_Member);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(882, 215);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "被控端信息";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // Nomarl_Member
            // 
            this.Nomarl_Member.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.IP,
            this.Software,
            this.Computer_Name,
            this.Customer,
            this.System_Name,
            this.CPU,
            this.Memory});
            this.Nomarl_Member.ContextMenuStrip = this.Online_Computer_PopMenu;
            this.Nomarl_Member.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Nomarl_Member.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Nomarl_Member.GridLines = true;
            this.Nomarl_Member.Location = new System.Drawing.Point(3, 3);
            this.Nomarl_Member.Name = "Nomarl_Member";
            this.Nomarl_Member.Size = new System.Drawing.Size(876, 209);
            this.Nomarl_Member.SmallImageList = this.Small_Icon_ImageList;
            this.Nomarl_Member.TabIndex = 0;
            this.Nomarl_Member.UseCompatibleStateImageBehavior = false;
            this.Nomarl_Member.View = System.Windows.Forms.View.Details;
            this.Nomarl_Member.DoubleClick += new System.EventHandler(this.Nomarl_Member_DoubleClick);
            // 
            // IP
            // 
            this.IP.Text = "被控端IP";
            this.IP.Width = 126;
            // 
            // Software
            // 
            this.Software.Text = "软件版本";
            this.Software.Width = 80;
            // 
            // Computer_Name
            // 
            this.Computer_Name.Text = "计算机名";
            this.Computer_Name.Width = 130;
            // 
            // Customer
            // 
            this.Customer.Text = "客户注释";
            this.Customer.Width = 70;
            // 
            // System_Name
            // 
            this.System_Name.Text = "操作系统";
            this.System_Name.Width = 220;
            // 
            // CPU
            // 
            this.CPU.Text = "CPU信息";
            this.CPU.Width = 120;
            // 
            // Memory
            // 
            this.Memory.Text = "内存容量";
            this.Memory.Width = 70;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Location = new System.Drawing.Point(4, 111);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(890, 241);
            this.tabControl1.TabIndex = 0;
            // 
            // Main_Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(894, 610);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.tabControl2);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Main_Form";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "远程广播控制(主控端)";
            this.Load += new System.EventHandler(this.Main_Form_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Main_Form_FormClosing);
            this.panel1.ResumeLayout(false);
            this.Online_Computer_PopMenu.ResumeLayout(false);
            this.tabControl2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.tabPage1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.TabControl tabControl2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.ListView Customer_Online_Info;
        private System.Windows.Forms.ColumnHeader Ip_Info;
        private System.Windows.Forms.ColumnHeader Marks;
        private System.Windows.Forms.ListView listView2;
        private System.Windows.Forms.ColumnHeader columnHeader17;
        private System.Windows.Forms.ColumnHeader columnHeader18;
        private System.Windows.Forms.ColumnHeader columnHeader19;
        private System.Windows.Forms.ColumnHeader columnHeader20;
        private System.Windows.Forms.ColumnHeader columnHeader21;
        private System.Windows.Forms.NotifyIcon Sys_Icon;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel Program_State;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel3;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel4;
        private System.Windows.Forms.ToolStripStatusLabel OnLine_Counter;
        private System.Windows.Forms.ImageList Small_Icon_ImageList;
        private System.Windows.Forms.ContextMenuStrip Online_Computer_PopMenu;
        private System.Windows.Forms.ToolStripMenuItem File_C;
        private System.Windows.Forms.ToolStripMenuItem Process_C;
        private System.Windows.Forms.ToolStripMenuItem Reg_Con;
        private System.Windows.Forms.ToolStripMenuItem Dos_C;
        private System.Windows.Forms.ToolStripMenuItem Service_C;
        private System.Windows.Forms.ToolStripMenuItem Remote_C;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.ListView Nomarl_Member;
        private System.Windows.Forms.ColumnHeader IP;
        private System.Windows.Forms.ColumnHeader Software;
        private System.Windows.Forms.ColumnHeader Computer_Name;
        private System.Windows.Forms.ColumnHeader Customer;
        private System.Windows.Forms.ColumnHeader System_Name;
        private System.Windows.Forms.ColumnHeader CPU;
        private System.Windows.Forms.ColumnHeader Memory;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button8;
    }
}

