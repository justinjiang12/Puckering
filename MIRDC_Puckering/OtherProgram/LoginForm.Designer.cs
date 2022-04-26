namespace MIRDC_Puckering.OtherProgram
{
    partial class LoginForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btn_Leave = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tex_name = new System.Windows.Forms.TextBox();
            this.tex_password = new System.Windows.Forms.TextBox();
            this.btn_Login = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btn_Leave
            // 
            this.btn_Leave.Font = new System.Drawing.Font("微軟正黑體", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btn_Leave.Location = new System.Drawing.Point(15, 149);
            this.btn_Leave.Name = "btn_Leave";
            this.btn_Leave.Size = new System.Drawing.Size(168, 54);
            this.btn_Leave.TabIndex = 0;
            this.btn_Leave.Text = "Leave";
            this.btn_Leave.UseVisualStyleBackColor = true;
            this.btn_Leave.Click += new System.EventHandler(this.btn_Leave_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微軟正黑體", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label1.Location = new System.Drawing.Point(1, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(182, 35);
            this.label1.TabIndex = 0;
            this.label1.Text = "User Name : ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微軟正黑體", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label2.Location = new System.Drawing.Point(9, 89);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(174, 35);
            this.label2.TabIndex = 1;
            this.label2.Text = "Pass Word : ";
            // 
            // tex_name
            // 
            this.tex_name.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.tex_name.Location = new System.Drawing.Point(189, 30);
            this.tex_name.Name = "tex_name";
            this.tex_name.Size = new System.Drawing.Size(195, 34);
            this.tex_name.TabIndex = 2;
            // 
            // tex_password
            // 
            this.tex_password.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.tex_password.Location = new System.Drawing.Point(189, 90);
            this.tex_password.Name = "tex_password";
            this.tex_password.Size = new System.Drawing.Size(195, 34);
            this.tex_password.TabIndex = 3;
            // 
            // btn_Login
            // 
            this.btn_Login.Font = new System.Drawing.Font("微軟正黑體", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btn_Login.Location = new System.Drawing.Point(219, 149);
            this.btn_Login.Name = "btn_Login";
            this.btn_Login.Size = new System.Drawing.Size(165, 54);
            this.btn_Login.TabIndex = 4;
            this.btn_Login.Text = "Login";
            this.btn_Login.UseVisualStyleBackColor = true;
            this.btn_Login.Click += new System.EventHandler(this.btn_Login_Click);
            // 
            // LoginForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(403, 215);
            this.ControlBox = false;
            this.Controls.Add(this.btn_Login);
            this.Controls.Add(this.tex_password);
            this.Controls.Add(this.tex_name);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btn_Leave);
            this.Name = "LoginForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Login";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_Leave;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tex_name;
        private System.Windows.Forms.TextBox tex_password;
        private System.Windows.Forms.Button btn_Login;
    }
}