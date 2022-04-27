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
        public bool Error = false, Alarm = false, isBusy = false, ServoON = false, HoldON = false;

        private string RobotIP;

        private FesIF fesIF = new FesIF();
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
                                DOUTEnd = ")";//DOUTInitial + number + DOUTEnd
        #endregion

        public int SaveFile(RouteBook_Yaskawa _routeBook, string _filename, int _initialpoint)
        {
            CurrentState = "Tanslate to JBI format & upload to controller";

            string path = ".\\" + _filename + ".JBI";
            StreamWriter _streamWriter = new StreamWriter(path, false);
            _streamWriter.Write(JobInitial);
            _streamWriter.Write(FileName + _filename + Environment.NewLine);
            _streamWriter.Write(PoseInitial);
            _streamWriter.Write(PointNumber + "0,0,0,{0},0,0" + Environment.NewLine, _routeBook.PointNumber);
            if (_routeBook.Workspace != 0)
                _streamWriter.Write(UserGroup + _routeBook.Workspace.ToString() + Environment.NewLine);
            if (_routeBook.Workspace != 0)
                _streamWriter.Write(PoseType + "USER" + Environment.NewLine);
            else
                _streamWriter.Write(PoseType + "ROBOT" + Environment.NewLine);
            if (_routeBook.FlipMode == 0)
                _streamWriter.Write(FirstDefineEnd + FlipMode);
            else
                _streamWriter.Write(FirstDefineEnd + NonFlipMode);

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

            _streamWriter.Write(CommandInitial + DateTime.Now.ToString("yyyy/MM/dd HH:mm") + Environment.NewLine);
            _streamWriter.Write(JobAttribute);
            if (_routeBook.Workspace != 0)
                _streamWriter.Write(Frame + "USER " + _routeBook.Workspace.ToString() + Environment.NewLine);
            _streamWriter.Write(SecondDefineEnd);

            _routeBook.CommandCount = 0;
            _routeBook.PointCount = 0;
            _routeBook.DoutCount = 0;
            for (_routeBook.CommandCount = 0; _routeBook.CommandCount < _routeBook.PointNumber + _routeBook.DoutNumber + _routeBook.RobotCommandNumber; _routeBook.CommandCount++)
            {
                switch (_routeBook.ProcessQueue[_routeBook.CommandCount])
                {
                    case 1:
                        switch (_routeBook.MovingMode[_routeBook.PointCount])
                        {
                            case 1:
                                _streamWriter.Write(MovL
                                                                   + PointInitial + (_routeBook.PointCount + _initialpoint).ToString().PadLeft(4, '0')
                                                                   + Override + _routeBook.Override[_routeBook.PointCount].ToString("#0.0")
                                                                   + ACC + _routeBook.Accerlerate[_routeBook.PointCount].ToString()
                                                                   + DEC + _routeBook.Decerlerate[_routeBook.PointCount].ToString()
                                                                   + Environment.NewLine);
                                break;
                            case 2:
                                _streamWriter.Write(MovS
                                                                   + PointInitial + (_routeBook.PointCount + _initialpoint).ToString().PadLeft(4, '0')
                                                                   + Override + _routeBook.Override[_routeBook.PointCount].ToString("#0.0")
                                                                   + ACC + _routeBook.Accerlerate[_routeBook.PointCount].ToString()
                                                                   + DEC + _routeBook.Decerlerate[_routeBook.PointCount].ToString()
                                                                   + Environment.NewLine);
                                break;
                        }

                        _routeBook.PointCount++;
                        break;
                    case 2:
                        if (_routeBook.DOutMode[_routeBook.DoutCount] > 0)
                            _streamWriter.Write(DOUTInitial + _routeBook.DOutMode[_routeBook.DoutCount].ToString() + DOUTEnd + " ON" +  Environment.NewLine);
                        else
                            _streamWriter.Write(DOUTInitial + (0 - _routeBook.DOutMode[_routeBook.DoutCount]).ToString() + DOUTEnd + " OFF" + Environment.NewLine);

                        _routeBook.DoutCount++;
                        break;
                    case 3:
                        _streamWriter.Write(_routeBook.RobotCommand[_routeBook.RobotCommandCount] + Environment.NewLine);

                        _routeBook.RobotCommandCount++;
                        break;
                }
            }

            _streamWriter.Write(ProgramEnd);

            _streamWriter.Close();
            _streamWriter.Dispose();

            return 0;
        }

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

        public int CheckStatus()
        {
            CurrentState = "Check status";

            Connect(RobotIP, "normal");

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
                _decode1[i] = (byte)_binaryarray1[_binaryarray1.Length - 1 - i];

            isBusy = (_decode1[3] == 49) ? true : false;

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
            for (int i = 0; i < _binaryarray1.Length; i++)
                _decode2[i] = (byte)_binaryarray2[_binaryarray2.Length - 1 - i];

            HoldON = (_decode2[1] == 49) ? true : false;
            Alarm = (_decode2[4] == 49) ? true : false;
            Error = (_decode2[5] == 49) ? true : false;
            ServoON = (_decode2[6] == 49) ? true : false;

            CurrentState = "Idle";

            Close();

            if (_socketreturn1 == 0 && _socketreturn2 == 0)
                return 0;
            else
                return 1;
        }

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

            if (_socketreturn1 == 0 && _socketreturn2 == 0)
                return 0;
            else
                return 1;
        }

        public int RunProgram(string _filename)
        {
            CurrentState = "Run Rpogram : " + _filename;

            Connect(RobotIP, "normal");

            short[] _errorcode = new short[2];
            int _socketreturn1 = fesIF.Cycle2Cycle(_errorcode);
            ExecuteError = _errorcode[0].ToString() + "," + _errorcode[1].ToString();

            SelectJob _selectJob = new SelectJob();
            _selectJob.name = _filename;
            _selectJob.line = 0;
            int _socketreturn2 = fesIF.JobSelect(1, _selectJob, _errorcode);
            ExecuteError += "," + _errorcode[0].ToString() + "," + _errorcode[1].ToString();

            int _socketreturn3 = fesIF.JobStart(_errorcode);
            ExecuteError += "," + _errorcode[0].ToString() + "," + _errorcode[1].ToString();

            CurrentState = "Idle";

            Close();

            if (_socketreturn1 == 0 && _socketreturn2 == 0 && _socketreturn3 == 0)
                return 0;
            else
                return 1;
        }

        public int ServoSwitch(int _index)//1 on ; 2 off
        {
            CurrentState = "Switch servo";

            Connect(RobotIP, "normal");

            short[] _errorcode = new short[2];
            int _socketreturn = fesIF.ServoSwitch(_index, _errorcode);
            ExecuteError = _errorcode[0].ToString() + "," + _errorcode[1].ToString();

            if (_socketreturn == 0 && _index == 1)
                CurrentState = "Servo ON";
            else if (_socketreturn == 0 && _index == 2)
                CurrentState = "Servo OFF";

            CurrentState = "Idle";

            Close();

            return _socketreturn;
        }

        public int HoldSwitch(int _index)//1 hold ; 2 free
        {
            CurrentState = "Switch hold";

            Connect(RobotIP, "normal");

            short[] _errorcode = new short[2];
            int _socketreturn = fesIF.HoldSwitch(_index, _errorcode);
            ExecuteError = _errorcode[0].ToString() + "," + _errorcode[1].ToString();

            if (_socketreturn == 0 && _index == 1)
                CurrentState = "Hold robot";
            else if (_socketreturn == 0 && _index == 2)
                CurrentState = "Robot free";

            CurrentState = "Idle";

            Close();

            return _socketreturn;
        }

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

        private int Close()
        {
            return fesIF.Close();
        }
    }
}
