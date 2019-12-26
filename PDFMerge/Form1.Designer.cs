namespace PDFMerge
{
    partial class Form1
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
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.btn_openfile = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.btn_run = new System.Windows.Forms.Button();
            this.btn_MergePDF = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btn_openfile
            // 
            this.btn_openfile.Location = new System.Drawing.Point(34, 67);
            this.btn_openfile.Name = "btn_openfile";
            this.btn_openfile.Size = new System.Drawing.Size(95, 23);
            this.btn_openfile.TabIndex = 0;
            this.btn_openfile.Text = "打开PDF";
            this.btn_openfile.UseVisualStyleBackColor = true;
            this.btn_openfile.Click += new System.EventHandler(this.btn_openfile_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(162, 68);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(418, 25);
            this.textBox1.TabIndex = 1;
            // 
            // btn_run
            // 
            this.btn_run.Location = new System.Drawing.Point(659, 63);
            this.btn_run.Name = "btn_run";
            this.btn_run.Size = new System.Drawing.Size(76, 29);
            this.btn_run.TabIndex = 2;
            this.btn_run.Text = "运行";
            this.btn_run.UseVisualStyleBackColor = true;
            this.btn_run.Click += new System.EventHandler(this.btn_run_Click);
            // 
            // btn_MergePDF
            // 
            this.btn_MergePDF.Location = new System.Drawing.Point(301, 158);
            this.btn_MergePDF.Name = "btn_MergePDF";
            this.btn_MergePDF.Size = new System.Drawing.Size(75, 23);
            this.btn_MergePDF.TabIndex = 3;
            this.btn_MergePDF.Text = "合并PDF";
            this.btn_MergePDF.UseVisualStyleBackColor = true;
            this.btn_MergePDF.Click += new System.EventHandler(this.btn_MergePDF_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btn_MergePDF);
            this.Controls.Add(this.btn_run);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.btn_openfile);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_openfile;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button btn_run;
        private System.Windows.Forms.Button btn_MergePDF;
    }
}

