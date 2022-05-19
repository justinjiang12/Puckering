using System;
using System.Windows.Forms;
using System.ComponentModel;
using System.IO;
using System.Collections.Generic;
using System.Text;
using FESIFS;
using RouteButler_Yaskawa;

namespace FesIF_Demo
{
    public partial class YaskawRobot : Form
	{

        #region  欄位


        private readonly Yaskawa YaskawaController = new Yaskawa();
        private int _Welding=0;
        private int _TimmerCunt = 0;
        int label;

        DirectoryInfo dirInfo;
        FileSystemWatcher _watch = new FileSystemWatcher();
        BindingList<PathData> PathDataList = new BindingList<PathData>();
        PosData _GetPosData = new PosData();
        #endregion

        /// <summary>
        /// 建構子
        /// </summary>
        public YaskawRobot()
        {
            InitializeComponent();
            PCFileRefresh();
        }


        #region 控制物件管理
        /// <summary>
        /// 按鈕管理巨集
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_Click(object sender, EventArgs e)
        {
            int _rslt;
            Button btn = (Button)sender;
            string tag = (string)btn.Tag;
            try
            {

                switch (tag)
                {
                    #region 連線
                    case "btn_connect":

                        _rslt = YaskawaController.Connect(textBox1.Text);
                        RobotProgramRefresh();
                        if (_rslt == 0)
                        {

                        }

                        RobotListBoxTimer.Enabled = true;
                        StateTimer.Enabled = true;
                        
                        break;
                    #endregion

                    #region 激磁
                    case "btn_SVON":

                        _rslt = YaskawaController.ServoSwitch(1);
                        //txt_systemstate.Text = yaskawa.ExecuteError;
                        if (YaskawaController.ExecuteError == "0,0")
                        {
                            //btn_srvon.Enabled = false;
                            //btn_srvoff.Enabled = true;
                        }

                        break;

                    #endregion

                    #region 激磁關閉
                    case "btn_SVOFF":

                        _rslt = YaskawaController.ServoSwitch(2);
                        //txt_systemstate.Text = yaskawa.ExecuteError;
                        if (YaskawaController.ExecuteError == "0,0")
                        {
                            //btn_srvon.Enabled = true;
                            //btn_srvoff.Enabled = false;
                        }

                        break;
                    #endregion

                    #region 取得點位資料路徑
                    case "btn_BRO":

                            OpenFileDialog dlg = new OpenFileDialog();
                            dlg.ShowDialog();
                            Browse_textbox.Text = dlg.FileName;
                        
                        break;

                    #endregion 

                    #region 寫入 dataGridView
                    case "btn_LOADDATA":
                        
                            dataGridView1.Rows.Clear();
                            LoadCSV(Browse_textbox.Text);
                        
                        break;
                    #endregion

                    #region 編輯成JBI檔案，並顯示於tex_ProgramTXT
                    case "btn_PROCOOMPILE":
                        
                        tex_ProgramTXT.Clear();
                        RobotCompileProgram(tex_ProgramName.Text.ToUpper());
                        
                        string path = ".\\" + tex_ProgramName.Text.ToUpper() + ".JBI";
                        tex_ProgramTXT.Text = File.ReadAllText(path);
                        if (listBox1.Items.Count > 0) { listBox1.Items.Clear(); }
                        PCFileRefresh();
                        MessageBox.Show("完成");
                        break;
                    #endregion



                    #region 更新Contriller內部程式資料
                    case "btn_RefProgram":
                        if (listBox2.Items.Count > 0) { listBox2.Items.Clear(); }
                        RobotProgramRefresh();

                        break;
                    #endregion

                    #region 啟動程式
                    case "btn_ProgramGO": //ProgramRun
                        _rslt = YaskawaController.RunProgram(tex_ProgramName.Text);

                        break;
                    #endregion

                    #region Auto(啟動)
                    case "btn_GoCycle": //start
                        _rslt = YaskawaController.RobotStopSwitch(1);

                        break;
                    #endregion

                    #region Auto(停止)
                    case "btn_Stop": //stop
                        _rslt = YaskawaController.RobotStopSwitch(2);

                        break;
                    #endregion

                    #region Reg Write
                    case "btn_RegWrite": //RegWrite

                        RobotRegWrite(Convert.ToInt16(tex_RegData.Text),Convert.ToInt16(tex_RegNum.Text));

                        break;
                    #endregion

                    #region Reg Read
                    case "btn_RegRead": //RegRead
                        short _var = 0;
                        int _RegDataNum = Convert.ToInt32(tex_RegData.Text) + 1;
                        RobotRegRead(Convert.ToInt16(_RegDataNum),ref _var);
                        lab_RegData.Text = "Reg Data: " + _var.ToString();
                        break;

                    #endregion

                    #region Reg Scan Start
                    case "btn_RegScanStart": //RegScanStart

                        RegScanTimer.Interval = Convert.ToInt32(tex_RegTimerScan.Text);
                        RegScanTimer.Enabled = true;


                        break;
                    #endregion

                    #region Reg Scan Stop
                    case "btn_RegScanStop": //RegScanStop

                        
                        RegScanTimer.Enabled = false;
                    
                        break;
                    #endregion

                    #region 程序刷新按鈕觸發 (PC_JBI)
                    case "btn_RefPCFile":

                        PCFileRefresh();

                        break;
                    #endregion

                    #region 程序刷新按鈕觸發 (Controller_JBI)
                    case "btn_RefRobotFile":

                        RobotProgramRefresh();

                        break;
                    #endregion

                    #region 資料刪除按鈕 (PC_JBI)
                    case "btn_DeletePCFile":
                        if (listBox1.SelectedIndex != -1)
                        {
                            DialogResult dr = MessageBox.Show("確定要刪除 " + listBox1.SelectedItem.ToString() + " ???"
                                , "Closing event!", MessageBoxButtons.YesNo);
                            if (dr == DialogResult.Yes)
                            {
                                PCFileDelete(listBox1.SelectedItem.ToString());
                                PCFileRefresh();
                            }
                        }
                        else { MessageBox.Show("請點選 PC File!!!"); }
                        

                        break;
                    #endregion


                    #region 資料上載 (PC --> Controller)
                    case "btn_PROLoad":
                        if (listBox1.SelectedIndex != -1)
                        {
                            DialogResult dr = MessageBox.Show("確定要上載 " + listBox1.SelectedItem.ToString() + " ???"
                            , "Closing event!", MessageBoxButtons.YesNo);
                            if (dr == DialogResult.Yes)
                            {
                                _rslt = YaskawaController.Upload2Controller(listBox1.SelectedItem.ToString());
                                MessageBox.Show("完成");
                                RobotProgramRefresh();
                            }

                        }
                        else { MessageBox.Show("請點選 PC File!!!"); }

                        break;
                    #endregion


                    #region 資料下載 (Controller --> PC)
                    case "btn_PRODonload":

                        if (listBox2.SelectedIndex != -1)
                        {
                            DialogResult dr = MessageBox.Show("確定要下載 " + listBox2.SelectedItem.ToString() + " ???"
                                , "Closing event!", MessageBoxButtons.YesNo);
                            if (dr == DialogResult.Yes)
                            {
                                _rslt = YaskawaController.DonwloadFile(listBox2.SelectedItem.ToString() + ".JBI");
                                PCFileRefresh();
                            }
                        }
                        else { MessageBox.Show("請點選 Controller File!!!"); }
                            

                        break;
                    #endregion


                    #region 資料刪除按鈕 (Controller_JBI)
                    case "btn_DeleteRobotFile":

                        if (listBox2.SelectedIndex != -1)
                        {
                            DialogResult dr = MessageBox.Show("確定要刪除 "+ listBox2.SelectedItem.ToString()+" ???"
                                , "Closing event!", MessageBoxButtons.YesNo);
                            if (dr == DialogResult.Yes)
                            {
                                ContrillerFileDelete(listBox2.SelectedItem.ToString() + ".JBI");
                                RobotProgramRefresh();
                            }
                        }
                        else { MessageBox.Show("請點選 Controller File!!!"); }

                        break;

                        #endregion

                        

                    #region 取得點位資料按鈕 
                    case "btn_GetPos":


                        RobotPosGet(Convert.ToInt16(tex_PointDataNum.Text));


                        break;

                        #endregion


                    #region 取得目前點位資料按鈕 
                    case "btn_GetCurPos":


                        RobotCurPosGet(Convert.ToInt16(tex_PointDataNum.Text));


                        break;

                    #endregion

                    #region 寫入點位資料按鈕 
                    case "btn_SetPos":


                        RobotPosSet_TestBtn(Convert.ToInt16(tex_WPointNum.Text));

                        break;

                    #endregion

                    #region 定位觸發(Base)
                    case "btn_PosBaseGo":

                        RobotPosMoveBase();

                        break;

                    #endregion

                    #region 定位觸發(Pluse)
                    case "btn_PosPlsGo":

                        RobotPosMovePluse();


                        break;

                        #endregion


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
            //int p_num = 0;

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
        /// 編寫Robot Program (for Yaskawa)
        /// </summary>
        public bool RobotCompileProgram(string _programName)
        {
            try
            {
                // 實作程式物件
                RouteBook_Yaskawa _routeBook_Welding = new RouteBook_Yaskawa(_programName,dataGridView1.RowCount, 0, 0);
                //寫入工作環境生成狀態
                _routeBook_Welding.Workspace = 0;
                _routeBook_Welding.FlipMode = 1;

                for (int i = 0; i < dataGridView1.RowCount-1; i++)
                {
                    if (rad_MOVL.Checked)
                    {
                        //填入Point 資料屬性
                        _routeBook_Welding.ProcessQueue[i] = 1;  // (1: Point  2: Dout  3: 註解)
                        _routeBook_Welding.MovingMode[i] = 1; //  (1: MOVL  2: MOVS )
                        _routeBook_Welding.Tool[i] = 0;
                        _routeBook_Welding.Override[i] = 10;
                        _routeBook_Welding.Accerlerate[i] = 70;
                        _routeBook_Welding.Decerlerate[i] = 70;
                        //填入Point 資料數值
                        _routeBook_Welding.X[i] = (double)dataGridView1.Rows[i].Cells[1].Value;
                        _routeBook_Welding.Y[i] = (double)dataGridView1.Rows[i].Cells[2].Value;
                        _routeBook_Welding.Z[i] = (double)dataGridView1.Rows[i].Cells[3].Value;
                        _routeBook_Welding.A[i] = (double)dataGridView1.Rows[i].Cells[4].Value;
                        _routeBook_Welding.B[i] = (double)dataGridView1.Rows[i].Cells[5].Value;
                        _routeBook_Welding.C[i] = (double)dataGridView1.Rows[i].Cells[6].Value;
                    }
                    if (rad_MOVS.Checked)
                    {
                        //填入Point 資料屬性
                        _routeBook_Welding.ProcessQueue[i] = 1;  // (1: Point  2: Dout  3: 註解)
                        _routeBook_Welding.MovingMode[i] = 2; //  (1: MOVL  2: MOVS )
                        _routeBook_Welding.Tool[i] = 0;
                        _routeBook_Welding.Override[i] = 10;
                        _routeBook_Welding.Accerlerate[i] = 70;
                        _routeBook_Welding.Decerlerate[i] = 70;
                        //填入Point 資料數值
                        _routeBook_Welding.X[i] = (double)dataGridView1.Rows[i].Cells[1].Value;
                        _routeBook_Welding.Y[i] = (double)dataGridView1.Rows[i].Cells[2].Value;
                        _routeBook_Welding.Z[i] = (double)dataGridView1.Rows[i].Cells[3].Value;
                        _routeBook_Welding.A[i] = (double)dataGridView1.Rows[i].Cells[4].Value;
                        _routeBook_Welding.B[i] = (double)dataGridView1.Rows[i].Cells[5].Value;
                        _routeBook_Welding.C[i] = (double)dataGridView1.Rows[i].Cells[6].Value;
                    }
                                     
                }

                //程式寫入
                int _rslt;
                if (rad_Base.Checked) 
                { _rslt = YaskawaController.CompileFile(_routeBook_Welding, _programName, 1); }
                if (rad_Speed.Checked) 
                { _rslt = YaskawaController.CompileFile(_routeBook_Welding, _programName, 1,Convert.ToInt32(tex_SpeedReg.Text)); }
                if (rad_Welding.Checked) 
                { _rslt = YaskawaController.CompileFile(_routeBook_Welding, _programName, 1, Convert.ToInt32(tex_WAReg.Text), Convert.ToInt32(tex_WVReg.Text)); }
                if (rad_SpeedWelding.Checked) 
                { _rslt = YaskawaController.CompileFile(_routeBook_Welding, _programName, 1, Convert.ToInt32(tex_SpeedReg.Text), Convert.ToInt32(tex_WAReg.Text), Convert.ToInt32(tex_WVReg.Text)); }


                MessageBox.Show("OK!!!"); 
                return true;
            }
            catch { MessageBox.Show("system error"); return false; }
        }

        /// <summary>
        /// Robot Program Refresh
        /// </summary>
        /// <returns></returns>
        private void RobotProgramRefresh()
        {
            try
            {
                string _fileName= String.Empty;
                string _txtPath = "RobotProgeamFile.txt";
                var _CurrentDirectory = Directory.GetCurrentDirectory(); //取得目前執行路徑
                string _rtxtpath = _CurrentDirectory + "\\" + _txtPath;
                int _counter = 0;
                string _robotFileName = String.Empty;
                if (listBox2.Items.Count > 0) { listBox2.Items.Clear(); }
                if (comboBox_RobotFile.Items.Count > 0) { comboBox_RobotFile.Items.Clear(); }
                YaskawaController.ReadFileList(ref _fileName);
                File.WriteAllText(_txtPath, _fileName, Encoding.UTF8);

                foreach (string line in File.ReadLines(_rtxtpath))
                {
                    _robotFileName = line.Remove(line.Length - 4, 4);
                    listBox2.Items.Add(_robotFileName);
                    comboBox_RobotFile.Items.Add(_robotFileName);
                    _counter++;
                }

            }
            catch { MessageBox.Show("system error"); }
            
        }

        /// <summary>
        /// Write robot [I***] data
        /// </summary>
        /// <param name="_num"></param>
        /// <param name="_data"></param>
        private void RobotRegWrite(short _num,short _data )
        {
            int _rslt;
            int _Dnum = _num;
            _rslt = YaskawaController.WriteIData(Convert.ToInt16(_Dnum+1), _data);

            /*
            short[] _var = new short[_num+1];
            _var[_num] = _data;
            _rslt = YaskawaController.WriteIData(0, _var);
            */
        }

        /// <summary>
        /// Read robot [I***] data
        /// </summary>
        /// <param name="_num"></param>
        /// <param name="_data"></param>
        private void RobotRegRead(short _num,ref short _data)
        {
            int _rslt;
            short[] _getNum = new short[1];
            _rslt = YaskawaController.ReadIData(_num, 1, ref _getNum);
            _data = _getNum[0];
        }
        
        /// <summary>
        /// Robot Position Data Get [P***]
        /// </summary>
        /// <param name="_posnum"></param>
        private void RobotPosGet(short _posnum)
        {
            YaskawaController.GetPosData(_posnum, ref _GetPosData);
            lab_posData.Text =
                ("P[ " + _posnum.ToString() + " ]:" + "\n\n Type = " + _GetPosData.type + ", 形態 = " + _GetPosData.pattern + ",\n Tool number = " + _GetPosData.tool_no
                + ", User number = " + _GetPosData.user_coord_no + ",\n 擴張形態 = " + _GetPosData.ex_pattern
                + "\n\n X = " + _GetPosData.axis[0]
                + "\n Y = " + _GetPosData.axis[1]
                + "\n Z = " + _GetPosData.axis[2]
                + "\n A = " + _GetPosData.axis[3]
                + "\n B = " + _GetPosData.axis[4]
                + "\n C = " + _GetPosData.axis[5]
                + "\n NULL = " + _GetPosData.axis[6]
                + "\n NULL = " + _GetPosData.axis[7]);
        }


        /// <summary>
        /// Robot Position Data Get [P***]
        /// </summary>
        /// <param name="_posnum"></param>
        private void RobotCurPosGet(short _posnum)
        {
            YaskawaController.GetCurPosData(_posnum, ref _GetPosData);
            lab_posData.Text =
                ("P[ " + _posnum.ToString() + " ]:" + "\n\n Type = " + _GetPosData.type + ", 形態 = " + _GetPosData.pattern + ",\n Tool number = " + _GetPosData.tool_no
                + ", User number = " + _GetPosData.user_coord_no + ",\n 擴張形態 = " + _GetPosData.ex_pattern
                + "\n\n X = " + _GetPosData.axis[0]
                + "\n Y = " + _GetPosData.axis[1]
                + "\n Z = " + _GetPosData.axis[2]
                + "\n A = " + _GetPosData.axis[3]
                + "\n B = " + _GetPosData.axis[4]
                + "\n C = " + _GetPosData.axis[5]
                + "\n NULL = " + _GetPosData.axis[6]
                + "\n NULL = " + _GetPosData.axis[7]);
        }


        /// <summary>
        /// Robot Pos Set_Test Btn
        /// </summary>
        /// <param name="_pointNum"></param>
        private void RobotPosSet_TestBtn(short _pointNum)
        {
            PosData _posD = new PosData();
            int _axisValue = 0;
            _posD.type = uint.Parse(tex_WPointType.Text);
            _posD.pattern = int.Parse(tex_WPointPattern.Text);
            _posD.tool_no = uint.Parse(tex_WPointToolNum.Text);
            _posD.user_coord_no = uint.Parse(tex_WPointUserNum.Text);
            _posD.ex_pattern = int.Parse(tex_WPointExPattern.Text);
            _posD.axis[0] = int.Parse(tex_WPoinX.Text);
            _posD.axis[1] = int.Parse(tex_WPoinY.Text);
            _posD.axis[2] = int.Parse(tex_WPoinZ.Text);
            _posD.axis[3] = int.Parse(tex_WPoinA.Text);
            _posD.axis[4] = int.Parse(tex_WPoinB.Text);
            _posD.axis[5] = int.Parse(tex_WPoinC.Text);
            _posD.axis[6] = _axisValue;
            _posD.axis[7] = _axisValue;
            YaskawaController.SetPosData(_pointNum, _posD);

        }



        /// <summary>
        /// Robot Pos Move Base (XYZ)
        /// </summary>
        private void RobotPosMoveBase()
        {
            CoordMove _baseData = new CoordMove();
            _baseData.des.robot_group = 1;
            _baseData.des.station_group = 0;
            if (combox_BMovetype.Text== "MoveJ") { _baseData.des.speed_class = 0; }
            if (combox_BMovetype.Text == "MoveL") { _baseData.des.speed_class = 1; }
            _baseData.des.speed = uint.Parse(tex_PosP_Speed.Text);
            _baseData.act_coord_des = 16;
            _baseData.x_coord = int.Parse(tex_PosP_S.Text);
            _baseData.y_coord = int.Parse(tex_PosP_L.Text);
            _baseData.z_coord = int.Parse(tex_PosP_U.Text);
            _baseData.Tx_coord = int.Parse(tex_PosP_R.Text);
            _baseData.Ty_coord = int.Parse(tex_PosP_B.Text);
            _baseData.Tz_coord = int.Parse(tex_PosP_T.Text);

            _baseData.reserve = 0;
            _baseData.reserve2 = 0;
            _baseData.ex_pattern = 0;
            _baseData.tool_no = 0;
            _baseData.user_coord_no = 1;
            _baseData.axis.base_pos[0] = 0;
            _baseData.axis.base_pos[1] = 0;
            _baseData.axis.base_pos[2] = 0;
            _baseData.axis.station_pos[0] = 0;
            _baseData.axis.station_pos[1] = 0;
            _baseData.axis.station_pos[2] = 0;
            _baseData.axis.station_pos[3] = 0;
            _baseData.axis.station_pos[4] = 0;
            _baseData.axis.station_pos[5] = 0;

            YaskawaController.RobotMoveBase(_baseData);

        }

        /// <summary>
        /// Robot Pos Move Pluse (J1/J2/J3)
        /// </summary>
        private void RobotPosMovePluse()
        {
            PulseMove _plsData = new PulseMove();
            _plsData.des.robot_group = 1;
            _plsData.des.station_group = 0; 
            if (combox_PMovetype.Text == "MoveJ") { _plsData.des.speed_class = 0; }
            if (combox_PMovetype.Text == "MoveL") { _plsData.des.speed_class = 1; }
            _plsData.des.speed = uint.Parse(tex_PosP_Speed.Text);
            _plsData.robot_pulse[0] = int.Parse(tex_PosP_S.Text);
            _plsData.robot_pulse[1] = int.Parse(tex_PosP_L.Text);
            _plsData.robot_pulse[2] = int.Parse(tex_PosP_U.Text);
            _plsData.robot_pulse[3] = int.Parse(tex_PosP_R.Text);
            _plsData.robot_pulse[4] = int.Parse(tex_PosP_B.Text);
            _plsData.robot_pulse[5] = int.Parse(tex_PosP_T.Text);
            _plsData.robot_pulse[6] = 0;
            _plsData.robot_pulse[7] = 0;
            YaskawaController.RobotMovePls(_plsData);

        }


        private void RobotStateCheck()
        {
            YaskawaController.CheckStatus();
            lab_Step.Text = "Step : " + YaskawaController.Step.ToString();
            lab_OneCycle.Text = "Step : " + YaskawaController.OneCycle.ToString();
            lab_Automatic.Text = "Step : " + YaskawaController.AutomaticAndContinuos.ToString();
            lab_Running.Text = "Step : " + YaskawaController.isBusy.ToString();
            lab_GuardSafeOp.Text = "Step : " + YaskawaController.InGuardSafeOperation.ToString();
            lab_Teach.Text = "Step : " + YaskawaController.Teach.ToString();
            lab_Play.Text = "Step : " + YaskawaController.Play.ToString();
            lab_ComRemote.Text = "Step : " + YaskawaController.CommendRemote.ToString();
            lab_HoldP.Text = "Step : " + YaskawaController.HoldON.ToString();
            lab_HoldE.Text = "Step : " + YaskawaController.HoldStatusE.ToString();
            lab_HoldC.Text = "Step : " + YaskawaController.HoldStatusC.ToString();
            lab_Alarm.Text = "Step : " + YaskawaController.Alarm.ToString();
            lab_Error.Text = "Step : " + YaskawaController.Error.ToString();
            lab_SVON.Text = "Step : " + YaskawaController.ServoON.ToString();

        }




        #endregion

        #region File ListBox

        /// <summary>
        /// PC File Refresh
        /// </summary>
        /// <returns></returns>
        private void PCFileRefresh()
        {
            var _CurrentDirectory = Directory.GetCurrentDirectory(); //取得目前執行路徑
            string[] _files = Directory.GetFiles(_CurrentDirectory, "*.JBI"); //取得目前.JBI 路徑
            if (listBox1.Items.Count > 0) { listBox1.Items.Clear(); }
            //依序寫入 listBox1 Items 
            foreach (var _file in _files)
            {                
                listBox1.Items.Add(Path.GetFileNameWithoutExtension(_file)); //不含副檔名
                //listBox1.Items.Add(Path.GetFileName(_file)); //含副檔名
            }

        }

        /// <summary>
        /// PC 刪除JBI資料
        /// </summary>
        /// <param name="_fileName"></param>
        private void PCFileDelete(string _fileName)
        {
            var _CurrentDirectory = Directory.GetCurrentDirectory(); //取得目前執行路徑
            string path = _CurrentDirectory+"\\"+ _fileName+".JBI";
            bool result = File.Exists(path);
            if (result == true)
            {
                Console.WriteLine("File Found");
                File.Delete(path);
                Console.WriteLine("File Deleted Successfully");
            }
            else
            {
                Console.WriteLine("File Not Found");
            }
        }

        /// <summary>
        /// Yaskawa Controller 刪除JBI資料
        /// </summary>
        /// <param name="_fileName"></param>
        private void ContrillerFileDelete(string _fileName)
        {
            int _rslt = YaskawaController.DeleteFile(_fileName);
            if (_rslt == 1)
            {
                Console.WriteLine("File Found");
                Console.WriteLine("File Deleted Successfully");
            }
            else
            {
                Console.WriteLine("File Not Found");
            }
        }

        #endregion

        #region Timer Funtion

        /// <summary>
        /// RegScanTimer (Test)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RegScanTimer_Tick(object sender, EventArgs e)
        {
            /*

            if (_TimmerCunt < 50)
            {
                RobotRegWrite(Convert.ToInt16(tex_RegData.Text), _TimmerCunt); //Write

                short _var = 0;
                RobotRegRead(Convert.ToInt16(tex_RegData.Text), ref _var); //Read
                lab_RegData.Text = "Reg Data: " + _var.ToString();
                _TimmerCunt++;
            }
            else { _TimmerCunt = 0; }
            */
        }

        /// <summary>
        /// Robot ListBoxTimer Tick
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RobotListBoxTimer_Tick(object sender, EventArgs e)
        {

        }

        private void StateTimer_Tick(object sender, EventArgs e)
        {
            RobotStateCheck();

        }



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
