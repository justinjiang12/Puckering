using System;
using System.IO;

using FESIFS;

namespace RouteButler_Yaskawa
{
    public class Yaskawa
    {
        #region 欄位宣告
        public string CurrentState = "null";
        public string ExecuteError = "null";
        

        private string RobotIP;
        private FesIF fesIF = new FesIF();

        #region bool
        public bool Error = false, Alarm = false, isBusy = false, 
            ServoON = false, HoldON = false, Step=false ,OneCycle=false, AutomaticAndContinuos=false, Teach=false,
            InGuardSafeOperation = false , Play = false, CommendRemote = false, HoldStatusE = false, HoldStatusC = false;

        #endregion

        #region readonly string
        private readonly string JobInitial = "/JOB" + Environment.NewLine,
                                FileName = "//NAME ",//NAME filename
                                PoseInitial = "//POS" + Environment.NewLine,
                                PointNumber = "///NPOS ",//10 point NPOS 0,0,0,10,0,0
                                UserGroup = "///USER ",//default 0 ; 1~23
                                Tool = "///TOOL ",//default 0 ; 1~23
                                PoseType = "///POSTYPE ",
                                FirstDefineEnd = "///RECTAN" + Environment.NewLine
                                               + "///RCONF ",
                                FlipMode = "0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0" + Environment.NewLine,
                                NonFlipMode = "1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0" + Environment.NewLine,
                                CommandInitial = "//INST" + Environment.NewLine
                                               + "///DATE ", //DateTime.Now.ToString("yyyy/MM/dd HH:mm")
                                JobAttribute = "///ATTR SC,RW,RJ" + Environment.NewLine,
                                Frame = "////FRAME ",
                                SecondDefineEnd = "///GROUP1 RB1" + Environment.NewLine
                                                + "NOP" + Environment.NewLine,
                                ProgramEnd = "END" + Environment.NewLine,
                                PointInitial = "P",//PointInitial + number + PointEnd (mm)
                                PointEnd = "=",//P2=1,2,3,4,5,6
                                Override = " V=",//unit 0.1mm/s
                                ACC = " ACC=",//20~100
                                DEC = " DEC=",//20~100
                                MovL = "MOVL ",//MOVL P2 BP2 V=10.1 ACC=50 DCC=80
                                MovS = "MOVS ",//MOVS P2 BP2 V=10.1 ACC=50 DCC=80
                                DOUTInitial = "DOUT OT#(",//1~2048 ; DOUT OT#(12) ON
                                DOUTEnd = ")",//DOUTInitial + number + DOUTEnd
                                ARCON = "ARCON ",  //Welding Start
                                ARCSET = "ARCSET ", //Welding set
                                ARCOF = "ARCOF ", // Welding Off
                                AC = "AC=I", //Welding AC Setting
                                AVP = "AVP=I"; //Welding AVP Setting

        #endregion


        #endregion

        #region 連線

        /// <summary>
        /// 連線(多載)
        /// </summary>
        /// <param name="_ip"></param>
        /// <param name="_type"></param>
        /// <returns></returns>
        private int Connect(string _ip, string _type)
        {
            int _socketreturn = 1;
            try
            {
                switch (_type)
                {
                    case "normal":
                        _socketreturn = fesIF.Open(_ip, 10040);
                        break;
                    case "file":
                        _socketreturn = fesIF.Open(_ip, 10041);
                        break;
                }
                return _socketreturn;
            }
            catch
            { return 1; }
        }

        /// <summary>
        /// 連線(多載)
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        public int Connect(string ip)
        {
            int _socketreturn = Connect(ip, "normal");
            if (_socketreturn == 0)
            {
                RobotIP = ip;
                Close();
                return 0;
            }
            else
            {
                RobotIP = "null";
                Close();
                return 1;
            }
        }

        #endregion

        #region 程式資料(撰寫/上載/下載/讀取/啟動)

        /// <summary>
        /// 編寫JBI檔案 (多載-基本)
        /// </summary>
        /// <param name="_routeBook"></param>
        /// <param name="_filename"></param>
        /// <param name="_initialpoint"></param>
        /// <returns></returns>
        public int CompileFile(RouteBook_Yaskawa _routeBook, string _filename, int _initialpoint)
        {
            CurrentState = "Tanslate to JBI format & upload to controller";

            string path = ".\\" + _filename + ".JBI";
            StreamWriter _streamWriter = new StreamWriter(path, false);
            _streamWriter.Write(JobInitial); // </JOB>
            _streamWriter.Write(FileName + _filename + Environment.NewLine); // <//NAME WedingProgrm>
            _streamWriter.Write(PoseInitial); // <//POS>
            _streamWriter.Write(PointNumber + "0,0,0,{0},0,0" + Environment.NewLine, _routeBook.PointNumber);   //  <///NPOS 0,0,0,256,0,0>
            if (_routeBook.Workspace != 0)
                _streamWriter.Write(UserGroup + _routeBook.Workspace.ToString() + Environment.NewLine); 
            if (_routeBook.Workspace != 0)
                _streamWriter.Write(PoseType + "USER" + Environment.NewLine);    //   <///POSTYPE USER>
            else
                _streamWriter.Write(PoseType + "ROBOT" + Environment.NewLine);   //   <///POSTYPE ROBOT>
            if (_routeBook.FlipMode == 0)
                _streamWriter.Write(FirstDefineEnd + FlipMode);  
            else
                _streamWriter.Write(FirstDefineEnd + NonFlipMode);  //   <///RCONF 1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0>

            //寫入點位數據(Data)    <ex. ///TOOL 0  P0001 = 265.320,187.550,-152.780,0.0000,0.0000,0.0000 >
            _routeBook.PointCount = 0;               
            for (int i = 0; i < _routeBook.PointNumber; i++)
            {
                _streamWriter.Write(Tool + _routeBook.Tool[_routeBook.PointCount].ToString() + Environment.NewLine);

                _streamWriter.Write(PointInitial + (_routeBook.PointCount + _initialpoint).ToString().PadLeft(4, '0') + PointEnd
                                  + (Math.Round(_routeBook.X[_routeBook.PointCount], 3, MidpointRounding.AwayFromZero)).ToString("#0.000") + ","
                                  + (Math.Round(_routeBook.Y[_routeBook.PointCount], 3, MidpointRounding.AwayFromZero)).ToString("#0.000") + ","
                                  + (Math.Round(_routeBook.Z[_routeBook.PointCount], 3, MidpointRounding.AwayFromZero)).ToString("#0.000") + ","
                                  + (Math.Round(_routeBook.A[_routeBook.PointCount], 4, MidpointRounding.AwayFromZero)).ToString("#0.0000") + ","
                                  + (Math.Round(_routeBook.B[_routeBook.PointCount], 4, MidpointRounding.AwayFromZero)).ToString("#0.0000") + ","
                                  + (Math.Round(_routeBook.C[_routeBook.PointCount], 4, MidpointRounding.AwayFromZero)).ToString("#0.0000")
                                  + Environment.NewLine);

                _routeBook.PointCount++;
            }

            _streamWriter.Write(CommandInitial + DateTime.Now.ToString("yyyy/MM/dd HH:mm") + Environment.NewLine);  //    <//INST///DATE 2022/05/03 14:18>
            _streamWriter.Write(JobAttribute);    //    <///ATTR SC,RW,RJ>
            if (_routeBook.Workspace != 0)
                _streamWriter.Write(Frame + "USER " + _routeBook.Workspace.ToString() + Environment.NewLine);     
            _streamWriter.Write(SecondDefineEnd); //      <///GROUP1 RB1   NOP>


            //寫入控制語法    <ex. MOVS P0001 V=10.0 ACC=70 DEC=70>
            _routeBook.CommandCount = 0;  //總計數
            _routeBook.PointCount = 0; //點位計數
            _routeBook.DoutCount = 0; //輸出計數
            
            for (_routeBook.CommandCount = 0; _routeBook.CommandCount < _routeBook.PointNumber + _routeBook.DoutNumber + _routeBook.RobotCommandNumber; _routeBook.CommandCount++)
            {
                switch (_routeBook.ProcessQueue[_routeBook.CommandCount])
                {
                    #region Point 語法
                    case 1:
                        switch (_routeBook.MovingMode[_routeBook.PointCount])
                        {
                            #region MovL Point
                            case 1:
                                _streamWriter.Write(MovL
                                                                   + PointInitial + (_routeBook.PointCount + _initialpoint).ToString().PadLeft(4, '0')
                                                                   + Override + _routeBook.Override[_routeBook.PointCount].ToString("#0.0")
                                                                   + ACC + _routeBook.Accerlerate[_routeBook.PointCount].ToString()
                                                                   + DEC + _routeBook.Decerlerate[_routeBook.PointCount].ToString()
                                                                   + Environment.NewLine);
                                
                                break;

                            #endregion


                            #region MovS Point

                            case 2:
                                _streamWriter.Write(MovS
                                                                   + PointInitial + (_routeBook.PointCount + _initialpoint).ToString().PadLeft(4, '0')
                                                                   + Override + _routeBook.Override[_routeBook.PointCount].ToString("#0.0")
                                                                   + ACC + _routeBook.Accerlerate[_routeBook.PointCount].ToString()
                                                                   + DEC + _routeBook.Decerlerate[_routeBook.PointCount].ToString()
                                                                   + Environment.NewLine);
                                
                                break;

                            #endregion
                        }

                        _routeBook.PointCount++;
                        break;
                    #endregion

                    #region Dout 語法
                    case 2:
                        if (_routeBook.DOutMode[_routeBook.DoutCount] > 0)
                            _streamWriter.Write(DOUTInitial + _routeBook.DOutMode[_routeBook.DoutCount].ToString() + DOUTEnd + " ON" +  Environment.NewLine);
                        else
                            _streamWriter.Write(DOUTInitial + (0 - _routeBook.DOutMode[_routeBook.DoutCount]).ToString() + DOUTEnd + " OFF" + Environment.NewLine);

                        _routeBook.DoutCount++;
                        break;

                    #endregion

                    #region 註解

                    case 3:
                        _streamWriter.Write(_routeBook.RobotCommand[_routeBook.RobotCommandCount] + Environment.NewLine);

                        _routeBook.RobotCommandCount++;
                        break;

                    #endregion


                }
            }
            
            _streamWriter.Write(ProgramEnd);   //      <END>

            _streamWriter.Close();
            _streamWriter.Dispose();

            return 0;
        }

        /// <summary>
        /// 編寫JBI檔案 (多載-點位移動[速度])
        /// </summary>
        /// <param name="_routeBook"></param>
        /// <param name="_filename"></param>
        /// <param name="_initialpoint"></param>
        /// <returns></returns>
        public int CompileFile(RouteBook_Yaskawa _routeBook, string _filename, int _initialpoint, int _speed_RegNum)
        {
            CurrentState = "Tanslate to JBI format & upload to controller";

            string path = ".\\" + _filename + ".JBI";
            StreamWriter _streamWriter = new StreamWriter(path, false);
            _streamWriter.Write(JobInitial); // </JOB>
            _streamWriter.Write(FileName + _filename + Environment.NewLine); // <//NAME WedingProgrm>
            _streamWriter.Write(PoseInitial); // <//POS>
            _streamWriter.Write(PointNumber + "0,0,0,{0},0,0" + Environment.NewLine, _routeBook.PointNumber);   //  <///NPOS 0,0,0,256,0,0>
            if (_routeBook.Workspace != 0)
                _streamWriter.Write(UserGroup + _routeBook.Workspace.ToString() + Environment.NewLine);
            if (_routeBook.Workspace != 0)
                _streamWriter.Write(PoseType + "USER" + Environment.NewLine);    //   <///POSTYPE USER>
            else
                _streamWriter.Write(PoseType + "ROBOT" + Environment.NewLine);   //   <///POSTYPE ROBOT>
            if (_routeBook.FlipMode == 0)
                _streamWriter.Write(FirstDefineEnd + FlipMode);
            else
                _streamWriter.Write(FirstDefineEnd + NonFlipMode);  //   <///RCONF 1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0>

            //寫入點位數據(Data)    <ex. ///TOOL 0  P0001 = 265.320,187.550,-152.780,0.0000,0.0000,0.0000 >
            _routeBook.PointCount = 0;
            for (int i = 0; i < _routeBook.PointNumber; i++)
            {
                _streamWriter.Write(Tool + _routeBook.Tool[_routeBook.PointCount].ToString() + Environment.NewLine);

                _streamWriter.Write(PointInitial + (_routeBook.PointCount + _initialpoint).ToString().PadLeft(4, '0') + PointEnd
                                  + (Math.Round(_routeBook.X[_routeBook.PointCount], 3, MidpointRounding.AwayFromZero)).ToString("#0.000") + ","
                                  + (Math.Round(_routeBook.Y[_routeBook.PointCount], 3, MidpointRounding.AwayFromZero)).ToString("#0.000") + ","
                                  + (Math.Round(_routeBook.Z[_routeBook.PointCount], 3, MidpointRounding.AwayFromZero)).ToString("#0.000") + ","
                                  + (Math.Round(_routeBook.A[_routeBook.PointCount], 4, MidpointRounding.AwayFromZero)).ToString("#0.0000") + ","
                                  + (Math.Round(_routeBook.B[_routeBook.PointCount], 4, MidpointRounding.AwayFromZero)).ToString("#0.0000") + ","
                                  + (Math.Round(_routeBook.C[_routeBook.PointCount], 4, MidpointRounding.AwayFromZero)).ToString("#0.0000")
                                  + Environment.NewLine);

                _routeBook.PointCount++;
            }

            _streamWriter.Write(CommandInitial + DateTime.Now.ToString("yyyy/MM/dd HH:mm") + Environment.NewLine);  //    <//INST///DATE 2022/05/03 14:18>
            _streamWriter.Write(JobAttribute);    //    <///ATTR SC,RW,RJ>
            if (_routeBook.Workspace != 0)
                _streamWriter.Write(Frame + "USER " + _routeBook.Workspace.ToString() + Environment.NewLine);
            _streamWriter.Write(SecondDefineEnd); //      <///GROUP1 RB1   NOP>
            
            //寫入控制語法    <ex. MOVS P0001 V=10.0 ACC=70 DEC=70>
            _routeBook.CommandCount = 0;  //總計數
            _routeBook.PointCount = 0; //點位計數
            _routeBook.DoutCount = 0; //輸出計數

            for (_routeBook.CommandCount = 0; _routeBook.CommandCount < _routeBook.PointNumber + _routeBook.DoutNumber + _routeBook.RobotCommandNumber; _routeBook.CommandCount++)
            {
                switch (_routeBook.ProcessQueue[_routeBook.CommandCount])
                {
                    #region Point 語法
                    case 1:
                        switch (_routeBook.MovingMode[_routeBook.PointCount])
                        {
                            #region MovL Point
                            case 1:
                                _streamWriter.Write(MovL
                                                                   + PointInitial + (_routeBook.PointCount + _initialpoint).ToString().PadLeft(4, '0')
                                                                   + Override + "I" + (_speed_RegNum.ToString()).PadLeft(3, '0')
                                                                   + ACC + _routeBook.Accerlerate[_routeBook.PointCount].ToString()
                                                                   + DEC + _routeBook.Decerlerate[_routeBook.PointCount].ToString()
                                                                   + Environment.NewLine);

                                break;

                            #endregion


                            #region MovS Point

                            case 2:
                                _streamWriter.Write(MovS
                                                                   + PointInitial + (_routeBook.PointCount + _initialpoint).ToString().PadLeft(4, '0')
                                                                   + Override + "I" + (_speed_RegNum.ToString()).PadLeft(3, '0')
                                                                   + ACC + _routeBook.Accerlerate[_routeBook.PointCount].ToString()
                                                                   + DEC + _routeBook.Decerlerate[_routeBook.PointCount].ToString()
                                                                   + Environment.NewLine);
                           
                                break;

                                #endregion
                        }

                        _routeBook.PointCount++;
                        break;
                    #endregion

                    #region Dout 語法
                    case 2:
                        if (_routeBook.DOutMode[_routeBook.DoutCount] > 0)
                            _streamWriter.Write(DOUTInitial + _routeBook.DOutMode[_routeBook.DoutCount].ToString() + DOUTEnd + " ON" + Environment.NewLine);
                        else
                            _streamWriter.Write(DOUTInitial + (0 - _routeBook.DOutMode[_routeBook.DoutCount]).ToString() + DOUTEnd + " OFF" + Environment.NewLine);

                        _routeBook.DoutCount++;
                        break;

                    #endregion

                    #region 註解

                    case 3:
                        _streamWriter.Write(_routeBook.RobotCommand[_routeBook.RobotCommandCount] + Environment.NewLine);

                        _routeBook.RobotCommandCount++;
                        break;

                        #endregion


                }
            }
            
            _streamWriter.Write(ProgramEnd);   //      <END>

            _streamWriter.Close();
            _streamWriter.Dispose();

            return 0;
        }

        /// <summary>
        /// 編寫JBI檔案 (多載-焊接[電流/電壓])
        /// </summary>
        /// <param name="_routeBook"></param>
        /// <param name="_filename"></param>
        /// <param name="_initialpoint"></param>
        /// <returns></returns>
        public int CompileFile(RouteBook_Yaskawa _routeBook, string _filename, int _initialpoint, int _welding_A_RegNum, int _welding_V_RegNum)
        {
            CurrentState = "Tanslate to JBI format & upload to controller";

            string path = ".\\" + _filename + ".JBI";
            StreamWriter _streamWriter = new StreamWriter(path, false);
            _streamWriter.Write(JobInitial); // </JOB>
            _streamWriter.Write(FileName + _filename + Environment.NewLine); // <//NAME WedingProgrm>
            _streamWriter.Write(PoseInitial); // <//POS>
            _streamWriter.Write(PointNumber + "0,0,0,{0},0,0" + Environment.NewLine, _routeBook.PointNumber);   //  <///NPOS 0,0,0,256,0,0>
            if (_routeBook.Workspace != 0)
                _streamWriter.Write(UserGroup + _routeBook.Workspace.ToString() + Environment.NewLine);
            if (_routeBook.Workspace != 0)
                _streamWriter.Write(PoseType + "USER" + Environment.NewLine);    //   <///POSTYPE USER>
            else
                _streamWriter.Write(PoseType + "ROBOT" + Environment.NewLine);   //   <///POSTYPE ROBOT>
            if (_routeBook.FlipMode == 0)
                _streamWriter.Write(FirstDefineEnd + FlipMode);
            else
                _streamWriter.Write(FirstDefineEnd + NonFlipMode);  //   <///RCONF 1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0>

            //寫入點位數據(Data)    <ex. ///TOOL 0  P0001 = 265.320,187.550,-152.780,0.0000,0.0000,0.0000 >
            _routeBook.PointCount = 0;
            for (int i = 0; i < _routeBook.PointNumber; i++)
            {
                _streamWriter.Write(Tool + _routeBook.Tool[_routeBook.PointCount].ToString() + Environment.NewLine);

                _streamWriter.Write(PointInitial + (_routeBook.PointCount + _initialpoint).ToString().PadLeft(4, '0') + PointEnd
                                  + (Math.Round(_routeBook.X[_routeBook.PointCount], 3, MidpointRounding.AwayFromZero)).ToString("#0.000") + ","
                                  + (Math.Round(_routeBook.Y[_routeBook.PointCount], 3, MidpointRounding.AwayFromZero)).ToString("#0.000") + ","
                                  + (Math.Round(_routeBook.Z[_routeBook.PointCount], 3, MidpointRounding.AwayFromZero)).ToString("#0.000") + ","
                                  + (Math.Round(_routeBook.A[_routeBook.PointCount], 4, MidpointRounding.AwayFromZero)).ToString("#0.0000") + ","
                                  + (Math.Round(_routeBook.B[_routeBook.PointCount], 4, MidpointRounding.AwayFromZero)).ToString("#0.0000") + ","
                                  + (Math.Round(_routeBook.C[_routeBook.PointCount], 4, MidpointRounding.AwayFromZero)).ToString("#0.0000")
                                  + Environment.NewLine);

                _routeBook.PointCount++;
            }

            _streamWriter.Write(CommandInitial + DateTime.Now.ToString("yyyy/MM/dd HH:mm") + Environment.NewLine);  //    <//INST///DATE 2022/05/03 14:18>
            _streamWriter.Write(JobAttribute);    //    <///ATTR SC,RW,RJ>
            if (_routeBook.Workspace != 0)
                _streamWriter.Write(Frame + "USER " + _routeBook.Workspace.ToString() + Environment.NewLine);
            _streamWriter.Write(SecondDefineEnd); //      <///GROUP1 RB1   NOP>
            // 起始焊接(起弧)
            _streamWriter.Write(ARCON
                                       + AC
                                       + (_welding_A_RegNum.ToString()).PadLeft(3, '0')
                                       + " "
                                       + AVP
                                       + (_welding_V_RegNum.ToString()).PadLeft(3, '0')
                                       + Environment.NewLine);
            
            //寫入控制語法    <ex. MOVS P0001 V=10.0 ACC=70 DEC=70>
            _routeBook.CommandCount = 0;  //總計數
            _routeBook.PointCount = 0; //點位計數
            _routeBook.DoutCount = 0; //輸出計數

            for (_routeBook.CommandCount = 0; _routeBook.CommandCount < _routeBook.PointNumber + _routeBook.DoutNumber + _routeBook.RobotCommandNumber; _routeBook.CommandCount++)
            {
                switch (_routeBook.ProcessQueue[_routeBook.CommandCount])
                {
                    #region Point 語法
                    case 1:
                        switch (_routeBook.MovingMode[_routeBook.PointCount])
                        {
                            #region MovL Point
                            case 1:
                                _streamWriter.Write(MovL
                                                                   + PointInitial + (_routeBook.PointCount + _initialpoint).ToString().PadLeft(4, '0')
                                                                   + Override + _routeBook.Override[_routeBook.PointCount].ToString("#0.0")
                                                                   + ACC + _routeBook.Accerlerate[_routeBook.PointCount].ToString()
                                                                   + DEC + _routeBook.Decerlerate[_routeBook.PointCount].ToString()
                                                                   + Environment.NewLine);

                                //Welding
                                _streamWriter.Write(ARCSET
                                                                    + AC
                                                                    + (_welding_A_RegNum.ToString()).PadLeft(3, '0')
                                                                    + " "
                                                                    + AVP
                                                                    + (_welding_V_RegNum.ToString()).PadLeft(3, '0')
                                                                    + Environment.NewLine);

                                
                                break;

                            #endregion


                            #region MovS Point

                            case 2:
                                _streamWriter.Write(MovS
                                                                   + PointInitial + (_routeBook.PointCount + _initialpoint).ToString().PadLeft(4, '0')
                                                                   + Override + _routeBook.Override[_routeBook.PointCount].ToString("#0.0")
                                                                   + ACC + _routeBook.Accerlerate[_routeBook.PointCount].ToString()
                                                                   + DEC + _routeBook.Decerlerate[_routeBook.PointCount].ToString()
                                                                   + Environment.NewLine);

                                //Welding
                                _streamWriter.Write(ARCSET
                                                                    + AC
                                                                    + (_welding_A_RegNum.ToString()).PadLeft(3, '0')
                                                                    + " "
                                                                    + AVP
                                                                    + (_welding_V_RegNum.ToString()).PadLeft(3, '0')
                                                                    + Environment.NewLine);
                               
                                break;

                                #endregion
                        }

                        _routeBook.PointCount++;
                        break;
                    #endregion

                    #region Dout 語法
                    case 2:
                        if (_routeBook.DOutMode[_routeBook.DoutCount] > 0)
                            _streamWriter.Write(DOUTInitial + _routeBook.DOutMode[_routeBook.DoutCount].ToString() + DOUTEnd + " ON" + Environment.NewLine);
                        else
                            _streamWriter.Write(DOUTInitial + (0 - _routeBook.DOutMode[_routeBook.DoutCount]).ToString() + DOUTEnd + " OFF" + Environment.NewLine);

                        _routeBook.DoutCount++;
                        break;

                    #endregion

                    #region 註解

                    case 3:
                        _streamWriter.Write(_routeBook.RobotCommand[_routeBook.RobotCommandCount] + Environment.NewLine);

                        _routeBook.RobotCommandCount++;
                        break;

                        #endregion


                }
            }
            //收弧
            _streamWriter.Write(ARCOF + Environment.NewLine);// CHECK Welding (ex.ARCOF)
            _streamWriter.Write(ProgramEnd);   //      <END>

            _streamWriter.Close();
            _streamWriter.Dispose();

            return 0;
        }

        /// <summary>
        /// 編寫JBI檔案 (多載-點位移動[速度]+焊接[電流/電壓])
        /// </summary>
        /// <param name="_routeBook"></param>
        /// <param name="_filename"></param>
        /// <param name="_initialpoint"></param>
        /// <returns></returns>
        public int CompileFile(RouteBook_Yaskawa _routeBook, string _filename, int _initialpoint,int _speed_RegNum, int _welding_A_RegNum, int _welding_V_RegNum)
        {
            CurrentState = "Tanslate to JBI format & upload to controller";

            string path = ".\\" + _filename + ".JBI";
            StreamWriter _streamWriter = new StreamWriter(path, false);
            _streamWriter.Write(JobInitial); // </JOB>
            _streamWriter.Write(FileName + _filename + Environment.NewLine); // <//NAME WedingProgrm>
            _streamWriter.Write(PoseInitial); // <//POS>
            _streamWriter.Write(PointNumber + "0,0,0,{0},0,0" + Environment.NewLine, _routeBook.PointNumber);   //  <///NPOS 0,0,0,256,0,0>
            if (_routeBook.Workspace != 0)
                _streamWriter.Write(UserGroup + _routeBook.Workspace.ToString() + Environment.NewLine);
            if (_routeBook.Workspace != 0)
                _streamWriter.Write(PoseType + "USER" + Environment.NewLine);    //   <///POSTYPE USER>
            else
                _streamWriter.Write(PoseType + "ROBOT" + Environment.NewLine);   //   <///POSTYPE ROBOT>
            if (_routeBook.FlipMode == 0)
                _streamWriter.Write(FirstDefineEnd + FlipMode);
            else
                _streamWriter.Write(FirstDefineEnd + NonFlipMode);  //   <///RCONF 1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0>

            //寫入點位數據(Data)    <ex. ///TOOL 0  P0001 = 265.320,187.550,-152.780,0.0000,0.0000,0.0000 >
            _routeBook.PointCount = 0;
            for (int i = 0; i < _routeBook.PointNumber; i++)
            {
                _streamWriter.Write(Tool + _routeBook.Tool[_routeBook.PointCount].ToString() + Environment.NewLine);

                _streamWriter.Write(PointInitial + (_routeBook.PointCount + _initialpoint).ToString().PadLeft(4, '0') + PointEnd
                                  + (Math.Round(_routeBook.X[_routeBook.PointCount], 3, MidpointRounding.AwayFromZero)).ToString("#0.000") + ","
                                  + (Math.Round(_routeBook.Y[_routeBook.PointCount], 3, MidpointRounding.AwayFromZero)).ToString("#0.000") + ","
                                  + (Math.Round(_routeBook.Z[_routeBook.PointCount], 3, MidpointRounding.AwayFromZero)).ToString("#0.000") + ","
                                  + (Math.Round(_routeBook.A[_routeBook.PointCount], 4, MidpointRounding.AwayFromZero)).ToString("#0.0000") + ","
                                  + (Math.Round(_routeBook.B[_routeBook.PointCount], 4, MidpointRounding.AwayFromZero)).ToString("#0.0000") + ","
                                  + (Math.Round(_routeBook.C[_routeBook.PointCount], 4, MidpointRounding.AwayFromZero)).ToString("#0.0000")
                                  + Environment.NewLine);

                _routeBook.PointCount++;
            }

            _streamWriter.Write(CommandInitial + DateTime.Now.ToString("yyyy/MM/dd HH:mm") + Environment.NewLine);  //    <//INST///DATE 2022/05/03 14:18>
            _streamWriter.Write(JobAttribute);    //    <///ATTR SC,RW,RJ>
            if (_routeBook.Workspace != 0)
                _streamWriter.Write(Frame + "USER " + _routeBook.Workspace.ToString() + Environment.NewLine);
            _streamWriter.Write(SecondDefineEnd); //      <///GROUP1 RB1   NOP>
            // 起始焊接(起弧)
            _streamWriter.Write(ARCON
                                       + AC
                                       + (_welding_A_RegNum.ToString()).PadLeft(3, '0')
                                       + " "
                                       + AVP
                                       + (_welding_V_RegNum.ToString()).PadLeft(3, '0')
                                       + Environment.NewLine);

            //寫入控制語法    <ex. MOVS P0001 V=10.0 ACC=70 DEC=70>
            _routeBook.CommandCount = 0;  //總計數
            _routeBook.PointCount = 0; //點位計數
            _routeBook.DoutCount = 0; //輸出計數

            for (_routeBook.CommandCount = 0; _routeBook.CommandCount < _routeBook.PointNumber + _routeBook.DoutNumber + _routeBook.RobotCommandNumber; _routeBook.CommandCount++)
            {
                switch (_routeBook.ProcessQueue[_routeBook.CommandCount])
                {
                    #region Point 語法
                    case 1:
                        switch (_routeBook.MovingMode[_routeBook.PointCount])
                        {
                            #region MovL Point
                            case 1:
                                _streamWriter.Write(MovL
                                                                   + PointInitial + (_routeBook.PointCount + _initialpoint).ToString().PadLeft(4, '0')
                                                                   + Override +"I"+(_speed_RegNum.ToString()).PadLeft(3, '0')
                                                                   + ACC + _routeBook.Accerlerate[_routeBook.PointCount].ToString()
                                                                   + DEC + _routeBook.Decerlerate[_routeBook.PointCount].ToString()
                                                                   + Environment.NewLine);

                                //Welding
                                _streamWriter.Write(ARCSET
                                                                    + AC
                                                                    + (_welding_A_RegNum.ToString()).PadLeft(3, '0')
                                                                    + " "
                                                                    + AVP
                                                                    + (_welding_V_RegNum.ToString()).PadLeft(3, '0')
                                                                    + Environment.NewLine);


                                break;

                            #endregion


                            #region MovS Point

                            case 2:
                                _streamWriter.Write(MovS
                                                                   + PointInitial + (_routeBook.PointCount + _initialpoint).ToString().PadLeft(4, '0')
                                                                   + Override + "I" + (_speed_RegNum.ToString()).PadLeft(3, '0')
                                                                   + ACC + _routeBook.Accerlerate[_routeBook.PointCount].ToString()
                                                                   + DEC + _routeBook.Decerlerate[_routeBook.PointCount].ToString()
                                                                   + Environment.NewLine);

                                //Welding
                                _streamWriter.Write(ARCSET
                                                                    + AC
                                                                    + (_welding_A_RegNum.ToString()).PadLeft(3, '0')
                                                                    + " "
                                                                    + AVP
                                                                    + (_welding_V_RegNum.ToString()).PadLeft(3, '0')
                                                                    + Environment.NewLine);

                                break;

                                #endregion
                        }

                        _routeBook.PointCount++;
                        break;
                    #endregion

                    #region Dout 語法
                    case 2:
                        if (_routeBook.DOutMode[_routeBook.DoutCount] > 0)
                            _streamWriter.Write(DOUTInitial + _routeBook.DOutMode[_routeBook.DoutCount].ToString() + DOUTEnd + " ON" + Environment.NewLine);
                        else
                            _streamWriter.Write(DOUTInitial + (0 - _routeBook.DOutMode[_routeBook.DoutCount]).ToString() + DOUTEnd + " OFF" + Environment.NewLine);

                        _routeBook.DoutCount++;
                        break;

                    #endregion

                    #region 註解

                    case 3:
                        _streamWriter.Write(_routeBook.RobotCommand[_routeBook.RobotCommandCount] + Environment.NewLine);

                        _routeBook.RobotCommandCount++;
                        break;

                        #endregion


                }
            }
            //收弧
            _streamWriter.Write(ARCOF + Environment.NewLine);// CHECK Welding (ex.ARCOF)
            _streamWriter.Write(ProgramEnd);   //      <END>

            _streamWriter.Close();
            _streamWriter.Dispose();

            return 0;
        }

        /// <summary>
        /// 上傳程式資料
        /// </summary>
        /// <param name="_filename"></param>
        /// <returns></returns>
        public int Upload2Controller(string _filename)
        {
            CurrentState = "Upload JBI file";

            Connect(RobotIP, "file");

            short[] _errorcode = new short[2];
            int _socketreturn1 = fesIF.DelFile(_filename + ".JBI", _errorcode);
            ExecuteError = _errorcode[0].ToString() + "," + _errorcode[1].ToString();

            int _socketreturn2 = fesIF.LoadFile(_filename + ".JBI", _errorcode);
            ExecuteError += "," + _errorcode[0].ToString() + "," + _errorcode[1].ToString();

            CurrentState = "Idle";

            Close();

            if (_socketreturn2 == 0)
                return 0;
            else
                return 1;
        }

        /// <summary>
        /// 程式內容啟動
        /// </summary>
        /// <param name="_filename"></param>
        /// <returns></returns>
        public int RunProgram(string _filename)
        {
            CurrentState = "Run Rpogram : " + _filename;

            Connect(RobotIP, "normal");

            short[] _errorcode = new short[2];
            int _socketreturn1 = fesIF.Cycle2Cycle(_errorcode); //循環模式
            ExecuteError = _errorcode[0].ToString() + "," + _errorcode[1].ToString();

            SelectJob _selectJob = new SelectJob();  //實作SelectJob()物件
            _selectJob.name = _filename;
            _selectJob.line = 0;
            int _socketreturn2 = fesIF.JobSelect(1, _selectJob, _errorcode); //選擇工作
            ExecuteError += "," + _errorcode[0].ToString() + "," + _errorcode[1].ToString();

            int _socketreturn3 = fesIF.JobStart(_errorcode); //啟動工作
            ExecuteError += "," + _errorcode[0].ToString() + "," + _errorcode[1].ToString();

            CurrentState = "Idle";

            Close();

            if (_socketreturn1 == 0 && _socketreturn2 == 0 && _socketreturn3 == 0)
                return 0;
            else
                return 1;
        }

        /// <summary>
        /// 下載程式資料
        /// </summary>
        /// <param name="_filename"></param>
        /// <returns></returns>
        public int DonwloadFile(string _filename)
        {
            Connect(RobotIP, "file");
            short[] _errorcode = new short[2];
            int _socketreturn1 = fesIF.SaveFile(_filename, _errorcode); //下載程式
            ExecuteError += "," + _errorcode[0].ToString() + "," + _errorcode[1].ToString();

            CurrentState = "Idle";
            Close();

            if (_socketreturn1 == 0) { return 0; }
            else { return 1; }
                
        }

        /// <summary>
        /// 取得程式資料表單
        /// </summary>
        /// <param name="_filename"></param>
        /// <returns></returns>
        public int ReadFileList(ref string _fileList)
        {
            Connect(RobotIP, "file");
            short[] _err_code = new short[2];
            int _socketreturn1 = fesIF.ListFile("*.JBI", _err_code, ref _fileList); //取得資料程式表單
            ExecuteError += "," + _err_code[0].ToString() + "," + _err_code[1].ToString();

            CurrentState = "Idle";
            Close();

            if (_socketreturn1 == 0) { return 0; }
            else { return 1; }

        }

        /// <summary>
        /// 刪除程式資料
        /// </summary>
        /// <param name="_filename"></param>
        /// <returns></returns>
        public int DeleteFile(string _filename)
        {
            Connect(RobotIP, "file");
            short[] _err_code = new short[2];
            int _socketreturn1 = fesIF.DelFile(_filename, _err_code); //刪除得資料程式
            ExecuteError += "," + _err_code[0].ToString() + "," + _err_code[1].ToString();

            CurrentState = "Idle";
            Close();

            if (_socketreturn1 == 0) { return 0; }
            else { return 1; }

        }


        #endregion

        #region 控制暫存器(R/W)

        #region Read 
        /// <summary>
        /// 讀取整數(批次)資料 (起始點位 , 讀取資料量 , ref 匯入陣列) --多載
        /// </summary>
        /// <param name="_startNum"></param>
        /// <param name="_readNum"></param>
        /// <param name="_IData"></param>
        /// <returns></returns>
        public int ReadIData(short _startNum, short _readNum, ref int[] _IData)
        {
            Connect(RobotIP, "normal");
            int[] _I_read = new int[_readNum]; //宣告讀取陣列
            short[] _errorcode = new short[2];
            int _socketreturn1 = fesIF.NumMultR(_startNum, _I_read, _errorcode); //讀取資料

            Close();

            for (int i = 0; i < _I_read.Length; i++)
            {
                _IData[i] = _I_read[i]; //依次寫入並輸出
            }

            if (_socketreturn1 == 0) { return 0; }
            else { return 1; }

        }

        /// <summary>
        /// 讀取整數(批次)資料 (起始點位 , 讀取資料量 , ref 匯入陣列) --多載
        /// </summary>
        /// <param name="_startNum"></param>
        /// <param name="_readNum"></param>
        /// <param name="_IData"></param>
        /// <returns></returns>
        public int ReadIData(short _startNum, short _readNum, ref short[] _IData)
        {
            Connect(RobotIP, "normal");
            short[] _I_read = new short[_readNum]; //宣告讀取陣列
            short[] _errorcode = new short[2];
            int _socketreturn1 = fesIF.NumMultR(_startNum, _I_read, _errorcode); //讀取資料


            Close();


            for (int i = 0; i < _I_read.Length; i++)
            {
                _IData[i] = _I_read[i]; //依次寫入並輸出
            }

            if (_socketreturn1 == 0) { return 0; }
            else { return 1; }

        }
        #endregion

        #region Write

        #region 批次
        /* 寫入參數設定 (Robot Controller)
        Robot Controller 須設定參數 (S2C0541=0 / S2C0542=0) 開放權限
        */

        /// <summary>
        /// 寫入整數(批次)資料 (起始點位 , 寫入資料陣列 ) --多載
        /// </summary>
        /// <param name="_startNum"></param>
        /// <param name="_IData"></param>
        /// <returns></returns>
        public int WriteIData(short _startNum, int[] _IData) 
        {
            Connect(RobotIP, "normal");
            int[] _I_write = new int[_IData.Length]; //宣告讀取陣列
            short[] _errorcode = new short[2];
            int _socketreturn1 = fesIF.NumMultW(_startNum, _I_write, _errorcode); //寫入資料
            
            Close();

            if (_socketreturn1 == 0) { return 0; }
            else { return 1; }
        }

        /// <summary>
        /// 寫入整數(批次)資料 (起始點位 , 寫入資料陣列 ) --多載
        /// </summary>
        /// <param name="_startNum"></param>
        /// <param name="_IData"></param>
        /// <returns></returns>
        public int WriteIData(short _startNum, short[] _IData) 
        {
            Connect(RobotIP, "normal");
            short[] _I_write = new short[_IData.Length]; //宣告讀取陣列
            short[] _errorcode = new short[2];
            int _WStartNum = _startNum;

            for (int i = 0; i < _I_write.Length; i++)
            {
                _I_write[i] = _IData[i];
            }

            int _socketreturn1 = fesIF.NumMultW(Convert.ToInt16(_WStartNum+1), _I_write, _errorcode); //寫入資料
            Console.WriteLine("result=0x{0:x2} error_code=0x{1:x4},0x{2:x4}", _socketreturn1, _errorcode[0], _errorcode[1]);

            Close();

            if (_socketreturn1 == 0) { return 0; }
            else { return 1; }
        
        }
        #endregion

        #region 單筆
        /// <summary>
        /// 寫入整數(單筆)資料 (起始點位 , 寫入資料) --多載
        /// </summary>
        /// <param name="_startNum"></param>
        /// <param name="_IData"></param>
        /// <returns></returns>
        public int WriteIData(short _startNum, short _IData) 
        {
            Connect(RobotIP, "normal");
            short[] _errorcode = new short[2];
            int _result = fesIF.NumSnglW(_startNum, _IData, _errorcode);

            Console.WriteLine("result=0x{0:x2} error_code=0x{1:x4},0x{2:x4}", _result, _errorcode[0], _errorcode[1]);

            Close();

            if (_result == 0) { return 0; }
            else { return 1; }

        }

        /// <summary>
        /// 寫入整數(單筆)資料 (起始點位 , 寫入資料) --多載
        /// </summary>
        /// <param name="_startNum"></param>
        /// <param name="_IData"></param>
        /// <returns></returns>
        public int WriteIData(short _startNum, int _IData)
        {
            Connect(RobotIP, "normal");
            short[] _errorcode = new short[2];
            int _result = fesIF.NumSnglW(_startNum, _IData, _errorcode);

            Console.WriteLine("result=0x{0:x2} error_code=0x{1:x4},0x{2:x4}", _result, _errorcode[0], _errorcode[1]);

            Close();

            if (_result == 0) { return 0; }
            else { return 1; }

        }

        /// <summary>
        /// 寫入整數(單筆)資料 (起始點位 , 寫入資料) --多載
        /// </summary>
        /// <param name="_startNum"></param>
        /// <param name="_IData"></param>
        /// <returns></returns>
        public int WriteIData(short _startNum, float _IData)
        {
            Connect(RobotIP, "normal");
            short[] _errorcode = new short[2];
            int _result = fesIF.NumSnglW(_startNum, _IData, _errorcode);

            Console.WriteLine("result=0x{0:x2} error_code=0x{1:x4},0x{2:x4}", _result, _errorcode[0], _errorcode[1]);

            Close();

            if (_result == 0) { return 0; }
            else { return 1; }

        }





        #endregion

        #endregion


        #endregion

        #region Position Point Data

        /// <summary>
        /// 取得(單筆)P[***]變數
        /// </summary>
        /// <param name="_PosNum"></param>
        /// <returns></returns>
        public int GetPosData(short _PosNum ,ref PosData _posData) 
        {
            
            int _result;
            PosData read_data = new PosData();
            short[] err_code = new short[2];

            Connect(RobotIP, "normal");
            _result = fesIF.PosSnglR(_PosNum, ref read_data, err_code);
            _posData = read_data;
            Close();

            Console.WriteLine("result=0x{0:x2} error_code=0x{1:x4},0x{2:x4}", _result, err_code[0], err_code[1]);
            Console.WriteLine("type={0},形態={1},tool number={2},user number={3},擴張形態={4},1st={5},2nd={6},3rd={7},4th={8},5th={9},6th={10},7th={11},8th={12}",
                                    read_data.type, read_data.pattern, read_data.tool_no, read_data.user_coord_no, read_data.ex_pattern,
                                    read_data.axis[0], read_data.axis[1], read_data.axis[2], read_data.axis[3],
                                    read_data.axis[4], read_data.axis[5], read_data.axis[6], read_data.axis[7]);


            if (_result == 0) { return 0; }
            else { return 1; }
        }


        /// <summary>
        /// Get Cur Pos Data
        /// </summary>
        /// <param name="_PosNum"></param>
        /// <param name="_posData"></param>
        /// <returns></returns>
        public int GetCurPosData(short _type, ref PosData _posData)
        {

            int _result;
            PosData read_data = new PosData();
            short[] err_code = new short[2];

            Connect(RobotIP, "normal");
            if (_type==0){ _result = fesIF.RobPosSnglR(101, ref read_data, err_code);}
            else { _result = fesIF.RobPosSnglR(1, ref read_data, err_code); }
            
            
            _posData = read_data;
            Close();
            /*
            Console.WriteLine("result=0x{0:x2} error_code=0x{1:x4},0x{2:x4}", _result, err_code[0], err_code[1]);
            Console.WriteLine("type={0},形態={1},tool number={2},user number={3},擴張形態={4},1st={5},2nd={6},3rd={7},4th={8},5th={9},6th={10},7th={11},8th={12}",
                                    read_data.type, read_data.pattern, read_data.tool_no, read_data.user_coord_no, read_data.ex_pattern,
                                    read_data.axis[0], read_data.axis[1], read_data.axis[2], read_data.axis[3],
                                    read_data.axis[4], read_data.axis[5], read_data.axis[6], read_data.axis[7]);

            */
            if (_result == 0) { return 0; }
            else { return 1; }
        }


        /// <summary>
        /// 寫入(單筆)P[***]變數
        /// </summary>
        /// <param name="_PosNum"></param>
        /// <param name="_posData"></param>
        /// <returns></returns>
        public int SetPosData(short _PosNum, PosData _posData)
        {

            int _result;
            short[] _err_code = new short[2];
            

            Connect(RobotIP, "normal");
            _result = fesIF.PosSnglW(_PosNum, _posData, _err_code);

            Close();

            Console.WriteLine("result=0x{0:x2} error_code=0x{1:x4},0x{2:x4}", _result, _err_code[0], _err_code[1]);


            if (_result == 0) { return 0; }
            else { return 1; }
        }

        /// <summary>
        /// Robot Move Pls (S,L,U,R,B,T) 各軸
        /// </summary>
        /// <param name="_pulseMoveData"></param>
        /// <returns></returns>
        public int RobotMovePls(PulseMove _pulseMoveData)
        {
            int _result;
            short[] _err_code = new short[2];
            Connect(RobotIP, "normal");

            _result = fesIF.MovePulse(1, _pulseMoveData, _err_code);

            Close();
            if (_result == 0) { return 0; }
            else { return 1; }
        }



        /// <summary>
        /// Robot Move Base (X,Y,Z,A,B,C) 基座標
        /// </summary>
        /// <param name="_pulseMoveData"></param>
        /// <returns></returns>
        public int RobotMoveBase(CoordMove _pulseMoveData)
        {
            int _result;
            short[] _err_code = new short[2];
            Connect(RobotIP, "normal");

            _result = fesIF.MoveCoord(1, _pulseMoveData, _err_code);

            Close();
            if (_result == 0) { return 0; }
            else { return 1; }

        }





        #endregion



        #region 其他控制

        /// <summary>
        /// 確認狀態
        /// </summary>
        /// <returns></returns>
        public int CheckStatus()
        {
            CurrentState = "Check status";
            Connect(RobotIP, "normal");

            //=================================== < _decode1 > ============================================

            short[] _errorcode = new short[2];
            uint _readData = 0;
            int _socketreturn1 = fesIF.StsElemR(1, ref _readData, _errorcode);
            ExecuteError = _errorcode[0].ToString() + "," + _errorcode[1].ToString();

            const int _mask1 = 1;
            var _binary1 = string.Empty;
            while (_readData > 0)
            {
                // Logical AND the number and prepend it to the result string
                _binary1 = (_readData & _mask1) + _binary1;
                _readData = _readData >> 1;
            }
            _binary1 = _binary1.PadLeft(8, '0');
            char[] _binaryarray1 = _binary1.ToCharArray();
            byte[] _decode1 = new byte[8];
            for (int i = 0; i < _binaryarray1.Length; i++) 
            { _decode1[i] = (byte)_binaryarray1[_binaryarray1.Length - 1 - i]; }


            Step = (_decode1[0] == 49) ? true : false;
            OneCycle = (_decode1[1] == 49) ? true : false;
            AutomaticAndContinuos = (_decode1[2] == 49) ? true : false;
            isBusy = (_decode1[3] == 49) ? true : false;
            InGuardSafeOperation = (_decode1[4] == 49) ? true : false;
            Teach = (_decode1[5] == 49) ? true : false;
            Play = (_decode1[6] == 49) ? true : false;
            CommendRemote = (_decode1[7] == 49) ? true : false;


            //=================================== < _decode2 > ============================================

            _errorcode = new short[2];
            _readData = 0;
            int _socketreturn2 = fesIF.StsElemR(2, ref _readData, _errorcode);
            ExecuteError += "," + _errorcode[0].ToString() + "," + _errorcode[1].ToString();

            const int _mask2 = 1;
            var _binary2 = string.Empty;
            while (_readData > 0)
            {
                // Logical AND the number and prepend it to the result string
                _binary2 = (_readData & _mask2) + _binary1;
                _readData = _readData >> 1;
            }
            _binary2 = _binary2.PadLeft(8, '0');
            char[] _binaryarray2 = _binary2.ToCharArray();
            byte[] _decode2 = new byte[8];
            for (int i = 0; i < _binaryarray1.Length; i++) { _decode2[i] = (byte)_binaryarray2[_binaryarray2.Length - 1 - i]; }
                

            HoldON = (_decode2[1] == 49) ? true : false;
            HoldStatusE = (_decode2[2] == 49) ? true : false;
            HoldStatusC = (_decode2[3] == 49) ? true : false;
            Alarm = (_decode2[4] == 49) ? true : false;
            Error = (_decode2[5] == 49) ? true : false;
            ServoON = (_decode2[6] == 49) ? true : false;

            //=============================================================================================

            CurrentState = "Idle";

            Close();

            if (_socketreturn1 == 0 && _socketreturn2 == 0) { return 0; }
            else { return 1; }
               
        }

        /// <summary>
        /// Alarm 清除
        /// </summary>
        /// <returns></returns>
        public int ResetAlarm()
        {
            CurrentState = "Reset alarm";

            Connect(RobotIP, "normal");

            short[] _errorcode = new short[2];
            int _socketreturn1 = fesIF.ErrorCancel(_errorcode);
            ExecuteError = _errorcode[0].ToString() + "," + _errorcode[1].ToString();

            int _socketreturn2 = fesIF.AlmReset(_errorcode);
            ExecuteError += "," + _errorcode[0].ToString() + "," + _errorcode[1].ToString();

            CurrentState = "Idle";

            Close();

            if (_socketreturn1 == 0 && _socketreturn2 == 0) { return 0; }
            else { return 1; }
                
        }

        /// <summary>
        /// 伺服激磁
        /// </summary>
        /// <param name="_index"></param>
        /// <returns></returns>
        public int ServoSwitch(int _index)//1 on ; 2 off
        {
            CurrentState = "Switch servo";

            Connect(RobotIP, "normal");

            short[] _errorcode = new short[2];
            int _socketreturn = fesIF.ServoSwitch(_index, _errorcode);
            ExecuteError = _errorcode[0].ToString() + "," + _errorcode[1].ToString();

            if (_socketreturn == 0 && _index == 1) { CurrentState = "Servo ON"; }
            else if (_socketreturn == 0 && _index == 2) { CurrentState = "Servo OFF"; }
                

            CurrentState = "Idle";

            Close();

            return _socketreturn;
        }

        /// <summary>
        /// 啟動/暫停
        /// </summary>
        /// <param name="_index"></param>
        /// <returns></returns>
        public int RobotStopSwitch(int _index) //1: ON 2: OFF
        {
            CurrentState = "Switch hold";

            Connect(RobotIP, "normal");

            short[] _errorcode = new short[2];
            int _socketreturn = fesIF.HoldSwitch(_index, _errorcode);
            ExecuteError = _errorcode[0].ToString() + "," + _errorcode[1].ToString();

            if (_socketreturn == 0 && _index == 1) { CurrentState = "Hold robot"; }    
            else if (_socketreturn == 0 && _index == 2) { CurrentState = "Robot free"; }
            
            CurrentState = "Idle";

            Close();

            return _socketreturn;
        }

        /// <summary>
        /// 取得各軸資料
        /// </summary>
        /// <param name="_index"></param>
        /// <param name="_x"></param>
        /// <param name="_y"></param>
        /// <param name="_z"></param>
        /// <param name="_a"></param>
        /// <param name="_b"></param>
        /// <param name="_c"></param>
        /// <returns></returns>
        public int GetPosture(short _index, out float _x, out float _y, out float _z, out float _a, out float _b, out float _c)//1 pulse ; 101 base
        {
            CurrentState = "Get Posture";

            Connect(RobotIP, "normal");

            short[] _errorcode = new short[2];

            PosData nowpos = new PosData();
            nowpos.type = 0;
            nowpos.pattern = 0;
            nowpos.tool_no = 0;
            nowpos.user_coord_no = 0;
            nowpos.ex_pattern = 0;

            int _socketreturn = fesIF.RobPosSnglR(_index, ref nowpos, _errorcode);
            ExecuteError = _errorcode[0].ToString() + "," + _errorcode[1].ToString();
            if (_socketreturn == 0 && _index == 1)
            {
                _x = Convert.ToSingle(nowpos.axis[0]);
                _y = Convert.ToSingle(nowpos.axis[1]);
                _z = Convert.ToSingle(nowpos.axis[2]);
                _a = Convert.ToSingle(nowpos.axis[3]);
                _b = Convert.ToSingle(nowpos.axis[4]);
                _c = Convert.ToSingle(nowpos.axis[5]);
            }
            else if (_socketreturn == 0 && _index == 101)
            {
                _x = Convert.ToSingle((double)nowpos.axis[0] / 1000);
                _y = Convert.ToSingle((double)nowpos.axis[1] / 1000);
                _z = Convert.ToSingle((double)nowpos.axis[2] / 1000);
                _a = Convert.ToSingle((double)nowpos.axis[3] / 10000);
                _b = Convert.ToSingle((double)nowpos.axis[4] / 10000);
                _c = Convert.ToSingle((double)nowpos.axis[5] / 10000);
            }
            else
            {
                _x = 0;
                _y = 0;
                _z = 0;
                _a = 0;
                _b = 0;
                _c = 0;
            }

            CurrentState = "Idle";

            Close();

            return _socketreturn;
        }

        #endregion

        #region 物件關閉
        /// <summary>
        /// 關閉Yaskawa物件
        /// </summary>
        /// <returns></returns>
        private int Close()
        {
            return fesIF.Close();
        }

        #endregion
    }
}
