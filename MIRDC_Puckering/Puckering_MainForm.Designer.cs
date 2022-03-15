namespace MIRDC_Puckering
{
    partial class Puckering_MainForm
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置受控資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Puckering_MainForm));
            this.button6 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button8 = new System.Windows.Forms.Button();
            this.button9 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(6, 21);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(103, 57);
            this.button6.TabIndex = 12;
            this.button6.Tag = "FormLoad";
            this.button6.Text = "FormLoad";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button_Click);
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(6, 83);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(103, 57);
            this.button7.TabIndex = 13;
            this.button7.Tag = "FormClose";
            this.button7.Text = "FormClose";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.button_Click);
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(2, 168);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1549, 506);
            this.panel1.TabIndex = 14;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.button2);
            this.groupBox4.Controls.Add(this.button1);
            this.groupBox4.Controls.Add(this.button8);
            this.groupBox4.Controls.Add(this.button9);
            this.groupBox4.Controls.Add(this.button6);
            this.groupBox4.Controls.Add(this.button7);
            this.groupBox4.Location = new System.Drawing.Point(2, 3);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(1549, 159);
            this.groupBox4.TabIndex = 16;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "groupBox4";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(224, 83);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(103, 57);
            this.button1.TabIndex = 16;
            this.button1.Tag = "FormClose_clear";
            this.button1.Text = "FormClose_1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button_Click);
            // 
            // button8
            // 
            this.button8.Location = new System.Drawing.Point(115, 21);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(103, 57);
            this.button8.TabIndex = 14;
            this.button8.Tag = "FormLoad_1";
            this.button8.Text = "FormLoad_1";
            this.button8.UseVisualStyleBackColor = true;
            this.button8.Click += new System.EventHandler(this.button_Click);
            // 
            // button9
            // 
            this.button9.Location = new System.Drawing.Point(115, 83);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(103, 57);
            this.button9.TabIndex = 15;
            this.button9.Tag = "FormClose_1";
            this.button9.Text = "FormClose_1";
            this.button9.UseVisualStyleBackColor = true;
            this.button9.Click += new System.EventHandler(this.button_Click);
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.button2.Image = ((System.Drawing.Image)(resources.GetObject("button2.Image")));
            this.button2.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.button2.Location = new System.Drawing.Point(1452, 9);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(91, 78);
            this.button2.TabIndex = 18;
            this.button2.Tag = "btn_esc";
            this.button2.Text = "ESC";
            this.button2.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button_Click);
            // 
            // Puckering_MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1556, 675);
            this.ControlBox = false;
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.IsMdiContainer = true;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Puckering_MainForm";
            this.Text = "MIRDC Puckering";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.Button button9;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
    }
}

