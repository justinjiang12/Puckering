﻿using System;
using System.Windows.Forms;
using System.ComponentModel;
using System.IO;
using System.Collections.Generic;
using FESIFS;
using RouteButler_Yaskawa;

namespace FesIF_Demo
{

    public partial class YaskawRobot : Form
	{

        #region Justin 欄位

        private readonly Yaskawa yaskawa = new Yaskawa();


        #endregion


        #region yaskawa <官方>
        /*必要宣告項目*/
        string IP ;
        int Port;
		short[]	err_code	= new short[2];
		private FesIF FESIF = new FesIF();
		int	socket_rtn		= -1; 
		int RESULT_OK		= 0;
        /*------------*/

        /*額外宣告區間*/

        int label;

        DirectoryInfo dirInfo;
        FileSystemWatcher _watch = new FileSystemWatcher();
        BindingList<PathData> PathDataList = new BindingList<PathData>();


        /*------------*/

        //視窗顯示內容
        public YaskawRobot()
		{
			InitializeComponent();
			tbMsg.Text = "請先輸入IP Address以及Port"+"\r\n"+"輸入後請按輸入完成";
		}

        //取得異常碼
		public string GetErr (short[] err_code)
		{
			string err_log = "\r\n《err_code》= " + err_code[0] + " , " + err_code[1] ;
			return err_log;
		}

        //IP位置及Port開設定
        private void button13_Click(object sender, EventArgs e)
        {
            tbMsg.Clear();
            IP = textBox10.Text;
            Port = int.Parse(textBox9.Text);
            tbMsg.Text += "請確認IP Address以及Port" + "\r\n" + "\r\n" + "IP Address = " + IP + "\r\n" + "Port = " + Port+ "\r\n" + "\r\n" + "若正確請勾選啟動通信";
        }

        //錯誤清除
        private void sample_Click(object sender, EventArgs e)
        {
            try
            {
                tbMsg.Text = "";
                if (cbSocket.Checked)
                {
                    /* open socket */
                    socket_rtn = FESIF.Open(IP, Port);
                    FESIF.ErrorCancel(err_code);
                    tbMsg.Text += "錯誤清除完畢" + GetErr(err_code);
                }
                else
                {
                    /* close socket */
                    if (RESULT_OK == socket_rtn) FESIF.Close();
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //警報解除
        private void cancel_Click(object sender, EventArgs e)
        {
            try
            {
                tbMsg.Text = "";
                if (cbSocket.Checked)
                {
                    /* open socket */
                    socket_rtn = FESIF.Open(IP, Port);
                    FESIF.AlmReset(err_code);
                    tbMsg.Text += "警報解除完畢" + GetErr(err_code);
                }
                else
                {
                    /* close socket */
                    if (RESULT_OK == socket_rtn) FESIF.Close();
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //伺服啟動
        private void serveron_Click(object sender, EventArgs e)
        {
            try
            {
                tbMsg.Text = "";
                if (cbSocket.Checked)
                {
                    /* open socket */
                    socket_rtn = FESIF.Open(IP, Port);
                    FESIF.ServoSwitch(1,err_code);
                    tbMsg.Text += "機器手臂伺服已啟動" + GetErr(err_code);
                }
                else
                {
                    /* close socket */
                    if (RESULT_OK == socket_rtn) FESIF.Close();
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //伺服關閉
        private void serveroff_Click(object sender, EventArgs e)
        {
            try
            {
                tbMsg.Text = "";
                if (cbSocket.Checked)
                {
                    /* open socket */
                    socket_rtn = FESIF.Open(IP, Port);
                    FESIF.ServoSwitch(2, err_code);
                    tbMsg.Text += "機器手臂伺服已關閉" + GetErr(err_code);
                }
                else
                {
                    /* close socket */
                    if (RESULT_OK == socket_rtn) FESIF.Close();
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //暫停
        private void hold_Click(object sender, EventArgs e)
        {
            try
            {
                tbMsg.Text = "";
                if (cbSocket.Checked)
                {
                    /* open socket */
                    socket_rtn = FESIF.Open(IP, Port);
                    FESIF.HoldSwitch(1, err_code);
                    tbMsg.Text += "機器手臂暫停中" + GetErr(err_code);
                }
                else
                {
                    /* close socket */
                    if (RESULT_OK == socket_rtn) FESIF.Close();
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //暫停解除
        private void no_hold_Click(object sender, EventArgs e)
        {
            try
            {
                tbMsg.Text = "";
                if (cbSocket.Checked)
                {
                    /* open socket */
                    socket_rtn = FESIF.Open(IP, Port);
                    FESIF.HoldSwitch(2, err_code);
                    tbMsg.Text += "機器手臂暫停解除" + GetErr(err_code);
                }
                else
                {
                    /* close socket */
                    if (RESULT_OK == socket_rtn) FESIF.Close();
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //教導盒鎖定
        private void hlock_Click(object sender, EventArgs e)
        {
            try
            {
                tbMsg.Text = "";
                if (cbSocket.Checked)
                {
                    /* open socket */
                    socket_rtn = FESIF.Open(IP, Port);
                    FESIF.HLockSwitch(1, err_code);
                    tbMsg.Text += "教導盒鎖定" + GetErr(err_code);
                }
                else
                {
                    /* close socket */
                    if (RESULT_OK == socket_rtn) FESIF.Close();
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //教導盒解鎖
        private void hunlock_Click(object sender, EventArgs e)
        {
            try
            {
                tbMsg.Text = "";
                if (cbSocket.Checked)
                {
                    /* open socket */
                    socket_rtn = FESIF.Open(IP, Port);
                    FESIF.HLockSwitch(2, err_code);
                    tbMsg.Text += "教導盒解鎖" + GetErr(err_code);
                }
                else
                {
                    /* close socket */
                    if (RESULT_OK == socket_rtn) FESIF.Close();
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //單步循環
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                tbMsg.Text = "";
                if (cbSocket.Checked)
                {
                    /* open socket */
                    socket_rtn = FESIF.Open(IP, Port);
                    FESIF.Cycle2Step(err_code);
                    tbMsg.Text += "已設定為單步循環" + GetErr(err_code);
                }
                else
                {
                    /* close socket */
                    if (RESULT_OK == socket_rtn) FESIF.Close();
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //一次循環
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                tbMsg.Text = "";
                if (cbSocket.Checked)
                {
                    /* open socket */
                    socket_rtn = FESIF.Open(IP, Port);
                    FESIF.Cycle2Cycle(err_code);
                    tbMsg.Text += "已設定為一次循環" + GetErr(err_code);
                }
                else
                {
                    /* close socket */
                    if (RESULT_OK == socket_rtn) FESIF.Close();
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //連續循環
        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                tbMsg.Text = "";
                if (cbSocket.Checked)
                {
                    /* open socket */
                    socket_rtn = FESIF.Open(IP, Port);
                    FESIF.Cycle2Cont(err_code);
                    tbMsg.Text += "已設定為連續循環" + GetErr(err_code);
                }
                else
                {
                    /* close socket */
                    if (RESULT_OK == socket_rtn) FESIF.Close();
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //呼叫JOB
        private void jobselect_Click(object sender, EventArgs e)
        {
            try
            {
                tbMsg.Text = "";
                if (cbSocket.Checked)
                {
                    /* open socket */
                    SelectJob select_job = new SelectJob();
                    select_job.name = textBox3.Text;
                    select_job.line = 0;
                    socket_rtn = FESIF.Open(IP, Port);
                    FESIF.JobSelect(1, select_job , err_code);
                    tbMsg.Text += "已呼叫 : " + textBox3.Text + GetErr(err_code);

                   
                }
                else
                {
                    /* close socket */
                    if (RESULT_OK == socket_rtn) FESIF.Close();
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //教導盒顯示訊息
        private void displayshow_Click(object sender, EventArgs e)
        {
            try
            {
                tbMsg.Text = "";
                if (cbSocket.Checked)
                {
                    /* open socket */
                    socket_rtn = FESIF.Open(IP, Port);
                    string msg = textBox2.Text;
                    FESIF.Display(msg,err_code);
                    tbMsg.Text += "教導盒顯示訊息 : " + textBox2.Text + GetErr(err_code);
                }
                else
                {
                    /* close socket */
                    if (RESULT_OK == socket_rtn) FESIF.Close();
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //機器手臂目前位置讀出(脈衝)
        private void getpospluse_Click(object sender, EventArgs e)
        {
            try
            {
                tbMsg.Text = "";
                if (cbSocket.Checked)
                {
                    /* open socket */
                    socket_rtn = FESIF.Open(IP, Port);
                    PosData nowpos = new PosData();
                    FESIF.RobPosSnglR(1,ref nowpos, err_code);
                    tbMsg.Text += "機器手臂目前位置為(脈衝) : " +
                        "\r\n" + "型態 = " + nowpos.pattern +
                        "\r\n" + "工具編號 = " + nowpos.tool_no +
                        "\r\n" + "使用者座標編號 = " + nowpos.user_coord_no +
                        "\r\n" + "擴張型態 = " + nowpos.ex_pattern +
                        "\r\n" + "S = " + nowpos.axis[0] +
                        "\r\n" + "L = " + nowpos.axis[1] +
                        "\r\n" + "U = " + nowpos.axis[2] +
                        "\r\n" + "R = " + nowpos.axis[3] +
                        "\r\n" + "B = " + nowpos.axis[4] +
                        "\r\n" + "T = " + nowpos.axis[5] +
                        "\r\n" + GetErr(err_code);
                }
                else
                {
                    /* close socket */
                    if (RESULT_OK == socket_rtn) FESIF.Close();
                    tbMsg.Text = "連線失敗。";
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //機器手臂目前位置讀出(基座)
        private void getposbase_Click(object sender, EventArgs e)
        {
            try
            {
                tbMsg.Text = "";
                if (cbSocket.Checked)
                {
                    /* open socket */
                    socket_rtn = FESIF.Open(IP, Port);

                    PosData nowpos = new PosData();
                    nowpos.type = 0;
                    nowpos.pattern = 0;
                    nowpos.tool_no = 0;
                    nowpos.user_coord_no = 0;
                    nowpos.ex_pattern = 0;
       
                    FESIF.RobPosSnglR(101, ref nowpos, err_code);
                    tbMsg.Text += "機器手臂目前位置為(基座) : " +
                        "\r\n" + "型態 = " + nowpos.pattern +
                        "\r\n" + "工具編號 = " + nowpos.tool_no +
                        "\r\n" + "使用者座標編號 = " + nowpos.user_coord_no +
                        "\r\n" + "擴張型態 = " + nowpos.ex_pattern +
                        "\r\n" + "X = " + nowpos.axis[0] +
                        "\r\n" + "Y = " + nowpos.axis[1] +
                        "\r\n" + "Z = " + nowpos.axis[2] +
                        "\r\n" + "Rx = " + nowpos.axis[3] +
                        "\r\n" + "Ry = " + nowpos.axis[4] +
                        "\r\n" + "Rz = " + nowpos.axis[5] +
                        "\r\n" + GetErr(err_code);
                }
                else
                {
                    /* close socket */
                    if (RESULT_OK == socket_rtn) FESIF.Close();
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //移動命令(脈衝)
        private void movpluse_Click(object sender, EventArgs e)
        {
            try
            {
                tbMsg.Text = "";
                if (cbSocket.Checked)
                {
                    if (checkBox1.Checked)
                    {
                        /* open socket */
                        socket_rtn = FESIF.Open(IP, Port);
                        PulseMove movpos = new PulseMove();

                        movpos.des.robot_group = 1;
                        movpos.des.station_group = 0;
                        movpos.des.speed_class = uint.Parse(movtype.Text);
                        movpos.des.speed = uint.Parse(movspeed.Text);
                        movpos.robot_pulse[0] = int.Parse(s.Text);
                        movpos.robot_pulse[1] = int.Parse(l.Text);
                        movpos.robot_pulse[2] = int.Parse(u.Text);
                        movpos.robot_pulse[3] = int.Parse(r.Text);
                        movpos.robot_pulse[4] = int.Parse(b.Text);
                        movpos.robot_pulse[5] = int.Parse(t.Text);
                        movpos.robot_pulse[6] = 0;
                        movpos.robot_pulse[7] = 0;

                        FESIF.MovePulse(1, movpos, err_code);
                        tbMsg.Text += "機器手臂移動至(脈衝) : " +
                            "\r\n" + "S = " + s.Text +
                            "\r\n" + "L = " + l.Text +
                            "\r\n" + "U = " + u.Text +
                            "\r\n" + "R = " + r.Text +
                            "\r\n" + "B = " + b.Text +
                            "\r\n" + "T = " + t.Text +
                            "\r\n" + GetErr(err_code);
                    }
                    else
                    {
                        tbMsg.Text += "請確認周遭是否安全";
                    }
                }
                else
                {
                    /* close socket */
                    if (RESULT_OK == socket_rtn) FESIF.Close();
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //移動命令(基座)
        private void movbase_Click(object sender, EventArgs e)
        {
            try
            {
                tbMsg.Text = "";
                if (cbSocket.Checked)
                {
                    if (checkBox1.Checked)
                    {
                        /* open socket */
                        socket_rtn = FESIF.Open(IP, Port);
                        CoordMove movpos = new CoordMove();

                        //CoordMove move_data = new CoordMove();
                        //move_data.des.robot_group = 1;
                        //move_data.des.station_group = 0;
                        //move_data.des.speed_class = 1;
                        //move_data.des.speed = 50;
                        //move_data.act_coord_des = 16;
                        //move_data.x_coord = 400000;
                        //move_data.y_coord = 30000;
                        //move_data.z_coord = 200000;
                        //move_data.Tx_coord = 1800000;
                        //move_data.Ty_coord = 0;
                        //move_data.Tz_coord = -400000;
                        //move_data.reserve = 0;
                        //move_data.ex_pattern = 0;
                        //move_data.tool_no = 0;
                        //move_data.user_coord_no = 1;
                        //move_data.axis.base_pos[0] = 0;
                        //move_data.axis.base_pos[1] = 0;
                        //move_data.axis.base_pos[2] = 0;
                        //move_data.axis.station_pos[0] = 0;
                        //move_data.axis.station_pos[1] = 0;
                        //move_data.axis.station_pos[2] = 0;
                        //move_data.axis.station_pos[3] = 0;
                        //move_data.axis.station_pos[4] = 0;
                        //move_data.axis.station_pos[5] = 0;

                        movpos.des.robot_group = 1;
                        movpos.des.station_group = 0;
                        movpos.des.speed_class = uint.Parse(movtype.Text);
                        movpos.des.speed = uint.Parse(movspeed.Text);
                        movpos.x_coord = int.Parse(s.Text);
                        movpos.y_coord = int.Parse(l.Text);
                        movpos.z_coord = int.Parse(u.Text);
                        movpos.Tx_coord = int.Parse(r.Text);
                        movpos.Ty_coord = int.Parse(b.Text);
                        movpos.Tz_coord = int.Parse(t.Text);
                        FESIF.MoveCoord(1, movpos, err_code);
                        tbMsg.Text += "機器手臂移動至(脈衝) : " +
                            "\r\n" + "X = " + s.Text +
                            "\r\n" + "Y = " + l.Text +
                            "\r\n" + "Z = " + u.Text +
                            "\r\n" + "Rx = " + r.Text +
                            "\r\n" + "Ry = " + b.Text +
                            "\r\n" + "Rz = " + t.Text +
                            "\r\n" + GetErr(err_code);
                    }
                    else
                    {
                        tbMsg.Text += "請確認周遭是否安全";
                    }
                }
                else
                {
                    /* close socket */
                    if (RESULT_OK == socket_rtn) FESIF.Close();
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            socket_rtn = FESIF.Open(IP, Port);
            short[] _errorcode = new short[2];
            uint _readData = 0;
            int _socketreturn = FESIF.StsElemR(2, ref _readData, _errorcode);

            const int mask = 1;
            var binary = string.Empty;
            while (_readData > 0)
            {
                // Logical AND the number and prepend it to the result string
                binary = (_readData & mask) + binary;
                _readData = _readData >> 1;
            }
            binary = binary.PadLeft(8, '0');
            char[] binaryarray = binary.ToCharArray();
            byte[] decode = new byte[8];
            for (int i = 0; i < binaryarray.Length; i++)
                decode[i] = (byte)binaryarray[binaryarray.Length - 1 - i];
            FESIF.Close();
        }

        #endregion

        #region Justin test

        #region 控制物件管理
        /// <summary>
        /// 按鈕管理巨集
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_Click(object sender, EventArgs e)
        {
            int rslt;
            Button btn = (Button)sender;
            string tag = (string)btn.Tag;
            try
            {

                switch (tag)
                {
                    case "btn_connect":

                        rslt = yaskawa.Connect(textBox1.Text);
                        if (rslt == 0)
                        {
                            //FormStart();
                            //timer_robot.Start();
                            //timer_UI.Start();
                        }

                        break;

                    case "btn_SVON":

                        rslt = yaskawa.ServoSwitch(1);
                        //txt_systemstate.Text = yaskawa.ExecuteError;
                        if (yaskawa.ExecuteError == "0,0")
                        {
                            //btn_srvon.Enabled = false;
                            //btn_srvoff.Enabled = true;
                        }

                        break;

                    case "btn_SVOFF":

                        rslt = yaskawa.ServoSwitch(2);
                        //txt_systemstate.Text = yaskawa.ExecuteError;
                        if (yaskawa.ExecuteError == "0,0")
                        {
                            //btn_srvon.Enabled = true;
                            //btn_srvoff.Enabled = false;
                        }

                        break;

                    case "btn_STOP":


                        break;

                    case "btn_START":


                        break;

                    case "btn_PROLOAD":


                        break;

                    case "btn_PRORUN":


                        break;

                    case "btn_BRO":

                        try
                        {
                            OpenFileDialog dlg = new OpenFileDialog();
                            dlg.ShowDialog();
                            Browse_textbox.Text = dlg.FileName;
                        }
                        catch { MessageBox.Show("system error"); }

                        break;

                    case "btn_LOADDATA":
                        try
                        {
                            dataGridView1.Rows.Clear();
                            LoadCSV(Browse_textbox.Text);
                        }
                        catch { MessageBox.Show("system error"); }

                        break;

                    case "btn_PROWRITE":


                        break;



                    case "btn_PRODLOAD":

                        RobotCompileProgram(textBox4.Text);
                        break;

                        


                }
            }
            catch (Exception x) { MessageBox.Show(x.ToString(), "systen error!!!"); }
        }
        #endregion

        #region 程式編寫內容

        /// <summary>
        /// 取得csv並匯入DataGridView
        /// </summary>
        public void LoadCSV(string csvFile)
        {

            var listOfStrings = new List<string>();
            string[] ss = listOfStrings.ToArray();
            int p_num = 0;

            dataGridView1.DataSource = PathDataList;

            foreach (string s in File.ReadAllLines(csvFile))
            {
                ss = s.Split(','); //將一列的資料，以逗號的方式進行資料切割，並將資料放入一個字串陣列       

                PathDataList.Add(new PathData
                {
                    Name = ss[0],
                    X = Math.Round(double.Parse(ss[1]) + 170, 2, MidpointRounding.AwayFromZero),
                    Y = Math.Round(double.Parse(ss[2]) + 150, 2, MidpointRounding.AwayFromZero),
                    Z = Math.Round(0 - double.Parse(ss[3]), 2, MidpointRounding.AwayFromZero),
                    A = double.Parse(ss[4]),
                    B = double.Parse(ss[5]),
                    C = double.Parse(ss[6])
                });

            }

        }



        /// <summary>
        /// 編寫Robot Program
        /// </summary>
        public bool RobotCompileProgram(string _programName)
        {
            try
            {
                RouteBook_Yaskawa routeBook_Welding = new RouteBook_Yaskawa(_programName,dataGridView1.RowCount, 0, 0);
                routeBook_Welding.Workspace = 0;
                routeBook_Welding.FlipMode = 1;

                for (int i = 0; i < dataGridView1.RowCount-1; i++)
                {
                    routeBook_Welding.ProcessQueue[i] = 1;
                    routeBook_Welding.MovingMode[i] = 2;
                    routeBook_Welding.Tool[i] = 0;
                    routeBook_Welding.Override[i] = 10;
                    routeBook_Welding.Accerlerate[i] = 70;
                    routeBook_Welding.Decerlerate[i] = 70;

                    routeBook_Welding.X[i] = (double)dataGridView1.Rows[i].Cells[0].Value;
                    routeBook_Welding.Y[i] = (double)dataGridView1.Rows[i].Cells[1].Value;
                    routeBook_Welding.Z[i] = (double)dataGridView1.Rows[i].Cells[2].Value;
                    routeBook_Welding.A[i] = (double)dataGridView1.Rows[i].Cells[3].Value;
                    routeBook_Welding.B[i] = (double)dataGridView1.Rows[i].Cells[4].Value;
                    routeBook_Welding.C[i] = (double)dataGridView1.Rows[i].Cells[5].Value;

                }

                int rslt = yaskawa.SaveFile(routeBook_Welding, _programName, 1);
                return true;
            }
            catch { MessageBox.Show("system error"); return false; }


        }

        #endregion

        #endregion

    }


    /// <summary>
    /// 新增一個類別For 點位 Data 欄位
    /// </summary>
    public class PathData : INotifyPropertyChanged
    {
        #region 插入更改事件
        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(string p)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(p));
        }

        #endregion

        private string _Name;
        private double _X;
        private double _Y;
        private double _Z;
        private double _A;
        private double _B;
        private double _C;

        public string Name
        {
            get { return _Name; }
            set
            {
                _Name = value;
                NotifyPropertyChanged(nameof(Name));
            }
        }

        public double X
        {
            get { return _X; }
            set
            {
                _X = value;
                NotifyPropertyChanged(nameof(X));
            }
        }

        public double Y
        {
            get { return _Y; }
            set
            {
                _Y = value;
                NotifyPropertyChanged(nameof(Y));
            }
        }

        public double Z
        {
            get { return _Z; }
            set
            {
                _Z = value;
                NotifyPropertyChanged(nameof(Z));
            }
        }

        public double A
        {
            get { return _A; }
            set
            {
                _A = value;
                NotifyPropertyChanged(nameof(A));
            }
        }

        public double B
        {
            get { return _B; }
            set
            {
                _B = value;
                NotifyPropertyChanged(nameof(B));
            }
        }


        public double C
        {
            get { return _C; }
            set
            {
                _C = value;
                NotifyPropertyChanged(nameof(C));
            }
        }


    }
}
