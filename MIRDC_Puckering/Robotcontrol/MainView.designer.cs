namespace MxAutomation_Example{
	partial class MainView {
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing) {
			if (disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainView));
            this.bStart = new System.Windows.Forms.Button();
            this.bStop = new System.Windows.Forms.Button();
            this.cycle = new System.Windows.Forms.Timer(this.components);
            this.tbDebug = new System.Windows.Forms.TextBox();
            this.tbAxisGroupRef = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.bReset = new System.Windows.Forms.Button();
            this.cbMoveEnable = new System.Windows.Forms.CheckBox();
            this.tbOverride = new System.Windows.Forms.TrackBar();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.textBox7 = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.textBox8 = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.textBox9 = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.textBox10 = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.textBox11 = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.textBox12 = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.textBox6 = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.bMoveToPos2 = new System.Windows.Forms.Button();
            this.bMoveToPos1 = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.lErrorID = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.textBox13 = new System.Windows.Forms.TextBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.button10 = new System.Windows.Forms.Button();
            this.button19 = new System.Windows.Forms.Button();
            this.button14 = new System.Windows.Forms.Button();
            this.button20 = new System.Windows.Forms.Button();
            this.button16 = new System.Windows.Forms.Button();
            this.button21 = new System.Windows.Forms.Button();
            this.button15 = new System.Windows.Forms.Button();
            this.button22 = new System.Windows.Forms.Button();
            this.button18 = new System.Windows.Forms.Button();
            this.button23 = new System.Windows.Forms.Button();
            this.button17 = new System.Windows.Forms.Button();
            this.button24 = new System.Windows.Forms.Button();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbOverride)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.groupBox8.SuspendLayout();
            this.SuspendLayout();
            // 
            // bStart
            // 
            this.bStart.Cursor = System.Windows.Forms.Cursors.Hand;
            resources.ApplyResources(this.bStart, "bStart");
            this.bStart.Name = "bStart";
            this.bStart.UseVisualStyleBackColor = true;
            this.bStart.Click += new System.EventHandler(this.bStart_Click);
            // 
            // bStop
            // 
            this.bStop.Cursor = System.Windows.Forms.Cursors.Hand;
            resources.ApplyResources(this.bStop, "bStop");
            this.bStop.Name = "bStop";
            this.bStop.UseVisualStyleBackColor = true;
            this.bStop.Click += new System.EventHandler(this.bStop_Click);
            // 
            // cycle
            // 
            this.cycle.Interval = 2;
            this.cycle.Tick += new System.EventHandler(this.cycle_Tick);
            // 
            // tbDebug
            // 
            resources.ApplyResources(this.tbDebug, "tbDebug");
            this.tbDebug.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbDebug.Name = "tbDebug";
            // 
            // tbAxisGroupRef
            // 
            resources.ApplyResources(this.tbAxisGroupRef, "tbAxisGroupRef");
            this.tbAxisGroupRef.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbAxisGroupRef.Name = "tbAxisGroupRef";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.bStart);
            this.groupBox1.Controls.Add(this.bStop);
            this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.groupBox1.ForeColor = System.Drawing.Color.White;
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.bReset);
            this.groupBox2.Controls.Add(this.cbMoveEnable);
            this.groupBox2.ForeColor = System.Drawing.Color.White;
            resources.ApplyResources(this.groupBox2, "groupBox2");
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.TabStop = false;
            // 
            // bReset
            // 
            this.bReset.Cursor = System.Windows.Forms.Cursors.Hand;
            resources.ApplyResources(this.bReset, "bReset");
            this.bReset.Name = "bReset";
            this.bReset.UseVisualStyleBackColor = true;
            this.bReset.Click += new System.EventHandler(this.bReset_Click);
            // 
            // cbMoveEnable
            // 
            resources.ApplyResources(this.cbMoveEnable, "cbMoveEnable");
            this.cbMoveEnable.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cbMoveEnable.Name = "cbMoveEnable";
            this.cbMoveEnable.UseVisualStyleBackColor = true;
            this.cbMoveEnable.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MainView_MouseDown);
            this.cbMoveEnable.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MainView_MouseMove);
            // 
            // tbOverride
            // 
            resources.ApplyResources(this.tbOverride, "tbOverride");
            this.tbOverride.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(165)))), ((int)(((byte)(166)))), ((int)(((byte)(173)))));
            this.tbOverride.Cursor = System.Windows.Forms.Cursors.Hand;
            this.tbOverride.LargeChange = 10;
            this.tbOverride.Maximum = 100;
            this.tbOverride.Name = "tbOverride";
            this.tbOverride.Value = 10;
            this.tbOverride.Scroll += new System.EventHandler(this.tbOverride_Scroll);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.panel2);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.label9);
            this.groupBox3.Controls.Add(this.textBox7);
            this.groupBox3.Controls.Add(this.label10);
            this.groupBox3.Controls.Add(this.textBox8);
            this.groupBox3.Controls.Add(this.label11);
            this.groupBox3.Controls.Add(this.textBox9);
            this.groupBox3.Controls.Add(this.label12);
            this.groupBox3.Controls.Add(this.textBox10);
            this.groupBox3.Controls.Add(this.label13);
            this.groupBox3.Controls.Add(this.textBox11);
            this.groupBox3.Controls.Add(this.label14);
            this.groupBox3.Controls.Add(this.textBox12);
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Controls.Add(this.textBox6);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.textBox5);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.textBox4);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.textBox3);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.textBox2);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.textBox1);
            this.groupBox3.Controls.Add(this.bMoveToPos2);
            this.groupBox3.Controls.Add(this.bMoveToPos1);
            this.groupBox3.Controls.Add(this.tbOverride);
            this.groupBox3.ForeColor = System.Drawing.Color.White;
            resources.ApplyResources(this.groupBox3, "groupBox3");
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.TabStop = false;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.White;
            resources.ApplyResources(this.panel2, "panel2");
            this.panel2.Name = "panel2";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // label9
            // 
            resources.ApplyResources(this.label9, "label9");
            this.label9.Name = "label9";
            this.label9.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MainView_MouseDown);
            this.label9.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MainView_MouseMove);
            // 
            // textBox7
            // 
            resources.ApplyResources(this.textBox7, "textBox7");
            this.textBox7.Name = "textBox7";
            // 
            // label10
            // 
            resources.ApplyResources(this.label10, "label10");
            this.label10.Name = "label10";
            this.label10.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MainView_MouseDown);
            this.label10.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MainView_MouseMove);
            // 
            // textBox8
            // 
            resources.ApplyResources(this.textBox8, "textBox8");
            this.textBox8.Name = "textBox8";
            // 
            // label11
            // 
            resources.ApplyResources(this.label11, "label11");
            this.label11.Name = "label11";
            this.label11.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MainView_MouseDown);
            this.label11.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MainView_MouseMove);
            // 
            // textBox9
            // 
            resources.ApplyResources(this.textBox9, "textBox9");
            this.textBox9.Name = "textBox9";
            // 
            // label12
            // 
            resources.ApplyResources(this.label12, "label12");
            this.label12.Name = "label12";
            this.label12.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MainView_MouseDown);
            this.label12.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MainView_MouseMove);
            // 
            // textBox10
            // 
            resources.ApplyResources(this.textBox10, "textBox10");
            this.textBox10.Name = "textBox10";
            // 
            // label13
            // 
            resources.ApplyResources(this.label13, "label13");
            this.label13.Name = "label13";
            this.label13.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MainView_MouseDown);
            this.label13.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MainView_MouseMove);
            // 
            // textBox11
            // 
            resources.ApplyResources(this.textBox11, "textBox11");
            this.textBox11.Name = "textBox11";
            // 
            // label14
            // 
            resources.ApplyResources(this.label14, "label14");
            this.label14.Name = "label14";
            this.label14.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MainView_MouseDown);
            this.label14.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MainView_MouseMove);
            // 
            // textBox12
            // 
            resources.ApplyResources(this.textBox12, "textBox12");
            this.textBox12.Name = "textBox12";
            // 
            // label8
            // 
            resources.ApplyResources(this.label8, "label8");
            this.label8.Name = "label8";
            this.label8.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MainView_MouseDown);
            this.label8.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MainView_MouseMove);
            // 
            // textBox6
            // 
            resources.ApplyResources(this.textBox6, "textBox6");
            this.textBox6.Name = "textBox6";
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.Name = "label7";
            this.label7.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MainView_MouseDown);
            this.label7.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MainView_MouseMove);
            // 
            // textBox5
            // 
            resources.ApplyResources(this.textBox5, "textBox5");
            this.textBox5.Name = "textBox5";
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            this.label6.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MainView_MouseDown);
            this.label6.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MainView_MouseMove);
            // 
            // textBox4
            // 
            resources.ApplyResources(this.textBox4, "textBox4");
            this.textBox4.Name = "textBox4";
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            this.label5.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MainView_MouseDown);
            this.label5.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MainView_MouseMove);
            // 
            // textBox3
            // 
            resources.ApplyResources(this.textBox3, "textBox3");
            this.textBox3.Name = "textBox3";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            this.label4.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MainView_MouseDown);
            this.label4.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MainView_MouseMove);
            // 
            // textBox2
            // 
            resources.ApplyResources(this.textBox2, "textBox2");
            this.textBox2.Name = "textBox2";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            this.label3.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MainView_MouseDown);
            this.label3.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MainView_MouseMove);
            // 
            // textBox1
            // 
            resources.ApplyResources(this.textBox1, "textBox1");
            this.textBox1.Name = "textBox1";
            // 
            // bMoveToPos2
            // 
            this.bMoveToPos2.Cursor = System.Windows.Forms.Cursors.Hand;
            resources.ApplyResources(this.bMoveToPos2, "bMoveToPos2");
            this.bMoveToPos2.Name = "bMoveToPos2";
            this.bMoveToPos2.UseVisualStyleBackColor = true;
            this.bMoveToPos2.Click += new System.EventHandler(this.bMoveToPos2_Click);
            // 
            // bMoveToPos1
            // 
            this.bMoveToPos1.Cursor = System.Windows.Forms.Cursors.Hand;
            resources.ApplyResources(this.bMoveToPos1, "bMoveToPos1");
            this.bMoveToPos1.Name = "bMoveToPos1";
            this.bMoveToPos1.UseVisualStyleBackColor = true;
            this.bMoveToPos1.Click += new System.EventHandler(this.bMoveToPos1_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.lErrorID);
            this.groupBox4.Controls.Add(this.label2);
            this.groupBox4.ForeColor = System.Drawing.Color.White;
            resources.ApplyResources(this.groupBox4, "groupBox4");
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.TabStop = false;
            // 
            // lErrorID
            // 
            resources.ApplyResources(this.lErrorID, "lErrorID");
            this.lErrorID.Name = "lErrorID";
            this.lErrorID.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MainView_MouseDown);
            this.lErrorID.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MainView_MouseMove);
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            this.label2.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MainView_MouseDown);
            this.label2.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MainView_MouseMove);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.textBox13);
            this.groupBox5.ForeColor = System.Drawing.Color.White;
            resources.ApplyResources(this.groupBox5, "groupBox5");
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.TabStop = false;
            // 
            // textBox13
            // 
            resources.ApplyResources(this.textBox13, "textBox13");
            this.textBox13.Name = "textBox13";
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.button10);
            this.groupBox6.Controls.Add(this.button19);
            this.groupBox6.Controls.Add(this.button14);
            this.groupBox6.Controls.Add(this.button20);
            this.groupBox6.Controls.Add(this.button16);
            this.groupBox6.Controls.Add(this.button21);
            this.groupBox6.Controls.Add(this.button15);
            this.groupBox6.Controls.Add(this.button22);
            this.groupBox6.Controls.Add(this.button18);
            this.groupBox6.Controls.Add(this.button23);
            this.groupBox6.Controls.Add(this.button17);
            this.groupBox6.Controls.Add(this.button24);
            resources.ApplyResources(this.groupBox6, "groupBox6");
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.TabStop = false;
            // 
            // button10
            // 
            this.button10.BackColor = System.Drawing.Color.Sienna;
            resources.ApplyResources(this.button10, "button10");
            this.button10.Name = "button10";
            this.button10.Tag = "A1_X_-";
            this.button10.UseVisualStyleBackColor = false;
            this.button10.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ButtonControl_MouseDown);
            this.button10.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ButtonControl_MouseUp);
            // 
            // button19
            // 
            this.button19.BackColor = System.Drawing.Color.Sienna;
            resources.ApplyResources(this.button19, "button19");
            this.button19.Name = "button19";
            this.button19.Tag = "A6_RZ_+";
            this.button19.UseVisualStyleBackColor = false;
            this.button19.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ButtonControl_MouseDown);
            this.button19.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ButtonControl_MouseUp);
            // 
            // button14
            // 
            this.button14.BackColor = System.Drawing.Color.Sienna;
            resources.ApplyResources(this.button14, "button14");
            this.button14.Name = "button14";
            this.button14.Tag = "A1_X_+";
            this.button14.UseVisualStyleBackColor = false;
            this.button14.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ButtonControl_MouseDown);
            this.button14.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ButtonControl_MouseUp);
            // 
            // button20
            // 
            this.button20.BackColor = System.Drawing.Color.Sienna;
            resources.ApplyResources(this.button20, "button20");
            this.button20.Name = "button20";
            this.button20.Tag = "A6_RZ_-";
            this.button20.UseVisualStyleBackColor = false;
            this.button20.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ButtonControl_MouseDown);
            this.button20.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ButtonControl_MouseUp);
            // 
            // button16
            // 
            this.button16.BackColor = System.Drawing.Color.Sienna;
            resources.ApplyResources(this.button16, "button16");
            this.button16.Name = "button16";
            this.button16.Tag = "A2_Y_-";
            this.button16.UseVisualStyleBackColor = false;
            this.button16.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ButtonControl_MouseDown);
            this.button16.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ButtonControl_MouseUp);
            // 
            // button21
            // 
            this.button21.BackColor = System.Drawing.Color.Sienna;
            resources.ApplyResources(this.button21, "button21");
            this.button21.Name = "button21";
            this.button21.Tag = "A5_RY_+";
            this.button21.UseVisualStyleBackColor = false;
            this.button21.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ButtonControl_MouseDown);
            this.button21.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ButtonControl_MouseUp);
            // 
            // button15
            // 
            this.button15.BackColor = System.Drawing.Color.Sienna;
            resources.ApplyResources(this.button15, "button15");
            this.button15.Name = "button15";
            this.button15.Tag = "A2_Y_+";
            this.button15.UseVisualStyleBackColor = false;
            this.button15.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ButtonControl_MouseDown);
            this.button15.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ButtonControl_MouseUp);
            // 
            // button22
            // 
            this.button22.BackColor = System.Drawing.Color.Sienna;
            resources.ApplyResources(this.button22, "button22");
            this.button22.Name = "button22";
            this.button22.Tag = "A5_RY_-";
            this.button22.UseVisualStyleBackColor = false;
            this.button22.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ButtonControl_MouseDown);
            this.button22.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ButtonControl_MouseUp);
            // 
            // button18
            // 
            this.button18.BackColor = System.Drawing.Color.Sienna;
            resources.ApplyResources(this.button18, "button18");
            this.button18.Name = "button18";
            this.button18.Tag = "A3_Z_-";
            this.button18.UseVisualStyleBackColor = false;
            this.button18.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ButtonControl_MouseDown);
            this.button18.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ButtonControl_MouseUp);
            // 
            // button23
            // 
            this.button23.BackColor = System.Drawing.Color.Sienna;
            resources.ApplyResources(this.button23, "button23");
            this.button23.Name = "button23";
            this.button23.Tag = "A4_RX_+";
            this.button23.UseVisualStyleBackColor = false;
            this.button23.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ButtonControl_MouseDown);
            this.button23.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ButtonControl_MouseUp);
            // 
            // button17
            // 
            this.button17.BackColor = System.Drawing.Color.Sienna;
            resources.ApplyResources(this.button17, "button17");
            this.button17.Name = "button17";
            this.button17.Tag = "A3_Z_+";
            this.button17.UseVisualStyleBackColor = false;
            this.button17.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ButtonControl_MouseDown);
            this.button17.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ButtonControl_MouseUp);
            // 
            // button24
            // 
            this.button24.BackColor = System.Drawing.Color.Sienna;
            resources.ApplyResources(this.button24, "button24");
            this.button24.Name = "button24";
            this.button24.Tag = "A4_RX_-";
            this.button24.UseVisualStyleBackColor = false;
            this.button24.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ButtonControl_MouseDown);
            this.button24.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ButtonControl_MouseUp);
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.label15);
            this.groupBox7.Controls.Add(this.label16);
            this.groupBox7.Controls.Add(this.label17);
            this.groupBox7.Controls.Add(this.label18);
            this.groupBox7.Controls.Add(this.label19);
            this.groupBox7.Controls.Add(this.label20);
            resources.ApplyResources(this.groupBox7, "groupBox7");
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.TabStop = false;
            // 
            // label15
            // 
            resources.ApplyResources(this.label15, "label15");
            this.label15.Name = "label15";
            // 
            // label16
            // 
            resources.ApplyResources(this.label16, "label16");
            this.label16.Name = "label16";
            // 
            // label17
            // 
            resources.ApplyResources(this.label17, "label17");
            this.label17.Name = "label17";
            // 
            // label18
            // 
            resources.ApplyResources(this.label18, "label18");
            this.label18.Name = "label18";
            // 
            // label19
            // 
            resources.ApplyResources(this.label19, "label19");
            this.label19.Name = "label19";
            // 
            // label20
            // 
            resources.ApplyResources(this.label20, "label20");
            this.label20.Name = "label20";
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.radioButton2);
            this.groupBox8.Controls.Add(this.radioButton1);
            resources.ApplyResources(this.groupBox8, "groupBox8");
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.TabStop = false;
            // 
            // radioButton2
            // 
            resources.ApplyResources(this.radioButton2, "radioButton2");
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.UseVisualStyleBackColor = true;
            // 
            // radioButton1
            // 
            resources.ApplyResources(this.radioButton1, "radioButton1");
            this.radioButton1.Checked = true;
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.TabStop = true;
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // MainView
            // 
            this.AllowDrop = true;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(165)))), ((int)(((byte)(166)))), ((int)(((byte)(173)))));
            resources.ApplyResources(this, "$this");
            this.Controls.Add(this.groupBox8);
            this.Controls.Add(this.groupBox7);
            this.Controls.Add(this.groupBox6);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.tbAxisGroupRef);
            this.Controls.Add(this.tbDebug);
            this.Controls.Add(this.groupBox4);
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainView";
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MainView_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MainView_MouseMove);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbOverride)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            this.groupBox8.ResumeLayout(false);
            this.groupBox8.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button bStart;
		private System.Windows.Forms.Button bStop;
		private System.Windows.Forms.Timer cycle;
		private System.Windows.Forms.TextBox tbDebug;
		private System.Windows.Forms.TextBox tbAxisGroupRef;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button bReset;
        private System.Windows.Forms.CheckBox cbMoveEnable;
		private System.Windows.Forms.TrackBar tbOverride;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.Button bMoveToPos2;
		private System.Windows.Forms.Button bMoveToPos1;
		private System.Windows.Forms.GroupBox groupBox4;
		private System.Windows.Forms.Label lErrorID;
		private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox textBox7;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox textBox8;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox textBox9;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox textBox10;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox textBox11;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox textBox12;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox textBox6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.TextBox textBox13;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.Button button10;
        private System.Windows.Forms.Button button19;
        private System.Windows.Forms.Button button14;
        private System.Windows.Forms.Button button20;
        private System.Windows.Forms.Button button16;
        private System.Windows.Forms.Button button21;
        private System.Windows.Forms.Button button15;
        private System.Windows.Forms.Button button22;
        private System.Windows.Forms.Button button18;
        private System.Windows.Forms.Button button23;
        private System.Windows.Forms.Button button17;
        private System.Windows.Forms.Button button24;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.RadioButton radioButton1;
    }
}

