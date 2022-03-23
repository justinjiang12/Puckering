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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Puckering_MainForm));
            this.button6 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button8 = new System.Windows.Forms.Button();
            this.button9 = new System.Windows.Forms.Button();
            this.btn_Auto = new System.Windows.Forms.Button();
            this.btn_Manual = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.thr_timer = new System.Windows.Forms.Timer(this.components);
            this.btn_Start = new System.Windows.Forms.Button();
            this.btn_Stop = new System.Windows.Forms.Button();
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
            this.panel1.Size = new System.Drawing.Size(1396, 596);
            this.panel1.TabIndex = 14;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.label5);
            this.groupBox4.Controls.Add(this.label4);
            this.groupBox4.Controls.Add(this.label3);
            this.groupBox4.Controls.Add(this.label2);
            this.groupBox4.Controls.Add(this.label1);
            this.groupBox4.Controls.Add(this.button1);
            this.groupBox4.Controls.Add(this.button8);
            this.groupBox4.Controls.Add(this.button9);
            this.groupBox4.Controls.Add(this.button6);
            this.groupBox4.Controls.Add(this.button7);
            this.groupBox4.Location = new System.Drawing.Point(2, 3);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(1396, 159);
            this.groupBox4.TabIndex = 16;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "groupBox4";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(903, 121);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(81, 15);
            this.label5.TabIndex = 23;
            this.label5.Text = "VisionStep : ";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(903, 70);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(71, 15);
            this.label4.TabIndex = 22;
            this.label4.Text = "PushStep : ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(903, 20);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(72, 15);
            this.label3.TabIndex = 21;
            this.label3.Text = "GrabStep : ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(672, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(76, 15);
            this.label2.TabIndex = 20;
            this.label2.Text = "SysModel : ";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(389, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 15);
            this.label1.TabIndex = 19;
            this.label1.Text = "label1";
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
            // btn_Auto
            // 
            this.btn_Auto.Location = new System.Drawing.Point(1404, 302);
            this.btn_Auto.Name = "btn_Auto";
            this.btn_Auto.Size = new System.Drawing.Size(147, 67);
            this.btn_Auto.TabIndex = 22;
            this.btn_Auto.Tag = "btn_Auto";
            this.btn_Auto.Text = "Auto";
            this.btn_Auto.UseVisualStyleBackColor = true;
            this.btn_Auto.Click += new System.EventHandler(this.button_Click);
            // 
            // btn_Manual
            // 
            this.btn_Manual.Location = new System.Drawing.Point(1404, 238);
            this.btn_Manual.Name = "btn_Manual";
            this.btn_Manual.Size = new System.Drawing.Size(147, 58);
            this.btn_Manual.TabIndex = 21;
            this.btn_Manual.Tag = "btn_Manual";
            this.btn_Manual.Text = "Manual";
            this.btn_Manual.UseVisualStyleBackColor = true;
            this.btn_Manual.Click += new System.EventHandler(this.button_Click);
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.button2.Image = ((System.Drawing.Image)(resources.GetObject("button2.Image")));
            this.button2.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.button2.Location = new System.Drawing.Point(1472, 3);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(79, 78);
            this.button2.TabIndex = 18;
            this.button2.Tag = "btn_esc";
            this.button2.Text = "ESC";
            this.button2.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button_Click);
            // 
            // thr_timer
            // 
            this.thr_timer.Enabled = true;
            this.thr_timer.Tick += new System.EventHandler(this.thr_timer_Tick);
            // 
            // btn_Start
            // 
            this.btn_Start.Location = new System.Drawing.Point(1404, 624);
            this.btn_Start.Name = "btn_Start";
            this.btn_Start.Size = new System.Drawing.Size(147, 67);
            this.btn_Start.TabIndex = 28;
            this.btn_Start.Tag = "btn_Start";
            this.btn_Start.Text = "Start";
            this.btn_Start.UseVisualStyleBackColor = true;
            this.btn_Start.Click += new System.EventHandler(this.button_Click);
            // 
            // btn_Stop
            // 
            this.btn_Stop.Location = new System.Drawing.Point(1404, 697);
            this.btn_Stop.Name = "btn_Stop";
            this.btn_Stop.Size = new System.Drawing.Size(147, 67);
            this.btn_Stop.TabIndex = 29;
            this.btn_Stop.Tag = "btn_Stop";
            this.btn_Stop.Text = "Stop";
            this.btn_Stop.UseVisualStyleBackColor = true;
            this.btn_Stop.Click += new System.EventHandler(this.button_Click);
            // 
            // Puckering_MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1561, 770);
            this.ControlBox = false;
            this.Controls.Add(this.btn_Stop);
            this.Controls.Add(this.btn_Start);
            this.Controls.Add(this.btn_Manual);
            this.Controls.Add(this.btn_Auto);
            this.Controls.Add(this.button2);
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
            this.groupBox4.PerformLayout();
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
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_Auto;
        private System.Windows.Forms.Button btn_Manual;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Timer thr_timer;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btn_Start;
        private System.Windows.Forms.Button btn_Stop;
    }
}

