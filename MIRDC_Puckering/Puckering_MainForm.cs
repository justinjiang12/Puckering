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
using MIRDC_Puckering.OtherProgram;




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


        public MitusbiahiRobotForm F_MRC = new MitusbiahiRobotForm();
        public ThreadTest F_ThrT = new ThreadTest();


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
            F_ThrT.LoopState += testfuntion;
            ISystem.OnSysModelChanging += ChangeSysModelState;
            ISystem.OnSysControlChanging += ChangeSysControlState;
            //委派方法
            //thr_control.L_GrabRobot.OnChangeLoopStep += ChangeGLoopStep;
            ISystem.Model_State = SysModel.Manual;

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
                F_ThrT.Form1_FormClosing(sender, e); //關閉ThreadTest_Form
                thr_control.End_thread();
                e.Cancel = false;//確認離開

            }
            

        }
        #endregion

        #region 相關事件觸發方法
        /// <summary>
        /// 事件觸發方法
        /// </summary>
        private void testfuntion()
        {
            eventnum += eventnum;
            label1.Text = eventnum.ToString();
        }

        /// <summary>
        /// 當系統狀態改變事件觸發
        /// </summary>
        /// <param name="State"></param>
        private void ChangeSysModelState(SysModel State)
        {
            label2.Text = "SysModel : "+State.ToString();
            CheckSysModel(State);

        }



        /// <summary>
        /// 當系統控制狀態改變事件觸發
        /// </summary>
        /// <param name="State"></param>
        private void ChangeSysControlState(SysControl State)
        {

            CheckSysControl(State);

        }


        /* //委派方法
        private void ChangeGLoopStep(string step)
        {

            label3.Text = "GrabRobot_Step : " + step;

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
            btn_Stop.Enabled = false;
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
            btn_Stop.Enabled = false;
            thr_control.Run_thread();
        }


        /// <summary>
        /// 自動流程啟動
        /// </summary>
        private void autoLoopStart()
        {
            thr_control.L_GrabRobot.loopStop = false;
            thr_control.L_PushRobot.loopStop = false;
            thr_control.L_Vision.loopStop = false;
        }

        /// <summary>
        /// 自動流程暫停
        /// </summary>
        private void autoLoopStop()
        {
            thr_control.L_GrabRobot.loopStop = true;
            thr_control.L_PushRobot.loopStop = true;
            thr_control.L_Vision.loopStop = true;
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
                    
                    case "FormLoad":

                        ShowForm(F_MRC);
                        pageState = "Mitusbishi_Page";
                        break;


                    case "FormClose":

                        panel1.Controls.Remove(F_MRC);
                        break;

                    case "FormLoad_1":

                        ShowForm(F_ThrT);
                        pageState = "Thread_Page";
                        break;


                    case "FormClose_1":
                            
                        panel1.Controls.Remove(F_ThrT);
                        
                        break;


                    case "FormClose_clear":

                        panel1.Controls.Clear();

                        break;

                    case "btn_esc":

                        this.Close();

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
                        btn_Stop.Enabled = true;
                        break;

                    case "btn_Stop":

                        ISystem.Control_State = SysControl.Auto_Stop;
                        btn_Stop.Enabled = false;
                        btn_Start.Enabled = true;
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
            if (pageName == "Thread_Page") { panel1.Controls.Remove(F_ThrT); }

        }

        /// <summary>
        /// 插入子分頁
        /// </summary>
        /// <param name="frm"></param>
        public void ShowForm(Form frm)
        {
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

            label3.Text = "GrabRobot_Step : " + thr_control.L_GrabRobot.num;
            label4.Text = "PushRobot_Step : " + thr_control.L_PushRobot.num;
            label5.Text = "Vision_Step : " + thr_control.L_Vision.num;

        }
    }


}
