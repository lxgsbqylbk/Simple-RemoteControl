namespace 主控端
{
    partial class Process_Manager
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Process_Manager));
            this.label1 = new System.Windows.Forms.Label();
            this.Ip_Addr = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.Process_Count = new System.Windows.Forms.Label();
            this.Process_ES = new System.Windows.Forms.ListView();
            this.Process_Name = new System.Windows.Forms.ColumnHeader();
            this.Handle = new System.Windows.Forms.ColumnHeader();
            this.ID = new System.Windows.Forms.ColumnHeader();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("黑体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(10, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(106, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "目标机器 :     ";
            // 
            // Ip_Addr
            // 
            this.Ip_Addr.AutoSize = true;
            this.Ip_Addr.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Ip_Addr.Location = new System.Drawing.Point(82, 18);
            this.Ip_Addr.Name = "Ip_Addr";
            this.Ip_Addr.Size = new System.Drawing.Size(0, 15);
            this.Ip_Addr.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("黑体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(10, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(125, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "当前进程数量 :    ";
            // 
            // Process_Count
            // 
            this.Process_Count.AutoSize = true;
            this.Process_Count.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Process_Count.Location = new System.Drawing.Point(110, 46);
            this.Process_Count.Name = "Process_Count";
            this.Process_Count.Size = new System.Drawing.Size(0, 15);
            this.Process_Count.TabIndex = 3;
            // 
            // Process_ES
            // 
            this.Process_ES.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Process_Name,
            this.Handle,
            this.ID});
            this.Process_ES.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Process_ES.GridLines = true;
            this.Process_ES.Location = new System.Drawing.Point(12, 73);
            this.Process_ES.Name = "Process_ES";
            this.Process_ES.Size = new System.Drawing.Size(334, 473);
            this.Process_ES.SmallImageList = this.imageList1;
            this.Process_ES.StateImageList = this.imageList1;
            this.Process_ES.TabIndex = 4;
            this.Process_ES.UseCompatibleStateImageBehavior = false;
            this.Process_ES.View = System.Windows.Forms.View.Details;
            // 
            // Process_Name
            // 
            this.Process_Name.Text = "进程名";
            this.Process_Name.Width = 150;
            // 
            // Handle
            // 
            this.Handle.Text = "进程句柄";
            this.Handle.Width = 97;
            // 
            // ID
            // 
            this.ID.Text = "PID";
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "stickers_029.png");
            // 
            // button1
            // 
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button1.Font = new System.Drawing.Font("黑体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button1.Location = new System.Drawing.Point(75, 564);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 5;
            this.button1.Text = "杀死进程";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button2.Font = new System.Drawing.Font("黑体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button2.Location = new System.Drawing.Point(180, 564);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 5;
            this.button2.Text = "刷新进程";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // Process_Manager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(358, 597);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.Process_ES);
            this.Controls.Add(this.Process_Count);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.Ip_Addr);
            this.Controls.Add(this.label1);
            this.Name = "Process_Manager";
            this.Text = "进程管理";
            this.Load += new System.EventHandler(this.Process_Manager_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label Ip_Addr;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label Process_Count;
        private System.Windows.Forms.ListView Process_ES;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.ColumnHeader Process_Name;
        private System.Windows.Forms.ColumnHeader Handle;
        private System.Windows.Forms.ColumnHeader ID;
        private System.Windows.Forms.ImageList imageList1;
    }
}