namespace 主控端
{
    partial class File_Manager
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(File_Manager));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.Disk_Dir_Tree = new System.Windows.Forms.TreeView();
            this.Small_Icon_ImageList = new System.Windows.Forms.ImageList(this.components);
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.File_List = new System.Windows.Forms.ListView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.groupBox1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.panel1);
            this.groupBox1.Controls.Add(this.Disk_Dir_Tree);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(209, 521);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "硬盘 - 文件夹目录列表";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(6, 493);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(157, 22);
            this.panel1.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(21, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(114, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Please Waitting......";
            // 
            // Disk_Dir_Tree
            // 
            this.Disk_Dir_Tree.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Disk_Dir_Tree.ImageIndex = 0;
            this.Disk_Dir_Tree.ImageList = this.Small_Icon_ImageList;
            this.Disk_Dir_Tree.Location = new System.Drawing.Point(6, 20);
            this.Disk_Dir_Tree.Name = "Disk_Dir_Tree";
            this.Disk_Dir_Tree.SelectedImageIndex = 0;
            this.Disk_Dir_Tree.Size = new System.Drawing.Size(197, 495);
            this.Disk_Dir_Tree.StateImageList = this.Small_Icon_ImageList;
            this.Disk_Dir_Tree.TabIndex = 0;
            this.Disk_Dir_Tree.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.Disk_Dir_Tree_NodeMouseDoubleClick);
            // 
            // Small_Icon_ImageList
            // 
            this.Small_Icon_ImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("Small_Icon_ImageList.ImageStream")));
            this.Small_Icon_ImageList.TransparentColor = System.Drawing.Color.Transparent;
            this.Small_Icon_ImageList.Images.SetKeyName(0, "Online.bmp");
            this.Small_Icon_ImageList.Images.SetKeyName(1, "Disk_Root.bmp");
            this.Small_Icon_ImageList.Images.SetKeyName(2, "Folder_Root.bmp");
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.File_List);
            this.groupBox2.Location = new System.Drawing.Point(227, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(476, 521);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "文件 - 文件夹目录列表";
            // 
            // File_List
            // 
            this.File_List.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.File_List.LargeImageList = this.imageList1;
            this.File_List.Location = new System.Drawing.Point(6, 20);
            this.File_List.Name = "File_List";
            this.File_List.Size = new System.Drawing.Size(464, 495);
            this.File_List.TabIndex = 0;
            this.File_List.UseCompatibleStateImageBehavior = false;
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "Folder.bmp");
            this.imageList1.Images.SetKeyName(1, "File.bmp");
            // 
            // File_Manager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(715, 545);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "File_Manager";
            this.Text = "文件管理 - ";
            this.Load += new System.EventHandler(this.File_Manager_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.File_Manager_FormClosing);
            this.groupBox1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TreeView Disk_Dir_Tree;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ListView File_List;
        private System.Windows.Forms.ImageList Small_Icon_ImageList;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ImageList imageList1;


    }
}