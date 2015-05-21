namespace 主控端
{
    partial class Service_Manager
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Service_Manager));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.Service_List = new System.Windows.Forms.ListView();
            this.Service_Name = new System.Windows.Forms.ColumnHeader();
            this.Service_Run = new System.Windows.Forms.ColumnHeader();
            this.Service_Description = new System.Windows.Forms.ColumnHeader();
            this.Service_ImageList = new System.Windows.Forms.ImageList(this.components);
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.Service_List);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox1.Location = new System.Drawing.Point(0, 24);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(645, 618);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "被控端服务管理";
            // 
            // Service_List
            // 
            this.Service_List.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Service_Name,
            this.Service_Run,
            this.Service_Description});
            this.Service_List.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Service_List.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Service_List.GridLines = true;
            this.Service_List.Location = new System.Drawing.Point(3, 17);
            this.Service_List.Name = "Service_List";
            this.Service_List.Size = new System.Drawing.Size(639, 598);
            this.Service_List.SmallImageList = this.Service_ImageList;
            this.Service_List.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.Service_List.StateImageList = this.Service_ImageList;
            this.Service_List.TabIndex = 0;
            this.Service_List.UseCompatibleStateImageBehavior = false;
            this.Service_List.View = System.Windows.Forms.View.Details;
            // 
            // Service_Name
            // 
            this.Service_Name.Text = "服务名";
            this.Service_Name.Width = 168;
            // 
            // Service_Run
            // 
            this.Service_Run.Text = "服务启动状态";
            this.Service_Run.Width = 97;
            // 
            // Service_Description
            // 
            this.Service_Description.Text = "服务描述";
            this.Service_Description.Width = 342;
            // 
            // Service_ImageList
            // 
            this.Service_ImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("Service_ImageList.ImageStream")));
            this.Service_ImageList.TransparentColor = System.Drawing.Color.Transparent;
            this.Service_ImageList.Images.SetKeyName(0, "Service.bmp");
            // 
            // Service_Manager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(645, 642);
            this.Controls.Add(this.groupBox1);
            this.Name = "Service_Manager";
            this.Text = "服务管理";
            this.Load += new System.EventHandler(this.Service_Manager_Load);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ListView Service_List;
        private System.Windows.Forms.ColumnHeader Service_Name;
        private System.Windows.Forms.ColumnHeader Service_Run;
        private System.Windows.Forms.ColumnHeader Service_Description;
        private System.Windows.Forms.ImageList Service_ImageList;
    }
}