using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace MIRDC_Puckering
{
    class GrabRobotLoop
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

    class PushRobotLoop
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

    class VisionLoop
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
