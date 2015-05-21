namespace Control_Client
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
            this.Sys_Icon = new System.Windows.Forms.NotifyIcon(this.components);
            this.Desktop_Timer = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // Sys_Icon
            // 
            this.Sys_Icon.Icon = ((System.Drawing.Icon)(resources.GetObject("Sys_Icon.Icon")));
            this.Sys_Icon.Text = "远程广播控制(被控端)";
            this.Sys_Icon.Visible = true;
            // 
            // Desktop_Timer
            // 
            this.Desktop_Timer.Interval = 200;
            this.Desktop_Timer.Tick += new System.EventHandler(this.Desktop_Timer_Tick);
            // 
            // Main_Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 13);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.Name = "Main_Form";
            this.ShowIcon = false;
            this.Text = "远程广播控制(被控端)";
            this.Load += new System.EventHandler(this.Main_Form_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Main_Form_FormClosing);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.NotifyIcon Sys_Icon;
        private System.Windows.Forms.Timer Desktop_Timer;
    }
}

