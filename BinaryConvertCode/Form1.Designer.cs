namespace BinaryConvertCode
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
            this.btn_GetCode = new System.Windows.Forms.Button();
            this.textBox_ShowCode = new System.Windows.Forms.TextBox();
            this.label_Code = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btn_GetCode
            // 
            this.btn_GetCode.Location = new System.Drawing.Point(615, 258);
            this.btn_GetCode.Name = "btn_GetCode";
            this.btn_GetCode.Size = new System.Drawing.Size(136, 42);
            this.btn_GetCode.TabIndex = 0;
            this.btn_GetCode.Text = "获取点码";
            this.btn_GetCode.UseVisualStyleBackColor = true;
            this.btn_GetCode.Click += new System.EventHandler(this.btn_GetCode_Click);
            // 
            // textBox_ShowCode
            // 
            this.textBox_ShowCode.Location = new System.Drawing.Point(41, 258);
            this.textBox_ShowCode.Multiline = true;
            this.textBox_ShowCode.Name = "textBox_ShowCode";
            this.textBox_ShowCode.Size = new System.Drawing.Size(527, 51);
            this.textBox_ShowCode.TabIndex = 1;
            // 
            // label_Code
            // 
            this.label_Code.AutoSize = true;
            this.label_Code.Font = new System.Drawing.Font("华文楷体", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label_Code.Location = new System.Drawing.Point(803, 262);
            this.label_Code.Name = "label_Code";
            this.label_Code.Size = new System.Drawing.Size(132, 27);
            this.label_Code.TabIndex = 2;
            this.label_Code.Text = "二进制数据";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1600, 335);
            this.Controls.Add(this.label_Code);
            this.Controls.Add(this.textBox_ShowCode);
            this.Controls.Add(this.btn_GetCode);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_GetCode;
        private System.Windows.Forms.TextBox textBox_ShowCode;
        private System.Windows.Forms.Label label_Code;
    }
}

