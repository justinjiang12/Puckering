using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace MIRDC_Puckering.AutoLoop
{
    delegate void A_OTHER_DO(ushort num, bool state);

    class OTHER_LOOP
    {

        #region 委派專用欄位
        public event A_OTHER_DO e_Signal_output_1;
        public event A_OTHER_DO e_Signal_output_2;
        public event A_OTHER_DO e_Signal_output_3;
        public event A_OTHER_DO e_Signal_output_4;
        public event A_OTHER_DO e_Signal_output_5;
        public event A_OTHER_DO e_Signal_output_6;
        #endregion

        #region 外部實作欄位

        //步序數據
        public int Other_step { get { return _step; } set { _step = value; } } //對外訊號(R/W)
        private int _step = 0; //對內訊號
        //步序速度
        public int Other_loopSpeed { get { return _loopSpeed; } set { _loopSpeed = value; } }//對外訊號(R/W)
        private int _loopSpeed = 100; //對內訊號
        //步序停止訊號
        public bool Other_loopStop { get { return _loopStop; } set { _loopStop = value; } }//對外訊號(R/W)
        private bool _loopStop; //對內訊號
        //步序數據狀態
        public static string Other_StepNum { get { return _StepNum; } set { _StepNum = value; } }//對外訊號(R)
        private static string _StepNum = "0"; //對內訊號

        #endregion


        #region 內部(for Other Loop)交握欄位
        public static bool Other_signal_a1 { get { return _signal_a1; } set { _signal_a1 = value; } }//訊號(R/W)
        private static bool _signal_a1 = false; //訊號
        public static bool Other_signal_a2 { get { return _signal_a2; } set { _signal_a2 = value; } }//訊號(R/W)
        private static bool _signal_a2 = false; //訊號
        public static bool Other_signal_a3 { get { return _signal_a3; } set { _signal_a3 = value; } }//訊號(R/W)
        private static bool _signal_a3 = false; //訊號
        public static bool Other_signal_a4 { get { return _signal_a4; } set { _signal_a4 = value; } }//訊號(R/W)
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
                LoopCase();  //步序內容              
            }
        }

        /// <summary>
        /// 步序方法
        /// </summary>
        public void LoopCase()
        {
            switch (_step)
            {

                #region 步序(0)
                case 0:
                    while (_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊


                    //if (!Puckering_MainForm.F_IO.DI_Read(0)) {Puckering_MainForm.F_IO.DO_Write(1, true);}//IO輸出
                    e_Signal_output_1(1, true);//委派觸發
                    //判斷該回饋訊號
                    if (true)
                    {
                        _StepNum = _step.ToString(); //步序顯示用
                        _step++; //下一步序控制
                    }


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
                    e_Signal_output_1(1, false);//委派觸發

                    //判斷該回饋訊號
                    if (!Puckering_MainForm.F_IO.DI_Read(1))
                    {
                        _StepNum = _step.ToString(); //步序顯示用
                        _step++; //下一步序控制
                    }

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
                    _signal_a1 = true;
                    _StepNum = _step.ToString(); //步序顯示用
                    _step = 0; //下一步序控制


                    break;

                    #endregion

            }
            Thread.Sleep(_loopSpeed); //單步序時間設定
        }
        /// <summary>
        /// 訊號初始化
        /// </summary>
        public void ResetData()
        {
            Other_signal_a1 = false;
            Other_signal_a2 = false;
            Other_signal_a3 = false;
            Other_signal_a4 = false;
            Other_StepNum = "0";
        }
    }
}
