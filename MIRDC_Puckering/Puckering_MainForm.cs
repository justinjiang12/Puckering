using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading; //需匯入System.Threading
using MIRDC_Puckering.Robotcontrol;
using DIO;
using MxAutomation_Example;
using MIRDC_Puckering.OtherProgram;
using FesIF_Demo;




namespace MIRDC_Puckering
{
    public partial class Puckering_MainForm : Form
    {
        #region 欄位宣告

        string pageState = "HomePage";
        int eventnum=1;

        /// <summary>
        /// 實作執行續控制
        /// </summary>
        private ThreadControl thr_control = new ThreadControl();


        public static MitusbiahiRobotForm F_MRC = new MitusbiahiRobotForm();
        public static IOForm F_IO = new IOForm();
        public static MainView F_KRC = new MainView();
        public static LoginForm F_LOGIN = new LoginForm();
        public static YaskawRobot F_YRC = new YaskawRobot();

        #endregion

        /// <summary>
        /// 建構子
        /// </summary>
        public Puckering_MainForm()
        {
            InitializeComponent();
        }
         
        #region 視窗管理
        /// <summary>
        /// 視窗建立
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Load(object sender, EventArgs e)
        {
            ISystem_EventLoad();
            Thread_EventLoad();
            IPermission_EventLoad();
        }

        private void ISystem_EventLoad()
        {
            ISystem.OnSysModelChanging += ChangeSysModelState;
            ISystem.OnSysControlChanging += ChangeSysControlState;
            //委派方法
            //thr_control.L_GrabRobot.OnChangeLoopStep += ChangeGLoopStep;
            ISystem.Model_State = SysModel.Manual;
        }

        private void IPermission_EventLoad()
        {
            IPermission.OnSysLevelChanging += ChangePermissionLevel;
            //委派方法
            IPermission.Permission_Level = PermissionList.Level_0_Guest;
        }

        private void Thread_EventLoad()
        {
            thr_control.L_GrabRobot.e_Signal_output += GrabRobot_output;
        }

        private void GrabRobot_output(ushort num,bool state)
        {
            F_IO.DO_Write(num,state);
        }

        /// <summary>
        /// 視窗關閉
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            
            DialogResult dr = MessageBox.Show("確定要關閉程式嗎?",
            "Closing event!", MessageBoxButtons.YesNo);
            if (dr == DialogResult.No)
            {
                e.Cancel = true;//取消離開
            }
            else
            {
                thr_control.End_thread();
                e.Cancel = false;//確認離開

            }
            

        }
        #endregion

        #region 相關事件觸發方法

        /// <summary>
        /// 當系統狀態改變事件觸發
        /// </summary>
        /// <param name="State"></param>
        private void ChangeSysModelState(SysModel State)
        {
            label2.Text = "SysModel : "+State.ToString();
            CheckSysModel(State);

        }

        private void ChangePermissionLevel (PermissionList Level)
        {
            string _num = "***";
            if (Level== PermissionList.Level_0_Guest) { _num = "Guest"; }
            if (Level == PermissionList.Level_1_Operator) { _num = "Operator"; }
            if (Level == PermissionList.Level_2_Engineer) { _num = "Engineer"; }
            if (Level == PermissionList.Level_3_SeniorEngineer) { _num = "SeniorEngineer"; }
            if (Level == PermissionList.Level_10_Designer) { _num = "Designer"; }


            label9.Text = "Permission : " + _num;
            CheckPermission(Level);

        }



        /// <summary>
        /// 當系統控制狀態改變事件觸發
        /// </summary>
        /// <param name="State"></param>
        private void ChangeSysControlState(SysControl State)
        {

            CheckSysControl(State);

        }

        /*
         //委派方法
        private void ChangeGLoopStep()
        {

          //  label3.Text = "GrabRobot_Step : " + GrabRobotLoop.GR_StepNum;

        }
        */

        #endregion

        #region 系統狀態執行辦法
        /// <summary>
        /// 確認目前系統模式並處理之
        /// </summary>
        /// <param name="State"></param>
        private void CheckSysModel(SysModel State)
        {
            switch (State)
            {
                case SysModel.System_PowerOn:
                    SysPowerOn();
                    break;

                case SysModel.Initial_Run:
                    SysInitial();
                    break;

                case SysModel.Manual:
                    SysManual();
                    break;

                case SysModel.Prepare_Run:
                    SysPrepare();
                    break;

                case SysModel.Auto:
                    SysAuto();
                    break;

                case SysModel.AtoM_Run:
                    SysAutoToManual();
                    break;

            }
        }


        /// <summary>
        /// 確認目前系統模式並處理之
        /// </summary>
        /// <param name="State"></param>
        private void CheckSysControl(SysControl State)
        {
            switch (State)
            {
                case SysControl.Auto_Start:
                    autoLoopStart();
                    break;

                case SysControl.Auto_Stop:
                    autoLoopStop();
                    break;

            }
        }

        /// <summary>
        /// 確認目前權限並處理之
        /// </summary>
        /// <param name="State"></param>
        private void CheckPermission(PermissionList Level)
        {
            switch (Level)
            {
                case PermissionList.Level_0_Guest:


                    break;

                case PermissionList.Level_1_Operator:


                    break;

                case PermissionList.Level_2_Engineer:


                    break;

                case PermissionList.Level_3_SeniorEngineer:


                    break;

                case PermissionList.Level_10_Designer:


                    break;
            }
        }

        #endregion

        #region 系統控制方法巨集

        /// <summary>
        /// 系統狀態執行方法(PowerOn)
        /// </summary>
        private void SysPowerOn()
        {

        }
        /// <summary>
        /// 系統狀態執行方法(Initial)
        /// </summary>
        private void SysInitial()
        {

        }
        /// <summary>
        /// 系統狀態執行方法(Prepare)
        /// </summary>
        private void SysPrepare()
        {

        }
        /// <summary>
        /// 系統狀態執行方法(AutoToManual)
        /// </summary>
        private void SysAutoToManual()
        {

        }
        /// <summary>
        /// 系統狀態執行方法(Manual)
        /// </summary>
        private void SysManual()
        {
            btn_Auto.Enabled = true;

            btn_Manual.Enabled = false;
            btn_Start.Enabled = false;
            btn_Start.BackColor = Color.DarkGreen;
            btn_Stop.Enabled = false;
            btn_Stop.BackColor = Color.DarkRed;
            thr_control.End_thread();
        }

        /// <summary>
        /// 系統狀態執行方法(Auto)
        /// </summary>
        private void SysAuto()
        {
            btn_Auto.Enabled = false;
            btn_Manual.Enabled = true;
            btn_Start.Enabled = true;
            btn_Start.BackColor = Color.LimeGreen;
            btn_Stop.Enabled = false;
            btn_Stop.BackColor = Color.DarkRed;
            thr_control.Run_thread();
        }


        /// <summary>
        /// 自動流程啟動
        /// </summary>
        private void autoLoopStart()
        {
            thr_control.L_GrabRobot.GR_loopStop = false;
            thr_control.L_PushRobot.PR_loopStop = false;
            thr_control.L_Vision.VI_loopStop = false;
        }

        /// <summary>
        /// 自動流程暫停
        /// </summary>
        private void autoLoopStop()
        {
            thr_control.L_GrabRobot.GR_loopStop = true;
            thr_control.L_PushRobot.PR_loopStop = true;
            thr_control.L_Vision.VI_loopStop = true;
        }



        #endregion


        #region 控制物件管理
        /// <summary>
        /// 按鈕管理巨集
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            string tag = (string)btn.Tag;
            try {

                switch (tag)
                {
                    case "HomePage":

                        
                        break;

                    case "VisionPage":


                        break;

                    case "MitubiahiPage":

                        ShowForm(F_MRC);
                        pageState = "Mitusbishi_Page";

                        break;

                    case "KukaPage":
                        
                        ShowForm(F_KRC);
                        pageState = "KUKA_Page";
                        break;

                    case "YaskawaPage":

                        ShowForm(F_YRC);
                        pageState = "Yaskawa_Page";
                        break;

                        


                    case "IOPage":
                        ShowForm(F_IO);
                        pageState = "IO_Page";
                        break;


                    case "FormClose":

                        panel1.Controls.Remove(F_MRC);

                        break;





                    case "FormClose_clear":

                        panel1.Controls.Clear();

                        break;

                    case "btn_esc":

                        this.Close();

                        break;


                    case "btn_Login":

                        F_LOGIN.Show();

                        break;


                    case "btn_Manual":

                        DialogResult dr1 = MessageBox.Show("確定要手動模式嗎?","Closing event!", MessageBoxButtons.YesNo);
                        if (dr1 == DialogResult.Yes)
                        {
                            ISystem.Model_State = SysModel.Manual;
                        }


                        break;


                    case "btn_Auto":

                        DialogResult dr2 = MessageBox.Show("確定要自動模式嗎?", "Closing event!", MessageBoxButtons.YesNo);
                        if (dr2 == DialogResult.Yes)
                        {
                            ISystem.Model_State = SysModel.Auto;
                        }

                        break;


                    case "btn_Start":

                        ISystem.Control_State = SysControl.Auto_Start;
                        btn_Start.Enabled = false;
                        btn_Start.BackColor = Color.DarkGreen;
                        btn_Stop.Enabled = true;
                        btn_Stop.BackColor = Color.Red;
                        break;

                    case "btn_Stop":

                        ISystem.Control_State = SysControl.Auto_Stop;
                        btn_Stop.Enabled = false;
                        btn_Start.BackColor = Color.LimeGreen;
                        btn_Start.Enabled = true;
                        btn_Stop.BackColor = Color.DarkRed;
                        break;


                }
            }
            catch(Exception x) { MessageBox.Show(x.ToString(),"systen error!!!"); }
        }
        #endregion


        /// <summary>
        /// 關閉子分頁
        /// </summary>
        /// <param name="pageName"></param>
        public void RemovePage(string pageName)
        {
            if (pageName == "Mitusbishi_Page") { panel1.Controls.Remove(F_MRC); }
            if (pageName == "IO_Page") { panel1.Controls.Remove(F_IO); }
            if (pageName == "KUKA_Page") { panel1.Controls.Remove(F_KRC); }
            if (pageName == "Yaskawa_Page") { panel1.Controls.Remove(F_YRC); }
        }

        /// <summary>
        /// 插入子分頁
        /// </summary>
        /// <param name="frm"></param>
        public void ShowForm(Form frm)
        {

            #region 範例
            /*  //魏展範例
            frm.MdiParent = this;
            frm.FormBorderStyle = FormBorderStyle.None;
            frm.ControlBox = false;
            frm.Dock = DockStyle.Fill;
            ////frmYaskawa.WindowState = FormWindowState.Maximized;
            ////frm.TopMost = true;
            frm.BringToFront();
            frm.Show();
            */
            #endregion
            RemovePage(pageState);
            frm.MdiParent = this;//指定當前窗體為頂級Mdi窗體
            frm.TopLevel = false;
            frm.Dock = DockStyle.Fill;            
            frm.FormBorderStyle = FormBorderStyle.None;//隱藏子窗體邊框，當然也可以在子窗體的窗體載入事件中實現
            frm.Parent = panel1;//指定子窗體的父容器為
            frm.Show();

        }

        private void thr_timer_Tick(object sender, EventArgs e)
        {

            label3.Text = "GrabRobot_Step : " + GrabRobotLoop.GR_StepNum;
            label4.Text = "PushRobot_Step : " + PushRobotLoop.PR_StepNum;
            label5.Text = "Vision_Step : " + VisionLoop.VI_StepNum;

        }


    }

}
