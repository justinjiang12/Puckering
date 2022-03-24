using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace MIRDC_Puckering
{
    //delegate void ChangeLoopStep(string step);

    class OtherControl
    {
        public static void ResetData()
        {
            GrabRobotLoop.GR_signal_a1 = false;
            GrabRobotLoop.GR_signal_a2 = false;
            GrabRobotLoop.GR_signal_a3 = false;
            GrabRobotLoop.GR_signal_a4 = false;
            //GrabRobotLoop.
            PushRobotLoop.PR_signal_a1 = false;
            PushRobotLoop.PR_signal_a2 = false;
            PushRobotLoop.PR_signal_a3 = false;
            PushRobotLoop.PR_signal_a4 = false;
            PushRobotLoop.PR_StepNum = "0";
            VisionLoop.VI_signal_a1 = false;
            VisionLoop.VI_signal_a2 = false;
            VisionLoop.VI_signal_a3 = false;
            VisionLoop.VI_signal_a4 = false;
            VisionLoop.VI_StepNum = "0";

        }
    }


    class GrabRobotLoop
    {
        #region 委派專用欄位
        //public event ChangeLoopStep OnChangeLoopStep;

        #endregion

        #region 外部實作欄位

        //步序數據
        public int step { get { return m_step; } set { m_step = value; } } //對外訊號(R/W)
        private int m_step = 0; //對內訊號
        //步序速度
        public int loopSpeed { get { return m_loopSpeed; } set { m_loopSpeed = value; } }//對外訊號(R/W)
        private int m_loopSpeed = 100; //對內訊號
        //步序數據狀態
        public string num { get { return m_num; }  set { m_num = value; } }//對外訊號(R)
        private string m_num = "0"; //對內訊號
        //步序停止訊號
        public bool loopStop { get { return m_loopStop; } set { m_loopStop = value; } }//對外訊號(R/W)
        private bool m_loopStop ; //對內訊號

        #endregion


        #region 內部(for Other Loop)交握欄位
        public static bool GR_signal_a1 { get { return _signal_a1; } set { _signal_a1 = value; } }//訊號(R/W)
        private static bool _signal_a1 = false; //訊號
        public static bool GR_signal_a2 { get { return _signal_a2; } set { _signal_a2 = value; } }//訊號(R/W)
        private static bool _signal_a2 = false; //訊號
        public static bool GR_signal_a3 { get { return _signal_a3; } set { _signal_a3 = value; } }//訊號(R/W)
        private static bool _signal_a3 = false; //訊號
        public static bool GR_signal_a4 { get { return _signal_a4; } set { _signal_a4 = value; } }//訊號(R/W)
        private static bool _signal_a4 = false; //訊號


        #endregion



        /// <summary>
        /// 類別執行緒主要控制
        /// </summary>
        public void LoopRun()
        {
            //基本參數初始化
            m_loopStop = true; //步序暫停(關閉)
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

                    // OnChangeLoopStep(m_num);//委派觸發

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
                    _signal_a1 = true;
                    m_num = m_step.ToString(); //步序顯示用
                    m_step = 0; //下一步序控制
                    break;

                    #endregion

            }
            Thread.Sleep(m_loopSpeed); //單步序時間設定
        }

    }

    class PushRobotLoop
    {
        #region 欄位

        //步序數據
        public int PR_step { get { return _step; } set { _step = value; } } //對外訊號(R/W)
        private int _step = 0; //對內訊號
        //步序速度
        public int PR_loopSpeed { get { return _loopSpeed; } set { _loopSpeed = value; } }//對外訊號(R/W)
        private int _loopSpeed = 100; //對內訊號
        //步序停止訊號
        public bool PR_loopStop { get { return m_loopStop; } set { m_loopStop = value; } }//對外訊號(R/W)
        private bool m_loopStop ; //對內訊號                                  
        //步序數據狀態
        public static string PR_StepNum { get { return _StepNum; } set { _StepNum = value; } }//對外訊號(R)
        private static string _StepNum = "0"; //對內訊號
        #endregion


        #region 內部(for Other Loop)交握欄位
        public static bool PR_signal_a1 { get { return _signal_a1; } set { _signal_a1 = value; } }//訊號(R/W)
        private static bool _signal_a1 = false; //訊號
        public static bool PR_signal_a2 { get { return _signal_a2; } set { _signal_a2 = value; } }//訊號(R/W)
        private static bool _signal_a2 = false; //訊號
        public static bool PR_signal_a3 { get { return _signal_a3; } set { _signal_a3 = value; } }//訊號(R/W)
        private static bool _signal_a3 = false; //訊號
        public static bool PR_signal_a4 { get { return _signal_a4; } set { _signal_a4 = value; } }//訊號(R/W)
        private static bool _signal_a4 = false; //訊號


        #endregion



        /// <summary>
        /// 類別執行緒主要控制
        /// </summary>
        public void LoopRun()
        {
            //基本參數初始化
            m_loopStop = true; //步序暫停(關閉)
            _step = 0; //步序歸零
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
            switch (_step)
            {

                #region 步序(0)
                case 0:
                    while (m_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊
                    if (GrabRobotLoop.GR_signal_a1)
                    {
                        _step++; //下一步序控制
                    }

                    _StepNum = _step.ToString(); //步序顯示用 
                    //m_step++; //下一步序控制
                    break;

                #endregion

                #region 步序(1)
                case 1:
                    while (m_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊

                    _StepNum = _step.ToString(); //步序顯示用 
                    _step++; //下一步序控制
                    break;

                #endregion

                #region 步序(2)
                case 2:
                    while (m_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊

                    _StepNum = _step.ToString(); //步序顯示用 
                    _step++; //下一步序控制
                    break;

                #endregion

                #region 步序(3)
                case 3:
                    while (m_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊

                    _StepNum = _step.ToString(); //步序顯示用
                    _step++; //下一步序控制
                    break;

                #endregion

                #region 步序(4)
                case 4:
                    while (m_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊

                    _StepNum = _step.ToString(); //步序顯示用
                    _step++; //下一步序控制
                    break;

                #endregion

                #region 步序(5)
                case 5:
                    while (m_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊

                    _StepNum = _step.ToString(); //步序顯示用
                    _step++; //下一步序控制
                    break;

                #endregion

                #region 步序(6)
                case 6:
                    while (m_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊

                    _StepNum = _step.ToString(); //步序顯示用
                    _step++; //下一步序控制
                    break;

                #endregion

                #region 步序(7)
                case 7:
                    while (m_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊

                    _StepNum = _step.ToString(); //步序顯示用
                    _step++; //下一步序控制
                    break;

                #endregion

                #region 步序(8)
                case 8:
                    while (m_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊

                    _StepNum = _step.ToString(); //步序顯示用
                    _step++; //下一步序控制
                    break;

                #endregion

                #region 步序(9)
                case 9:
                    while (m_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊

                    _StepNum = _step.ToString(); //步序顯示用
                    _step++; //下一步序控制
                    break;

                #endregion

                #region 步序(10)
                case 10:
                    while (m_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊

                    _StepNum = _step.ToString(); //步序顯示用
                    _step++; //下一步序控制
                    break;

                #endregion

                #region 步序(11)
                case 11:
                    while (m_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊

                    _StepNum = _step.ToString(); //步序顯示用
                    _step++; //下一步序控制
                    break;

                #endregion

                #region 步序(12)
                case 12:
                    while (m_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊
                    _signal_a1 = true;
                    _StepNum = _step.ToString(); //步序顯示用
                    _step = 0; //下一步序控制
                    break;

                    #endregion

            }
            Thread.Sleep(_loopSpeed); //單步序時間設定
        }

    }

    class VisionLoop
    {
        #region 欄位

        //步序數據
        public int VI_step { get { return _step; } set { _step = value; } } //對外訊號(R/W)
        private int _step = 0; //對內訊號
        //步序速度
        public int VI_loopSpeed { get { return _loopSpeed; } set { _loopSpeed = value; } }//對外訊號(R/W)
        private int _loopSpeed = 100; //對內訊號
        //步序停止訊號
        public bool VI_loopStop { get { return _loopStop; } set { _loopStop = value; } }//對外訊號(R/W)
        private bool _loopStop; //對內訊號
        //步序數據狀態
        public static string VI_StepNum { get { return _StepNum; } set { _StepNum = value; } }//對外訊號(R)
        private static string _StepNum = "0"; //對內訊號
        #endregion


        #region 內部(for Other Loop)交握欄位
        public static bool VI_signal_a1 { get { return _signal_a1; } set { _signal_a1 = value; } }//訊號(R/W)
        private static bool _signal_a1 = false; //訊號
        public static bool VI_signal_a2 { get { return _signal_a2; } set { _signal_a2 = value; } }//訊號(R/W)
        private static bool _signal_a2 = false; //訊號
        public static bool VI_signal_a3 { get { return _signal_a3; } set { _signal_a3 = value; } }//訊號(R/W)
        private static bool _signal_a3 = false; //訊號
        public static bool VI_signal_a4 { get { return _signal_a4; } set { _signal_a4 = value; } }//訊號(R/W)
        private static bool _signal_a4 = false; //訊號


        #endregion

        /// <summary>
        /// 類別執行緒主要控制
        /// </summary>
        public void LoopRun()
        {
            //基本參數初始化
            _loopStop = true; //步序暫停(關閉)
            _step = 0; //步序歸零
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
            switch (_step)
            {

                #region 步序(0)
                case 0:
                    while (_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊
                    if (PushRobotLoop.PR_signal_a1) 
                    {
                        _step++; //下一步序控制 
                    }



                    _StepNum = _step.ToString(); //步序顯示用 
                    
                    break;

                #endregion

                #region 步序(1)
                case 1:
                    while (_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊

                    _StepNum = _step.ToString(); //步序顯示用 
                    _step++; //下一步序控制
                    break;

                #endregion

                #region 步序(2)
                case 2:
                    while (_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊

                    _StepNum = _step.ToString(); //步序顯示用 
                    _step++; //下一步序控制
                    break;

                #endregion

                #region 步序(3)
                case 3:
                    while (_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊

                    _StepNum = _step.ToString(); //步序顯示用
                    _step++; //下一步序控制
                    break;

                #endregion

                #region 步序(4)
                case 4:
                    while (_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊

                    _StepNum = _step.ToString(); //步序顯示用
                    _step++; //下一步序控制
                    break;

                #endregion

                #region 步序(5)
                case 5:
                    while (_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊

                    _StepNum = _step.ToString(); //步序顯示用
                    _step++; //下一步序控制
                    break;

                #endregion

                #region 步序(6)
                case 6:
                    while (_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊

                    _StepNum = _step.ToString(); //步序顯示用
                    _step++; //下一步序控制
                    break;

                #endregion

                #region 步序(7)
                case 7:
                    while (_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊

                    _StepNum = _step.ToString(); //步序顯示用
                    _step++; //下一步序控制
                    break;

                #endregion

                #region 步序(8)
                case 8:
                    while (_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊

                    _StepNum = _step.ToString(); //步序顯示用
                    _step++; //下一步序控制
                    break;

                #endregion

                #region 步序(9)
                case 9:
                    while (_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊

                    _StepNum = _step.ToString(); //步序顯示用
                    _step++; //下一步序控制
                    break;

                #endregion

                #region 步序(10)
                case 10:
                    while (_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊

                    _StepNum = _step.ToString(); //步序顯示用
                    _step++; //下一步序控制
                    break;

                #endregion

                #region 步序(11)
                case 11:
                    while (_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊

                    _StepNum = _step.ToString(); //步序顯示用
                    _step++; //下一步序控制
                    break;

                #endregion

                #region 步序(12)
                case 12:
                    while (_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊

                    _StepNum = _step.ToString(); //步序顯示用
                    _step = 0; //下一步序控制
                    break;

                    #endregion

            }
            Thread.Sleep(_loopSpeed); //單步序時間設定
        }

    }

}
