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

namespace MIRDC_Puckering.OtherProgram
{
    /// <summary>
    /// 委派宣告
    /// </summary>
    /// <param name="State"></param>
    public delegate void LoopState();

    public partial class ThreadTest : Form
    {

        #region 欄位宣告
        //實作function類別之物件
        private function fun_mirdc = new function();
        //宣告main_MIRDC_testLoop 欄位(thread)
        private Thread main_MIRDC_testLoop;
        private bool state_MIRDC_testLoop = false;

        //宣告事件(對外)
        public event LoopState LoopState;


        #endregion


        public ThreadTest()
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
            //按鍵狀態管理
            button1.Enabled = false;
            button2.Enabled = false;
            button3.Enabled = true;
            button4.Enabled = true;
            button5.Enabled = true;
            //建立並觸發執行緒
            state_MIRDC_testLoop = creat_thread();
           
        }
        /// <summary>
        /// 視窗關閉
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            //關閉執行緒
            end_thread();
        }
        #endregion

        #region 執行緒管理
        /// <summary>
        /// 建立執行緒
        /// </summary>
        private bool creat_thread()
        {
            try
            {
                //實作執行緒thr_mirdc
                Thread thr_mirdc = new Thread(fun_mirdc.LoopRun);
                //將實作之執行緒傳遞於main_MIRDC_testLoop欄位
                main_MIRDC_testLoop = thr_mirdc;
                //啟動main_MIRDC_testLoop執行緒
                main_MIRDC_testLoop.Start();
                return true;
            }
            catch(Exception x) 
            {
                MessageBox.Show(x.ToString(), "systen error!!!");
                return false;
            }

        }
        /// <summary>
        /// 關閉執行緒
        /// </summary>
        private void end_thread()
        {
            try
            {
                if (state_MIRDC_testLoop)
                {
                    //暫停執行緒旗標
                    fun_mirdc.loopStop = true;
                    //關閉執行緒
                    main_MIRDC_testLoop.Abort();
                    //確認關閉執行緒動作

                    while (main_MIRDC_testLoop.ThreadState != ThreadState.Aborted)
                    {
                        //當調用Abort方法後，如果thread線程的狀態不為Aborted，主線程就一直在這裡做迴圈，直到thread線程的狀態變為Aborted為止
                        Thread.Sleep(100);
                    }
                }
            }
            catch { MessageBox.Show("sys error!!"); }
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
            try
            {

                switch (tag)
                {
                    case "Loop Open":

                        creat_thread();
                        if (main_MIRDC_testLoop.IsAlive)
                        {
                            button1.Enabled = false;
                            button3.Enabled = true;
                            button2.Enabled = false;
                            button4.Enabled = true;
                            button5.Enabled = true;
                            LoopState(); //事件觸發
                        }
                        else
                        {
                            MessageBox.Show("Loop Open ERROR!!!");
                        }

                        break;

                    case "Loop End":

                        if (main_MIRDC_testLoop.IsAlive)
                        {
                            end_thread();
                            button1.Enabled = true;
                            button3.Enabled = false;
                            button2.Enabled = false;
                            button4.Enabled = false;
                            button5.Enabled = false;
                        }

                        break;

                    case "Start":

                        fun_mirdc.loopStop = false;
                        button2.Enabled = false;
                        button4.Enabled = true;


                        break;

                    case "Stop":

                        fun_mirdc.loopStop = true;
                        button2.Enabled = true;
                        button4.Enabled = false;

                        break;


                    case "Set":

                        fun_mirdc.loopSpeed = Convert.ToInt32(textBox1.Text);

                        break;



                }
            }
            catch (Exception x) { MessageBox.Show(x.ToString(), "systen error!!!"); }
        }
        #endregion


        #region 監控物件管理
        /// <summary>
        /// 監測各項參數(Timer)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer1_Tick(object sender, EventArgs e)
        {
            label1.Text = fun_mirdc.num;
            label2.Text = "Scan time(ms) : " + fun_mirdc.loopSpeed.ToString();
            label3.Text = "Loop Step : " + fun_mirdc.step.ToString();
            label4.Text = "Loop Stop : " + fun_mirdc.loopStop.ToString();

        }
        #endregion

    }


    /// <summary>
    /// 欲建立執行緒之類別
    /// </summary>
    public class function
    {
        #region 欄位

        //步序數據
        public int step { get { return m_step; } set { m_step = value; } } //對外訊號(R/W)
        private int m_step = 0; //對內訊號
        //步序速度
        public int loopSpeed { get { return m_loopSpeed; } set { m_loopSpeed = value; } }//對外訊號(R/W)
        private int m_loopSpeed = 100; //對內訊號
        //步序數據狀態
        public string num { get { return m_num; } private set { } }//對外訊號(R)
        private string m_num = "0"; //對內訊號
        //步序停止訊號
        public bool loopStop { get { return m_loopStop; } set { m_loopStop = value; } }//對外訊號(R/W)
        private bool m_loopStop; //對內訊號
        #endregion

        /// <summary>
        /// 類別執行緒主要控制
        /// </summary>
        public void LoopRun()
        {
            //基本參數初始化
            m_loopStop = false; //步序暫停(關閉)
            m_step = 0; //步序歸零
            //迴圈內部執行步序方法
            while (true)
            {
                loopCase();  //步序內容              
            }
        }

        /// <summary>
        /// 步序方法
        /// </summary>
        public void loopCase()
        {
            switch (m_step)
            {

                #region 步序(0)
                case 0:
                    while (m_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊

                    m_num = m_step.ToString(); //步序顯示用 
                    m_step++; //下一步序控制
                    break;

                #endregion

                #region 步序(1)
                case 1:
                    while (m_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊

                    m_num = m_step.ToString(); //步序顯示用 
                    m_step++; //下一步序控制
                    break;

                #endregion

                #region 步序(2)
                case 2:
                    while (m_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊

                    m_num = m_step.ToString(); //步序顯示用 
                    m_step++; //下一步序控制
                    break;

                #endregion

                #region 步序(3)
                case 3:
                    while (m_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊

                    m_num = m_step.ToString(); //步序顯示用
                    m_step++; //下一步序控制
                    break;

                #endregion

                #region 步序(4)
                case 4:
                    while (m_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊

                    m_num = m_step.ToString(); //步序顯示用
                    m_step++; //下一步序控制
                    break;

                #endregion

                #region 步序(5)
                case 5:
                    while (m_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊

                    m_num = m_step.ToString(); //步序顯示用
                    m_step++; //下一步序控制
                    break;

                #endregion

                #region 步序(6)
                case 6:
                    while (m_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊

                    m_num = m_step.ToString(); //步序顯示用
                    m_step++; //下一步序控制
                    break;

                #endregion

                #region 步序(7)
                case 7:
                    while (m_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊

                    m_num = m_step.ToString(); //步序顯示用
                    m_step++; //下一步序控制
                    break;

                #endregion

                #region 步序(8)
                case 8:
                    while (m_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊

                    m_num = m_step.ToString(); //步序顯示用
                    m_step++; //下一步序控制
                    break;

                #endregion

                #region 步序(9)
                case 9:
                    while (m_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊

                    m_num = m_step.ToString(); //步序顯示用
                    m_step++; //下一步序控制
                    break;

                #endregion

                #region 步序(10)
                case 10:
                    while (m_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊

                    m_num = m_step.ToString(); //步序顯示用
                    m_step++; //下一步序控制
                    break;

                #endregion

                #region 步序(11)
                case 11:
                    while (m_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊

                    m_num = m_step.ToString(); //步序顯示用
                    m_step++; //下一步序控制
                    break;

                #endregion

                #region 步序(12)
                case 12:
                    while (m_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊

                    m_num = m_step.ToString(); //步序顯示用
                    m_step = 0; //下一步序控制
                    break;

                    #endregion

            }
            Thread.Sleep(m_loopSpeed); //單步序時間設定
        }

    }

}
