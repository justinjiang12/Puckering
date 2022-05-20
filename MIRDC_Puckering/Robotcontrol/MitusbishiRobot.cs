using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading;
using System.IO;
//using System.Linq;

namespace MIRDC_Puckering.Robotcontrol
{
    public partial class MitusbiahiRobotForm : Form
    {
        #region  欄位宣告

        DirectoryInfo dirInfo;
        FileSystemWatcher _watch = new FileSystemWatcher();
        BindingList<PathData> PathDataList = new BindingList<PathData>();

        //委派From 物件(prg_lis)
        private delegate void InvokeUpdateState(int type, string FileName);

        //判斷輸入檔名<A-Z,a-z,0-9>範圍
        System.Text.RegularExpressions.Regex ProgramNameCheck = new System.Text.RegularExpressions.Regex(@"^[A-Za-z0-9]+$");


        int lRobotID = 1; //手臂控制ID
        string sErrMsg = ""; //異常訊息欄位
        int lStatus = 0; //手臂參考狀態欄位
        int jogSpeed = 10;
        string RobotPrgName = "";

        #endregion

        public MitusbiahiRobotForm()
        {
            InitializeComponent();
        }


        #region Form control
        /// <summary>
        /// Form control
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ControlForm_Load(object sender, EventArgs e)
        {
            bool robotState = InitialRobot();
            Robot_GetInf(robotState);
            InitalRobotSpeed();
        }
        private void ControlForm_closing(object sender, FormClosingEventArgs e)
        {
            axMelfaRxM1.ServerKill();
        }
        #endregion

        #region 按鈕集

        /// <summary>
        /// Button(Click) 集合 ---> Justin
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonControl_Click(object sender, EventArgs e)
        {
            #region for **** button

            int f_i_state = 0;
            bool f_b_state = false;

            int iStep = 1;
            string sPrgName = "", sCmdLines = "", sDefPos = "", sPosRef = "", sPrgServoOn = "", sSPD = "", sMvs = "", sPrgHlt = "";
            string s1 = "";
            string pr_path = GetUpLevelDirectory(this.GetType().Assembly.Location, 3) + @"\RobotPathPrg\";
            //string pr_path = this.GetType().Assembly.Location;



            #endregion

            Button btn = (Button)sender; //轉型            
            string m_btnName = (string)btn.Tag;
            switch (m_btnName)
            {
                #region 測試按鈕
                case "test_btn":

                    try
                    {
                        /*點選資料確認測試
                    if (prg_lis.SelectedIndex!=-1)
                    {  
                        //寫入prg_textbox內容
                        prg_textbox.Clear();                 
                        prg_textbox.Text = prg_lis.SelectedItem.ToString();   
                    }
                    else{MessageBox.Show("請點選資料!!!");}
                    */

                        /*
                         //程式寫入測試
                        string Pro6 = "TestMove\n7\n1 Spd 20\n2 Cnt 0\n3 MvTune 1\n4 Mov P1\n5 Mov P2\n6 Dly 5\n7 Hlt";
                        string Pos6 = "TestMove\n2\nP1=(362.58,-149.30,429.05,-180,60,-180)(7,0)\nP2=(362.58,-140.30,429.05,-180,60,-180)(7,0)";
                        f_b_state = RobotProgramWrite(Pro6, Pos6);
                        */

                        //呼叫RTtlbx2Wrap 內部已定義之字串內容 (Commands.*)
                        //prg_textbox.Text= Commands.Mov;

                        /* 方法測試
                        string data = "";
                        get_dataGridView(Convert.ToInt32(rows_textBox.Text), Convert.ToInt32(cells_textBox.Text), ref  data);
                        MessageBox.Show(data);
                        */


                        //read robot program
                        //string programList = "";


                        /*
                        //lStatus = axMelfaRxM1.RequestServiceM(lRobotID, 106, 0, string.Empty, 1, 0, 0);
                        lStatus = axMelfaRxM1.RequestServiceM(lRobotID, 106, 0, string.Empty, 1, 0, 0);
                        textBox1.Text = programList;
                        if (lStatus != 1)
                        {
                            string msg = "(E1601) Robot Program list request transmission Error!";
                            Console.WriteLine(msg);
                            //cLog.WriteLog((int)LOGTYPE.ALERT, s1);
                            //AddInfoInvoke.Invoke((int)LOGTYPE.ALERT, "Robot", s1);
                            //MessageBox.Show("Send error");
                        }
                        */


                        //測試取得dataGridView行數
                        //MessageBox.Show(Convert.ToString(dataGridView1.RowCount) + "\n");


                        /*
                        //角度計算(Atan math)
                        string aaa = "";
                        double data = CalAngle((float)610.00, (float)250.00, (float)10.00, (float)50.00); //運算角度方法
                        double Result = Math.Round(data, 2, MidpointRounding.AwayFromZero); //求至小數點第二位
                        aaa = Result.ToString(); //轉換字串
                        label10.Text = aaa+" 度";
                        */

                        //匯入C角度運算資料
                        GetPathCData();

                    }
                    catch { MessageBox.Show("system error"); }


                    break;
                #endregion

                #region 取得dataGridView內部資料按鍵
                case "getdata_btn":

                    try
                    {
                        //MessageBox.Show(dataGridView1.Rows.Count.ToString()); //取得dataGridView內部列數<包含項目列>
                        //MessageBox.Show(dataGridView1.Columns.Count.ToString()); //取得dataGridView內部欄位數<包含點位項目>

                        //Rows(行)需-1才是正確

                        MessageBox.Show(dataGridView1.Rows[Convert.ToInt32(rows_textBox.Text) - 1].Cells[Convert.ToInt32(cells_textBox.Text)].Value.ToString());

                        //MessageBox.Show(dataGridView1.Rows(0).Cells.Item("Column1").Value.ToString());
                    }
                    catch { MessageBox.Show("system error"); }


                    break;
                #endregion

                #region 取得dataGridView內部資料按鍵
                case "Load_PositionData_btn":



                    textBox2.Clear();
                    RobotCompilePointData();


                    /*
                    try
                    {
                        for (int i = 1; i < dataGridView1.RowCount; i++)
                        {
                            textBox2.Text += AddPositionData(i);
                        }

                    }
                    catch { MessageBox.Show("system error"); }
                    */


                    break;
                #endregion

                #region 取得dataGridView內部資料按鍵
                case "Load_Program_btn":


                    textBox1.Clear();
                    RobotCompileProgram();


                    /*
                    int step = 1;
                    try
                    {
                        for (int i = 1; i < dataGridView1.RowCount; i++)
                        {
                            textBox1.Text += step++.ToString() + AddRobotProgram(i);
                        }

                    }
                    catch { MessageBox.Show("system error"); }
                    */

                    break;
                #endregion

                #region 取得dataGridView內部資料按鍵
                case "Load_ORG_btn":
                    try
                    {
                        RobotUpdateORGProgram();
                    }
                    catch { MessageBox.Show("system error"); }

                    break;
                #endregion

                #region 上傳至手臂控制器點位資料
                case "LoadProgram_btn":

                    try
                    {
                        RobotProgramWrite(textBox1.Text, textBox2.Text);
                    }
                    catch { MessageBox.Show("system error"); }

                    break;
                #endregion

                #region 取得旋轉角並寫入
                case "btn_getC":


                    try
                    {
                        GetPathCData();
                    }
                    catch
                    { MessageBox.Show("system error"); }



                    /*
                    try
                    {
                        string[] ABCData = new string[3];
                        CreatRobotPathABC(Convert.ToInt32(Pointdata_textBox.Text), ref ABCData);

                        MessageBox.Show("Arm Angle :\n" + "A(rX) : " + ABCData[0] + "°" + "\n" + "B(rY) : " + ABCData[1] + "°" + "\n" + "C(rZ) : " + ABCData[2] + "°" + "\n");
                    }
                    catch { MessageBox.Show("system error"); }
                    */

                    break;
                #endregion

                #region 取得傾角並寫入
                case "btn_getX":


                    try
                    {
                        GetPathXData();
                    }
                    catch
                    { MessageBox.Show("system error"); }





                    break;
                #endregion

                #region 取得路徑繞軸角並寫入
                case "btn_getY":


                    try
                    {
                        GetPathYData();
                    }
                    catch
                    { MessageBox.Show("system error"); }





                    break;
                #endregion

                #region 取得點位ATen2(Y,X) -> C
                case "getaten2_btn":


                    try
                    {
                        string[] PData = new string[3];
                        string[] PPData = new string[3];
                        string c = "0";

                        for (int i = 0; i < PData.Length; i++)
                        {
                            get_dataGridView(Convert.ToInt32(Pointdata_textBox.Text) - 1, i + 1, ref PData[i]);
                            get_dataGridView(Convert.ToInt32(Pointdata_textBox.Text) - 2, i + 1, ref PPData[i]);
                        }
                        float ATen2_X = float.Parse(PData[0]) - float.Parse(PPData[0]);
                        float ATen2_Y = float.Parse(PData[1]) - float.Parse(PPData[1]);
                        string s_ATen2_X = Math.Round(ATen2_X, 2, MidpointRounding.AwayFromZero).ToString();
                        string s_ATen2_Y = Math.Round(ATen2_Y, 2, MidpointRounding.AwayFromZero).ToString();

                        get_dataGridView(Convert.ToInt32(Pointdata_textBox.Text) - 1, 6, ref c);
                        MessageBox.Show(
                            "POINT : " + Pointdata_textBox.Text + "\n" + " (Y,X) -> C \n" + "(" + s_ATen2_Y + " , " + s_ATen2_X + ")" + " -> " + c);
                    }
                    catch
                    { MessageBox.Show("system error"); }




                    break;
                #endregion

                #region 取得dataGridView內部資料並排列成點位數據資料<按鍵>
                case "getPointdata_btn":

                    try
                    {
                        MessageBox.Show(AddPositionData(Convert.ToInt32(Pointdata_textBox.Text)));
                    }
                    catch { MessageBox.Show("system error"); }


                    break;
                #endregion

                #region 取得dataGridView內部資料並排列成點位數據資料<按鍵>
                case "getPointdataABC_btn":
                    try
                    {
                        string[] ABCData = new string[3];
                        CreatRobotPathABC(Convert.ToInt32(Pointdata_textBox.Text) - 1, ref ABCData);

                        MessageBox.Show("Arm Angle :\n" + "A(rX) : " + ABCData[0] + "°" + "\n" + "B(rY) : " + ABCData[1] + "°" + "\n" + "C(rZ) : " + ABCData[2] + "°" + "\n");
                    }
                    catch { MessageBox.Show("system error"); }


                    break;
                #endregion

                #region csv匯入dataGridView內部<按鍵>
                case "LoadData_btn":

                    try
                    {

                        //dataGridView1.DataSource = LoadCSV(Browse_textbox.Text);
                        dataGridView1.Rows.Clear();
                        LoadCSV(Browse_textbox.Text);
                    }
                    catch { MessageBox.Show("system error"); }


                    break;
                #endregion

                #region 資料路徑尋找<按鍵>
                case "Browse_btn":

                    try
                    {
                        OpenFileDialog dlg = new OpenFileDialog();
                        dlg.ShowDialog();
                        Browse_textbox.Text = dlg.FileName;
                    }
                    catch { MessageBox.Show("system error"); }


                    break;
                #endregion

                #region Robot SV ON (Mitusbishi)
                case "svon_btn":

                    RobotServoOn();

                    break;
                #endregion

                #region Robot SV OFF (Mitusbishi)
                case "svoff_btn":

                    RobotServoOff();

                    break;
                #endregion

                #region Robot SV Alarm RST(Mitusbishi)
                case "Alarm Rst":

                    RobotResetError();

                    break;
                #endregion

                #region 寫入 Robot Program (Mitusbishi)
                case "WriteProgram":

                    //lisbox_Selet = prg_lis.SelectedItem.ToString();


                    /*
                    if (prg_lis.SelectedIndex != -1)
                    {
                        Pdata_textbox.Clear();
                        StreamReader str = new StreamReader(@pr_path + prg_lis.SelectedItem.ToString());

                        Pdata_textbox.Text += str.ReadToEnd();
                        s1 += str.ReadToEnd();
                        //寫入三菱資料
                        f_i_state = rTmonitor1.Wrap.robot_ProgramWrite(currentRobotIndex, s1, "");
                        str.Close(); //(關閉str)

                    }
                    else { MessageBox.Show("請點選上傳資料!!!"); }

                    */


                    //寫入三菱Driver程式資料

                    //f_i_state = rTmonitor1.Wrap.robot_ProgramWrite(currentRobotIndex, "","");


                    break;
                #endregion

                #region 讀取 Robot Program (Mitusbishi)
                case "ReadProgram":



                    //* f_b_state = rTmonitor1.Wrap.robot_RobotProgramRead(currentRobotIndex, "");

                    break;
                #endregion

                #region 讀取 Robot Program (Mitusbishi)
                case "aRobtIni_btn":

                    RobotProgramInitial();

                    break;
                #endregion

                #region 程序刷新按鈕觸發 (Mitusbishi)
                case "RefreshProgram":

                    if (listBox2.Items.Count > 0) { listBox2.Items.Clear(); }
                    RobotProgramRefresh();
                    //* rTmonitor1.Wrap.robot_ProgramRefresh(currentRobotIndex);


                    /*
                    lStatus = axMelfaRxM1.RequestServiceM(lRobotID, 106, 0, string.Empty, 1, 0, 0);
                    if (lStatus != 1)
                    {
                        string msg = "(E1601) Robot Program list request transmission Error!";
                        Console.WriteLine(msg);

                    }
                    */
                    break;
                #endregion

                #region 手臂回原點程式並上傳至手臂Driver (Mitusbishi)
                case "SendORG":


                    //string text = System.IO.File.ReadAllText(@"C:\Users\j0910\Desktop\三菱手臂控制\MIRDC_AB_Program\MIRDC_PRG\bin\x86\Release\PathFolder\JustinPath.prg");

                    //Robot回原點程式撰寫
                    iStep = 1;
                    sPrgName = "ORG" + "\n";// "REFORG" + "\n";
                    sCmdLines = "6\n";
                    sDefPos = iStep++.ToString() + " Def Pos REFPOS" + "\n";
                    sPosRef = iStep++.ToString() + " REFPOS = (+410.00,-2.67,+397.00,+180.00,+0.00,+0.00)(7,0)" + "\n";
                    sPrgServoOn = iStep++.ToString() + " SERVO ON\n";
                    sSPD = iStep++.ToString() + " Ovrd 10\n";
                    sMvs = iStep++.ToString() + " Mvs REFPOS" + "\n";
                    sPrgHlt = iStep++.ToString() + " Hlt";


                    s1 = sPrgName + sCmdLines + sDefPos + sPosRef + sPrgServoOn + sSPD + sMvs + sPrgHlt;
                    textBox1.Text += s1;

                    //寫入三菱Driver程式資料

                    bool bRet = RobotProgramWrite(s1, "");
                    //f_i_state = rTmonitor1.Wrap.robot_ProgramWrite(currentRobotIndex, s1, "");


                    break;
                #endregion

                #region 執行Robot Program (Mitusbishi)
                case "ProgramGO":

                    GetListBoxData(ref RobotPrgName);
                    if (RobotPrgName != "")
                    {
                        RobotProgramInitial();
                        RobotProgramStart(RobotPrgName);
                    }
                    else
                    {
                        MessageBox.Show("請選用欲啟動程式");
                    }

                    break;
                #endregion

                #region 執行Robot Program(Cycle) (Mitusbishi)
                case "GoCycle":

                    GetListBoxData(ref RobotPrgName);
                    if (RobotPrgName != "")
                    {
                        RobotProgramInitial();
                        RobotProgramStartCycle(RobotPrgName);
                    }
                    else
                    {
                        MessageBox.Show("請選用欲啟動程式");
                    }

                    break;
                #endregion




                #region 執行Robot Program(Cycle stop) (Mitusbishi)
                case "StopCycle":


                    RobotProgramStopCycle();

                    break;
                #endregion



                #region Other
                case "Other":

                    break;
                    #endregion

            }
        }

        /// <summary>
        /// Button(MouseDown) 集合 ---> Justin
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonControl_MouseDown(object sender, MouseEventArgs e)
        {
            #region for **** button

            #endregion

            Button btn = (Button)sender; //轉型            
            string m_btnName = (string)btn.Tag;
            switch (m_btnName)
            {

                #region Jog X / J1 (-)
                case "btn_xj1-":


                    if (rad_robotxyz.Checked) { RobotJog(1, jogSpeed, 0, -1); }
                    else { RobotJog(0, jogSpeed, 0, -1); }

                    break;
                #endregion

                #region Jog X / J1 (+) 
                case "btn_xj1+":


                    if (rad_robotxyz.Checked) { RobotJog(1, jogSpeed, 0, 1); }
                    else { RobotJog(0, jogSpeed, 0, 1); }

                    break;
                #endregion

                #region Jog Y / J2 (-) 
                case "btn_xj2-":


                    if (rad_robotxyz.Checked) { RobotJog(1, jogSpeed, 1, -1); }
                    else { RobotJog(0, jogSpeed, 1, -1); }


                    break;
                #endregion

                #region Jog Y / J2 (+) 
                case "btn_xj2+":
                    if (rad_robotxyz.Checked) { RobotJog(1, jogSpeed, 1, 1); }
                    else { RobotJog(0, jogSpeed, 1, 1); }

                    break;
                #endregion

                #region  Jog Z / J3 (-)
                case "btn_xj3-":
                    if (rad_robotxyz.Checked) { RobotJog(1, jogSpeed, 2, -1); }
                    else { RobotJog(0, jogSpeed, 2, -1); }


                    break;
                #endregion

                #region Jog Z / J3 (+) 
                case "btn_xj3+":
                    if (rad_robotxyz.Checked) { RobotJog(1, jogSpeed, 2, 1); }
                    else { RobotJog(0, jogSpeed, 2, 1); }

                    break;
                #endregion

                #region Jog A / J4 (-) 
                case "btn_xj4-":
                    if (rad_robotxyz.Checked) { RobotJog(1, jogSpeed, 3, -1); }
                    else { RobotJog(0, jogSpeed, 3, -1); }

                    break;
                #endregion

                #region Jog A / J4 (+) 
                case "btn_xj4+":
                    if (rad_robotxyz.Checked) { RobotJog(1, jogSpeed, 3, 1); }
                    else { RobotJog(0, jogSpeed, 3, 1); }
                    break;
                #endregion

                #region Jog B / J5 (-) 
                case "btn_xj5-":
                    if (rad_robotxyz.Checked) { RobotJog(1, jogSpeed, 4, -1); }
                    else { RobotJog(0, jogSpeed, 4, -1); }

                    break;
                #endregion

                #region Jog B / J5 (+) 
                case "btn_xj5+":
                    if (rad_robotxyz.Checked) { RobotJog(1, jogSpeed, 4, 1); }
                    else { RobotJog(0, jogSpeed, 4, 1); }

                    break;
                #endregion

                #region Jog C / J6 (-) 
                case "btn_xj6-":
                    if (rad_robotxyz.Checked) { RobotJog(1, jogSpeed, 5, -1); }
                    else { RobotJog(0, jogSpeed, 5, -1); }

                    break;
                #endregion

                #region Jog C / J6 (+) 
                case "btn_xj6+":
                    if (rad_robotxyz.Checked) { RobotJog(1, jogSpeed, 5, 1); }
                    else { RobotJog(0, jogSpeed, 5, 1); }

                    break;
                #endregion

                #region 
                case "other":



                    break;
                    #endregion

            }
        }

        /// <summary>
        /// Button(MouseUp) 集合 ---> Justin
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonControl_MouseUp(object sender, MouseEventArgs e)
        {
            #region for **** button

            #endregion

            Button btn = (Button)sender; //轉型            
            string m_btnName = (string)btn.Tag;
            switch (m_btnName)
            {
                #region Jog X / J1 (-)
                case "btn_xj1-":

                    RobotCancelRequest(405);
                    RobotStop();

                    break;
                #endregion

                #region Jog X / J1 (+) 
                case "btn_xj1+":

                    RobotCancelRequest(405);
                    RobotStop();

                    break;
                #endregion

                #region Jog Y / J2 (-) 
                case "btn_xj2-":
                    RobotCancelRequest(405);
                    RobotStop();

                    break;
                #endregion

                #region Jog Y / J2 (+) 
                case "btn_xj2+":
                    RobotCancelRequest(405);
                    RobotStop();

                    break;
                #endregion

                #region  Jog Z / J3 (-)
                case "btn_xj3-":
                    RobotCancelRequest(405);
                    RobotStop();

                    break;
                #endregion

                #region Jog Z / J3 (+) 
                case "btn_xj3+":
                    RobotCancelRequest(405);
                    RobotStop();

                    break;
                #endregion

                #region Jog A / J4 (-) 
                case "btn_xj4-":
                    RobotCancelRequest(405);
                    RobotStop();
                    break;
                #endregion

                #region Jog A / J4 (+) 
                case "btn_xj4+":
                    RobotCancelRequest(405);
                    RobotStop();

                    break;
                #endregion

                #region Jog B / J5 (-) 
                case "btn_xj5-":
                    RobotCancelRequest(405);
                    RobotStop();

                    break;
                #endregion

                #region Jog B / J5 (+) 
                case "btn_xj5+":
                    RobotCancelRequest(405);
                    RobotStop();

                    break;
                #endregion

                #region Jog C / J6 (-) 
                case "btn_xj6-":
                    RobotCancelRequest(405);
                    RobotStop();

                    break;
                #endregion

                #region Jog C / J6 (+) 
                case "btn_xj6+":
                    RobotCancelRequest(405);
                    RobotStop();

                    break;
                #endregion

                #region 
                case "other":



                    break;
                    #endregion

            }
        }

        #endregion

        #region 相關方法控制


        /// <summary>
        /// 取得選用手臂程式選單內容
        /// </summary>
        /// <param name="ListData"></param>
        private void GetListBoxData(ref string ListData)
        {
            if (listBox2.SelectedIndex != -1)
            {
                ListData = listBox2.SelectedItem.ToString();

            }
        }


        /// <summary>
        /// 取得父階層文件路徑 (方法)
        /// </summary>
        /// <param name="path"></param>
        /// <param name="upLevel"></param>
        /// <returns></returns>
        private string GetUpLevelDirectory(string path, int upLevel)
        {
            var directory = File.GetAttributes(path).HasFlag(FileAttributes.Directory)
                ? path : Path.GetDirectoryName(path);

            upLevel = upLevel < 0 ? 0 : upLevel;

            for (var i = 0; i < upLevel; i++)
            {
                directory = Path.GetDirectoryName(directory);
            }

            return directory;
        }




        /*
        /// <summary>
        /// 取得csv並匯入DataGridView
        /// </summary>
        public List<PathData> LoadCSV(string csvFile)
        {


            var query = from l in File.ReadAllLines(csvFile)
                        let data = l.Split(',')
                        select new PathData
                        {
                            Name = data[0],
                            X = data[1],
                            Y = data[2],
                            Z = data[3],
                            A = data[4],
                            B = data[5],
                            C = data[6]
                        };

            
            return query.ToList();
        }
        */


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

                //ss[0] = Math.Round(Convert.ToDouble(ss[0]), 2, MidpointRounding.AwayFromZero).ToString();
                //ss[1] = Math.Round(Convert.ToDouble(ss[1]), 2, MidpointRounding.AwayFromZero).ToString();
                //ss[2] = Math.Round(Convert.ToDouble(ss[2]), 2, MidpointRounding.AwayFromZero).ToString();
                //ss[3] = Math.Round(Convert.ToDouble(ss[3]), 2, MidpointRounding.AwayFromZero).ToString();
                //ss[4] = Math.Round(Convert.ToDouble(ss[4]), 2, MidpointRounding.AwayFromZero).ToString();
                //ss[5] = Math.Round(Convert.ToDouble(ss[5]), 2, MidpointRounding.AwayFromZero).ToString();
                //ss[6] = Math.Round(Convert.ToDouble(ss[6]), 2, MidpointRounding.AwayFromZero).ToString();

                PathDataList.Add(new PathData
                {
                    Name = ss[0],
                    X = Math.Round(float.Parse(ss[1]) + 170, 2, MidpointRounding.AwayFromZero).ToString(),
                    Y = Math.Round(float.Parse(ss[2]) + 150, 2, MidpointRounding.AwayFromZero).ToString(),
                    Z = Math.Round(0 - float.Parse(ss[3]), 2, MidpointRounding.AwayFromZero).ToString(),
                    A = ss[4],
                    B = ss[5],
                    C = ss[6]



                });

            }

        }






        /// <summary>
        /// 寫入Get Path C Data
        /// </summary>
        private void GetPathCData()
        {
            short p;
            short p_p;
            string[] P_data = new string[3]; //X,Y,Z
            string[] PP_data = new string[3]; //X',Y',Z'
            float[] P_fdata = new float[3]; //X,Y,Z (float)
            float[] PP_fdata = new float[3]; //X',Y',Z'(float)
            double Prv_C_Data;
            string C_Data = "";


            try
            {
                //依序取得各點數據(取得後運算)
                for (int n = 0; n < dataGridView1.RowCount - 1; n++)
                {
                    //依序取得各點X,Y數據
                    for (int i = 0; i < P_data.Length; i++)
                    {
                        //取得數據並判斷最後一個點位接續第一個點位資料運算
                        if (n >= 1)
                        {
                            get_dataGridView(n, i + 1, ref P_data[i]);
                            get_dataGridView(n - 1, i + 1, ref PP_data[i]);
                            P_fdata[i] = float.Parse(P_data[i]);
                            PP_fdata[i] = float.Parse(PP_data[i]);
                        }
                        else if (n < 1)
                        {
                            get_dataGridView(1, i + 1, ref P_data[i]);
                            get_dataGridView(1, i + 1, ref PP_data[i]);
                            P_fdata[i] = float.Parse(P_data[i]);
                            PP_fdata[i] = float.Parse(PP_data[i]);
                        }


                    }

                    //計算夾角 <Aten2+取小數後兩位>
                    Prv_C_Data = Math.Round(CalAngle(PP_fdata[0], P_fdata[0], PP_fdata[1], P_fdata[1]), 2, MidpointRounding.AwayFromZero);


                    if (Prv_C_Data < 0 && radioButton5.Checked) { Prv_C_Data = 0 - Prv_C_Data; }
                    else if (Prv_C_Data > 0 && radioButton5.Checked) { Prv_C_Data = -Prv_C_Data; }



                    C_Data = Prv_C_Data.ToString();
                    PathDataList[n].C = C_Data;

                }

            }
            catch
            {
                MessageBox.Show("CreatRobotPathABC error!!!!");
            }

        }


        /// <summary>
        /// 寫入Get Path X Data
        /// </summary>
        private void GetPathXData()
        {
            short p;
            short p_p;
            string[] P_data = new string[3]; //X,Y,Z
            string[] PP_data = new string[3]; //X',Y',Z'
            float[] P_fdata = new float[3]; //X,Y,Z (float)
            float[] PP_fdata = new float[3]; //X',Y',Z'(float)
            double Prv_X_Data;
            string X_Data = "";


            try
            {
                //依序取得各點數據(取得後運算)
                for (int n = 0; n < dataGridView1.RowCount - 1; n++)
                {
                    //依序取得各點X,Y數據
                    for (int i = 0; i < P_data.Length; i++)
                    {
                        //取得數據並判斷最後一個點位接續第一個點位資料運算
                        if (n >= 1)
                        {
                            get_dataGridView(n, i + 1, ref P_data[i]);
                            get_dataGridView(n - 1, i + 1, ref PP_data[i]);
                            P_fdata[i] = float.Parse(P_data[i]);
                            PP_fdata[i] = float.Parse(PP_data[i]);
                        }
                        else if (n < 1)
                        {
                            get_dataGridView(1, i + 1, ref P_data[i]);
                            get_dataGridView(1, i + 1, ref PP_data[i]);
                            P_fdata[i] = float.Parse(P_data[i]);
                            PP_fdata[i] = float.Parse(PP_data[i]);
                        }


                    }


                    //計算夾角 <Aten2+取小數後兩位>
                    Prv_X_Data = Math.Round(CalAngle(PP_fdata[2], P_fdata[2], PP_fdata[1], P_fdata[1]), 2, MidpointRounding.AwayFromZero);
                    //Prv_X_Data = Math.Round(CalAngle(PP_fdata[1], P_fdata[1], PP_fdata[2], P_fdata[2]), 2, MidpointRounding.AwayFromZero);

                    /*
                    if (Prv_X_Data < 0 && radioButton8.Checked) { Prv_X_Data = Prv_X_Data+180; }
                    else if (Prv_X_Data > 0 && radioButton8.Checked) { Prv_X_Data = Prv_X_Data-180; }
                    */


                    X_Data = Prv_X_Data.ToString();
                    PathDataList[n].A = X_Data;

                }

            }
            catch
            {
                MessageBox.Show("CreatRobotPathABC error!!!!");
            }

        }



        /// <summary>
        /// 寫入Get Path X Data
        /// </summary>
        private void GetPathYData()
        {
            short p;
            short p_p;
            string[] P_data = new string[3]; //X,Y,Z
            string[] PP_data = new string[3]; //X',Y',Z'
            float[] P_fdata = new float[3]; //X,Y,Z (float)
            float[] PP_fdata = new float[3]; //X',Y',Z'(float)
            double Prv_C_Data;
            string Y_Data = "";


            try
            {
                //依序取得各點數據(取得後運算)
                for (int n = 0; n < dataGridView1.RowCount - 1; n++)
                {
                    Y_Data = textBox4.Text;
                    PathDataList[n].B = Y_Data;
                }

            }
            catch
            {
                MessageBox.Show("CreatRobotPathABC error!!!!");
            }

        }





        /// <summary>
        /// 確認數據正負數
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private bool CheckValueSign(float value)
        {
            Math.Sign(value);



            return true;
        }


        /// <summary>
        /// 取得dataGridView欄位資料
        /// </summary>
        /// <param name="rows_num"></param>
        /// <param name="cells_num"></param>
        /// <param name="data"></param>
        private void get_dataGridView(int rows_num, int cells_num, ref string data)
        {
            try
            {
                data = dataGridView1.Rows[rows_num].Cells[cells_num].Value.ToString();
            }
            catch
            {
                MessageBox.Show("get dataGridView ERROR!!!");
            }

        }

        /// <summary>
        /// 取得單列單筆(點位數據)
        /// </summary>
        /// <param name="p_num"></param>
        /// <returns></returns>
        private string AddPositionData(int p_num)
        {
            string sp = "";
            string[] p_data = new string[7];
            for (int i = 0; i < 7; i++)
            {
                if (i == 0)
                {
                    get_dataGridView(p_num - 1, i, ref p_data[i]);
                    sp += p_data[i] + "=(";
                }
                else if (i > 0 && i < 6)
                {
                    if (i == 1)
                    {
                        //X座標
                        get_dataGridView(p_num - 1, i, ref p_data[i]);
                        p_data[i] = Math.Round(Convert.ToDouble(p_data[i]), 2, MidpointRounding.AwayFromZero).ToString();
                        // p_data[i] = Math.Round((Convert.ToDouble(p_data[i])-150), 2, MidpointRounding.AwayFromZero).ToString(); //Offset (-150)
                        sp += p_data[i] + ",";
                    }
                    else if (i == 2)
                    {
                        //Y座標
                        get_dataGridView(p_num - 1, i, ref p_data[i]);
                        p_data[i] = Math.Round(Convert.ToDouble(p_data[i]), 2, MidpointRounding.AwayFromZero).ToString();
                        // p_data[i] = Math.Round((Convert.ToDouble(p_data[i])-100), 2, MidpointRounding.AwayFromZero).ToString(); //Offset (-100)
                        sp += p_data[i] + ",";
                    }
                    else if (i == 3)
                    {
                        //Z座標
                        get_dataGridView(p_num - 1, i, ref p_data[i]);
                        p_data[i] = Math.Round(Convert.ToDouble(p_data[i]), 2, MidpointRounding.AwayFromZero).ToString();
                        // p_data[i] = Math.Round((Convert.ToDouble(p_data[i])+ 250), 2, MidpointRounding.AwayFromZero).ToString();   //Offset (+250)
                        sp += p_data[i] + ",";
                    }
                    else
                    {
                        get_dataGridView(p_num - 1, i, ref p_data[i]);
                        p_data[i] = Math.Round(Convert.ToDouble(p_data[i]), 2, MidpointRounding.AwayFromZero).ToString();
                        sp += p_data[i] + ",";
                    }
                }
                else
                {
                    get_dataGridView(p_num - 1, i, ref p_data[i]);
                    p_data[i] = Math.Round(Convert.ToDouble(p_data[i]), 2, MidpointRounding.AwayFromZero).ToString();
                    sp += p_data[i] + ")(7,0)\n";
                }

            }
            //sp += "\n";

            return sp;

        }

        /// <summary>
        /// 寫入控制單元內容
        /// </summary>
        /// <param name="p_num"></param>
        /// <returns></returns>
        private string AddRobotProgram(int p_num)
        {
            string sp = "";

            if (radioButton3.Checked) { sp = "Mov P" + p_num.ToString() + "\n"; }
            else { sp = "Mvs P" + p_num.ToString() + "\n"; }

            return sp;
        }

        #region 資料夾監控

        /// <summary>
        /// 資料監控(Form Load用)
        /// </summary>
        private void MyFileSystemWatcher()
        {
            string pr_path = GetUpLevelDirectory(this.GetType().Assembly.Location, 3) + @"\RobotPathPrg";
            //設定所要監控的資料夾
            _watch.Path = pr_path;

            //設定所要監控的變更類型
            _watch.NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite | NotifyFilters.FileName | NotifyFilters.DirectoryName;

            //設定所要監控的檔案
            _watch.Filter = "*.txt";

            //設定是否監控子資料夾
            _watch.IncludeSubdirectories = false;

            //設定是否啟動元件，此部分必須要設定為 true，不然事件是不會被觸發的
            _watch.EnableRaisingEvents = true;

            //設定觸發事件
            _watch.Created += new FileSystemEventHandler(_watch_Created);
            _watch.Changed += new FileSystemEventHandler(_watch_Changed);
            _watch.Renamed += new RenamedEventHandler(_watch_Renamed);
            _watch.Deleted += new FileSystemEventHandler(_watch_Deleted);

        }

        /// <summary>
        /// 當所監控的資料夾有建立文字檔時觸發
        /// </summary>
        private void _watch_Created(object sender, FileSystemEventArgs e)
        {


            dirInfo = new DirectoryInfo(e.FullPath.ToString());
            prg_lis_control(1, dirInfo.Name);
            #region Bypass
            //sb = new StringBuilder();
            //sb.AppendLine("新建檔案於：" + dirInfo.FullName.Replace(dirInfo.Name, ""));
            //sb.AppendLine("新建檔案名稱：" + dirInfo.Name);
            //sb.AppendLine("建立時間：" + dirInfo.CreationTime.ToString());
            //sb.AppendLine("目錄下共有：" + dirInfo.Parent.GetFiles().Count() + " 檔案");
            //sb.AppendLine("目錄下共有：" + dirInfo.Parent.GetDirectories().Count() + " 資料夾");

            //MessageBox.Show(sb.ToString());


            //prg_lis.Items.Add((object)dirInfo.Name);
            //prg_lis.Items.Add("Justin.txt");
            #endregion

        }

        /// <summary>
        /// 當所監控的資料夾有文字檔檔案內容有異動時觸發
        /// </summary>
        private void _watch_Changed(object sender, FileSystemEventArgs e)
        {
            #region Bypass
            //sb = new StringBuilder();

            //dirInfo = new DirectoryInfo(e.FullPath.ToString());

            //sb.AppendLine("被異動的檔名為：" + e.Name);
            //sb.AppendLine("檔案所在位址為：" + e.FullPath.Replace(e.Name, ""));
            //sb.AppendLine("異動內容時間為：" + dirInfo.LastWriteTime.ToString());

            //MessageBox.Show(sb.ToString());
            //prg_lis.Text = sb.ToString();
            //prg_lis_control(2, dirInfo.Name);
            #endregion

        }

        /// <summary>
        /// 當所監控的資料夾有文字檔檔案重新命名時觸發
        /// </summary>
        private void _watch_Renamed(object sender, RenamedEventArgs e)
        {
            #region Bypass
            //sb = new StringBuilder();

            //fi = new FileInfo(e.FullPath.ToString());

            //sb.AppendLine("檔名更新前：" + e.OldName.ToString());
            //sb.AppendLine("檔名更新後：" + e.Name.ToString());
            //sb.AppendLine("檔名更新前路徑：" + e.OldFullPath.ToString());
            //sb.AppendLine("檔名更新後路徑：" + e.FullPath.ToString());
            //sb.AppendLine("建立時間：" + fi.LastAccessTime.ToString());

            //MessageBox.Show(sb.ToString());
            //prg_lis.Text = sb.ToString();
            //prg_lis_control(3, dirInfo.Name);
            #endregion
        }

        /// <summary>
        /// 當所監控的資料夾有文字檔檔案有被刪除時觸發
        /// </summary>
        private void _watch_Deleted(object sender, FileSystemEventArgs e)
        {
            dirInfo = new DirectoryInfo(e.FullPath.ToString());
            prg_lis_control(3, dirInfo.Name);
            #region Bypass
            //sb = new StringBuilder();

            //sb.AppendLine("被刪除的檔名為：" + e.Name);
            //sb.AppendLine("檔案所在位址為：" + e.FullPath.Replace(e.Name, ""));
            //sb.AppendLine("刪除時間：" + DateTime.Now.ToString());

            //MessageBox.Show(sb.ToString());
            // prg_lis.Text = sb.ToString();
            #endregion
        }


        /// <summary>
        /// 插入From 物件(prg_lis)
        /// </summary>
        /// <param name="k"></param>
        private void prg_lis_control(int type, string FileName)
        {
            if (true) //* (this.prg_lis.InvokeRequired)
            {
                this.Invoke(
                  new InvokeUpdateState(this.prg_lis_control), new object[] { type, FileName }
                );
            }
            else
            {  // 原先寫好動作的部分
                switch (type) // type {1 = 寫入 ,2 = 更新 ,3 = 刪除, 4 = 清除}
                {

                    #region 寫入
                    case 1:
                        //*   prg_lis.Items.Add(FileName);
                        break;
                    #endregion

                    #region 更新
                    case 2:


                        break;
                    #endregion

                    #region 刪除
                    case 3:
                        //*   prg_lis.Items.Remove(FileName);
                        break;
                    #endregion

                    #region 清除
                    case 4:
                        //*   prg_lis.Items.Clear();
                        break;
                        #endregion
                }

            }
        }

        /// <summary>
        /// 初始prgfile_init讀取寫入資料內容
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void prgfile_init_read()
        {
            string[] FileCollection;
            string pr_path = GetUpLevelDirectory(this.GetType().Assembly.Location, 3) + @"\RobotPathPrg";
            //設定所要監控的資料夾
            FileInfo theFileInfo;

            FileCollection = Directory.GetFiles(pr_path, "*.txt");
            for (int i = 0; i < FileCollection.Length; i++)
            {
                theFileInfo = new FileInfo(FileCollection[i]);
                //*  prg_lis.Items.Add(theFileInfo.Name.ToString());

            }

        }


        #endregion

        /// <summary>
        /// 手臂速度拖曳調控制
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void speed_Scroll_Scroll(object sender, ScrollEventArgs e)
        {

        }



        /// <summary>
        /// 拖曳Robot速度
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void speed_Scroll_ValueChanged(object sender, EventArgs e)
        {

        }


        /// <summary>
        /// textBox3_Leave 文件檔控制判斷(當控制向不被關注)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBox3_Leave(object sender, EventArgs e)
        {

        }




        #region 演算法測試

        /// <summary>
        /// 角度演算法
        /// </summary>
        /// <param name="a"></param>
        /// <param name="pa"></param>
        /// <param name="b"></param>
        /// <param name="pb"></param>
        /// <returns></returns>
        private double CalAngle(float a, float pa, float b, float pb)
        {
            //求徑度並且換算成角度
            double angle = (Math.Atan2(pa - a, pb - b)) * 180 / Math.PI;
            return angle;
        }

        /// <summary>
        /// 演算單點(ABC)數值
        /// </summary>
        /// <param name="p_num"></param>
        /// <param name="RP_data"></param>
        private void CreatRobotPathABC(int p_num, ref string[] RP_data)
        {
            short p;
            short p_p;
            string[] P_data = new string[3]; //X,Y,Z
            string[] PP_data = new string[3]; //X',Y',Z'
            float[] P_fdata = new float[3]; //X,Y,Z (float)
            float[] PP_fdata = new float[3]; //X',Y',Z'(float)

            try
            {

                for (int i = 0; i < P_data.Length; i++)
                {
                    get_dataGridView(p_num, i + 1, ref P_data[i]);
                    get_dataGridView(p_num + 1, i + 1, ref PP_data[i]);
                    P_fdata[i] = float.Parse(P_data[i]);
                    PP_fdata[i] = float.Parse(PP_data[i]);
                }

                for (int i = 0; i < RP_data.Length; i++)
                {
                    if (i == 0)
                    {

                        //求A(rx)
                        RP_data[i] = Math.Round(CalAngle(P_fdata[2], PP_fdata[2], P_fdata[1], PP_fdata[1]), 2, MidpointRounding.AwayFromZero).ToString();
                    }
                    else if (i == 1)
                    {
                        //求B(ry)
                        RP_data[i] = Math.Round(CalAngle(P_fdata[2], PP_fdata[2], P_fdata[0], PP_fdata[0]), 2, MidpointRounding.AwayFromZero).ToString();
                    }
                    else if (i == 2)
                    {
                        //求C(rz)
                        RP_data[i] = Math.Round(CalAngle(P_fdata[1], PP_fdata[1], P_fdata[0], PP_fdata[0]), 2, MidpointRounding.AwayFromZero).ToString();
                    }
                }
            }
            catch { MessageBox.Show("CreatRobotPathABC error!!!!"); }

        }


        #endregion


        #endregion

        #region Robot 控制方法 <Mitusbishi>

        /// <summary>
        /// Initial Robot
        /// </summary>
        /// <returns></returns>
        private bool InitialRobot()
        {
            string sRobotInf = "";
            string sRobotID;
            string sName;
            string sData;
            int lCnt = 0;
            bool bState = false;

            if (axMelfaRxM1.ServerLive() == false)
            {
                axMelfaRxM1.ServerStart();            //'Start the Communication Server.
                SpinWait.SpinUntil(() => false, 5000);
            }

            //'Robot connection information acquisitions
            bState = axMelfaRxM1.GetRoboComSetting(ref lCnt, ref sRobotInf);
            if (bState == true)
            {
                for (int i = 0; i < lCnt; i++)
                {
                    sRobotID = axMelfaRxM1.GetOneData(i * 2, sRobotInf);
                    sName = axMelfaRxM1.GetOneData(i * 2 + 1, sRobotInf);
                    sData = $"{sRobotID} : {sName}";
                    RobotID_comb.Items.Add(sData);
                    if (i == 0)
                        RobotID_comb.Text = sData;  // 'Show the top of the Dropdown List.
                }
            }
            else
            {
                sErrMsg = "Robot Un-connecting!";
                //cLog.WriteLog((int)LOGTYPE.ALERT, sErrMsg);
                //AddInfoInvoke.Invoke((int)LOGTYPE.ALERT, "Robot", sErrMsg);
                MessageBox.Show("Un-connecting.");
                return false;
            }
            return true;
        }

        /// <summary>
        /// 取得Robot數據
        /// </summary>
        private void Robot_GetInf(bool robotState)
        {
            if (axMelfaRxM1.CheckConnectingM(lRobotID) == true)
            {
                if (robotState)
                {
                    //The request to acquire the information on the actual location on the robot every second is transmitted. 
                    //ccc = '1' + Convert.ToChar(10) + '-1' + Convert.ToChar(10) + '1';
                    //lStatus = axMelfaRxM1.RequestServiceM(lRobotID, 235, ccc.Length, sSendData, 0, 1000, 0);
                    var Data = "1" + Convert.ToChar(10) + "1" + Convert.ToChar(10) + "1";
                    int ll = Data.Length;
                    //Get current position
                    lStatus = axMelfaRxM1.RequestServiceM(lRobotID, 235, ll, Data, 0, 500, 0);
                    if (lStatus != 1)
                    {
                        sErrMsg = "Robot Command 235 Send Error!";
                        //AddInfoInvoke.Invoke((int)LOGTYPE.ALERT, "Robot", sErrMsg);
                    }
                    Data = "1" + Convert.ToChar(10) + "1" + Convert.ToChar(10) + "0";
                    ll = Data.Length;
                    //Get current position
                    lStatus = axMelfaRxM1.RequestServiceM(lRobotID, 235, ll, Data, 0, 500, 0);
                    if (lStatus != 1)
                    {
                        sErrMsg = "Robot Command 235 Send Error!";
                        //AddInfoInvoke.Invoke((int)LOGTYPE.ALERT, "Robot", sErrMsg);
                    }

                    //Program list request transmission
                    lStatus = axMelfaRxM1.RequestServiceM(lRobotID, 106, 0, Data, 1, 0, 0);
                    if (lStatus != 1)
                    {
                        sErrMsg = "Robot Command 106 Send Error!";
                        //AddInfoInvoke.Invoke((int)LOGTYPE.ALERT, "Robot", sErrMsg);
                    }
                    //Robot Status
                    //RequestServiceM(SlotID,MsgID,Data.Length,Data,display,cycle,Priority)
                    Data = "1";
                    lStatus = axMelfaRxM1.RequestServiceM(lRobotID, 200, Data.Length, Data, 0, 100, 0);
                    if (lStatus != 1)
                    {
                        sErrMsg = "Robot Command 200 Send Error!";
                        //AddInfoInvoke.Invoke((int)LOGTYPE.ALERT, "Robot", sErrMsg);
                    }
                    //Get I/O signal information (specify port number)
                    //RequestServiceM(SlotID,MsgID,Data.Length,Data,display,cycle,Priority)
                    Data = "1" + Convert.ToChar(10) + "0" + Convert.ToChar(10) + "1" + Convert.ToChar(10) + "0";
                    lStatus = axMelfaRxM1.RequestServiceM(lRobotID, 214, Data.Length, Data, 0, 100, 0);
                    if (lStatus != 1)
                    {
                        sErrMsg = "Robot Command 214 Send Error!";
                        //AddInfoInvoke.Invoke((int)LOGTYPE.ALERT, "Robot", sErrMsg);
                    }

                    //Get the slot operation status. This is a simplified version.
                    //RequestServiceM(SlotID,MsgID,Data.Length,Data,display,cycle,Priority)
                    Data = "1";
                    lStatus = axMelfaRxM1.RequestServiceM(lRobotID, 242, Data.Length, Data, 0, 100, 0);
                    if (lStatus != 1)
                    {
                        sErrMsg = "Robot Command 242 Send Error!";
                        //AddInfoInvoke.Invoke((int)LOGTYPE.ALERT, "Robot", sErrMsg);
                    }
                }
            }
            else
            {
                sErrMsg = "Robot Unconnection";
                //AddInfoInvoke.Invoke((int)LOGTYPE.ALERT, "Robot", sErrMsg);
            }
        }

        /// <summary>
        /// Robot Sever On
        /// </summary>
        /// <returns></returns>
        public bool RobotServoOn()
        {
            string sSendData = "";
            int lStatus = 0;

            sSendData = "1" + "\n" + "1";
            //'Servo on transmission
            lStatus = axMelfaRxM1.RequestServiceM(lRobotID, 403, sSendData.Length, sSendData, 0, 0, 0);
            if (lStatus != 1)
            {
                sErrMsg = "Robot Command 403 Send Servo ON Error!";
                //cLog.WriteLog((int)LOGTYPE.ALERT, sErrMsg);
                //AddInfoInvoke.Invoke((int)LOGTYPE.ALERT, "Robot", sErrMsg);
                //MessageBox.Show("Send error");
                return false;
            }
            return true;
        }

        /// <summary>
        /// Inital Robot Speed
        /// </summary>
        private void InitalRobotSpeed()
        {
            /*
            sclOverride.Value = 10;
            lbOverride.Text = sclOverride.Value.ToString();
            RobotSetSpeed(sclOverride.Value);
            */
        }


        /// <summary>
        /// Robot Program Initial
        /// </summary>
        /// <returns></returns>
        public bool RobotProgramInitial()
        {
            string sSendData = "1";
            //Stop
            int lStatus = axMelfaRxM1.RequestServiceM(lRobotID, 408, sSendData.Length, sSendData, 0, 0, 0);
            if (lStatus != 1)
            {
                sErrMsg = "Robot Command 408 Send Initialize Error!";
                Console.WriteLine(sErrMsg);
                //cLog.WriteLog((int)LOGTYPE.ALERT, sErrMsg);
                //AddInfoInvoke.Invoke((int)LOGTYPE.ALERT, "Robot", sErrMsg);
                //MessageBox.Show("Send error");
                return false;
            }
            return true;
        }

        /// <summary>
        /// Robot Servo Off
        /// </summary>
        /// <returns></returns>
        public bool RobotServoOff()
        {
            string sSendData = "";
            int lStatus = 0;

            sSendData = "1" + "\n" + "0";
            //'Servo on transmission
            lStatus = axMelfaRxM1.RequestServiceM(lRobotID, 403, sSendData.Length, sSendData, 0, 0, 0);
            if (lStatus != 1)
            {
                sErrMsg = "Robot Command 403 Send Servo OFF Error!";
                //cLog.WriteLog((int)LOGTYPE.ALERT, sErrMsg);
                //AddInfoInvoke.Invoke((int)LOGTYPE.ALERT, "Robot", sErrMsg);
                //MessageBox.Show("Send error");
                return false;
            }
            return true;
        }

        /// <summary>
        /// Robot Reset Error
        /// </summary>
        /// <returns></returns>
        private bool RobotResetError()
        {
            string sSendData = "";
            int lStatus = 0;

            sSendData = "";
            //'Servo on transmission
            lStatus = axMelfaRxM1.RequestServiceM(lRobotID, 407, sSendData.Length, sSendData, 0, 0, 0);
            if (lStatus != 1)
            {
                sErrMsg = "Robot Command 403 Send Reset Error!";
                //cLog.WriteLog((int)LOGTYPE.ALERT, sErrMsg);
                //AddInfoInvoke.Invoke((int)LOGTYPE.ALERT, "Robot", sErrMsg);
                //MessageBox.Show("Send error");
                return false;
            }
            return true;
        }

        /// <summary>
        /// Robot Stop
        /// </summary>
        /// <returns></returns>
        public bool RobotStop()
        {
            string sSendData = "0";
            //Stop
            int lStatus = axMelfaRxM1.RequestServiceM(lRobotID, 401, sSendData.Length, sSendData, 0, 0, 0);
            if (lStatus != 1)
            {
                sErrMsg = "Robot Command 401 Send Stop Error!";
                //cLog.WriteLog((int)LOGTYPE.ALERT, sErrMsg);
                //AddInfoInvoke.Invoke((int)LOGTYPE.ALERT, "Robot", sErrMsg);
                //MessageBox.Show("Send error");
                return false;
            }
            return true;
        }


        /// <summary>
        /// Robot Cancel Request 確認狀態 iRequestID=詢問代號
        /// </summary>
        /// <param name="iRequestID"></param>
        /// <returns></returns>
        public bool RobotCancelRequest(int iRequestID)
        {
            //Stop
            int lStatus = axMelfaRxM1.RequestCancel(iRequestID);
            if (lStatus != 1)
            {
                sErrMsg = "Robot Command 403 Send Cancel Error!";
                //cLog.WriteLog((int)LOGTYPE.ALERT, sErrMsg);
                //AddInfoInvoke.Invoke((int)LOGTYPE.ALERT, "Robot", sErrMsg);
                //MessageBox.Show("Send error");
                return false;
            }
            return true;
        }


        /// <summary>
        /// Robot Jog
        /// </summary>
        /// <param name="iJogType"></param>
        /// <param name="iSpeed"></param>
        /// <param name="iAxis"></param>
        /// <param name="iDir"></param>
        public void RobotJog(int iJogType, int iSpeed, int iAxis, int iDir)
        {
            string sSendData = "";
            int lStatus = 0;

            string sMachNum = "1" + "\n";
            string sJogType = "1" + "\n"; //XYZ
            string sLevel = "1" + "\n";
            string sSpeed = "10" + "\n";
            string[] sDir = new string[8];
            for (int i = 0; i < 8; i++)
            {
                sDir[i] = "0" + "\n";
            }
            string sFixedLength = "0";

            //0:Joint/1:XYZ
            sJogType = iJogType.ToString() + "\n";

            if (iSpeed > 100 || iSpeed < 0) { iSpeed = 10; }
            sSpeed = iSpeed.ToString() + "\n";


            /*
            if (iSpeed > 100 ) { iSpeed = 100; }
            else if (iSpeed < 1) { iSpeed = 1; }
            else { sSpeed = iSpeed.ToString() + "\n"; }
            */


            //sSpeed = iSpeed.ToString() + "\n";

            sDir[iAxis] = iDir.ToString() + "\n";

            //Mechanism number<LF>
            //Jog type<LF>
            //Level type<LF>
            //Jog override [%] <LF>
            //Feed direction 1<LF>Feed direction 2<LF> … Feed direction 8<LF>
            //Fixed length       
            sSendData = sMachNum + sJogType + sLevel + sSpeed +
                        sDir[0] + sDir[1] + sDir[2] + sDir[3] + sDir[4] + sDir[5] + sDir[6] + sDir[7] +
                        sFixedLength;
            //Jog
            lStatus = axMelfaRxM1.RequestServiceM(lRobotID, 405, sSendData.Length, sSendData, 0, 0, 0);
            if (lStatus != 1)
            {
                RobotCancelRequest(0);
                RobotStop();
            }
        }


        /// <summary>
        /// Robot Set Speed <暫時無用>
        /// </summary>
        /// <param name="iSpeed"></param>
        /// <returns></returns>
        public bool RobotSetSpeed(int iSpeed)
        {
            if (iSpeed > 100) iSpeed = 100;
            if (iSpeed < 1) iSpeed = 1;
            string sSendData = iSpeed.ToString();

            //'Servo on transmission
            int lStatus = axMelfaRxM1.RequestServiceM(lRobotID, 404, sSendData.Length, sSendData, 0, 0, 0);
            if (lStatus != 1)
            {
                string msg = "(E1404) Robot Send Command 404 Error!";
                Console.WriteLine(msg);
                //cLog.WriteLog((int)LOGTYPE.ALERT, msg);
                //AddInfoInvoke.Invoke((int)LOGTYPE.ALERT, "Robot", msg);
                //MessageBox.Show("Send error");
                return false;
            }
            return true;
        }

        /// <summary>
        /// axMelfaRxM1_MsgRecvEvent (接收訊息事件)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //Robot Received Data Event
        private void axMelfaRxM1_MsgRecvEvent(object sender, EventArgs e)
        {
            int lReqID = 0;
            string sRecvData = "";
            int lStatus = 0;
            int lError = 0;
            int lRobotID = 0;
            try
            {
                int lState = axMelfaRxM1.GetRecvDataM(ref lRobotID, ref lReqID, ref sRecvData, ref lStatus, ref lError);

                if (lStatus != 1)
                {
                    //IsRobotCommOK = false;
                    string s1 = "E" + lReqID.ToString() + "-" + lStatus.ToString() + "-" + lError.ToString() + " " + sRecvData;
                    //cLog.WriteLog((int)LOGTYPE.ALERT, s1);                    
                    //AddInfoInvoke.Invoke((int)LOGTYPE.ALERT, "Robot", s1);
                    //TB_ConnErrorMSG.Text = s1;
                    Console.WriteLine(s1);
                    return;
                }
                else
                {
                    //IsRobotCommOK = true;
                    int lCnt = axMelfaRxM1.GetDataCnt(sRecvData);  //'data number
                    int lEleNo;
                    int lNum;
                    string sData;
                    string sOneData;
                    switch (lReqID)
                    {
                        case 100:
                            sData = "";
                            for (int i = 0; i < lCnt; i++) //'The data number loops
                            {
                                sOneData = axMelfaRxM1.GetOneData(i, sRecvData); //'one data get
                                sData += sOneData; //+ "\r\n"; //'add <CR>  <LF>
                            }
                            //* txtProgramEdit.Text = sData;
                            break;
                        case 104: //upload prog
                            //AddInfoInvoke.Invoke((int)LOGTYPE.COMM, "Robot", "(Info) Upload Program Finish");
                            break;
                        case 106:
                            sData = axMelfaRxM1.GetOneData(0, sRecvData);
                            lNum = Convert.ToInt32(sData);
                            if (lCnt == (lNum * 12 + 1))
                            {
                                lEleNo = 1;
                                for (int i = 0; i <= (lNum - 1); i++)
                                {
                                    sData = axMelfaRxM1.GetOneData(lEleNo, sRecvData); //'Program name acquisition                            
                                    sData = sData.Remove(sData.Length - 4); //". MB4" is deleted. 
                                    listBox2.Items.Add((sData)); //'It adds it to the list.
                                    lEleNo += 12; //The following program
                                }
                            }
                            else
                            {
                                //* IsRobotCommOK = false;
                                //string s1 = "(E" + lReqID.ToString() + " )The received data number is illegal. ";
                                //AddInfoInvoke.Invoke((int)LOGTYPE.ALERT, "Robot", s1);
                                return;
                            }
                            break;
                        case 128: //write positions 
                            //AddInfoInvoke.Invoke((int)LOGTYPE.COMM, "Robot", "(Info) Upload Positions Finish");
                            break;
                        case 200:

                            /*
                            if (IsRobotCommOK == false) return;

                            sData = axMelfaRxM1.GetOneData(0, sRecvData);
                            string[] tmp200 = sRecvData.Split('\n');

                            if (tmp200.Length < 9) return;

                            lbRbt1.Text = tmp200[2];
                            lbRbt2.Text = tmp200[5];
                            lbRbt3.Text = tmp200[6];
                            lbRbt4.Text = tmp200[9];
                            int iVal = Convert.ToInt32(tmp200[9], 16);
                            int Idx = 1;
                            lbRbt5.Text = (iVal & (Idx << 0)).ToString();     //During emergency stop
                            lbRbt6.Text = (iVal & (Idx << 1)).ToString();   //Paused or stopped
                            lbRbt7.Text = (iVal & (Idx << 2)).ToString();   //Paused
                            lbRbt8.Text = (iVal & (Idx << 8)).ToString();   //Cycle/continuous operation
                            lbRbt9.Text = (iVal & (Idx << 9)).ToString();   //Cycle being operated/stopped
                            lbRbt10.Text = (iVal & (Idx << 11)).ToString(); //AUTO/TEACH mode
                            lbRbt11.Text = (iVal & (Idx << 12)).ToString(); //A step operation or jog operation is being executed
                            lbRbt12.Text = (iVal & (Idx << 13)).ToString(); //Servo on/off status
                            lbRbt13.Text = (iVal & (Idx << 14)).ToString(); //Stopped/operating
                            lbRbt14.Text = (iVal & (Idx << 15)).ToString(); // Invalid/valid operation right

                            IsRobotRunning = (iVal & (Idx << 14)) > 0;
                            IsRobotProgExec = (iVal & (Idx << 1)) <= 0;

                            lbRbtRunning.Text = IsRobotRunning ? "Rbt Running" : "Rbt Idle";
                            //IsRobotStatusRefresh = true;
                            */

                            break;
                        case 203:
                            sData = axMelfaRxM1.GetOneData(0, sRecvData);
                            lNum = Convert.ToInt32(sData);
                            if (lCnt == (lNum * 8 + 1))
                            {
                                lEleNo = 1;
                                for (int i = 0; i < lNum; i++)
                                {
                                    sData = axMelfaRxM1.GetOneData(lEleNo, sRecvData); //'Error #
                                    sData = sData + "\n" + "\n" + ":" + axMelfaRxM1.GetOneData(lEleNo + 1, sRecvData);
                                    lEleNo += 7; //'Seven data reading position is advanced.
                                }
                                //* txtErrNo.Text = sData;
                            }
                            else
                            {
                                //IsRobotCommOK = false;
                                string s1 = $"(E{lReqID})The received data number is illegal. ";
                                //cLog.WriteLog((int)LOGTYPE.ALERT, s1);
                                //AddInfoInvoke.Invoke((int)LOGTYPE.ALERT, "Robot", s1);
                                //MessageBox.Show(s1, "Error", MessageBoxButtons.OK);
                                return;
                            }
                            break;
                        case 214:
                            sData = axMelfaRxM1.GetOneData(0, sRecvData);
                            lNum = Convert.ToInt32(sData);
                            //* lbInput.Text = sRecvData;
                            string[] tmp214 = sRecvData.Split('\n');
                            //* sDI = tmp214[2];
                            //* sDO = tmp214[5];

                            for (int i = 15; i >= 0; i--)
                            {
                                //* int iVal214 = Convert.ToInt16(sDO.Substring((15 - i) / 4, 1));
                                //* bDO[i] = (iVal214 & 1 << (i % 4)) > 0;
                            }
                            break;
                        case 235:
                            if (lCnt == 24)
                            {
                                lEleNo = 2;
                                string sType = axMelfaRxM1.GetOneData(lEleNo, sRecvData);
                                lEleNo = 16;
                                sData = "";
                                for (int i = 0; i < 8; i++)
                                {
                                    if (sType == "0")
                                    {
                                        //* sPosJoint[i] = axMelfaRxM1.GetOneData(lEleNo + i, sRecvData);
                                    }
                                    else
                                    {
                                        //* sPosOrthogonal[i] = axMelfaRxM1.GetOneData(lEleNo + i, sRecvData);
                                    }
                                    sData = sData + axMelfaRxM1.GetOneData(lEleNo + i, sRecvData) + " , ";
                                }
                                sData.Remove(sData.Length - 3); //'The last ','is deleted.
                                                                //txtCurrPos.Text = sData;
                                                                //* ShowPosition();
                            }
                            else
                            {
                                //IsRobotCommOK = false;
                                string s1 = $"(E{lReqID} )The received data number is illegal. ";
                                //AddInfoInvoke.Invoke((int)LOGTYPE.ALERT, "Robot", s1);
                                return;
                            }
                            break;
                        case 242: // Get slot status.
                            //* if (IsRobotCommOK == false) return;

                            sData = axMelfaRxM1.GetOneData(0, sRecvData);
                            string[] tmp242 = sRecvData.Split('\n');
                            //Slot No
                            //Program Name
                            //Execution Line No.
                            //Override
                            //Slot Status
                            //Operation Status
                            //Error Numbers

                            if (tmp242.Length < 7) return;

                            //* lbStateSlotNo.Text = tmp242[0];
                            //* lbStateProgName.Text = tmp242[1];
                            //* lbStateExecLine.Text = tmp242[2];
                            //* lbStateOverride.Text = tmp242[3];
                            //* lbStateSlotStatus.Text = tmp242[4];
                            //* lbStateOpStatus.Text = tmp242[5];
                            //* lbStateErrorNo.Text = tmp242[6];

                            //* if (tmp242[1].Length > 4) RobotCurrentProgName = tmp242[1].Substring(0, tmp242[1].Length - 4);
                            //* RobotCurrentExecLine = Convert.ToInt16(tmp242[2]);
                            //* RobotCurrentOverride = Convert.ToInt16(tmp242[3]);
                            //* RobotCurrentErrorNo = tmp242[6];

                            //* lbRbtProgName.Text = RobotCurrentProgName;

                            int iOpStatus = Convert.ToInt32(tmp242[5], 16);
                            //int iOpStatus = Convert.ToInt16( tmp2[5]);
                            int[] intArray = new int[16];
                            for (int i = 0; i < 16; i++)
                            {
                                intArray[i] = ((iOpStatus & (1 << i)) > 0) ? 1 : 0;
                                //* dgvStausOP.Rows[i].Cells[1].Value = intArray[i].ToString();
                            }

                            //IsRobotRunning = iV > 0 ? true : false;
                            //IsRobotProgExec = iV > 0 ? false : true;
                            //lbRbtRunning.Text = (IsRobotRunning == true) ? "Rbt Running" : "Rbt Idle";
                            //IsRobotStatusRefresh = true;

                            break;
                        case 400: // Exec Prog
                            //AddInfoInvoke.Invoke((int)LOGTYPE.COMM, "Robot", "(Info) Command Robot Strat Process Finish");
                            break;
                        case 123:
                            break;
                        default:
                            //* txtRecvID.Text = lReqID.ToString();
                            //* txtRecvStatus.Text = lStatus.ToString();
                            //* txtRecvError.Text = lError.ToString();
                            sData = "";
                            lCnt = axMelfaRxM1.GetDataCnt(sRecvData); //'data number
                            for (int i = 0; i < lCnt; i++)
                            {               //'The data number loops.
                                sOneData = axMelfaRxM1.GetOneData(i, sRecvData); //'one data get
                                sData = sData + sOneData + "\r\n"; //'add <CR>  <LF>
                            }
                            //* txtRecvData.Text = sData;
                            break;
                    }

                    ///Slot number<LF> 
                    ///Slot status<LF> 
                    ///Robot program name<LF> 
                    ///Not used (0 is set.) <LF> 
                    ///Connected mechanism number<LF> 
                    ///Start condition<LF>Operation mode<LF> 
                    ///Task priority order<LF> 
                    ///Controlled mechanism number<LF> 
                    ///Operation status 
                    ///--------------------
                    ///(1) Specify the appropriate number (the number of slots + 1) for the slot number in order to send the request to the editing slot. 
                    ///See 8.1, "About Task Slots" for the detailed explanation of slots. 
                    ///(2) The slot status information is a numerical data value, 
                    ///where a decimal number is used to represent the binary data. 
                    ///The following information is set for each of the bits (1: On, 0: Off). 
                    ///The least significant bit is bit 0 below. Bit 0: A program is opened in the editing slot Bit 1: Operating Bit 2: 
                    ///There is a change (set when the data in a program or the value of a variable in a program is changed.) 
                    ///(3) Start condition: REP: Continuous CYC: One cycle 
                    ///(4) Operation mode: START: Normal ERROR: At error occurrence ALWAYS: Always 
                    ///(5) Specify a value in the range from 1 to 31 for the task priority order. 
                    ///This number indicates the number of lines executed each time the task runs. 
                    ///It has the same meaning as the number of executed lines in the PRIORITY instruction of MELFA-BASIC IV. 
                    ///(6) The operation status information is a numerical data value, where a hexadecimal number is used to represent the binary data. 
                    ///The following values are set for each of the bits (1: On, 0: Off). 
                    ///The least significant bit is bit 0 below. 
                    ///Bit 0: During emergency stop                 ///Bit 1: Paused or stopped *1 
                    ///Bit 2: Paused                 ///Bit 3: The stop signal is turned on 
                    ///Bit 4: Status where a program can be selected *2                 ///Bit 5: Not used 
                    ///Bit 6: Not used                 ///Bit 7: Not used 
                    ///Bit 8: Cycle/continuous operation (0/1)                 ///Bit 9: Cycle being operated/stopped (0/1)
                    ///Bit 10: Machine lock off/on (0/1)                 ///Bit 11: AUTO/TEACH mode (0/1) *3 
                    ///Bit 12: A step operation or jog operation is being executed                 ///Bit 13: Servo on/off status (0/1) 
                    ///Bit 14: Stopped/operating (0/1)                 ///Bit 15: Invalid/valid operation right (0/1) 
                    ///*1  1 is set if a program is not executed. 
                    ///*2  Status where a program is not being executed. A program can be selected and executed from the start. 
                    ///*3  Status of the key switch. The bit is set to 0 if the key status is AUTO (OP or EXT) and 1 if the key status is TEACH. 
                    ///
                }
            }
            catch
            {
                //IsRobotCommOK = false;
                //return;
            }
        }


        /// <summary>
        /// 寫入回原點程式
        /// </summary>
        /// <returns></returns>
        public bool RobotUpdateORGProgram()
        {
            int iStep = 1;
            string sPrgName = "ORGC" + "\n";// "REFORG" + "\n";
            string sCmdLines = "8\n";
            string sDefPos = iStep++.ToString() + " Def Pos REFPOS" + "\n";
            //string sPosRef = iStep++.ToString() + " REFPOS = (+0.00,+0.00,+0.00,+0.00,+0.00,+0.00)(7,0)" + "\n";
            string sPosRef = iStep++.ToString() + " REFPOS = (+0.00,+0.00,-200.00,+0.00,+0.00,+0.00)(7,0)" + "\n";
            string sPrgBase = iStep++.ToString() + " Base 1\n";
            string sPrgTool = iStep++.ToString() + " Tool 2\n";
            string sPrgServoOn = iStep++.ToString() + " SERVO ON\n";
            string sSPD = iStep++.ToString() + " Ovrd 10\n";
            string sMvs = iStep++.ToString() + " Mov REFPOS" + "\n";
            string sPrgHlt = iStep++.ToString() + " Hlt";

            //string s1 = sPrgName + sCmdLines + sDefPos + sPosRef + sPrgServoOn + sSPD + sMvs + sPrgHlt;
            string s1 = sPrgName + sCmdLines + sDefPos + sPosRef + sPrgBase + sPrgTool + sPrgServoOn + sSPD + sMvs;
            bool bRet = RobotProgramWrite(s1, "");
            return bRet;
        }


        /// <summary>
        /// Robot Program Write
        /// </summary>
        /// <param name="sProgram"></param>
        /// <param name="sPositionData"></param>
        /// <returns></returns>
        private bool RobotProgramWrite(string sProgram, string sPositionData)
        {
            int lStatus = 0;

            //'Servo on transmission
            lStatus = axMelfaRxM1.RequestServiceM(lRobotID, 104, sProgram.Length, sProgram, 0, 0, 0);
            if (lStatus != 1)
            {
                sErrMsg = "Write Robot Program Error!";
                //cLog.WriteLog((int)LOGTYPE.ALERT, sErrMsg);
                //AddInfoInvoke.Invoke((int)LOGTYPE.ALERT, "Robot", sErrMsg);
                //MessageBox.Show("Write Program error");
                return false;
            }
            SpinWait.SpinUntil(() => false, 10);
            if (sPositionData.Length > 0)
            {
                lStatus = axMelfaRxM1.RequestServiceM(lRobotID, 128, sPositionData.Length, sPositionData, 0, 0, 0);
                if (lStatus != 1)
                {
                    sErrMsg = "Send Position Data error";
                    //cLog.WriteLog((int)LOGTYPE.ALERT, sErrMsg);
                    //AddInfoInvoke.Invoke((int)LOGTYPE.ALERT, "Robot", sErrMsg);
                    //MessageBox.Show("Send Position Data error");
                    return false;
                }
            }
            Console.WriteLine("Write Robot Success");
            return true;
        }

        /// <summary>
        /// Robot Program Read
        /// </summary>
        /// <param name="sProgramName"></param>
        /// <returns></returns>
        private bool RobotProgramRead(string sProgramName)
        {
            string sSendData = "";
            int lStatus = 0;

            sSendData = sProgramName;
            //'Servo on transmission
            lStatus = axMelfaRxM1.RequestServiceM(lRobotID, 100, sSendData.Length, sSendData, 0, 0, 0);
            if (lStatus != 1)
            {
                sErrMsg = "Read Robot Program Error!";
                //cLog.WriteLog((int)LOGTYPE.ALERT, sErrMsg);
                //AddInfoInvoke.Invoke((int)LOGTYPE.ALERT, "Robot", sErrMsg);
                //MessageBox.Show("Read Program error");
                return false;
            }
            return true;
        }

        /// <summary>
        /// Robot Program Refresh
        /// </summary>
        /// <returns></returns>
        private bool RobotProgramRefresh()
        {
            lStatus = axMelfaRxM1.RequestServiceM(lRobotID, 106, 0, string.Empty, 1, 0, 0);
            if (lStatus != 1)
            {
                string msg = "(E1601) Robot Program list request transmission Error!";
                Console.WriteLine(msg);
                //cLog.WriteLog((int)LOGTYPE.ALERT, s1);
                //AddInfoInvoke.Invoke((int)LOGTYPE.ALERT, "Robot", s1);
                //MessageBox.Show("Send error");
                return false;
            }
            return true;
        }


        /// <summary>
        /// 編寫Robot Program
        /// </summary>
        public void RobotCompileProgram()
        {

            textBox1.Clear();
            int step = 1;
            string PathProgram = "";
            string Prog_Name = textBox3.Text + "\n";
            //string Prog_Name = textBox3.Text + "\n";
            string Prog_ServoOn = "SERVO ON";
            string Prog_CNT = "Cnt 1";
            string Prog_Work = "Base 1";
            string Prog_Tool = "Tool 2";
            string Prog_speed = "spd 100";
            string Prog_end = "Hlt";
            string Prog_ORG = "CallP \"ORGC\"";
            // string sLines = Convert.ToString(dataGridView1.RowCount + 4) + "\n";
            //string sLines = Convert.ToString(dataGridView1.RowCount + 1) + "\n";
            string sLines = "";
            string Prog_Control = "";
            string Prog_PositionData = "";


            try
            {

                Prog_Control += step++.ToString() + Prog_CNT + "\n";
                Prog_Control += step++.ToString() + Prog_Work + "\n";
                Prog_Control += step++.ToString() + Prog_Tool + "\n";
                Prog_Control += step++.ToString() + Prog_speed + "\n";
                Prog_Control += step++.ToString() + Prog_ORG + "\n";

                for (int i = 1; i < dataGridView1.RowCount; i++)
                {
                    Prog_PositionData += step++.ToString() + AddRobotProgram(i);
                }

                string nProg_ORG = step++.ToString() + Prog_ORG + "\n";
                string nProg_end = step++.ToString() + Prog_end + "\n";


                sLines = step.ToString() + "\n";
                PathProgram = Prog_Name + sLines + Prog_Control + Prog_PositionData + nProg_ORG + nProg_end;


                textBox1.Text = PathProgram;
            }
            catch { MessageBox.Show("system error"); }


        }


        public void RobotCompilePointData()
        {
            textBox2.Clear();
            int step = 1;
            string PathPointData = "";
            string Prog_Name = textBox3.Text + "\n";
            string Prog_ServoOn = "SERVO ON\n";
            string sLines = Convert.ToString(dataGridView1.RowCount) + "\n";
            PathPointData = Prog_Name + sLines;

            try
            {
                for (int i = 1; i < dataGridView1.RowCount; i++)
                {
                    PathPointData += AddPositionData(i);
                }
                textBox2.Text = PathPointData;

            }
            catch { MessageBox.Show("system error"); }

        }

        /// <summary>
        /// Robot程式觸發
        /// </summary>
        /// <param name="prgName"></param>
        public void RobotProgramStart(string prgName)
        {
            var Data = $"1\n{prgName}\n1";
            //Program execution
            int lStatus = axMelfaRxM1.RequestServiceM(lRobotID, 400, Data.Length, Data, 0, 0, 0);
            if (lStatus != 1)
            {
                string s1 = "(E1602) Robot Program Start Error!";
                Console.WriteLine(s1);
            }
        }
        /// <summary>
        /// Robot程式觸發(Cycle)
        /// </summary>
        /// <param name="prgName"></param>
        public void RobotProgramStartCycle(string prgName)
        {
            var Data = $"1\n{prgName}\n0";
            //Program execution
            int lStatus = axMelfaRxM1.RequestServiceM(lRobotID, 400, Data.Length, Data, 0, 0, 0);
            if (lStatus != 1)
            {
                string s1 = "(E1603) Robot Program Start Cycle Error!";
                Console.WriteLine(s1);
            }
        }

        /// <summary>
        /// Robot 程式暫停
        /// </summary>
        /// <returns></returns>
        public bool RobotProgramStopCycle()
        {
            string sSendData = "0";
            //Stop
            int lStatus = axMelfaRxM1.RequestServiceM(lRobotID, 401, sSendData.Length, sSendData, 0, 0, 0);
            if (lStatus != 1)
            {
                sErrMsg = "Robot Command 406 Send Stop Cycle Error!";

                return false;
            }
            return true;
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
        private string _X;
        private string _Y;
        private string _Z;
        private string _A;
        private string _B;
        private string _C;

        public string Name
        {
            get { return _Name; }
            set
            {
                _Name = value;
                NotifyPropertyChanged(nameof(Name));
            }
        }

        public string X
        {
            get { return _X; }
            set
            {
                _X = value;
                NotifyPropertyChanged(nameof(X));
            }
        }

        public string Y
        {
            get { return _Y; }
            set
            {
                _Y = value;
                NotifyPropertyChanged(nameof(Y));
            }
        }

        public string Z
        {
            get { return _Z; }
            set
            {
                _Z = value;
                NotifyPropertyChanged(nameof(Z));
            }
        }

        public string A
        {
            get { return _A; }
            set
            {
                _A = value;
                NotifyPropertyChanged(nameof(A));
            }
        }

        public string B
        {
            get { return _B; }
            set
            {
                _B = value;
                NotifyPropertyChanged(nameof(B));
            }
        }


        public string C
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
