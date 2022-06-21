using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace MIRDC_Puckering.AutoLoop
{

    delegate void A_SYSControl_DO(ushort num, bool state);

    class AutoControl_Loop
    {
        #region 委派專用欄位
        public event A_SYSControl_DO e_Signal_output_1;
        //public event A_SYSControl_DO e_Signal_output_2;
        //public event A_SYSControl_DO e_Signal_output_3;
        //public event A_SYSControl_DO e_Signal_output_4;
        //public event A_SYSControl_DO e_Signal_output_5;
        //public event A_SYSControl_DO e_Signal_output_6;

        #endregion

        #region 外部實作欄位

        //步序數據
        public int SYSC_step { get { return _step; } set { _step = value; } } //對外訊號(R/W)
        private int _step = 0; //對內訊號
        //步序速度
        public int SYSC_loopSpeed { get { return _loopSpeed; } set { _loopSpeed = value; } }//對外訊號(R/W)
        private int _loopSpeed = 100; //對內訊號
        //步序停止訊號
        public bool SYSC_loopStop { get { return _loopStop; } set { _loopStop = value; } }//對外訊號(R/W)
        private bool _loopStop; //對內訊號
        //步序數據狀態
        public static string SYSC_StepNum { get { return _StepNum; } set { _StepNum = value; } }//對外訊號(R)
        private static string _StepNum = "0"; //對內訊號

        #endregion

        #region 內部(for Other Loop)交握欄位
        public static bool SYSC_signal_a1 { get { return _signal_a1; } set { _signal_a1 = value; } }//訊號(R/W)
        private static bool _signal_a1 = false; //訊號
        public static bool SYSC_signal_a2 { get { return _signal_a2; } set { _signal_a2 = value; } }//訊號(R/W)
        private static bool _signal_a2 = false; //訊號
        public static bool SYSC_signal_a3 { get { return _signal_a3; } set { _signal_a3 = value; } }//訊號(R/W)
        private static bool _signal_a3 = false; //訊號
        public static bool SYSC_signal_a4 { get { return _signal_a4; } set { _signal_a4 = value; } }//訊號(R/W)
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
                #region 步序(0) 啟動準備
                case 0:
                    while (_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊


                    //if (!Puckering_MainForm.F_IO.DI_Read(0)) {Puckering_MainForm.F_IO.DO_Write(1, true);}//IO輸出
                    e_Signal_output_1(1, true);//委派觸發
                    //判斷該回饋訊號
                    if (Puckering_MainForm.F_IO.DI_Read(1))
                    {

                        _StepNum = _step.ToString(); //步序顯示用
                        _step++; //下一步序控制
                    }


                    break;

                #endregion
                #region 步序(1) 確認各項狀態
                case 1:
                    while (_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊

                    _StepNum = _step.ToString(); //步序顯示用 
                    _step++; //下一步序控制
                    break;

                #endregion
                #region 步序(2) 確認各項狀態(完成)
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
                #region 步序(5) 各項元件復位動作_準備
                case 5:
                    while (_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊

                    _StepNum = _step.ToString(); //步序顯示用
                    _step++; //下一步序控制
                    break;

                #endregion
                #region 步序(6) 各項元件復位動作_開始
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
                #region 步序(7) 各項元件復位動作_完成
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
                #region 步序(10) 準備完成
                case 10:
                    while (_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊

                    _StepNum = _step.ToString(); //步序顯示用
                    _step++; //下一步序控制
                    break;

                #endregion
                #region 步序(11) 自動流程開始
                case 11:
                    while (_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊

                    _StepNum = _step.ToString(); //步序顯示用
                    _step++; //下一步序控制
                    break;

                #endregion
                #region 步序(12) 判斷張數
                case 12:
                    while (_loopStop) { } //暫停迴圈




                    //填入該步序動作區塊
                    _signal_a1 = true;
                    _StepNum = _step.ToString(); //步序顯示用
                    _step = 0; //下一步序控制


                    break;

                #endregion
                #region 步序(13)
                case 13:
                    while (_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊

                    _StepNum = _step.ToString(); //步序顯示用
                    _step++; //下一步序控制
                    break;

                #endregion
                #region 步序(14)
                case 14:
                    while (_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊

                    _StepNum = _step.ToString(); //步序顯示用
                    _step++; //下一步序控制
                    break;

                #endregion
                #region 步序(15) 汽缸復位(股)_啟動
                case 15:
                    while (_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊

                    _StepNum = _step.ToString(); //步序顯示用
                    _step++; //下一步序控制
                    break;

                #endregion
                #region 步序(16) 汽缸復位(股)_啟動到位
                case 16:
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
                #region 步序(17) 汽缸進位(紗)_啟動
                case 17:
                    while (_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊

                    _StepNum = _step.ToString(); //步序顯示用
                    _step++; //下一步序控制
                    break;

                #endregion
                #region 步序(18) 汽缸進位(紗)_啟動
                case 18:
                    while (_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊

                    _StepNum = _step.ToString(); //步序顯示用
                    _step++; //下一步序控制
                    break;

                #endregion
                #region 步序(19)
                case 19:
                    while (_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊

                    _StepNum = _step.ToString(); //步序顯示用
                    _step++; //下一步序控制
                    break;

                #endregion
                #region 步序(20) MR_取料動作_準備
                case 20:
                    while (_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊

                    _StepNum = _step.ToString(); //步序顯示用
                    _step++; //下一步序控制
                    break;

                #endregion
                #region 步序(21) MR_取料動作_開始
                case 21:
                    while (_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊

                    _StepNum = _step.ToString(); //步序顯示用
                    _step++; //下一步序控制
                    break;

                #endregion
                #region 步序(22) MR_取料動作_完成
                case 22:
                    while (_loopStop) { } //暫停迴圈




                    //填入該步序動作區塊
                    _signal_a1 = true;
                    _StepNum = _step.ToString(); //步序顯示用
                    _step = 0; //下一步序控制


                    break;

                #endregion
                #region 步序(23)
                case 23:
                    while (_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊

                    _StepNum = _step.ToString(); //步序顯示用
                    _step++; //下一步序控制
                    break;

                #endregion
                #region 步序(24)
                case 24:
                    while (_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊

                    _StepNum = _step.ToString(); //步序顯示用
                    _step++; //下一步序控制
                    break;

                #endregion
                #region 步序(25) 開啟柏努力汽缸
                case 25:
                    while (_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊

                    _StepNum = _step.ToString(); //步序顯示用
                    _step++; //下一步序控制
                    break;

                #endregion
                #region 步序(26) 開啟柏努力汽缸(完成)
                case 26:
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
                #region 步序(27)
                case 27:
                    while (_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊

                    _StepNum = _step.ToString(); //步序顯示用
                    _step++; //下一步序控制
                    break;

                #endregion
                #region 步序(28)
                case 28:
                    while (_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊

                    _StepNum = _step.ToString(); //步序顯示用
                    _step++; //下一步序控制
                    break;

                #endregion
                #region 步序(29)
                case 29:
                    while (_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊

                    _StepNum = _step.ToString(); //步序顯示用
                    _step++; //下一步序控制
                    break;

                #endregion
                #region 步序(30) MR_取料提高動作_準備
                case 30:
                    while (_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊


                    //if (!Puckering_MainForm.F_IO.DI_Read(0)) {Puckering_MainForm.F_IO.DO_Write(1, true);}//IO輸出
                    e_Signal_output_1(1, true);//委派觸發
                    //判斷該回饋訊號
                    if (Puckering_MainForm.F_IO.DI_Read(1))
                    {

                        _StepNum = _step.ToString(); //步序顯示用
                        _step++; //下一步序控制
                    }


                    break;

                #endregion
                #region 步序(31) MR_取料提高動作_開始
                case 31:
                    while (_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊

                    _StepNum = _step.ToString(); //步序顯示用 
                    _step++; //下一步序控制
                    break;

                #endregion
                #region 步序(32) MR_取料提高動作_完成
                case 32:
                    while (_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊

                    _StepNum = _step.ToString(); //步序顯示用 
                    _step++; //下一步序控制
                    break;

                #endregion
                #region 步序(33)
                case 33:
                    while (_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊

                    _StepNum = _step.ToString(); //步序顯示用
                    _step++; //下一步序控制
                    break;

                #endregion
                #region 步序(34)
                case 34:
                    while (_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊

                    _StepNum = _step.ToString(); //步序顯示用
                    _step++; //下一步序控制
                    break;

                #endregion
                #region 步序(35) 開啟夾爪汽缸
                case 35:
                    while (_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊

                    _StepNum = _step.ToString(); //步序顯示用
                    _step++; //下一步序控制
                    break;

                #endregion
                #region 步序(36) 開啟夾爪汽缸(完成)
                case 36:
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
                #region 步序(37) 關閉柏努力汽缸
                case 37:
                    while (_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊

                    _StepNum = _step.ToString(); //步序顯示用
                    _step++; //下一步序控制
                    break;

                #endregion 
                #region 步序(38) 關閉柏努力汽缸(完成)
                case 38:
                    while (_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊

                    _StepNum = _step.ToString(); //步序顯示用
                    _step++; //下一步序控制
                    break;

                #endregion 
                #region 步序(39)
                case 39:
                    while (_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊

                    _StepNum = _step.ToString(); //步序顯示用
                    _step++; //下一步序控制
                    break;

                #endregion
                #region 步序(40) MR_進入A區放料動作_準備
                case 40:
                    while (_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊

                    _StepNum = _step.ToString(); //步序顯示用
                    _step++; //下一步序控制
                    break;

                #endregion 
                #region 步序(41) MR_進入A區放料動作_開始
                case 41:
                    while (_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊

                    _StepNum = _step.ToString(); //步序顯示用
                    _step++; //下一步序控制
                    break;

                #endregion 
                #region 步序(42) MR_進入A區放料動作_完成
                case 42:
                    while (_loopStop) { } //暫停迴圈




                    //填入該步序動作區塊
                    _signal_a1 = true;
                    _StepNum = _step.ToString(); //步序顯示用
                    _step = 0; //下一步序控制


                    break;

                #endregion
                #region 步序(43)
                case 43:
                    while (_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊

                    _StepNum = _step.ToString(); //步序顯示用
                    _step++; //下一步序控制
                    break;

                #endregion
                #region 步序(44)
                case 44:
                    while (_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊

                    _StepNum = _step.ToString(); //步序顯示用
                    _step++; //下一步序控制
                    break;

                #endregion
                #region 步序(45) KR_壓布動作_準備
                case 45:
                    while (_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊

                    _StepNum = _step.ToString(); //步序顯示用
                    _step++; //下一步序控制
                    break;

                #endregion
                #region 步序(46) KR_壓布動作_開始
                case 46:
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
                #region 步序(47) KR_壓布動作_完成
                case 47:
                    while (_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊

                    _StepNum = _step.ToString(); //步序顯示用
                    _step++; //下一步序控制
                    break;

                #endregion
                #region 步序(48)
                case 48:
                    while (_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊

                    _StepNum = _step.ToString(); //步序顯示用
                    _step++; //下一步序控制
                    break;

                #endregion
                #region 步序(49)
                case 49:
                    while (_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊

                    _StepNum = _step.ToString(); //步序顯示用
                    _step++; //下一步序控制
                    break;

                #endregion
                #region 步序(50) 觸發視覺取像
                case 50:
                    while (_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊

                    _StepNum = _step.ToString(); //步序顯示用
                    _step++; //下一步序控制
                    break;

                #endregion
                #region 步序(51) 觸發視覺取像(取得路徑)
                case 51:
                    while (_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊

                    _StepNum = _step.ToString(); //步序顯示用
                    _step++; //下一步序控制
                    break;

                #endregion
                #region 步序(52)
                case 52:
                    while (_loopStop) { } //暫停迴圈




                    //填入該步序動作區塊
                    _signal_a1 = true;
                    _StepNum = _step.ToString(); //步序顯示用
                    _step = 0; //下一步序控制


                    break;

                #endregion
                #region 步序(53)
                case 53:
                    while (_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊

                    _StepNum = _step.ToString(); //步序顯示用
                    _step++; //下一步序控制
                    break;

                #endregion
                #region 步序(54)
                case 54:
                    while (_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊

                    _StepNum = _step.ToString(); //步序顯示用
                    _step++; //下一步序控制
                    break;

                #endregion
                #region 步序(55) MR_進入A區除皺動作_準備
                case 55:
                    while (_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊

                    _StepNum = _step.ToString(); //步序顯示用
                    _step++; //下一步序控制
                    break;

                #endregion
                #region 步序(56) MR_進入A區除皺動作_開始
                case 56:
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
                #region 步序(57) MR_進入A區除皺動作_完成
                case 57:
                    while (_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊

                    _StepNum = _step.ToString(); //步序顯示用
                    _step++; //下一步序控制
                    break;

                #endregion
                #region 步序(58)
                case 58:
                    while (_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊

                    _StepNum = _step.ToString(); //步序顯示用
                    _step++; //下一步序控制
                    break;

                #endregion
                #region 步序(59)
                case 59:
                    while (_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊

                    _StepNum = _step.ToString(); //步序顯示用
                    _step++; //下一步序控制
                    break;

                #endregion
                #region 步序(60) KR_滾布除皺A動作_準備
                case 60:
                    while (_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊


                    //if (!Puckering_MainForm.F_IO.DI_Read(0)) {Puckering_MainForm.F_IO.DO_Write(1, true);}//IO輸出
                    e_Signal_output_1(1, true);//委派觸發
                    //判斷該回饋訊號
                    if (Puckering_MainForm.F_IO.DI_Read(1))
                    {

                        _StepNum = _step.ToString(); //步序顯示用
                        _step++; //下一步序控制
                    }


                    break;

                #endregion
                #region 步序(61) KR_滾布除皺A動作_開始
                case 61:
                    while (_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊

                    _StepNum = _step.ToString(); //步序顯示用 
                    _step++; //下一步序控制
                    break;

                #endregion
                #region 步序(62) KR_滾布除皺A動作_完成
                case 62:
                    while (_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊

                    _StepNum = _step.ToString(); //步序顯示用 
                    _step++; //下一步序控制
                    break;

                #endregion
                #region 步序(63)
                case 63:
                    while (_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊

                    _StepNum = _step.ToString(); //步序顯示用
                    _step++; //下一步序控制
                    break;

                #endregion
                #region 步序(64)
                case 64:
                    while (_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊

                    _StepNum = _step.ToString(); //步序顯示用
                    _step++; //下一步序控制
                    break;

                #endregion
                #region 步序(65)
                case 65:
                    while (_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊

                    _StepNum = _step.ToString(); //步序顯示用
                    _step++; //下一步序控制
                    break;

                #endregion
                #region 步序(66)
                case 66:
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
                #region 步序(67)
                case 67:
                    while (_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊

                    _StepNum = _step.ToString(); //步序顯示用
                    _step++; //下一步序控制
                    break;

                #endregion
                #region 步序(68)
                case 68:
                    while (_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊

                    _StepNum = _step.ToString(); //步序顯示用
                    _step++; //下一步序控制
                    break;

                #endregion
                #region 步序(69)
                case 69:
                    while (_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊

                    _StepNum = _step.ToString(); //步序顯示用
                    _step++; //下一步序控制
                    break;

                #endregion
                #region 步序(70) MR_進入B區放料動作_準備
                case 70:
                    while (_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊

                    _StepNum = _step.ToString(); //步序顯示用
                    _step++; //下一步序控制
                    break;

                #endregion
                #region 步序(71) MR_進入B區放料動作_開始
                case 71:
                    while (_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊

                    _StepNum = _step.ToString(); //步序顯示用
                    _step++; //下一步序控制
                    break;

                #endregion
                #region 步序(72) MR_進入B區放料動作_完成
                case 72:
                    while (_loopStop) { } //暫停迴圈




                    //填入該步序動作區塊
                    _signal_a1 = true;
                    _StepNum = _step.ToString(); //步序顯示用
                    _step = 0; //下一步序控制


                    break;

                #endregion
                #region 步序(73)
                case 73:
                    while (_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊

                    _StepNum = _step.ToString(); //步序顯示用
                    _step++; //下一步序控制
                    break;

                #endregion
                #region 步序(74)
                case 74:
                    while (_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊

                    _StepNum = _step.ToString(); //步序顯示用
                    _step++; //下一步序控制
                    break;

                #endregion
                #region 步序(75) KR_壓布動作_準備
                case 75:
                    while (_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊

                    _StepNum = _step.ToString(); //步序顯示用
                    _step++; //下一步序控制
                    break;

                #endregion
                #region 步序(76) KR_壓布動作_開始
                case 76:
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
                #region 步序(77) KR_壓布動作_完成
                case 77:
                    while (_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊

                    _StepNum = _step.ToString(); //步序顯示用
                    _step++; //下一步序控制
                    break;

                #endregion
                #region 步序(78)
                case 78:
                    while (_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊

                    _StepNum = _step.ToString(); //步序顯示用
                    _step++; //下一步序控制
                    break;

                #endregion
                #region 步序(79)
                case 79:
                    while (_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊

                    _StepNum = _step.ToString(); //步序顯示用
                    _step++; //下一步序控制
                    break;

                #endregion
                #region 步序(80) 觸發視覺取像
                case 80:
                    while (_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊

                    _StepNum = _step.ToString(); //步序顯示用
                    _step++; //下一步序控制
                    break;

                #endregion
                #region 步序(81) 觸發視覺取像(取得路徑)
                case 81:
                    while (_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊

                    _StepNum = _step.ToString(); //步序顯示用
                    _step++; //下一步序控制
                    break;

                #endregion
                #region 步序(82)
                case 82:
                    while (_loopStop) { } //暫停迴圈




                    //填入該步序動作區塊
                    _signal_a1 = true;
                    _StepNum = _step.ToString(); //步序顯示用
                    _step = 0; //下一步序控制


                    break;

                #endregion
                #region 步序(83)
                case 83:
                    while (_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊

                    _StepNum = _step.ToString(); //步序顯示用
                    _step++; //下一步序控制
                    break;

                #endregion
                #region 步序(84)
                case 84:
                    while (_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊

                    _StepNum = _step.ToString(); //步序顯示用
                    _step++; //下一步序控制
                    break;

                #endregion
                #region 步序(85) MR_進入B區除皺動作_準備
                case 85:
                    while (_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊

                    _StepNum = _step.ToString(); //步序顯示用
                    _step++; //下一步序控制
                    break;

                #endregion
                #region 步序(86) MR_進入B區除皺動作_開始
                case 86:
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
                #region 步序(87) MR_進入B區除皺動作_完成
                case 87:
                    while (_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊

                    _StepNum = _step.ToString(); //步序顯示用
                    _step++; //下一步序控制
                    break;

                #endregion
                #region 步序(88)
                case 88:
                    while (_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊

                    _StepNum = _step.ToString(); //步序顯示用
                    _step++; //下一步序控制
                    break;

                #endregion
                #region 步序(89)
                case 89:
                    while (_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊

                    _StepNum = _step.ToString(); //步序顯示用
                    _step++; //下一步序控制
                    break;

                #endregion
                #region 步序(90) KR_滾布除皺B動作_準備
                case 90:
                    while (_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊


                    //if (!Puckering_MainForm.F_IO.DI_Read(0)) {Puckering_MainForm.F_IO.DO_Write(1, true);}//IO輸出
                    e_Signal_output_1(1, true);//委派觸發
                    //判斷該回饋訊號
                    if (Puckering_MainForm.F_IO.DI_Read(1))
                    {

                        _StepNum = _step.ToString(); //步序顯示用
                        _step++; //下一步序控制
                    }


                    break;

                #endregion
                #region 步序(91) KR_滾布除皺B動作_開始
                case 91:
                    while (_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊

                    _StepNum = _step.ToString(); //步序顯示用 
                    _step++; //下一步序控制
                    break;

                #endregion
                #region 步序(92) KR_滾布除皺B動作_完成
                case 92:
                    while (_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊

                    _StepNum = _step.ToString(); //步序顯示用 
                    _step++; //下一步序控制
                    break;

                #endregion
                #region 步序(93)
                case 93:
                    while (_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊

                    _StepNum = _step.ToString(); //步序顯示用
                    _step++; //下一步序控制
                    break;

                #endregion
                #region 步序(94)
                case 94:
                    while (_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊

                    _StepNum = _step.ToString(); //步序顯示用
                    _step++; //下一步序控制
                    break;

                #endregion
                #region 步序(95)
                case 95:
                    while (_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊

                    _StepNum = _step.ToString(); //步序顯示用
                    _step++; //下一步序控制
                    break;

                #endregion
                #region 步序(96)
                case 96:
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
                #region 步序(97)
                case 97:
                    while (_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊

                    _StepNum = _step.ToString(); //步序顯示用
                    _step++; //下一步序控制
                    break;

                #endregion
                #region 步序(98)
                case 98:
                    while (_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊

                    _StepNum = _step.ToString(); //步序顯示用
                    _step++; //下一步序控制
                    break;

                #endregion
                #region 步序(99)
                case 99:
                    while (_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊

                    _StepNum = _step.ToString(); //步序顯示用
                    _step++; //下一步序控制
                    break;

                #endregion
                #region 步序(100) MR_進入C區放料動作_準備
                case 100:
                    while (_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊


                    //if (!Puckering_MainForm.F_IO.DI_Read(0)) {Puckering_MainForm.F_IO.DO_Write(1, true);}//IO輸出
                    e_Signal_output_1(1, true);//委派觸發
                    //判斷該回饋訊號
                    if (Puckering_MainForm.F_IO.DI_Read(1))
                    {

                        _StepNum = _step.ToString(); //步序顯示用
                        _step++; //下一步序控制
                    }


                    break;

                #endregion
                #region 步序(101) MR_進入C區放料動作_開始
                case 101:
                    while (_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊

                    _StepNum = _step.ToString(); //步序顯示用 
                    _step++; //下一步序控制
                    break;

                #endregion
                #region 步序(102) MR_進入C區放料動作_完成
                case 102:
                    while (_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊

                    _StepNum = _step.ToString(); //步序顯示用 
                    _step++; //下一步序控制
                    break;

                #endregion
                #region 步序(103)
                case 103:
                    while (_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊

                    _StepNum = _step.ToString(); //步序顯示用
                    _step++; //下一步序控制
                    break;

                #endregion
                #region 步序(104)
                case 104:
                    while (_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊

                    _StepNum = _step.ToString(); //步序顯示用
                    _step++; //下一步序控制
                    break;

                #endregion
                #region 步序(105) KR_壓布動作_準備
                case 105:
                    while (_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊

                    _StepNum = _step.ToString(); //步序顯示用
                    _step++; //下一步序控制
                    break;

                #endregion
                #region 步序(106) KR_壓布動作_開始
                case 106:
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
                #region 步序(107) KR_壓布動作_完成
                case 107:
                    while (_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊

                    _StepNum = _step.ToString(); //步序顯示用
                    _step++; //下一步序控制
                    break;

                #endregion
                #region 步序(108)
                case 108:
                    while (_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊

                    _StepNum = _step.ToString(); //步序顯示用
                    _step++; //下一步序控制
                    break;

                #endregion
                #region 步序(109)
                case 109:
                    while (_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊

                    _StepNum = _step.ToString(); //步序顯示用
                    _step++; //下一步序控制
                    break;

                #endregion
                #region 步序(110) 觸發視覺取像
                case 110:
                    while (_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊

                    _StepNum = _step.ToString(); //步序顯示用
                    _step++; //下一步序控制
                    break;

                #endregion
                #region 步序(111) 觸發視覺取像(取得路徑)
                case 111:
                    while (_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊

                    _StepNum = _step.ToString(); //步序顯示用
                    _step++; //下一步序控制
                    break;

                #endregion
                #region 步序(112)
                case 112:
                    while (_loopStop) { } //暫停迴圈




                    //填入該步序動作區塊
                    _signal_a1 = true;
                    _StepNum = _step.ToString(); //步序顯示用
                    _step = 0; //下一步序控制


                    break;

                #endregion
                #region 步序(113)
                case 113:
                    while (_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊

                    _StepNum = _step.ToString(); //步序顯示用
                    _step++; //下一步序控制
                    break;

                #endregion
                #region 步序(114)
                case 114:
                    while (_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊

                    _StepNum = _step.ToString(); //步序顯示用
                    _step++; //下一步序控制
                    break;

                #endregion
                #region 步序(115) MR_進入C區除皺動作_準備
                case 115:
                    while (_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊

                    _StepNum = _step.ToString(); //步序顯示用
                    _step++; //下一步序控制
                    break;

                #endregion
                #region 步序(116) MR_進入C區除皺動作_開始
                case 116:
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
                #region 步序(117) MR_進入C區除皺動作_完成
                case 117:
                    while (_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊

                    _StepNum = _step.ToString(); //步序顯示用
                    _step++; //下一步序控制
                    break;

                #endregion
                #region 步序(118)
                case 118:
                    while (_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊

                    _StepNum = _step.ToString(); //步序顯示用
                    _step++; //下一步序控制
                    break;

                #endregion
                #region 步序(119)
                case 119:
                    while (_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊

                    _StepNum = _step.ToString(); //步序顯示用
                    _step++; //下一步序控制
                    break;

                #endregion
                #region 步序(120) KR_滾布除皺C動作_準備
                case 120:
                    while (_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊

                    _StepNum = _step.ToString(); //步序顯示用
                    _step++; //下一步序控制
                    break;

                #endregion
                #region 步序(121) KR_滾布除皺C動作_開始
                case 121:
                    while (_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊

                    _StepNum = _step.ToString(); //步序顯示用
                    _step++; //下一步序控制
                    break;

                #endregion
                #region 步序(122) KR_滾布除皺C動作_完成
                case 122:
                    while (_loopStop) { } //暫停迴圈




                    //填入該步序動作區塊
                    _signal_a1 = true;
                    _StepNum = _step.ToString(); //步序顯示用
                    _step = 0; //下一步序控制


                    break;

                #endregion
                #region 步序(123)
                case 123:
                    while (_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊

                    _StepNum = _step.ToString(); //步序顯示用
                    _step++; //下一步序控制
                    break;

                #endregion
                #region 步序(124)
                case 124:
                    while (_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊

                    _StepNum = _step.ToString(); //步序顯示用
                    _step++; //下一步序控制
                    break;

                #endregion
                #region 步序(125) 關閉夾爪汽缸
                case 125:
                    while (_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊

                    _StepNum = _step.ToString(); //步序顯示用
                    _step++; //下一步序控制
                    break;

                #endregion
                #region 步序(126) 關閉夾爪汽缸(完成)
                case 126:
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
                #region 步序(127)
                case 127:
                    while (_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊

                    _StepNum = _step.ToString(); //步序顯示用
                    _step++; //下一步序控制
                    break;

                #endregion
                #region 步序(128)
                case 128:
                    while (_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊

                    _StepNum = _step.ToString(); //步序顯示用
                    _step++; //下一步序控制
                    break;

                #endregion
                #region 步序(129)
                case 129:
                    while (_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊

                    _StepNum = _step.ToString(); //步序顯示用
                    _step++; //下一步序控制
                    break;

                #endregion
                #region 步序(130) MR_復位動作_準備
                case 130:
                    while (_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊


                    //if (!Puckering_MainForm.F_IO.DI_Read(0)) {Puckering_MainForm.F_IO.DO_Write(1, true);}//IO輸出
                    e_Signal_output_1(1, true);//委派觸發
                    //判斷該回饋訊號
                    if (Puckering_MainForm.F_IO.DI_Read(1))
                    {

                        _StepNum = _step.ToString(); //步序顯示用
                        _step++; //下一步序控制
                    }


                    break;

                #endregion
                #region 步序(131) MR_復位動作_開始
                case 131:
                    while (_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊

                    _StepNum = _step.ToString(); //步序顯示用 
                    _step++; //下一步序控制
                    break;

                #endregion
                #region 步序(132) MR_復位動作_完成
                case 132:
                    while (_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊

                    _StepNum = _step.ToString(); //步序顯示用 
                    _step++; //下一步序控制
                    break;

                #endregion
                #region 步序(133)
                case 133:
                    while (_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊

                    _StepNum = _step.ToString(); //步序顯示用
                    _step++; //下一步序控制
                    break;

                #endregion
                #region 步序(134)
                case 134:
                    while (_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊

                    _StepNum = _step.ToString(); //步序顯示用
                    _step++; //下一步序控制
                    break;

                #endregion
                #region 步序(135)
                case 135:
                    while (_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊

                    _StepNum = _step.ToString(); //步序顯示用
                    _step++; //下一步序控制
                    break;

                #endregion
                #region 步序(136)
                case 136:
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
                #region 步序(137) 張數計數
                case 137:
                    while (_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊

                    _StepNum = _step.ToString(); //步序顯示用
                    _step++; //下一步序控制
                    break;

                #endregion
                #region 步序(138)
                case 138:
                    while (_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊

                    _StepNum = _step.ToString(); //步序顯示用
                    _step++; //下一步序控制
                    break;

                #endregion
                #region 步序(139)
                case 139:
                    while (_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊

                    _StepNum = _step.ToString(); //步序顯示用
                    _step++; //下一步序控制
                    break;

                #endregion
                #region 步序(140) 判斷是否第五張
                case 140:
                    while (_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊

                    _StepNum = _step.ToString(); //步序顯示用
                    _step++; //下一步序控制
                    break;

                #endregion
                #region 步序(141) 判斷第五張後是否布料復位
                case 141:
                    while (_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊

                    _StepNum = _step.ToString(); //步序顯示用
                    _step++; //下一步序控制
                    break;

                #endregion
                #region 步序(142)
                case 142:
                    while (_loopStop) { } //暫停迴圈




                    //填入該步序動作區塊
                    _signal_a1 = true;
                    _StepNum = _step.ToString(); //步序顯示用
                    _step = 0; //下一步序控制


                    break;

                #endregion
                #region 步序(143)
                case 143:
                    while (_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊

                    _StepNum = _step.ToString(); //步序顯示用
                    _step++; //下一步序控制
                    break;

                #endregion
                #region 步序(144)
                case 144:
                    while (_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊

                    _StepNum = _step.ToString(); //步序顯示用
                    _step++; //下一步序控制
                    break;

                #endregion
                #region 步序(145) KR_第五張滾布除皺動作_準備
                case 145:
                    while (_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊

                    _StepNum = _step.ToString(); //步序顯示用
                    _step++; //下一步序控制
                    break;

                #endregion
                #region 步序(146) KR_第五張滾布除皺動作_開始
                case 146:
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
                #region 步序(147) KR_第五張滾布除皺動作_完成
                case 147:
                    while (_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊

                    _StepNum = _step.ToString(); //步序顯示用
                    _step++; //下一步序控制
                    break;

                #endregion
                #region 步序(148)
                case 148:
                    while (_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊

                    _StepNum = _step.ToString(); //步序顯示用
                    _step++; //下一步序控制
                    break;

                #endregion
                #region 步序(149)
                case 149:
                    while (_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊

                    _StepNum = _step.ToString(); //步序顯示用
                    _step++; //下一步序控制
                    break;

                #endregion
                #region 步序(150)
                case 150:
                    while (_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊

                    _StepNum = _step.ToString(); //步序顯示用
                    _step++; //下一步序控制
                    break;

                #endregion
                #region 步序(151)
                case 151:
                    while (_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊

                    _StepNum = _step.ToString(); //步序顯示用
                    _step++; //下一步序控制
                    break;

                #endregion
                #region 步序(152)
                case 152:
                    while (_loopStop) { } //暫停迴圈




                    //填入該步序動作區塊
                    _signal_a1 = true;
                    _StepNum = _step.ToString(); //步序顯示用
                    _step = 0; //下一步序控制


                    break;

                #endregion
                #region 步序(153)
                case 153:
                    while (_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊

                    _StepNum = _step.ToString(); //步序顯示用
                    _step++; //下一步序控制
                    break;

                #endregion
                #region 步序(154)
                case 154:
                    while (_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊

                    _StepNum = _step.ToString(); //步序顯示用
                    _step++; //下一步序控制
                    break;

                #endregion
                #region 步序(155)
                case 155:
                    while (_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊

                    _StepNum = _step.ToString(); //步序顯示用
                    _step++; //下一步序控制
                    break;

                #endregion
                #region 步序(156)
                case 156:
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
                #region 步序(157)
                case 157:
                    while (_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊

                    _StepNum = _step.ToString(); //步序顯示用
                    _step++; //下一步序控制
                    break;

                #endregion
                #region 步序(158)
                case 158:
                    while (_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊

                    _StepNum = _step.ToString(); //步序顯示用
                    _step++; //下一步序控制
                    break;

                #endregion
                #region 步序(159)
                case 159:
                    while (_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊

                    _StepNum = _step.ToString(); //步序顯示用
                    _step++; //下一步序控制
                    break;

                #endregion
                #region 步序(160)
                case 160:
                    while (_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊


                    //if (!Puckering_MainForm.F_IO.DI_Read(0)) {Puckering_MainForm.F_IO.DO_Write(1, true);}//IO輸出
                    e_Signal_output_1(1, true);//委派觸發
                    //判斷該回饋訊號
                    if (Puckering_MainForm.F_IO.DI_Read(1))
                    {

                        _StepNum = _step.ToString(); //步序顯示用
                        _step++; //下一步序控制
                    }


                    break;

                #endregion
                #region 步序(161)
                case 161:
                    while (_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊

                    _StepNum = _step.ToString(); //步序顯示用 
                    _step++; //下一步序控制
                    break;

                #endregion
                #region 步序(162)
                case 162:
                    while (_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊

                    _StepNum = _step.ToString(); //步序顯示用 
                    _step++; //下一步序控制
                    break;

                #endregion
                #region 步序(163)
                case 163:
                    while (_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊

                    _StepNum = _step.ToString(); //步序顯示用
                    _step++; //下一步序控制
                    break;

                #endregion
                #region 步序(164)
                case 164:
                    while (_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊

                    _StepNum = _step.ToString(); //步序顯示用
                    _step++; //下一步序控制
                    break;

                #endregion
                #region 步序(165)
                case 165:
                    while (_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊

                    _StepNum = _step.ToString(); //步序顯示用
                    _step++; //下一步序控制
                    break;

                #endregion
                #region 步序(166)
                case 166:
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
                #region 步序(167)
                case 167:
                    while (_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊

                    _StepNum = _step.ToString(); //步序顯示用
                    _step++; //下一步序控制
                    break;

                #endregion
                #region 步序(168)
                case 168:
                    while (_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊

                    _StepNum = _step.ToString(); //步序顯示用
                    _step++; //下一步序控制
                    break;

                #endregion
                #region 步序(169)
                case 169:
                    while (_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊

                    _StepNum = _step.ToString(); //步序顯示用
                    _step++; //下一步序控制
                    break;

                #endregion
                #region 步序(170)
                case 170:
                    while (_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊

                    _StepNum = _step.ToString(); //步序顯示用
                    _step++; //下一步序控制
                    break;

                #endregion
                #region 步序(171)
                case 171:
                    while (_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊

                    _StepNum = _step.ToString(); //步序顯示用
                    _step++; //下一步序控制
                    break;

                #endregion
                #region 步序(172)
                case 172:
                    while (_loopStop) { } //暫停迴圈




                    //填入該步序動作區塊
                    _signal_a1 = true;
                    _StepNum = _step.ToString(); //步序顯示用
                    _step = 0; //下一步序控制


                    break;

                #endregion
                #region 步序(173)
                case 173:
                    while (_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊

                    _StepNum = _step.ToString(); //步序顯示用
                    _step++; //下一步序控制
                    break;

                #endregion
                #region 步序(174)
                case 174:
                    while (_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊

                    _StepNum = _step.ToString(); //步序顯示用
                    _step++; //下一步序控制
                    break;

                #endregion
                #region 步序(175)
                case 175:
                    while (_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊

                    _StepNum = _step.ToString(); //步序顯示用
                    _step++; //下一步序控制
                    break;

                #endregion
                #region 步序(176)
                case 176:
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
                #region 步序(177)
                case 177:
                    while (_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊

                    _StepNum = _step.ToString(); //步序顯示用
                    _step++; //下一步序控制
                    break;

                #endregion
                #region 步序(178)
                case 178:
                    while (_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊

                    _StepNum = _step.ToString(); //步序顯示用
                    _step++; //下一步序控制
                    break;

                #endregion
                #region 步序(179)
                case 179:
                    while (_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊

                    _StepNum = _step.ToString(); //步序顯示用
                    _step++; //下一步序控制
                    break;

                #endregion
                #region 步序(180)
                case 180:
                    while (_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊

                    _StepNum = _step.ToString(); //步序顯示用
                    _step++; //下一步序控制
                    break;

                #endregion
                #region 步序(181)
                case 181:
                    while (_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊

                    _StepNum = _step.ToString(); //步序顯示用
                    _step++; //下一步序控制
                    break;

                #endregion
                #region 步序(182)
                case 182:
                    while (_loopStop) { } //暫停迴圈




                    //填入該步序動作區塊
                    _signal_a1 = true;
                    _StepNum = _step.ToString(); //步序顯示用
                    _step = 0; //下一步序控制


                    break;

                #endregion
                #region 步序(183)
                case 183:
                    while (_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊

                    _StepNum = _step.ToString(); //步序顯示用
                    _step++; //下一步序控制
                    break;

                #endregion
                #region 步序(184)
                case 184:
                    while (_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊

                    _StepNum = _step.ToString(); //步序顯示用
                    _step++; //下一步序控制
                    break;

                #endregion
                #region 步序(185)
                case 185:
                    while (_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊

                    _StepNum = _step.ToString(); //步序顯示用
                    _step++; //下一步序控制
                    break;

                #endregion
                #region 步序(186)
                case 186:
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
                #region 步序(187)
                case 187:
                    while (_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊

                    _StepNum = _step.ToString(); //步序顯示用
                    _step++; //下一步序控制
                    break;

                #endregion
                #region 步序(188)
                case 188:
                    while (_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊

                    _StepNum = _step.ToString(); //步序顯示用
                    _step++; //下一步序控制
                    break;

                #endregion
                #region 步序(189)
                case 189:
                    while (_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊

                    _StepNum = _step.ToString(); //步序顯示用
                    _step++; //下一步序控制
                    break;

                #endregion
                #region 步序(190)
                case 190:
                    while (_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊


                    //if (!Puckering_MainForm.F_IO.DI_Read(0)) {Puckering_MainForm.F_IO.DO_Write(1, true);}//IO輸出
                    e_Signal_output_1(1, true);//委派觸發
                    //判斷該回饋訊號
                    if (Puckering_MainForm.F_IO.DI_Read(1))
                    {

                        _StepNum = _step.ToString(); //步序顯示用
                        _step++; //下一步序控制
                    }


                    break;

                #endregion
                #region 步序(191)
                case 191:
                    while (_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊

                    _StepNum = _step.ToString(); //步序顯示用 
                    _step++; //下一步序控制
                    break;

                #endregion
                #region 步序(192)
                case 192:
                    while (_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊

                    _StepNum = _step.ToString(); //步序顯示用 
                    _step++; //下一步序控制
                    break;

                #endregion
                #region 步序(193)
                case 193:
                    while (_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊

                    _StepNum = _step.ToString(); //步序顯示用
                    _step++; //下一步序控制
                    break;

                #endregion
                #region 步序(194)
                case 194:
                    while (_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊

                    _StepNum = _step.ToString(); //步序顯示用
                    _step++; //下一步序控制
                    break;

                #endregion
                #region 步序(195)
                case 195:
                    while (_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊

                    _StepNum = _step.ToString(); //步序顯示用
                    _step++; //下一步序控制
                    break;

                #endregion
                #region 步序(196)
                case 196:
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
                #region 步序(197)
                case 197:
                    while (_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊

                    _StepNum = _step.ToString(); //步序顯示用
                    _step++; //下一步序控制
                    break;

                #endregion
                #region 步序(198)
                case 198:
                    while (_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊

                    _StepNum = _step.ToString(); //步序顯示用
                    _step++; //下一步序控制
                    break;

                #endregion
                #region 步序(199)
                case 199:
                    while (_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊

                    _StepNum = _step.ToString(); //步序顯示用
                    _step++; //下一步序控制
                    break;

                #endregion
                #region 步序(200) 進入排料動作
                case 200:
                    while (_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊


                    //if (!Puckering_MainForm.F_IO.DI_Read(0)) {Puckering_MainForm.F_IO.DO_Write(1, true);}//IO輸出
                    e_Signal_output_1(1, true);//委派觸發
                    //判斷該回饋訊號
                    if (Puckering_MainForm.F_IO.DI_Read(1))
                    {

                        _StepNum = _step.ToString(); //步序顯示用
                        _step++; //下一步序控制
                    }


                    break;

                #endregion
                #region 步序(201)
                case 201:
                    while (_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊

                    _StepNum = _step.ToString(); //步序顯示用 
                    _step++; //下一步序控制
                    break;

                #endregion
                #region 步序(202)
                case 202:
                    while (_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊

                    _StepNum = _step.ToString(); //步序顯示用 
                    _step++; //下一步序控制
                    break;

                #endregion
                #region 步序(203)
                case 203:
                    while (_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊

                    _StepNum = _step.ToString(); //步序顯示用
                    _step++; //下一步序控制
                    break;

                #endregion
                #region 步序(204)
                case 204:
                    while (_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊

                    _StepNum = _step.ToString(); //步序顯示用
                    _step++; //下一步序控制
                    break;

                #endregion
                #region 步序(205)
                case 205:
                    while (_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊

                    _StepNum = _step.ToString(); //步序顯示用
                    _step++; //下一步序控制
                    break;

                #endregion
                #region 步序(206)
                case 206:
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
                #region 步序(207)
                case 207:
                    while (_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊

                    _StepNum = _step.ToString(); //步序顯示用
                    _step++; //下一步序控制
                    break;

                #endregion
                #region 步序(208)
                case 208:
                    while (_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊

                    _StepNum = _step.ToString(); //步序顯示用
                    _step++; //下一步序控制
                    break;

                #endregion
                #region 步序(209)
                case 209:
                    while (_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊

                    _StepNum = _step.ToString(); //步序顯示用
                    _step++; //下一步序控制
                    break;

                #endregion
                #region 步序(210) 排料動作開始
                case 210:
                    while (_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊

                    _StepNum = _step.ToString(); //步序顯示用
                    _step++; //下一步序控制
                    break;

                #endregion
                #region 步序(211) 判斷組裝張數
                case 211:
                    while (_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊

                    _StepNum = _step.ToString(); //步序顯示用
                    _step++; //下一步序控制
                    break;

                #endregion
                #region 步序(212) 汽缸復位(股)_啟動
                case 212:
                    while (_loopStop) { } //暫停迴圈




                    //填入該步序動作區塊
                    _signal_a1 = true;
                    _StepNum = _step.ToString(); //步序顯示用
                    _step = 0; //下一步序控制


                    break;

                #endregion
                #region 步序(213) 汽缸復位(股)_啟動到位
                case 213:
                    while (_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊

                    _StepNum = _step.ToString(); //步序顯示用
                    _step++; //下一步序控制
                    break;

                #endregion
                #region 步序(214) 汽缸進位(紗)_啟動
                case 214:
                    while (_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊

                    _StepNum = _step.ToString(); //步序顯示用
                    _step++; //下一步序控制
                    break;

                #endregion
                #region 步序(215) 汽缸進位(紗)_啟動到位
                case 215:
                    while (_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊

                    _StepNum = _step.ToString(); //步序顯示用
                    _step++; //下一步序控制
                    break;

                #endregion
                #region 步序(216)
                case 216:
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
                #region 步序(217)
                case 217:
                    while (_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊

                    _StepNum = _step.ToString(); //步序顯示用
                    _step++; //下一步序控制
                    break;

                #endregion
                #region 步序(218)
                case 218:
                    while (_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊

                    _StepNum = _step.ToString(); //步序顯示用
                    _step++; //下一步序控制
                    break;

                #endregion
                #region 步序(219)
                case 219:
                    while (_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊

                    _StepNum = _step.ToString(); //步序顯示用
                    _step++; //下一步序控制
                    break;

                #endregion
                #region 步序(220) MR_排料區取料動作_準備
                case 220:
                    while (_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊

                    _StepNum = _step.ToString(); //步序顯示用
                    _step++; //下一步序控制
                    break;

                #endregion
                #region 步序(221) MR_排料區取料動作_開始
                case 221:
                    while (_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊

                    _StepNum = _step.ToString(); //步序顯示用
                    _step++; //下一步序控制
                    break;

                #endregion
                #region 步序(222) MR_排料區取料動作_完成
                case 222:
                    while (_loopStop) { } //暫停迴圈




                    //填入該步序動作區塊
                    _signal_a1 = true;
                    _StepNum = _step.ToString(); //步序顯示用
                    _step = 0; //下一步序控制


                    break;

                #endregion
                #region 步序(223)
                case 223:
                    while (_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊

                    _StepNum = _step.ToString(); //步序顯示用
                    _step++; //下一步序控制
                    break;

                #endregion
                #region 步序(224)
                case 224:
                    while (_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊

                    _StepNum = _step.ToString(); //步序顯示用
                    _step++; //下一步序控制
                    break;

                #endregion
                #region 步序(225) 開啟柏努力汽缸
                case 225:
                    while (_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊

                    _StepNum = _step.ToString(); //步序顯示用
                    _step++; //下一步序控制
                    break;

                #endregion
                #region 步序(226) 開啟柏努力汽缸(完成)
                case 226:
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
                #region 步序(227)
                case 227:
                    while (_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊

                    _StepNum = _step.ToString(); //步序顯示用
                    _step++; //下一步序控制
                    break;

                #endregion
                #region 步序(228)
                case 228:
                    while (_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊

                    _StepNum = _step.ToString(); //步序顯示用
                    _step++; //下一步序控制
                    break;

                #endregion
                #region 步序(229)
                case 229:
                    while (_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊

                    _StepNum = _step.ToString(); //步序顯示用
                    _step++; //下一步序控制
                    break;

                #endregion
                #region 步序(230) MR_取料提高動作_準備
                case 230:
                    while (_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊


                    //if (!Puckering_MainForm.F_IO.DI_Read(0)) {Puckering_MainForm.F_IO.DO_Write(1, true);}//IO輸出
                    e_Signal_output_1(1, true);//委派觸發
                    //判斷該回饋訊號
                    if (Puckering_MainForm.F_IO.DI_Read(1))
                    {

                        _StepNum = _step.ToString(); //步序顯示用
                        _step++; //下一步序控制
                    }


                    break;

                #endregion
                #region 步序(231) MR_取料提高動作_開始
                case 231:
                    while (_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊

                    _StepNum = _step.ToString(); //步序顯示用 
                    _step++; //下一步序控制
                    break;

                #endregion
                #region 步序(232) MR_取料提高動作_完成
                case 232:
                    while (_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊

                    _StepNum = _step.ToString(); //步序顯示用 
                    _step++; //下一步序控制
                    break;

                #endregion
                #region 步序(233)
                case 233:
                    while (_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊

                    _StepNum = _step.ToString(); //步序顯示用
                    _step++; //下一步序控制
                    break;

                #endregion
                #region 步序(234)
                case 234:
                    while (_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊

                    _StepNum = _step.ToString(); //步序顯示用
                    _step++; //下一步序控制
                    break;

                #endregion
                #region 步序(235) 開啟夾爪汽缸
                case 235:
                    while (_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊

                    _StepNum = _step.ToString(); //步序顯示用
                    _step++; //下一步序控制
                    break;

                #endregion
                #region 步序(236) 開啟夾爪汽缸(完成)
                case 236:
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
                #region 步序(237) 關閉柏努力汽缸
                case 237:
                    while (_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊

                    _StepNum = _step.ToString(); //步序顯示用
                    _step++; //下一步序控制
                    break;

                #endregion
                #region 步序(238) 關閉柏努力汽缸(完成)
                case 238:
                    while (_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊

                    _StepNum = _step.ToString(); //步序顯示用
                    _step++; //下一步序控制
                    break;

                #endregion
                #region 步序(239)
                case 239:
                    while (_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊

                    _StepNum = _step.ToString(); //步序顯示用
                    _step++; //下一步序控制
                    break;

                #endregion
                #region 步序(240) MR_原位取料區放料動作_準備
                case 240:
                    while (_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊

                    _StepNum = _step.ToString(); //步序顯示用
                    _step++; //下一步序控制
                    break;

                #endregion
                #region 步序(241) MR_原位取料區放料動作_開始
                case 241:
                    while (_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊

                    _StepNum = _step.ToString(); //步序顯示用
                    _step++; //下一步序控制
                    break;

                #endregion
                #region 步序(242) MR_原位取料區放料動作_完成
                case 242:
                    while (_loopStop) { } //暫停迴圈




                    //填入該步序動作區塊
                    _signal_a1 = true;
                    _StepNum = _step.ToString(); //步序顯示用
                    _step = 0; //下一步序控制


                    break;

                #endregion
                #region 步序(243)
                case 243:
                    while (_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊

                    _StepNum = _step.ToString(); //步序顯示用
                    _step++; //下一步序控制
                    break;

                #endregion
                #region 步序(244)
                case 244:
                    while (_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊

                    _StepNum = _step.ToString(); //步序顯示用
                    _step++; //下一步序控制
                    break;

                #endregion
                #region 步序(245) 關閉夾爪汽缸
                case 245:
                    while (_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊

                    _StepNum = _step.ToString(); //步序顯示用
                    _step++; //下一步序控制
                    break;

                #endregion
                #region 步序(246) 關閉夾爪汽缸(完成)
                case 246:
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
                #region 步序(247)
                case 247:
                    while (_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊

                    _StepNum = _step.ToString(); //步序顯示用
                    _step++; //下一步序控制
                    break;

                #endregion
                #region 步序(248)
                case 248:
                    while (_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊

                    _StepNum = _step.ToString(); //步序顯示用
                    _step++; //下一步序控制
                    break;

                #endregion
                #region 步序(249)
                case 249:
                    while (_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊

                    _StepNum = _step.ToString(); //步序顯示用
                    _step++; //下一步序控制
                    break;

                #endregion
                #region 步序(250) MR_復位動作_準備
                case 250:
                    while (_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊

                    _StepNum = _step.ToString(); //步序顯示用
                    _step++; //下一步序控制
                    break;

                #endregion
                #region 步序(251) MR_復位動作_開始
                case 251:
                    while (_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊

                    _StepNum = _step.ToString(); //步序顯示用
                    _step++; //下一步序控制
                    break;

                #endregion
                #region 步序(252) MR_復位動作_完成
                case 252:
                    while (_loopStop) { } //暫停迴圈




                    //填入該步序動作區塊
                    _signal_a1 = true;
                    _StepNum = _step.ToString(); //步序顯示用
                    _step = 0; //下一步序控制


                    break;

                #endregion
                #region 步序(253)
                case 253:
                    while (_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊

                    _StepNum = _step.ToString(); //步序顯示用
                    _step++; //下一步序控制
                    break;

                #endregion
                #region 步序(254)
                case 254:
                    while (_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊

                    _StepNum = _step.ToString(); //步序顯示用
                    _step++; //下一步序控制
                    break;

                #endregion
                #region 步序(255) 回放張數計數
                case 255:
                    while (_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊

                    _StepNum = _step.ToString(); //步序顯示用
                    _step++; //下一步序控制
                    break;

                #endregion
                #region 步序(256)
                case 256:
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
                #region 步序(257)
                case 257:
                    while (_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊

                    _StepNum = _step.ToString(); //步序顯示用
                    _step++; //下一步序控制
                    break;

                #endregion
                #region 步序(258)
                case 258:
                    while (_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊

                    _StepNum = _step.ToString(); //步序顯示用
                    _step++; //下一步序控制
                    break;

                #endregion
                #region 步序(259)
                case 259:
                    while (_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊

                    _StepNum = _step.ToString(); //步序顯示用
                    _step++; //下一步序控制
                    break;

                #endregion
                #region 步序(260)
                case 260:
                    while (_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊


                    //if (!Puckering_MainForm.F_IO.DI_Read(0)) {Puckering_MainForm.F_IO.DO_Write(1, true);}//IO輸出
                    e_Signal_output_1(1, true);//委派觸發
                    //判斷該回饋訊號
                    if (Puckering_MainForm.F_IO.DI_Read(1))
                    {

                        _StepNum = _step.ToString(); //步序顯示用
                        _step++; //下一步序控制
                    }


                    break;

                #endregion
                #region 步序(261)
                case 261:
                    while (_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊

                    _StepNum = _step.ToString(); //步序顯示用 
                    _step++; //下一步序控制
                    break;

                #endregion
                #region 步序(262)
                case 262:
                    while (_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊

                    _StepNum = _step.ToString(); //步序顯示用 
                    _step++; //下一步序控制
                    break;

                #endregion
                #region 步序(263)
                case 263:
                    while (_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊

                    _StepNum = _step.ToString(); //步序顯示用
                    _step++; //下一步序控制
                    break;

                #endregion
                #region 步序(264)
                case 264:
                    while (_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊

                    _StepNum = _step.ToString(); //步序顯示用
                    _step++; //下一步序控制
                    break;

                #endregion
                #region 步序(265)
                case 265:
                    while (_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊

                    _StepNum = _step.ToString(); //步序顯示用
                    _step++; //下一步序控制
                    break;

                #endregion
                #region 步序(266)
                case 266:
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
                #region 步序(267)
                case 267:
                    while (_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊

                    _StepNum = _step.ToString(); //步序顯示用
                    _step++; //下一步序控制
                    break;

                #endregion
                #region 步序(268)
                case 268:
                    while (_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊

                    _StepNum = _step.ToString(); //步序顯示用
                    _step++; //下一步序控制
                    break;

                #endregion
                #region 步序(269)
                case 269:
                    while (_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊

                    _StepNum = _step.ToString(); //步序顯示用
                    _step++; //下一步序控制
                    break;

                #endregion
                #region 步序(270)
                case 270:
                    while (_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊

                    _StepNum = _step.ToString(); //步序顯示用
                    _step++; //下一步序控制
                    break;

                #endregion
                #region 步序(271)
                case 271:
                    while (_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊

                    _StepNum = _step.ToString(); //步序顯示用
                    _step++; //下一步序控制
                    break;

                #endregion
                #region 步序(272)
                case 272:
                    while (_loopStop) { } //暫停迴圈




                    //填入該步序動作區塊
                    _signal_a1 = true;
                    _StepNum = _step.ToString(); //步序顯示用
                    _step = 0; //下一步序控制


                    break;

                #endregion
                #region 步序(273)
                case 273:
                    while (_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊

                    _StepNum = _step.ToString(); //步序顯示用
                    _step++; //下一步序控制
                    break;

                #endregion
                #region 步序(274)
                case 274:
                    while (_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊

                    _StepNum = _step.ToString(); //步序顯示用
                    _step++; //下一步序控制
                    break;

                #endregion
                #region 步序(275)
                case 275:
                    while (_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊

                    _StepNum = _step.ToString(); //步序顯示用
                    _step++; //下一步序控制
                    break;

                #endregion
                #region 步序(276)
                case 276:
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
                #region 步序(277)
                case 277:
                    while (_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊

                    _StepNum = _step.ToString(); //步序顯示用
                    _step++; //下一步序控制
                    break;

                #endregion
                #region 步序(278)
                case 278:
                    while (_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊

                    _StepNum = _step.ToString(); //步序顯示用
                    _step++; //下一步序控制
                    break;

                #endregion
                #region 步序(279)
                case 279:
                    while (_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊

                    _StepNum = _step.ToString(); //步序顯示用
                    _step++; //下一步序控制
                    break;

                #endregion
                #region 步序(280)
                case 280:
                    while (_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊

                    _StepNum = _step.ToString(); //步序顯示用
                    _step++; //下一步序控制
                    break;

                #endregion
                #region 步序(281)
                case 281:
                    while (_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊

                    _StepNum = _step.ToString(); //步序顯示用
                    _step++; //下一步序控制
                    break;

                #endregion
                #region 步序(282)
                case 282:
                    while (_loopStop) { } //暫停迴圈




                    //填入該步序動作區塊
                    _signal_a1 = true;
                    _StepNum = _step.ToString(); //步序顯示用
                    _step = 0; //下一步序控制


                    break;

                #endregion
                #region 步序(283)
                case 283:
                    while (_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊

                    _StepNum = _step.ToString(); //步序顯示用
                    _step++; //下一步序控制
                    break;

                #endregion
                #region 步序(284)
                case 284:
                    while (_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊

                    _StepNum = _step.ToString(); //步序顯示用
                    _step++; //下一步序控制
                    break;

                #endregion
                #region 步序(285)
                case 285:
                    while (_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊

                    _StepNum = _step.ToString(); //步序顯示用
                    _step++; //下一步序控制
                    break;

                #endregion
                #region 步序(286)
                case 286:
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
                #region 步序(287)
                case 287:
                    while (_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊

                    _StepNum = _step.ToString(); //步序顯示用
                    _step++; //下一步序控制
                    break;

                #endregion
                #region 步序(288)
                case 288:
                    while (_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊

                    _StepNum = _step.ToString(); //步序顯示用
                    _step++; //下一步序控制
                    break;

                #endregion
                #region 步序(289)
                case 289:
                    while (_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊

                    _StepNum = _step.ToString(); //步序顯示用
                    _step++; //下一步序控制
                    break;

                #endregion
                #region 步序(290)
                case 290:
                    while (_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊


                    //if (!Puckering_MainForm.F_IO.DI_Read(0)) {Puckering_MainForm.F_IO.DO_Write(1, true);}//IO輸出
                    e_Signal_output_1(1, true);//委派觸發
                    //判斷該回饋訊號
                    if (Puckering_MainForm.F_IO.DI_Read(1))
                    {

                        _StepNum = _step.ToString(); //步序顯示用
                        _step++; //下一步序控制
                    }


                    break;

                #endregion
                #region 步序(291)
                case 291:
                    while (_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊

                    _StepNum = _step.ToString(); //步序顯示用 
                    _step++; //下一步序控制
                    break;

                #endregion
                #region 步序(292)
                case 292:
                    while (_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊

                    _StepNum = _step.ToString(); //步序顯示用 
                    _step++; //下一步序控制
                    break;

                #endregion
                #region 步序(293)
                case 293:
                    while (_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊

                    _StepNum = _step.ToString(); //步序顯示用
                    _step++; //下一步序控制
                    break;

                #endregion
                #region 步序(294)
                case 294:
                    while (_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊

                    _StepNum = _step.ToString(); //步序顯示用
                    _step++; //下一步序控制
                    break;

                #endregion
                #region 步序(295)
                case 295:
                    while (_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊

                    _StepNum = _step.ToString(); //步序顯示用
                    _step++; //下一步序控制
                    break;

                #endregion
                #region 步序(296)
                case 296:
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
                #region 步序(297)
                case 297:
                    while (_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊

                    _StepNum = _step.ToString(); //步序顯示用
                    _step++; //下一步序控制
                    break;

                #endregion
                #region 步序(298) 流程結束
                case 298:
                    while (_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊

                    _StepNum = _step.ToString(); //步序顯示用
                    _step++; //下一步序控制
                    break;

                #endregion
                #region 步序(299) 流程復歸
                case 299:
                    while (_loopStop) { } //暫停迴圈

                    //填入該步序動作區塊

                    _StepNum = _step.ToString(); //步序顯示用
                    _step++; //下一步序控制
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
            SYSC_signal_a1 = false;
            SYSC_signal_a2 = false;
            SYSC_signal_a3 = false;
            SYSC_signal_a4 = false;
            SYSC_StepNum = "0";
        }
    }

    
}
