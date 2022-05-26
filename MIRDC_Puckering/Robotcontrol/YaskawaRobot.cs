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
        bool _connectState = false;

        DirectoryInfo dirInfo;
        FileSystemWatcher _watch = new FileSystemWatcher();
        BindingList<PathData> PathDataList = new BindingList<PathData>();
        
        #region PosData data 
        PosData _GetPosData = new PosData();
        PosData _GetCurXYZPosData = new PosData();
        PosData _GetCurSLUPosData = new PosData();
        PosData _GetJOGCurXYZPosData = new PosData();
        PosData _GetJOGCurSLUPosData = new PosData();
        PosData _HomePosData = new PosData();
        PosData _WTData_Load = new PosData();
        PosData _WTData_P1 = new PosData();
        PosData _WTData_P2 = new PosData();
        PosData _WTData_Unload = new PosData();
        #endregion

        bool _WTLoad = false, _WTP1 = false, _WTP2 = false, _WTUnload = false;

        //Robot State
        string _Step = string.Empty, _OneCycle = string.Empty, _AutomaticAndContinuos = string.Empty, _isBusy = string.Empty, 
                _InGuardSafeOperation = string.Empty,_Teach = string.Empty, _Play = string.Empty, _CommendRemote = string.Empty,
                _HoldON = string.Empty, _HoldStatusE = string.Empty,_HoldStatusC = string.Empty, _Alarm = string.Empty,
                _Error = string.Empty, _ServoON = string.Empty;

        //Jog Lim (-)
        int _JogLimM_X = 0, _JogLimM_Y = 0, _JogLimM_Z = 0, _JogLimM_RX = 0, _JogLimM_RY = 0, _JogLimM_RZ = 0, 
            _JogLimM_S = 0, _JogLimM_L = 0, _JogLimM_U = 0,_JogLimM_R = 0, _JogLimM_B = 0, _JogLimM_T = 0;
        //Jog Lim (+)
        int _JogLimP_X = 0, _JogLimP_Y = 0, _JogLimP_Z = 0, _JogLimP_RX = 0, _JogLimP_RY = 0, _JogLimP_RZ = 0,
            _JogLimP_S = 0, _JogLimP_L = 0, _JogLimP_U = 0, _JogLimP_R = 0, _JogLimP_B = 0, _JogLimP_T = 0;

        #endregion

        /// <summary>
        /// 建構子
        /// </summary>
        public YaskawRobot()
        {
            InitializeComponent();
            PCFileRefresh();
            SetHomePoint();
        }

        #region 控制物件管理

        /// <summary>
        /// 按鈕管理巨集 (Click)
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
                        StateTimer.Enabled = true;
                        WTLoopTimer.Enabled = true;
                        _connectState = true;
                        /*
                        if (_rslt != 0)
                        {
                            RobotListBoxTimer.Enabled = true;
                            StateTimer.Enabled = true;
                        }
                        else { MessageBox.Show("連線失敗"); }
                        */

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

                        tex_SYSMsg.Text += "<btn_t> Get Data Path\r\n";
                        OpenFileDialog dlg = new OpenFileDialog();
                        dlg.ShowDialog();
                        Browse_textbox.Text = dlg.FileName;

                        break;

                    #endregion 

                    #region 寫入 dataGridView
                    case "btn_LOADDATA":
                        tex_SYSMsg.Text += "<btn_t> Load Data\r\n";
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

                        RobotProgramRun(tex_ProgramName.Text);

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

                        //RegScanTimer.Interval = Convert.ToInt32(tex_RegTimerScan.Text);
                        //RegScanTimer.Enabled = true;


                        break;
                    #endregion

                    #region Reg Scan Stop
                    case "btn_RegScanStop": //RegScanStop

                        
                       // RegScanTimer.Enabled = false;
                    
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

                    #region 取得目前點位資料按鈕  (暫無用)
                    case "btn_GetCurPos":
                        /*
                        RobotCurPosGet("XYZ" , ref _GetCurXYZPosData);
                        CurPosWriteLab(_GetCurXYZPosData);
                        */

                        break;

                    #endregion
                   
                    #region 寫入點位資料按鈕 
                    case "btn_SetPos":


                        RobotPosSet(Convert.ToInt16(tex_WPointNum.Text));

                        break;

                    #endregion

                    #region 定位觸發(Base)
                    case "btn_PosBaseGo":

                        if (combox_BMovetype.Text=="MoveJ") { RobotSetPosMoveBaseData(0); }
                        else { RobotSetPosMoveBaseData(1); }
                        PositionXYZDataColerRest();

                        break;

                    #endregion

                    #region 定位觸發(Pluse)
                    case "btn_PosPlsGo":

                        if (combox_PMovetype.Text == "MoveJ") { RobotSetPosMovePluseData(0); }
                        else { RobotSetPosMovePluseData(1); }
                        PositionSLUDataColerRest();

                        break;

                    #endregion

                    #region 取得目前手臂位置並寫入(點位設定欄位)
                    case "btn_GetCPosW":

                        RobotCurPosGet("XYZ" , ref _GetCurXYZPosData);
                        tex_WPointType.Text = _GetCurXYZPosData.type.ToString();
                        tex_WPointPattern.Text = _GetCurXYZPosData.pattern.ToString();
                        tex_WPointToolNum.Text = _GetCurXYZPosData.tool_no.ToString();
                        tex_WPointUserNum.Text = _GetCurXYZPosData.user_coord_no.ToString();
                        tex_WPointExPattern.Text = _GetCurXYZPosData.ex_pattern.ToString();
                        tex_WPoinX.Text = _GetCurXYZPosData.axis[0].ToString();
                        tex_WPoinY.Text = _GetCurXYZPosData.axis[1].ToString();
                        tex_WPoinZ.Text = _GetCurXYZPosData.axis[2].ToString();
                        tex_WPoinA.Text = _GetCurXYZPosData.axis[3].ToString();
                        tex_WPoinB.Text = _GetCurXYZPosData.axis[4].ToString();
                        tex_WPoinC.Text = _GetCurXYZPosData.axis[5].ToString();


                        break;

                    #endregion

                    #region 取得目前手臂位置並寫入(點位定位基底座標欄位)
                    case "btn_GetCPosBP":
                        RobotCurPosGet("XYZ", ref _GetCurXYZPosData);
                        tex_PosB_X.Text = ((float)_GetCurXYZPosData.axis[0]/1000).ToString();
                        tex_PosB_Y.Text = ((float)_GetCurXYZPosData.axis[1]/1000).ToString();
                        tex_PosB_Z.Text = ((float)_GetCurXYZPosData.axis[2]/1000).ToString();
                        tex_PosB_RX.Text = ((float)_GetCurXYZPosData.axis[3]/10000).ToString();
                        tex_PosB_RY.Text = ((float)_GetCurXYZPosData.axis[4]/10000).ToString();
                        tex_PosB_RZ.Text = ((float)_GetCurXYZPosData.axis[5]/10000).ToString();
                        PositionXYZDataColerRest();
                        break;

                        #endregion

                    #region 取得手臂Home點並寫入(點位定位基底座標欄位)
                    case "btn_GetHomePosBP":
                        RobotCurPosGet("XYZ", ref _GetCurXYZPosData);
                        tex_PosB_X.Text = "500";
                        tex_PosB_Y.Text = "0";
                        tex_PosB_Z.Text = "250";
                        tex_PosB_RX.Text = "180";
                        tex_PosB_RY.Text = "30";
                        tex_PosB_RZ.Text = "0";
                        tex_PosP_S.Text = "-185";
                        tex_PosP_L.Text = "-55607";
                        tex_PosP_U.Text = "-52752";
                        tex_PosP_R.Text = "-50";
                        tex_PosP_B.Text = "25997";
                        tex_PosP_T.Text = "64";

                        PositionXYZDataColerRest();
                        PositionSLUDataColerRest();
                        break;

                    #endregion
                    
                    #region 取得手臂Welding點並寫入(點位定位基底座標欄位)
                    case "btn_GetWeldingPosBP":
                        RobotCurPosGet("XYZ", ref _GetCurXYZPosData);
                        tex_PosB_X.Text = "630";
                        tex_PosB_Y.Text = "210";
                        tex_PosB_Z.Text = "140";
                        tex_PosB_RX.Text = "180";
                        tex_PosB_RY.Text = "0";
                        tex_PosB_RZ.Text = "90";
                        tex_PosP_S.Text = "-14759";
                        tex_PosP_L.Text = "-17540";
                        tex_PosP_U.Text = "-58756";
                        tex_PosP_R.Text = "-36520";
                        tex_PosP_B.Text = "2645";
                        tex_PosP_T.Text = "51341";
                        PositionXYZDataColerRest();
                        PositionSLUDataColerRest();
                        break;

                    #endregion

                    #region 取得目前手臂位置並寫入(點位定位軸欄位)
                    case "btn_GetCPosPP":
                        RobotCurPosGet("SLU", ref _GetCurSLUPosData);
                        tex_PosP_S.Text = _GetCurSLUPosData.axis[0].ToString();
                        tex_PosP_L.Text = _GetCurSLUPosData.axis[1].ToString();
                        tex_PosP_U.Text = _GetCurSLUPosData.axis[2].ToString();
                        tex_PosP_R.Text = _GetCurSLUPosData.axis[3].ToString();
                        tex_PosP_B.Text = _GetCurSLUPosData.axis[4].ToString();
                        tex_PosP_T.Text = _GetCurSLUPosData.axis[5].ToString();
                        PositionSLUDataColerRest();
                        break;

                    #endregion

                    #region 手臂(啟動)
                    case "btn_RobotStart":

                        RobotStart();

                        break;

                    #endregion

                    #region 手臂(暫停)
                    case "btn_RobotStop":

                        RobotStop();

                        break;

                    #endregion

                    #region 手臂(Alarm Rst)
                    case "btn_RobotAlarmRst":
                        RobotAlarmRst();

                        break;

                    #endregion

                    #region 手臂(Error Rst)
                    case "btn_RobotErrorRst":

                        RobotErrorRst();

                        break;

                    #endregion

                    #region Jog lim (XYZ) Set
                    case "btn_JogXYZLimSet":

                        SetJogXYZLimData();
                        break;

                    #endregion

                    #region Jog lim (SLU) Set
                    case "btn_JogSLULimSet":

                        SetJogSLULimData();

                        break;

                        #endregion

                }
            }
            catch (Exception x) { MessageBox.Show(x.ToString(), "systen error!!!"); }
        }

        /// <summary>
        /// 按鈕管理巨集 Welding Test (Click)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_WeldingTest_Click(object sender, EventArgs e)
        {
            //int _rslt;
            Button btn = (Button)sender;
            string tag = (string)btn.Tag;
            try
            {
                switch (tag)
                {

                    #region Set P Data (Load) 
                    case "btn_WTTeachP_Load":

                        SetWTPositionData("Load");
                        _WTLoad = true;

                        break;

                    #endregion

                    #region Set P Data (P1) 
                    case "btn_WTTeachP_1":

                        SetWTPositionData("P1");
                        _WTP1 = true;

                        break;

                    #endregion

                    #region Set P Data (P2) 
                    case "btn_WTTeachP_2":

                        SetWTPositionData("P2");
                        _WTP2 = true;

                        break;

                    #endregion

                    #region Set P Data (Unload) 
                    case "btn_WTTeachP_Unload":

                        SetWTPositionData("Unload");
                        _WTUnload = true;

                        break;

                    #endregion

                    #region Program compile and load 
                    case "btn_WTProgramCompile":

                        WTCompileProgram();
                        _WTLoad = false;
                        _WTP1 = false;
                        _WTP2 = false;
                        _WTUnload = false;


                        break;

                    #endregion

                    #region Program recompile and load 
                    case "btn_WTProgramRecompile":

                        WTRedoSetData();
                        WTCompileProgram();

                        break;

                    #endregion

                    #region Program Run 
                    case "btn_WTProgramRun":

                        RobotProgramRun("WELDINGTEST");

                        break;

                    #endregion

                    #region ****
                    case "*****":

  
                        break;

                    #endregion

                    #region Other
                    case "Other":


                        break;

                        #endregion

                }
            }
            catch (Exception x) { MessageBox.Show(x.ToString(), "systen error!!!"); }
        }

        /// <summary>
        /// 按鈕管理巨集 (Position Offset Click)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_OFFSET_Click(object sender, EventArgs e)
        {
            //int _rslt;
            Button btn = (Button)sender;
            string tag = (string)btn.Tag;
            try
            {
                switch (tag)
                {
                    #region XYZ_Offset

                    #region B_Offset_X_-
                    case "btn_OffsetBM_X":

                        tex_PosB_X.Text = (Convert.ToDouble(tex_PosB_X.Text) - Convert.ToDouble(tex_OFS_X.Text)).ToString();
                        tex_PosB_X.ForeColor = System.Drawing.Color.Red;

                        break;
                    #endregion

                    #region B_Offset_X_+
                    case "btn_OffsetBP_X":

                        tex_PosB_X.Text = (Convert.ToDouble(tex_PosB_X.Text) + Convert.ToDouble(tex_OFS_X.Text)).ToString();
                        tex_PosB_X.ForeColor = System.Drawing.Color.Red;

                        break;
                    #endregion

                    #region B_Offset_Y_-
                    case "btn_OffsetBM_Y":

                        tex_PosB_Y.Text = (Convert.ToDouble(tex_PosB_Y.Text) - Convert.ToDouble(tex_OFS_Y.Text)).ToString();
                        tex_PosB_Y.ForeColor = System.Drawing.Color.Red;

                        break;
                    #endregion

                    #region B_Offset_Y_+
                    case "btn_OffsetBP_Y":

                        tex_PosB_Y.Text = (Convert.ToDouble(tex_PosB_Y.Text) + Convert.ToDouble(tex_OFS_Y.Text)).ToString();
                        tex_PosB_Y.ForeColor = System.Drawing.Color.Red;

                        break;
                    #endregion

                    #region B_Offset_Z_-
                    case "btn_OffsetBM_Z":

                        tex_PosB_Z.Text = (Convert.ToDouble(tex_PosB_Z.Text) - Convert.ToDouble(tex_OFS_Z.Text)).ToString();
                        tex_PosB_Z.ForeColor = System.Drawing.Color.Red;

                        break;
                    #endregion

                    #region B_Offset_Z_+
                    case "btn_OffsetBP_Z":

                        tex_PosB_Z.Text = (Convert.ToDouble(tex_PosB_Z.Text) + Convert.ToDouble(tex_OFS_Z.Text)).ToString();
                        tex_PosB_Z.ForeColor = System.Drawing.Color.Red;

                        break;
                    #endregion

                    #region B_Offset_RX_-
                    case "btn_OffsetBM_RX":

                        tex_PosB_RX.Text = (Convert.ToDouble(tex_PosB_RX.Text) - Convert.ToDouble(tex_OFS_RX.Text)).ToString();
                        tex_PosB_RX.ForeColor = System.Drawing.Color.Red;

                        break;
                    #endregion

                    #region B_Offset_RX_+
                    case "btn_OffsetBP_RX":

                        tex_PosB_RX.Text = (Convert.ToDouble(tex_PosB_RX.Text) + Convert.ToDouble(tex_OFS_RX.Text)).ToString();
                        tex_PosB_RX.ForeColor = System.Drawing.Color.Red;

                        break;
                    #endregion

                    #region B_Offset_RY_-
                    case "btn_OffsetBM_RY":

                        tex_PosB_RY.Text = (Convert.ToDouble(tex_PosB_RY.Text) - Convert.ToDouble(tex_OFS_RY.Text)).ToString();
                        tex_PosB_RY.ForeColor = System.Drawing.Color.Red;

                        break;
                    #endregion

                    #region B_Offset_RY_+
                    case "btn_OffsetBP_RY":

                        tex_PosB_RY.Text = (Convert.ToDouble(tex_PosB_RY.Text) + Convert.ToDouble(tex_OFS_RY.Text)).ToString();
                        tex_PosB_RY.ForeColor = System.Drawing.Color.Red;

                        break;
                    #endregion

                    #region B_Offset_RZ_-
                    case "btn_OffsetBM_RZ":

                        tex_PosB_RZ.Text = (Convert.ToDouble(tex_PosB_RZ.Text) - Convert.ToDouble(tex_OFS_RZ.Text)).ToString();
                        tex_PosB_RZ.ForeColor = System.Drawing.Color.Red;

                        break;
                    #endregion

                    #region B_Offset_RZ_+
                    case "btn_OffsetBP_RZ":

                        tex_PosB_RZ.Text = (Convert.ToDouble(tex_PosB_RZ.Text) + Convert.ToDouble(tex_OFS_RZ.Text)).ToString();
                        tex_PosB_RZ.ForeColor = System.Drawing.Color.Red;

                        break;
                    #endregion

                    #endregion

                    #region SLU_Offset

                    #region P_Offset_S_-
                    case "btn_OffsetPM_S":

                        tex_PosP_S.Text = (Convert.ToDouble(tex_PosP_S.Text) - Convert.ToDouble(tex_OFS_S.Text)).ToString();
                        tex_PosP_S.ForeColor = System.Drawing.Color.Red;

                        break;
                    #endregion

                    #region P_Offset_S_+
                    case "btn_OffsetPP_S":

                        tex_PosP_S.Text = (Convert.ToDouble(tex_PosP_S.Text) + Convert.ToDouble(tex_OFS_S.Text)).ToString();
                        tex_PosP_S.ForeColor = System.Drawing.Color.Red;

                        break;
                    #endregion

                    #region P_Offset_L_-
                    case "btn_OffsetPM_L":

                        tex_PosP_L.Text = (Convert.ToDouble(tex_PosP_L.Text) - Convert.ToDouble(tex_OFS_L.Text)).ToString();
                        tex_PosP_L.ForeColor = System.Drawing.Color.Red;

                        break;
                    #endregion

                    #region P_Offset_L_+
                    case "btn_OffsetPP_L":

                        tex_PosP_L.Text = (Convert.ToDouble(tex_PosP_L.Text) + Convert.ToDouble(tex_OFS_L.Text)).ToString();
                        tex_PosP_L.ForeColor = System.Drawing.Color.Red;

                        break;
                    #endregion

                    #region P_Offset_U_-
                    case "btn_OffsetPM_U":

                        tex_PosP_U.Text = (Convert.ToDouble(tex_PosP_U.Text) - Convert.ToDouble(tex_OFS_U.Text)).ToString();
                        tex_PosP_U.ForeColor = System.Drawing.Color.Red;

                        break;
                    #endregion

                    #region P_Offset_U_+
                    case "btn_OffsetPP_U":

                        tex_PosP_U.Text = (Convert.ToDouble(tex_PosP_U.Text) + Convert.ToDouble(tex_OFS_U.Text)).ToString();
                        tex_PosP_U.ForeColor = System.Drawing.Color.Red;

                        break;
                    #endregion

                    #region P_Offset_R_-
                    case "btn_OffsetPM_R":

                        tex_PosP_R.Text = (Convert.ToDouble(tex_PosP_R.Text) - Convert.ToDouble(tex_OFS_R.Text)).ToString();
                        tex_PosP_R.ForeColor = System.Drawing.Color.Red;

                        break;
                    #endregion

                    #region P_Offset_R_+
                    case "btn_OffsetPP_R":

                        tex_PosP_R.Text = (Convert.ToDouble(tex_PosP_R.Text) + Convert.ToDouble(tex_OFS_R.Text)).ToString();
                        tex_PosP_R.ForeColor = System.Drawing.Color.Red;

                        break;
                    #endregion

                    #region P_Offset_B_-
                    case "btn_OffsetPM_B":

                        tex_PosP_B.Text = (Convert.ToDouble(tex_PosP_B.Text) - Convert.ToDouble(tex_OFS_B.Text)).ToString();
                        tex_PosP_B.ForeColor = System.Drawing.Color.Red;

                        break;
                    #endregion

                    #region P_Offset_B_+
                    case "btn_OffsetPP_B":

                        tex_PosP_B.Text = (Convert.ToDouble(tex_PosP_B.Text) + Convert.ToDouble(tex_OFS_B.Text)).ToString();
                        tex_PosP_B.ForeColor = System.Drawing.Color.Red;

                        break;
                    #endregion

                    #region P_Offset_T_-
                    case "btn_OffsetPM_T":

                        tex_PosP_T.Text = (Convert.ToDouble(tex_PosP_T.Text) - Convert.ToDouble(tex_OFS_T.Text)).ToString();
                        tex_PosP_T.ForeColor = System.Drawing.Color.Red;

                        break;
                    #endregion

                    #region P_Offset_T_+
                    case "btn_OffsetPP_T":

                        tex_PosP_T.Text = (Convert.ToDouble(tex_PosP_T.Text) + Convert.ToDouble(tex_OFS_T.Text)).ToString();
                        tex_PosP_T.ForeColor = System.Drawing.Color.Red;

                        break;
                        #endregion


                        #endregion
                }
            }
            catch (Exception x) { MessageBox.Show(x.ToString(), "systen error!!!"); }
        }

        /// <summary>
        /// JOG 按鈕管理巨集 (Mouse Down)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_JogMouseDown(object sender, MouseEventArgs e)
        {
            //int _rslt;
            Button btn = (Button)sender;
            string tag = (string)btn.Tag;
            RobotStart();
            try
            {
                switch (tag)
                {
                    #region JOG_X/S_-
                    case "btn_xj1-":
                        if (rad_robotxyz.Checked) { RobotJog("X", 0); }
                        else { RobotJog("S", 0); }

                        break;
                    #endregion

                    #region JOG_X/S_+
                    case "btn_xj1+":


                        if (rad_robotxyz.Checked){RobotJog("X", 1);}
                        else { RobotJog("S", 1); }

                        break;
                    #endregion

                    #region JOG_Y/L_-
                    case "btn_xj2-":

                        if (rad_robotxyz.Checked) { RobotJog("Y", 0); }
                        else { RobotJog("L", 0); }

                        break;
                    #endregion

                    #region JOG_Y/L_+
                    case "btn_xj2+":

                        if (rad_robotxyz.Checked) { RobotJog("Y", 1); }
                        else { RobotJog("L", 1); }

                        break;
                    #endregion

                    #region JOG_Z/U_-
                    case "btn_xj3-":

                        if (rad_robotxyz.Checked) { RobotJog("Z", 0); }
                        else { RobotJog("U", 0); }

                        break;
                    #endregion

                    #region JOG_Z/U_+
                    case "btn_xj3+":

                        if (rad_robotxyz.Checked) { RobotJog("Z", 1); }
                        else { RobotJog("U", 1); }

                        break;
                    #endregion

                    #region JOG_RX/R_-
                    case "btn_xj4-":

                        if (rad_robotxyz.Checked) { RobotJog("RX", 0); }
                        else { RobotJog("R", 0); }

                        break;
                    #endregion

                    #region JOG_RX/R_+
                    case "btn_xj4+":

                        if (rad_robotxyz.Checked) { RobotJog("RX", 1); }
                        else { RobotJog("R", 1); }

                        break;
                    #endregion

                    #region JOG_RY/B_-
                    case "btn_xj5-":

                        if (rad_robotxyz.Checked) { RobotJog("RY", 0); }
                        else { RobotJog("B", 0); }

                        break;
                    #endregion

                    #region JOG_RY/B_+
                    case "btn_xj5+":

                        if (rad_robotxyz.Checked) { RobotJog("RY", 1); }
                        else { RobotJog("B", 1); }

                        break;
                    #endregion

                    #region JOG_RZ/T_-
                    case "btn_xj6-":

                        if (rad_robotxyz.Checked) { RobotJog("RZ", 0); }
                        else { RobotJog("T", 0); }

                        break;
                    #endregion

                    #region JOG_RZ/T_+
                    case "btn_xj6+":

                        if (rad_robotxyz.Checked) { RobotJog("RZ", 1); }
                        else { RobotJog("T", 1); }

                        break;
                    #endregion

                    #region Other
                    case "Other":


                        break;

                    #endregion

                }
            }
            catch (Exception x) { MessageBox.Show(x.ToString(), "systen error!!!"); }
        }

        /// <summary>
        /// JOG 按鈕管理巨集 (Mouse Up)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_JogMouseUp(object sender, MouseEventArgs e)
        {
            //int _rslt;
            Button btn = (Button)sender;
            string tag = (string)btn.Tag;
            try
            {
                switch (tag)
                {
                    #region JOG_X/S_-
                    case "btn_xj1-":

                        RobotStop();

                        break;
                    #endregion

                    #region JOG_X/S_+
                    case "btn_xj1+":

                        RobotStop();

                        break;
                    #endregion

                    #region JOG_Y/L_-
                    case "btn_xj2-":

                        RobotStop();

                        break;
                    #endregion

                    #region JOG_Y/L_+
                    case "btn_xj2+":

                        RobotStop();

                        break;
                    #endregion

                    #region JOG_Z/U_-
                    case "btn_xj3-":

                        RobotStop();

                        break;
                    #endregion

                    #region JOG_Z/U_+
                    case "btn_xj3+":

                        RobotStop();

                        break;
                    #endregion

                    #region JOG_RX/R_-
                    case "btn_xj4-":

                        RobotStop();

                        break;
                    #endregion

                    #region JOG_RX/R_+
                    case "btn_xj4+":

                        RobotStop();

                        break;
                    #endregion

                    #region JOG_RY/B_-
                    case "btn_xj5-":

                        RobotStop();

                        break;
                    #endregion

                    #region JOG_RY/B_+
                    case "btn_xj5+":

                        RobotStop();

                        break;
                    #endregion

                    #region JOG_RZ/T_-
                    case "btn_xj6-":

                        RobotStop();

                        break;
                    #endregion

                    #region JOG_RZ/T_+
                    case "btn_xj6+":

                        RobotStop();

                        break;
                    #endregion

                    #region Other
                    case "Other":


                        break;

                        #endregion

                }
            }
            catch (Exception x) { MessageBox.Show(x.ToString(), "systen error!!!"); }
        }

        /// <summary>
        /// 按鈕管理巨集 (Mouse Down)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_MouseDown(object sender, MouseEventArgs e)
        {
            //int _rslt;
            Button btn = (Button)sender;
            string tag = (string)btn.Tag;
            RobotStart();
            try
            {
                switch (tag)
                {
                   
                    #region Other
                    case "Other":


                        break;

                        #endregion

                }
            }
            catch (Exception x) { MessageBox.Show(x.ToString(), "systen error!!!"); }
        }



        /// <summary>
        /// 按鈕管理巨集 (Mouse Up)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_MouseUp(object sender, MouseEventArgs e)
        {
            //int _rslt;
            Button btn = (Button)sender;
            string tag = (string)btn.Tag;
            try
            {
                switch (tag)
                {
                    
                    #region Other
                    case "Other":


                        break;

                        #endregion

                }
            }
            catch (Exception x) { MessageBox.Show(x.ToString(), "systen error!!!"); }
        }

        #endregion

        #region 程式編寫內容

        #region Robot Funtion <Control>

        /// <summary>
        /// Robot State Check
        /// </summary>
        private void RobotStateCheck()
        {
            //get robot state
            YaskawaController.CheckStatus();

            //check state
            _Step = YaskawaController.Step ? "True" : "False";
            _OneCycle = YaskawaController.OneCycle ? "True" : "False";
            _AutomaticAndContinuos = YaskawaController.AutomaticAndContinuos ? "True" : "False";
            _isBusy = YaskawaController.isBusy ? "True" : "False";
            _InGuardSafeOperation = YaskawaController.InGuardSafeOperation ? "True" : "False";
            _Teach = YaskawaController.Teach ? "True" : "False";
            _Play = YaskawaController.Play ? "True" : "False";
            _CommendRemote = YaskawaController.CommendRemote ? "True" : "False";
            _HoldON = YaskawaController.HoldON ? "True" : "False";
            _HoldStatusE = YaskawaController.HoldStatusE ? "True" : "False";
            _HoldStatusC = YaskawaController.HoldStatusC ? "True" : "False";
            _Alarm = YaskawaController.Alarm ? "True" : "False";
            _Error = YaskawaController.Error ? "True" : "False";
            _ServoON = YaskawaController.ServoON ? "True" : "False";

            //chang string coler
            if (YaskawaController.Step) { lab_Step.ForeColor = System.Drawing.Color.Green; }
            else { lab_Step.ForeColor = System.Drawing.Color.Red; }
            if (YaskawaController.OneCycle) { lab_OneCycle.ForeColor = System.Drawing.Color.Green; }
            else { lab_OneCycle.ForeColor = System.Drawing.Color.Red; }
            if (YaskawaController.AutomaticAndContinuos) { lab_Automatic.ForeColor = System.Drawing.Color.Green; }
            else { lab_Automatic.ForeColor = System.Drawing.Color.Red; }
            if (YaskawaController.isBusy) { lab_Running.ForeColor = System.Drawing.Color.Green; }
            else { lab_Running.ForeColor = System.Drawing.Color.Red; }
            if (YaskawaController.InGuardSafeOperation) { lab_GuardSafeOp.ForeColor = System.Drawing.Color.Green; }
            else { lab_GuardSafeOp.ForeColor = System.Drawing.Color.Red; }
            if (YaskawaController.Teach) { lab_Teach.ForeColor = System.Drawing.Color.Green; }
            else { lab_Teach.ForeColor = System.Drawing.Color.Red; }
            if (YaskawaController.Play) { lab_Play.ForeColor = System.Drawing.Color.Green; }
            else { lab_Play.ForeColor = System.Drawing.Color.Red; }
            if (YaskawaController.CommendRemote) { lab_ComRemote.ForeColor = System.Drawing.Color.Green; }
            else { lab_ComRemote.ForeColor = System.Drawing.Color.Red; }
            if (YaskawaController.HoldON) { lab_HoldP.ForeColor = System.Drawing.Color.Green; }
            else { lab_HoldP.ForeColor = System.Drawing.Color.Red; }
            if (YaskawaController.HoldStatusE) { lab_HoldE.ForeColor = System.Drawing.Color.Green; }
            else { lab_HoldE.ForeColor = System.Drawing.Color.Red; }
            if (YaskawaController.HoldStatusC) { lab_HoldC.ForeColor = System.Drawing.Color.Green; }
            else { lab_HoldC.ForeColor = System.Drawing.Color.Red; }
            if (YaskawaController.Alarm) { lab_Alarm.ForeColor = System.Drawing.Color.Green; }
            else { lab_Alarm.ForeColor = System.Drawing.Color.Red; }
            if (YaskawaController.Error) { lab_Error.ForeColor = System.Drawing.Color.Green; }
            else { lab_Error.ForeColor = System.Drawing.Color.Red; }
            if (YaskawaController.ServoON) { lab_SVON.ForeColor = System.Drawing.Color.Green; }
            else { lab_SVON.ForeColor = System.Drawing.Color.Red; }


            //write lab.text
            lab_Step.Text = "Step : " + _Step;
            lab_OneCycle.Text = "OneCycle : " + _OneCycle;
            lab_Automatic.Text = "Automatic / Continuos : " + _AutomaticAndContinuos;
            lab_Running.Text = "Running : " + _isBusy;
            lab_GuardSafeOp.Text = "InGuardSafeOperation : " + _InGuardSafeOperation;
            lab_Teach.Text = "Teach : " + _Teach;
            lab_Play.Text = "Play : " + _Play;
            lab_ComRemote.Text = "Cammand Remote : " + _CommendRemote;
            lab_HoldP.Text = "Hold Status (P) : " + _HoldON;
            lab_HoldE.Text = "Hold Status (E) : " + _HoldStatusE;
            lab_HoldC.Text = "Hold Status (C) : " + _HoldStatusC;
            lab_Alarm.Text = "Alarming : " + _Alarm;
            lab_Error.Text = "Error : " + _Error;
            lab_SVON.Text = "Servo On : " + _ServoON;

        }

        /// <summary>
        /// Robot Stop
        /// </summary>
        private void RobotStop()
        {
            YaskawaController.RobotStopSwitch(1); //1: ON 2: OFF
        }

        /// <summary>
        /// Robot Start
        /// </summary>
        private void RobotStart()
        {
            YaskawaController.RobotStopSwitch(2); //1: ON 2: OFF
        }

        /// <summary>
        /// Robot Alarm Rst
        /// </summary>
        private void RobotAlarmRst()
        {
            YaskawaController.ResetAlarm();
        }

        /// <summary>
        /// Robot Error Rst
        /// </summary>
        private void RobotErrorRst()
        {

        }

        /// <summary>
        /// Robot Program Run
        /// </summary>
        /// <param name="_fileName"></param>
        private void RobotProgramRun(string _fileName)
        {
            int _rslt = YaskawaController.RunProgram(_fileName);
        }

        #endregion

        #region Robot Funtion <Program File>

        /// <summary>
        /// 編寫Robot Program (for Yaskawa)
        /// </summary>
        public bool RobotCompileProgram(string _programName)
        {
            try
            {
                // 實作程式物件
                RouteBook_Yaskawa _routeBook_Welding = new RouteBook_Yaskawa(_programName, dataGridView1.RowCount, 0, 0);
                //寫入工作環境生成狀態
                _routeBook_Welding.Workspace = 0;
                _routeBook_Welding.FlipMode = 1;
                
                for (int i = 0; i < dataGridView1.RowCount - 1; i++)
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
                { _rslt = YaskawaController.CompileFile(_routeBook_Welding, _programName, 1, Convert.ToInt32(tex_SpeedReg.Text)); }
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

        #region Robot Funtion <Data> 

        /// <summary>
        /// Set Home Point
        /// </summary>
        private void SetHomePoint()
        {
            _HomePosData.axis[0] = 500000;
            _HomePosData.axis[1] = 0;
            _HomePosData.axis[2] = 250000;
            _HomePosData.axis[3] = 180000;
            _HomePosData.axis[4] = 30000;
            _HomePosData.axis[5] = 0;
        }

        /// <summary>
        /// Write robot [I***] data
        /// </summary>
        /// <param name="_num"></param>
        /// <param name="_data"></param>
        private void RobotRegWrite(short _num, short _data)
        {
            int _rslt;
            int _Dnum = _num;
            _rslt = YaskawaController.WriteIData(Convert.ToInt16(_Dnum + 1), _data);

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
        private void RobotRegRead(short _num, ref short _data)
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
            int _fposnum = _posnum + 1;
            YaskawaController.GetPosData((short)_fposnum, ref _GetPosData);
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
        private void RobotCurPosGet(string _type, ref PosData _GetCurPosData)
        {
            if (_type == "XYZ") { YaskawaController.GetCurPosData(0, ref _GetCurPosData); }
            else if (_type == "SLU") { YaskawaController.GetCurPosData(1, ref _GetCurPosData); }

        }

        /// <summary>
        /// Robot Pos Set P[***]
        /// </summary>
        /// <param name="_pointNum"></param>
        private void RobotPosSet(short _pointNum)
        {
            PosData _posD = new PosData();
            int _axisValue = 0;
            _posD.type = uint.Parse(tex_WPointType.Text);// 基座 (16) , Pluse (0) ,機器人(17),工具(18) ,使用者(19)
            _posD.pattern = int.Parse(tex_WPointPattern.Text); //型態 (2進制) 常用為 4
            _posD.tool_no = uint.Parse(tex_WPointToolNum.Text); //工具 no.
            _posD.user_coord_no = uint.Parse(tex_WPointUserNum.Text); //工件座標(使用者座標用)
            _posD.ex_pattern = int.Parse(tex_WPointExPattern.Text);  //暫定 0
            _posD.axis[0] = int.Parse(tex_WPoinX.Text);
            _posD.axis[1] = int.Parse(tex_WPoinY.Text);
            _posD.axis[2] = int.Parse(tex_WPoinZ.Text);
            _posD.axis[3] = int.Parse(tex_WPoinA.Text);
            _posD.axis[4] = int.Parse(tex_WPoinB.Text);
            _posD.axis[5] = int.Parse(tex_WPoinC.Text);
            _posD.axis[6] = _axisValue; //預設 0
            _posD.axis[7] = _axisValue; //預設 0
            YaskawaController.SetPosData(_pointNum, _posD);

        }

        #endregion

        #region Robot Funtion <Position>

        /// <summary>
        /// Robot Set Pos Move Base
        /// </summary>
        /// <param name="_moveType"></param>
        private void RobotSetPosMoveBaseData(short _moveType)
        {
            CoordMove _baseData = new CoordMove();
            _baseData.des.robot_group = 1;
            _baseData.des.station_group = 0;
            if (_moveType == 0) { _baseData.des.speed_class = 0; }
            if (_moveType == 1) { _baseData.des.speed_class = 1; }
            _baseData.des.speed = uint.Parse(tex_PosP_Speed.Text);
            _baseData.act_coord_des = 16;
            _baseData.x_coord = (int)Math.Ceiling(Convert.ToDouble(tex_PosB_X.Text) * 1000);
            _baseData.y_coord = (int)Math.Ceiling(Convert.ToDouble(tex_PosB_Y.Text) * 1000);
            _baseData.z_coord = (int)Math.Ceiling(Convert.ToDouble(tex_PosB_Z.Text) * 1000);
            _baseData.Tx_coord = (int)Math.Ceiling(Convert.ToDouble(tex_PosB_RX.Text) * 10000);
            _baseData.Ty_coord = (int)Math.Ceiling(Convert.ToDouble(tex_PosB_RY.Text) * 10000);
            _baseData.Tz_coord = (int)Math.Ceiling(Convert.ToDouble(tex_PosB_RZ.Text) * 10000);


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

            RobotPosMoveBase(_baseData);

        }

        /// <summary>
        /// Robot Pos Move Base (XYZ)
        /// </summary>
        private void RobotPosMoveBase(CoordMove _baseData)
        {
            YaskawaController.RobotMoveBase(_baseData);

        }

        /// <summary>
        /// Robot Set Pos Move Pluse Data
        /// </summary>
        /// <param name="_moveType"></param>
        private void RobotSetPosMovePluseData(short _moveType)
        {
            PulseMove _plsData = new PulseMove();
            _plsData.des.robot_group = 1;
            _plsData.des.station_group = 0;
            if (_moveType == 0) { _plsData.des.speed_class = 0; }
            if (_moveType == 1) { _plsData.des.speed_class = 1; }
            _plsData.des.speed = uint.Parse(tex_PosP_Speed.Text);
            _plsData.robot_pulse[0] = int.Parse(tex_PosP_S.Text);
            _plsData.robot_pulse[1] = int.Parse(tex_PosP_L.Text);
            _plsData.robot_pulse[2] = int.Parse(tex_PosP_U.Text);
            _plsData.robot_pulse[3] = int.Parse(tex_PosP_R.Text);
            _plsData.robot_pulse[4] = int.Parse(tex_PosP_B.Text);
            _plsData.robot_pulse[5] = int.Parse(tex_PosP_T.Text);
            _plsData.robot_pulse[6] = 0;
            _plsData.robot_pulse[7] = 0;
            RobotPosMovePluse(_plsData);
        }

        /// <summary>
        /// Robot Pos Move Pluse (J1/J2/J3)
        /// </summary>
        private void RobotPosMovePluse(PulseMove _plsData)
        {
            YaskawaController.RobotMovePls(_plsData);
        }

        #endregion

        #region Robot Funtion <Jog>

        /// <summary>
        /// Robot Jog
        /// </summary>
        /// <param name="_name"></param>
        /// <param name="_state"></param>
        private void RobotJog(string _name, short _state)
        {
            _GetJOGCurXYZPosData = _GetCurXYZPosData;
            _GetJOGCurSLUPosData = _GetCurSLUPosData;

            switch (_name)
            {
                #region JOG_X
                case "X":

                    if (_state == 0) { _GetJOGCurXYZPosData.axis[0] = int.Parse(tex_JogLimM_X.Text) * 1000; }
                    else if (_state == 1) { _GetJOGCurXYZPosData.axis[0] = int.Parse(tex_JogLimP_X.Text) * 1000; }
                    RobotSetJOGPosMoveBaseData(_GetJOGCurXYZPosData);

                    break;

                #endregion

                #region JOG_Y
                case "Y":

                    if (_state == 0) { _GetJOGCurXYZPosData.axis[1] = int.Parse(tex_JogLimM_Y.Text) * 1000; }
                    else if (_state == 1) { _GetJOGCurXYZPosData.axis[1] = int.Parse(tex_JogLimP_Y.Text) * 1000; }
                    RobotSetJOGPosMoveBaseData(_GetJOGCurXYZPosData);

                    break;

                #endregion

                #region JOG_Z
                case "Z":

                    if (_state == 0) { _GetJOGCurXYZPosData.axis[2] = int.Parse(tex_JogLimM_Z.Text) * 1000; }
                    else if (_state == 1) { _GetJOGCurXYZPosData.axis[2] = int.Parse(tex_JogLimP_Z.Text) * 1000; }
                    RobotSetJOGPosMoveBaseData(_GetJOGCurXYZPosData);
                    break;

                #endregion

                #region JOG_RX
                case "RX":

                    if (_state == 0) { _GetJOGCurXYZPosData.axis[3] = int.Parse(tex_JogLimM_RX.Text) * 10000; }
                    else if (_state == 1) { _GetJOGCurXYZPosData.axis[3] = int.Parse(tex_JogLimP_RX.Text) * 10000; }
                    RobotSetJOGPosMoveBaseData(_GetJOGCurXYZPosData);

                    break;

                #endregion

                #region JOG_RY
                case "RY":

                    if (_state == 0) { _GetJOGCurXYZPosData.axis[4] = int.Parse(tex_JogLimM_RY.Text) * 10000; }
                    else if (_state == 1) { _GetJOGCurXYZPosData.axis[4] = int.Parse(tex_JogLimP_RY.Text) * 10000; }
                    RobotSetJOGPosMoveBaseData(_GetJOGCurXYZPosData);

                    break;

                #endregion

                #region JOG_RZ
                case "RZ":

                    if (_state == 0) { _GetJOGCurXYZPosData.axis[5] = int.Parse(tex_JogLimM_RZ.Text) * 10000; }
                    else if (_state == 1) { _GetJOGCurXYZPosData.axis[5] = int.Parse(tex_JogLimP_RZ.Text) * 10000; }
                    RobotSetJOGPosMoveBaseData(_GetJOGCurXYZPosData);

                    break;

                #endregion

                #region JOG_S
                case "S":

                    if (_state == 0) { _GetJOGCurSLUPosData.axis[0] = int.Parse(tex_JogLimM_S.Text) * 1000; }
                    else if (_state == 1) { _GetJOGCurSLUPosData.axis[0] = int.Parse(tex_JogLimP_S.Text) * 1000; }
                    RobotSetJOGPosMovePluseData(_GetJOGCurSLUPosData);

                    break;

                #endregion

                #region JOG_L
                case "L":

                    if (_state == 0) { _GetJOGCurSLUPosData.axis[1] = int.Parse(tex_JogLimM_L.Text) * 1000; }
                    else if (_state == 1) { _GetJOGCurSLUPosData.axis[1] = int.Parse(tex_JogLimP_L.Text) * 1000; }
                    RobotSetJOGPosMovePluseData(_GetJOGCurSLUPosData);

                    break;

                #endregion

                #region JOG_U
                case "U":

                    if (_state == 0) { _GetJOGCurSLUPosData.axis[2] = int.Parse(tex_JogLimM_U.Text) * 1000; }
                    else if (_state == 1) { _GetJOGCurSLUPosData.axis[2] = int.Parse(tex_JogLimP_U.Text) * 1000; }
                    RobotSetJOGPosMovePluseData(_GetJOGCurSLUPosData);

                    break;

                #endregion

                #region JOG_R
                case "R":

                    if (_state == 0) { _GetJOGCurSLUPosData.axis[3] = int.Parse(tex_JogLimM_R.Text) * 1000; }
                    else if (_state == 1) { _GetJOGCurSLUPosData.axis[3] = int.Parse(tex_JogLimP_R.Text) * 1000; }
                    RobotSetJOGPosMovePluseData(_GetJOGCurSLUPosData);

                    break;

                #endregion

                #region JOG_B
                case "B":

                    if (_state == 0) { _GetJOGCurSLUPosData.axis[4] = int.Parse(tex_JogLimM_B.Text) * 1000; }
                    else if (_state == 1) { _GetJOGCurSLUPosData.axis[4] = int.Parse(tex_JogLimP_B.Text) * 1000; }
                    RobotSetJOGPosMovePluseData(_GetJOGCurSLUPosData);

                    break;

                #endregion

                #region JOG_T
                case "T":

                    if (_state == 0) { _GetJOGCurSLUPosData.axis[5] = int.Parse(tex_JogLimM_T.Text) * 1000; }
                    else if (_state == 1) { _GetJOGCurSLUPosData.axis[5] = int.Parse(tex_JogLimP_T.Text) * 1000; }
                    RobotSetJOGPosMovePluseData(_GetJOGCurSLUPosData);

                    break;

                    #endregion

            }


        }

        /// <summary>
        /// Robot Set JOG Pos Move Base
        /// </summary>
        /// <param name="_moveType"></param>
        private void RobotSetJOGPosMoveBaseData(PosData _SetJOGPosData)
        {
            CoordMove _baseData = new CoordMove();
            _baseData.des.robot_group = 1;
            _baseData.des.station_group = 0;
            _baseData.des.speed_class = 0;
            _baseData.des.speed = 100;
            _baseData.act_coord_des = 16;
            _baseData.x_coord = _SetJOGPosData.axis[0];
            _baseData.y_coord = _SetJOGPosData.axis[1];
            _baseData.z_coord = _SetJOGPosData.axis[2];
            _baseData.Tx_coord = _SetJOGPosData.axis[3];
            _baseData.Ty_coord = _SetJOGPosData.axis[4];
            _baseData.Tz_coord = _SetJOGPosData.axis[5];


            _baseData.reserve = 0;
            _baseData.reserve2 = 0;
            _baseData.ex_pattern = 0;
            _baseData.tool_no = 0;
            _baseData.user_coord_no = 0;
            _baseData.axis.base_pos[0] = 0;
            _baseData.axis.base_pos[1] = 0;
            _baseData.axis.base_pos[2] = 0;
            _baseData.axis.station_pos[0] = 0;
            _baseData.axis.station_pos[1] = 0;
            _baseData.axis.station_pos[2] = 0;
            _baseData.axis.station_pos[3] = 0;
            _baseData.axis.station_pos[4] = 0;
            _baseData.axis.station_pos[5] = 0;

            RobotPosMoveBase(_baseData);

        }

        /// <summary>
        /// Robot Set JOG Pos Move Pluse
        /// </summary>
        /// <param name="_moveType"></param>
        private void RobotSetJOGPosMovePluseData(PosData _SetJOGPosData)
        {
            PulseMove _plsData = new PulseMove();
            _plsData.des.robot_group = 1;
            _plsData.des.station_group = 0;
            _plsData.des.speed_class = 0;
            _plsData.des.speed = 100;
            _plsData.robot_pulse[0] = _SetJOGPosData.axis[0];
            _plsData.robot_pulse[1] = _SetJOGPosData.axis[1];
            _plsData.robot_pulse[2] = _SetJOGPosData.axis[2];
            _plsData.robot_pulse[3] = _SetJOGPosData.axis[3];
            _plsData.robot_pulse[4] = _SetJOGPosData.axis[4];
            _plsData.robot_pulse[5] = _SetJOGPosData.axis[5];
            _plsData.robot_pulse[6] = 0;
            _plsData.robot_pulse[7] = 0;
            RobotPosMovePluse(_plsData);

        }

        #endregion

        #region Form Control Funtion

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
        /// Robot Program Refresh
        /// </summary>
        /// <returns></returns>
        private void RobotProgramRefresh()
        {
            try
            {
                string _fileName = String.Empty;
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
        /// Set Jog XYZ Lim Data
        /// </summary>
        private void SetJogXYZLimData()
        {
            _JogLimM_X = int.Parse(tex_JogLimM_X.Text);
            _JogLimM_Y = int.Parse(tex_JogLimM_Y.Text);
            _JogLimM_Z = int.Parse(tex_JogLimM_Z.Text);
            _JogLimM_RX = int.Parse(tex_JogLimM_RX.Text);
            _JogLimM_RY = int.Parse(tex_JogLimM_RY.Text);
            _JogLimM_RZ = int.Parse(tex_JogLimM_RZ.Text);
            _JogLimP_X = int.Parse(tex_JogLimP_X.Text);
            _JogLimP_Y = int.Parse(tex_JogLimP_Y.Text);
            _JogLimP_Z = int.Parse(tex_JogLimP_Z.Text);
            _JogLimP_RX = int.Parse(tex_JogLimP_RX.Text);
            _JogLimP_RY = int.Parse(tex_JogLimP_RY.Text);
            _JogLimP_RZ = int.Parse(tex_JogLimP_RZ.Text);

        }

        /// <summary>
        /// Set Jog SLU Lim Data
        /// </summary>
        private void SetJogSLULimData()
        {

            _JogLimM_S = int.Parse(tex_JogLimM_S.Text);
            _JogLimM_L = int.Parse(tex_JogLimM_L.Text);
            _JogLimM_U = int.Parse(tex_JogLimM_U.Text);
            _JogLimM_R = int.Parse(tex_JogLimM_R.Text);
            _JogLimM_B = int.Parse(tex_JogLimM_B.Text);
            _JogLimM_T = int.Parse(tex_JogLimM_T.Text);
            _JogLimP_S = int.Parse(tex_JogLimP_S.Text);
            _JogLimP_L = int.Parse(tex_JogLimP_L.Text);
            _JogLimP_U = int.Parse(tex_JogLimP_U.Text);
            _JogLimP_R = int.Parse(tex_JogLimP_R.Text);
            _JogLimP_B = int.Parse(tex_JogLimP_B.Text);
            _JogLimP_T = int.Parse(tex_JogLimP_T.Text);

        }

        /// <summary>
        /// Position XYZ Data Coler Rest
        /// </summary>
        private void PositionXYZDataColerRest()
        {
            tex_PosB_X.ForeColor = System.Drawing.Color.Black;
            tex_PosB_Y.ForeColor = System.Drawing.Color.Black;
            tex_PosB_Z.ForeColor = System.Drawing.Color.Black;
            tex_PosB_RX.ForeColor = System.Drawing.Color.Black;
            tex_PosB_RY.ForeColor = System.Drawing.Color.Black;
            tex_PosB_RZ.ForeColor = System.Drawing.Color.Black;
        }

        /// <summary>
        /// Position XYZ Data Coler Rest
        /// </summary>
        private void PositionSLUDataColerRest()
        {
            tex_PosP_S.ForeColor = System.Drawing.Color.Black;
            tex_PosP_L.ForeColor = System.Drawing.Color.Black;
            tex_PosP_U.ForeColor = System.Drawing.Color.Black;
            tex_PosP_R.ForeColor = System.Drawing.Color.Black;
            tex_PosP_B.ForeColor = System.Drawing.Color.Black;
            tex_PosP_T.ForeColor = System.Drawing.Color.Black;
        }

        /// <summary>
        /// Cur Pos Write Lab 寫入TXT
        /// </summary>
        /// <param name="_PosData"></param>
        private void CurPosShowLab(PosData _PosDataXYZ, PosData _PosDataSLU)
        {

            lab_CurPosXYZData.Text =
                ("P[ X,Y,Z ]:" + "\n\n Type = " + _PosDataXYZ.type + " \n 形態 = " + _PosDataXYZ.pattern + "\n Tool num = " + _PosDataXYZ.tool_no
                + "\n User num = " + _PosDataXYZ.user_coord_no + "\n 擴張形態 = " + _PosDataXYZ.ex_pattern
                + "\n\n X = " + _PosDataXYZ.axis[0]
                + "\n Y = " + _PosDataXYZ.axis[1]
                + "\n Z = " + _PosDataXYZ.axis[2]
                + "\n A = " + _PosDataXYZ.axis[3]
                + "\n B = " + _PosDataXYZ.axis[4]
                + "\n C = " + _PosDataXYZ.axis[5]);

            lab_CurPosSLUData.Text =
                ("P[ S,L,U ]:" + "\n\n Type = " + _PosDataSLU.type + " \n 形態 = " + _PosDataSLU.pattern + "\n Tool num = " + _PosDataSLU.tool_no
                + "\n User num = " + _PosDataSLU.user_coord_no + "\n 擴張形態 = " + _PosDataSLU.ex_pattern
                + "\n\n S = " + _PosDataSLU.axis[0]
                + "\n L = " + _PosDataSLU.axis[1]
                + "\n U = " + _PosDataSLU.axis[2]
                + "\n R = " + _PosDataSLU.axis[3]
                + "\n B = " + _PosDataSLU.axis[4]
                + "\n T = " + _PosDataSLU.axis[5]);
        }

        #endregion

        #region Welding Test Funtion (WT)

        /// <summary>
        /// Set WT Position Data to TXT
        /// </summary>
        /// <param name="_pNum"></param>
        /// <param name="_PosDataXYZ"></param>
        private void SetWTPositionDatatoTXT(string _pNum, PosData _PosDataXYZ)
        {
            switch (_pNum)
            {
                #region Load Point
                case "Load":

                    lab_WldTestPLoad.Text =
                          ("P[ 002 ] = Load" 
                             + "\n\n X = " + (float) _PosDataXYZ.axis[0] / 1000 + " mm"
                             + "\n Y = " + (float) _PosDataXYZ.axis[1] / 1000 + " mm"
                             + "\n Z = " + (float)_PosDataXYZ.axis[2] / 1000 + " mm"
                             + "\n A = " + (float)_PosDataXYZ.axis[3] / 10000 + " deg."
                             + "\n B = " + (float)_PosDataXYZ.axis[4] / 10000 + " deg."
                             + "\n C = " + (float)_PosDataXYZ.axis[5] / 10000 + " deg.");

                    break;

                #endregion

                #region P1
                case "P1":

                    lab_WldTestP1.Text =
                         ("P[ 003 ] = P1"
                            + "\n\n X = " + (float)_PosDataXYZ.axis[0] / 1000 + " mm"
                            + "\n Y = " + (float)_PosDataXYZ.axis[1] / 1000 + " mm"
                            + "\n Z = " + (float)_PosDataXYZ.axis[2] / 1000 + " mm"
                            + "\n A = " + (float)_PosDataXYZ.axis[3] / 10000 + " deg."
                            + "\n B = " + (float)_PosDataXYZ.axis[4] / 10000 + " deg."
                            + "\n C = " + (float)_PosDataXYZ.axis[5] / 10000 + " deg.");

                    break;

                #endregion

                #region P2
                case "P2":

                    lab_WldTestP2.Text =
                         ("P[ 001 ] = P2"
                            + "\n\n X = " + (float)_PosDataXYZ.axis[0] / 1000 + " mm"
                            + "\n Y = " + (float)_PosDataXYZ.axis[1] / 1000 + " mm"
                            + "\n Z = " + (float)_PosDataXYZ.axis[2] / 1000 + " mm"
                            + "\n A = " + (float)_PosDataXYZ.axis[3] / 10000 + " deg."
                            + "\n B = " + (float)_PosDataXYZ.axis[4] / 10000 + " deg."
                            + "\n C = " + (float)_PosDataXYZ.axis[5] / 10000 + " deg.");

                    break;

                #endregion

                #region Unload Point
                case "Unload":

                    lab_WldTestPUnload.Text =
                         ("P[ 001 ] = Unload"
                            + "\n\n X = " + (float)_PosDataXYZ.axis[0] / 1000 + " mm"
                            + "\n Y = " + (float)_PosDataXYZ.axis[1] / 1000 + " mm"
                            + "\n Z = " + (float)_PosDataXYZ.axis[2] / 1000 + " mm"
                            + "\n A = " + (float)_PosDataXYZ.axis[3] / 10000 + " deg."
                            + "\n B = " + (float)_PosDataXYZ.axis[4] / 10000 + " deg."
                            + "\n C = " + (float)_PosDataXYZ.axis[5] / 10000 + " deg.");

                    break;

                    #endregion
            }
        }

        /// <summary>
        /// Set WT Position Data
        /// </summary>
        /// <param name="_pNum"></param>
        private void SetWTPositionData(string _pNum)
        {
            switch (_pNum)
            {
                #region Load Point
                case "Load":
                    _WTData_Load = _GetCurXYZPosData;
                    SetWTPositionDatatoTXT(_pNum, _WTData_Load);

                    _WTLoad = true;
                    break;

                #endregion

                #region P1
                case "P1":
                    _WTData_P1 = _GetCurXYZPosData;
                    SetWTPositionDatatoTXT(_pNum, _WTData_P1);

                    _WTP1 = true;
                    break;

                #endregion

                #region P2
                case "P2":
                    _WTData_P2 = _GetCurXYZPosData;
                    SetWTPositionDatatoTXT(_pNum, _WTData_P2);

                    _WTP2 = true;
                    break;

                #endregion

                #region Unload Point
                case "Unload":
                    _WTData_Unload = _GetCurXYZPosData;
                    SetWTPositionDatatoTXT(_pNum, _WTData_Unload);

                    _WTUnload = true;
                    break;

                    #endregion

            }
        }

        /// <summary>
        ///  Welding test Compile Program 
        /// </summary>
        private bool WTCompileProgram()
        {
            try
            {
                // 實作程式物件
                RouteBook_Yaskawa _routeBook_Welding = new RouteBook_Yaskawa("WELDINGTEST",5, 0, 0); // ( Home,Load,P1,P2,Unload)
                //寫入工作環境生成狀態
                _routeBook_Welding.Workspace = 0;
                _routeBook_Welding.FlipMode = 1;

                for (int i = 0; i < 5 ; i++)
                {
                    if (i==0) 
                    {
                        _routeBook_Welding.ProcessQueue[i] = 1;  // (1: Point  2: Dout  3: 註解)
                        _routeBook_Welding.MovingMode[i] = 1; //  (1: MOVL  2: MOVS )
                        _routeBook_Welding.Tool[i] = 0;
                        _routeBook_Welding.Override[i] = 10;
                        _routeBook_Welding.Accerlerate[i] = 70;
                        _routeBook_Welding.Decerlerate[i] = 70;
                        //填入Point 資料數值
                        _routeBook_Welding.X[i] = _HomePosData.axis[0];
                        _routeBook_Welding.Y[i] = _HomePosData.axis[1];
                        _routeBook_Welding.Z[i] = _HomePosData.axis[2];
                        _routeBook_Welding.A[i] = _HomePosData.axis[3];
                        _routeBook_Welding.B[i] = _HomePosData.axis[4];
                        _routeBook_Welding.C[i] = _HomePosData.axis[5];

                    } //Home Point
                    else if(i == 1)
                    {
                        //填入Point 資料屬性
                        _routeBook_Welding.ProcessQueue[i] = 1;  // (1: Point  2: Dout  3: 註解)
                        _routeBook_Welding.MovingMode[i] = 1; //  (1: MOVL  2: MOVS )
                        _routeBook_Welding.Tool[i] = 0;
                        _routeBook_Welding.Override[i] = 10;
                        _routeBook_Welding.Accerlerate[i] = 70;
                        _routeBook_Welding.Decerlerate[i] = 70;
                        //填入Point 資料數值
                        _routeBook_Welding.X[i] = _WTData_Load.axis[0];
                        _routeBook_Welding.Y[i] = _WTData_Load.axis[1];
                        _routeBook_Welding.Z[i] = _WTData_Load.axis[2];
                        _routeBook_Welding.A[i] = _WTData_Load.axis[3];
                        _routeBook_Welding.B[i] = _WTData_Load.axis[4];
                        _routeBook_Welding.C[i] = _WTData_Load.axis[5];
                    } //Load
                    else if (i == 2)
                    {
                        //填入Point 資料屬性
                        _routeBook_Welding.ProcessQueue[i] = 1;  // (1: Point  2: Dout  3: 註解)
                        _routeBook_Welding.MovingMode[i] = 1; //  (1: MOVL  2: MOVS )
                        _routeBook_Welding.Tool[i] = 0;
                        _routeBook_Welding.Override[i] = 10;
                        _routeBook_Welding.Accerlerate[i] = 70;
                        _routeBook_Welding.Decerlerate[i] = 70;
                        //填入Point 資料數值
                        _routeBook_Welding.X[i] = _WTData_P1.axis[0];
                        _routeBook_Welding.Y[i] = _WTData_P1.axis[1];
                        _routeBook_Welding.Z[i] = _WTData_P1.axis[2];
                        _routeBook_Welding.A[i] = _WTData_P1.axis[3];
                        _routeBook_Welding.B[i] = _WTData_P1.axis[4];
                        _routeBook_Welding.C[i] = _WTData_P1.axis[5];
                    } //P1
                    else if (i == 3)
                    {
                        //填入Point 資料屬性
                        _routeBook_Welding.ProcessQueue[i] = 1;  // (1: Point  2: Dout  3: 註解)
                        _routeBook_Welding.MovingMode[i] = 1; //  (1: MOVL  2: MOVS )
                        _routeBook_Welding.Tool[i] = 0;
                        _routeBook_Welding.Override[i] = 10;
                        _routeBook_Welding.Accerlerate[i] = 70;
                        _routeBook_Welding.Decerlerate[i] = 70;
                        //填入Point 資料數值
                        _routeBook_Welding.X[i] = _WTData_P2.axis[0];
                        _routeBook_Welding.Y[i] = _WTData_P2.axis[1];
                        _routeBook_Welding.Z[i] = _WTData_P2.axis[2];
                        _routeBook_Welding.A[i] = _WTData_P2.axis[3];
                        _routeBook_Welding.B[i] = _WTData_P2.axis[4];
                        _routeBook_Welding.C[i] = _WTData_P2.axis[5];
                    } //P2
                    else if (i == 4)
                    {
                        //填入Point 資料屬性
                        _routeBook_Welding.ProcessQueue[i] = 1;  // (1: Point  2: Dout  3: 註解)
                        _routeBook_Welding.MovingMode[i] = 1; //  (1: MOVL  2: MOVS )
                        _routeBook_Welding.Tool[i] = 0;
                        _routeBook_Welding.Override[i] = 10;
                        _routeBook_Welding.Accerlerate[i] = 70;
                        _routeBook_Welding.Decerlerate[i] = 70;
                        //填入Point 資料數值
                        _routeBook_Welding.X[i] = _WTData_Unload.axis[0];
                        _routeBook_Welding.Y[i] = _WTData_Unload.axis[1];
                        _routeBook_Welding.Z[i] = _WTData_Unload.axis[2];
                        _routeBook_Welding.A[i] = _WTData_Unload.axis[3];
                        _routeBook_Welding.B[i] = _WTData_Unload.axis[4];
                        _routeBook_Welding.C[i] = _WTData_Unload.axis[5];
                    } //Unload

                }

                //程式編譯
                int _rslt;
                _rslt = YaskawaController.CompileFile(_routeBook_Welding, "WELDINGTEST","HOME", 1, 1 , 2 , 3 );
                if (_rslt==0) 
                { 
                    MessageBox.Show("Program CompileFile OK!!!");
                    if (rad_WTProgramType2.Checked)
                    {
                        //程式匯入
                        _rslt = YaskawaController.Upload2Controller("WELDINGTEST");
                        if (_rslt == 0) { MessageBox.Show("Program Load OK!!!"); }
                        else { MessageBox.Show("Program Load Fale!!!"); }
                    }
                    
                }else{MessageBox.Show("Program CompileFile Fale!!!");}
                
                return true;
            }
            catch { MessageBox.Show("system error"); return false; }
        }

        /// <summary>
        /// WT Read Point State
        /// </summary>
        private void WTReadPointState()
        {
            short _speedVal = 0;
            short _ACVal = 0;
            short _AVPVal = 0;

            if (_WTLoad) { lab_WTPSet_Load.ForeColor = System.Drawing.Color.Green; } else { lab_WTPSet_Load.ForeColor = System.Drawing.Color.Red; }
            if (_WTP1) { lab_WTPSet_P1.ForeColor = System.Drawing.Color.Green; } else { lab_WTPSet_P1.ForeColor = System.Drawing.Color.Red; }
            if (_WTP2) { lab_WTPSet_P2.ForeColor = System.Drawing.Color.Green; } else { lab_WTPSet_P2.ForeColor = System.Drawing.Color.Red; }
            if (_WTUnload) { lab_WTPSet_Unload.ForeColor = System.Drawing.Color.Green; } else { lab_WTPSet_Unload.ForeColor = System.Drawing.Color.Red; }

            
            RobotRegRead(1, ref _speedVal);
            RobotRegRead(2, ref _ACVal);
            RobotRegRead(3, ref _AVPVal);
            lab_WTSpeedReg.Text = "Speed : " + _speedVal.ToString();
            lab_WTACReg.Text = "AC : " + _ACVal.ToString();
            lab_WTAVPReg.Text = "AVP : " + _AVPVal.ToString();

        }

        /// <summary>
        /// WT Redo Set Data
        /// </summary>
        private void WTRedoSetData()
        {

            _WTData_Load.axis[0] += (Convert.ToInt32(tex_WTRedo_X.Text) * 1000);
            _WTData_Load.axis[1] += (Convert.ToInt32(tex_WTRedo_Y.Text) * 1000);
            _WTData_Load.axis[2] += (Convert.ToInt32(tex_WTRedo_Z.Text) * 1000);
            
            if (_connectState)
            {
                _WTData_P1.axis[0] += (Convert.ToInt32(tex_WTRedo_X.Text) * 1000);
                _WTData_P1.axis[1] += (Convert.ToInt32(tex_WTRedo_Y.Text) * 1000);
                _WTData_P1.axis[2] += (Convert.ToInt32(tex_WTRedo_Z.Text) * 1000);

                _WTData_P2.axis[0] += (Convert.ToInt32(tex_WTRedo_X.Text) * 1000);
                _WTData_P2.axis[1] += (Convert.ToInt32(tex_WTRedo_Y.Text) * 1000);
                _WTData_P2.axis[2] += (Convert.ToInt32(tex_WTRedo_Z.Text) * 1000);

                _WTData_Unload.axis[0] += (Convert.ToInt32(tex_WTRedo_X.Text) * 1000);
                _WTData_Unload.axis[1] += (Convert.ToInt32(tex_WTRedo_Y.Text) * 1000);
                _WTData_Unload.axis[2] += (Convert.ToInt32(tex_WTRedo_Z.Text) * 1000);

            }
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
            string path = _CurrentDirectory + "\\" + _fileName + ".JBI";
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

        #endregion

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

        /// <summary>
        /// Robot State timer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StateTimer_Tick(object sender, EventArgs e)
        {
            RobotStateCheck();
            RobotCurPosGet("XYZ", ref _GetCurXYZPosData);
            RobotCurPosGet("SLU", ref _GetCurSLUPosData);
            CurPosShowLab(_GetCurXYZPosData, _GetCurSLUPosData);
            WTReadPointState();
        }

        /// <summary>
        /// WT Loop Timer (需確認)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WTLoopTimer_Tick(object sender, EventArgs e)
        {
            if (_GetCurXYZPosData.axis[1] > _WTData_P1.axis[1])
            {
                RobotRegWrite(1, short.Parse(tex_WTSpeed_P1.Text));
                RobotRegWrite(2, short.Parse(tex_WTAC_P1.Text));
                RobotRegWrite(3, short.Parse(tex_WTAVP_P1.Text));
            } //寫入設定數值 P1 (Speed/AC/AVP)
            if (_GetCurXYZPosData.axis[1] > _WTData_P2.axis[1])
            {
                RobotRegWrite(1, short.Parse(tex_WTSpeed_P2.Text));
                RobotRegWrite(2, short.Parse(tex_WTAC_P2.Text));
                RobotRegWrite(3, short.Parse(tex_WTAVP_P2.Text));
            } //寫入設定數值 P2 (Speed/AC/AVP)
            if (_GetCurXYZPosData.axis[1] > _WTData_Unload.axis[1])
            {
                RobotRegWrite(1, short.Parse(tex_WTSpeed_ULD.Text));
                RobotRegWrite(2, short.Parse(tex_WTAC_ULD.Text));
                RobotRegWrite(3, short.Parse(tex_WTAVP_ULD.Text));
            } //寫入設定數值 Unload (Speed/AC/AVP)
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
