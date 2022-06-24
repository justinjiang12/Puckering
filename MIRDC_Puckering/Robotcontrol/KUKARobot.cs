/******************************************************************************
;// This material is the exclusive property of KUKA Roboter GmbH.
;// Except as expressly permitted by separate agreement, this material may only
;// be used by members of the development department of KUKA Roboter GmbH for
;// internal development purposes of KUKA Roboter GmbH.
;//
;// Copyright (C) 2016
;// KUKA Roboter GmbH, Germany. All Rights Reserved.
;//
/*****************************************************************************/

#region usings
using System;
using System.Drawing;
using System.Globalization;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;
using MxA;
//using MxAutomation_Example.Properties;

#endregion

namespace MxAutomation_Example
{
    public partial class MainView : Form
    {
        #region define variables
        //Global variables
        private readonly short _axisGroupIdx = 1;
        private bool _confirmMessages;

        //Miscellany     
        private int _counter;
        private Point _mouseposition;
        private bool _moveToPos1;
        private bool _moveToPos2;
        private long _myCycle;
        private short _overrideToSet;
        private readonly E6AXIS _position1;
        private readonly E6AXIS _position2;
        private IPEndPoint _receiveEndPoint;
        //Justin
        private bool _jogState = false;
        private short _moveType=0;
        private string _jogAxis = "";

        //Byte arrays for the communication
        private byte[] _krc4Input;
        private readonly byte[] _krc4Output;

        //Function blocks
        private readonly KRC_ERROR _mxAError;
        private readonly KRC_INITIALIZE _mxAInit;
        private readonly KRC_ABORT _mxAKrcAbort;
        private readonly KRC_AUTOMATICEXTERNAL _mxAKrcAutomaticExternal;
        private readonly KRC_READACTUALAXISPOSITION _mxAKrcReadactualaxisposition;
        private readonly KRC_SETOVERRIDE _mxAKrcSetOverride;
        private readonly KRC_MOVEAXISABSOLUTE _mxAMoveAxisAbsolute1;
        private readonly KRC_MOVEAXISABSOLUTE _mxAMoveAxisAbsolute2;
        private readonly KRC_READAXISGROUP _mxARag;
        private readonly KRC_WRITEAXISGROUP _mxAWag;
        private readonly KRC_DIAG _mxAKrcDiag;
        private readonly KRC_AUTOSTART _autoStart;
        //justin
        private readonly KRC_JOG _mxJog;

        //Justin
        /// <summary>
        /// 坐標系定義
        /// </summary>
        public COORDSYS cORS_COR = new COORDSYS
        {
            BASE = 2, //基座標
            IPO_MODE = 0, //IPO_MODE
            TOOL = 2 //工具座標
        };


        //Communication objects
        private readonly UdpClient _receiveSocket;
        private UdpClient _sendSocket;
        private int _recvTimeout;
        private bool _reset;
        private int _resetCount;


        #endregion

        #region init Maint View
        public MainView()
        {
            InitializeComponent();

            // init recv socket
            _receiveEndPoint = new IPEndPoint(IPAddress.Any, 1337);
            _receiveSocket = new UdpClient(_receiveEndPoint) { Client = { ReceiveTimeout = 10 } };

            // init send socket          
            _sendSocket = new UdpClient();

            //Initialize the function blocks
            _mxARag = new KRC_READAXISGROUP();
            _mxAWag = new KRC_WRITEAXISGROUP();
            _mxAInit = new KRC_INITIALIZE();
            _mxAKrcSetOverride = new KRC_SETOVERRIDE();
            _mxAKrcAutomaticExternal = new KRC_AUTOMATICEXTERNAL();
            _mxAMoveAxisAbsolute1 = new KRC_MOVEAXISABSOLUTE();
            _mxAMoveAxisAbsolute2 = new KRC_MOVEAXISABSOLUTE();
            _mxAError = new KRC_ERROR();
            _mxAKrcReadactualaxisposition = new KRC_READACTUALAXISPOSITION();
            _mxAKrcAbort = new KRC_ABORT();
            _mxAKrcDiag = new KRC_DIAG();
            _autoStart = new KRC_AUTOSTART();
            //justin
            _mxJog = new KRC_JOG();


            //IO arrays
            _krc4Output = new byte[256];
            _krc4Input = new byte[256];

            //Initialize miscellany       
            _counter = 0;
            _confirmMessages = false;
            _reset = false;
            _overrideToSet = 10;
            _moveToPos1 = false;
            _moveToPos2 = false;
            _position1 = new E6AXIS
            {
                A1 = (float)Convert.ToDouble(textBox1.Text, CultureInfo.InvariantCulture),
                A2 = (float)Convert.ToDouble(textBox2.Text, CultureInfo.InvariantCulture),
                A3 = (float)Convert.ToDouble(textBox3.Text, CultureInfo.InvariantCulture),
                A4 = (float)Convert.ToDouble(textBox4.Text, CultureInfo.InvariantCulture),
                A5 = (float)Convert.ToDouble(textBox5.Text, CultureInfo.InvariantCulture),
                A6 = (float)Convert.ToDouble(textBox6.Text, CultureInfo.InvariantCulture)


            };
            _position2 = new E6AXIS
            {
                A1 = (float)Convert.ToDouble(textBox12.Text, CultureInfo.InvariantCulture),
                A2 = (float)Convert.ToDouble(textBox11.Text, CultureInfo.InvariantCulture),
                A3 = (float)Convert.ToDouble(textBox10.Text, CultureInfo.InvariantCulture),
                A4 = (float)Convert.ToDouble(textBox9.Text, CultureInfo.InvariantCulture),
                A5 = (float)Convert.ToDouble(textBox8.Text, CultureInfo.InvariantCulture),
                A6 = (float)Convert.ToDouble(textBox7.Text, CultureInfo.InvariantCulture)


            };

            GLOBAL.KRC_AXISGROUPREFARR[_axisGroupIdx].HEARTBEATTO = 2000;
        }
        #endregion

        #region mxAutomation functionblocks
        private void cycle_Tick(object sender, EventArgs e)
        {
            try
            {
                ReadAxisGroup();

                

                // Initialize
                _mxAInit.AXISGROUPIDX = _axisGroupIdx;
                _mxAInit.OnCycle();
                if (_mxAInit.ERROR)
                {
                    Debug("Error by KRC_INITIALIZE: " + _mxAInit.ERRORID);
                    return;
                }

                _autoStart.AXISGROUPIDX = _axisGroupIdx;
                
                _autoStart.EXECUTERESET = _reset;

                //Automatic external
                _mxAKrcAutomaticExternal.AXISGROUPIDX = _axisGroupIdx;
                _mxAKrcAutomaticExternal.MOVE_ENABLE = cbMoveEnable.Checked;
                _mxAKrcAutomaticExternal.DRIVES_ON = false;
                _mxAKrcAutomaticExternal.DRIVES_OFF = false;
                _mxAKrcAutomaticExternal.RESET = _reset;
                _mxAKrcAutomaticExternal.ENABLE_T1 = true;
                _mxAKrcAutomaticExternal.ENABLE_EXT = true;
                _mxAKrcAutomaticExternal.OnCycle();

                //Read errors
                _mxAError.AXISGROUPIDX = _axisGroupIdx;
                _mxAError.MESSAGERESET = _confirmMessages;
                _mxAError.OnCycle();

                lErrorID.Text = _mxAError.ERRORID.ToString();

                //Set override
                _mxAKrcSetOverride.AXISGROUPIDX = _axisGroupIdx;
                _mxAKrcSetOverride.OVERRIDE = _overrideToSet;
                _mxAKrcSetOverride.OnCycle();
                
                if (_reset)
                {
                    _moveToPos1 = false;
                    _moveToPos2 = false;
                    _mxAKrcAbort.OnCycle();
                }
                else if (_resetCount == 0)
                {
                    _confirmMessages = false;

                    //Move to position 1
                    _mxAMoveAxisAbsolute1.AXISGROUPIDX = _axisGroupIdx;
                    _mxAMoveAxisAbsolute1.EXECUTECMD = _moveToPos1;
                    _mxAMoveAxisAbsolute1.AXISPOSITION = _position1;
                    _mxAMoveAxisAbsolute1.VELOCITY = 0;
                    _mxAMoveAxisAbsolute1.ACCELERATION = 0;
                    _mxAMoveAxisAbsolute1.BUFFERMODE = 2;
                    _mxAMoveAxisAbsolute1.OnCycle();

                    

                    if (_mxAMoveAxisAbsolute1.DONE)
                    {
                        _moveToPos1 = false;
                    }

                    //Move to position 2
                    _mxAMoveAxisAbsolute2.AXISGROUPIDX = _axisGroupIdx;
                    _mxAMoveAxisAbsolute2.EXECUTECMD = _moveToPos2;
                    _mxAMoveAxisAbsolute2.AXISPOSITION = _position2;
                    _mxAMoveAxisAbsolute2.VELOCITY = 0;
                    _mxAMoveAxisAbsolute2.ACCELERATION = 0;
                    _mxAMoveAxisAbsolute2.BUFFERMODE = 2;
                    _mxAMoveAxisAbsolute2.OnCycle();









                    //Jog <justin>
                    
                    if (_jogState){jogStart(_jogAxis);}else{jogStop(_jogAxis);}
                    


                    /*
                    if (_jogState)
                    {
                        _mxJog.AXISGROUPIDX = _axisGroupIdx;
                        if (radioButton1.Checked == true) { _moveType = 1; } else { _moveType = 0; }
                        _mxJog.MOVETYPE = _moveType;
                        _mxJog.VELOCITY = 0;
                        _mxJog.ACCELERATION = 0;
                        _mxJog.COORDINATESYSTEM = cORS_COR;
                        _mxJog.INCREMENT = 0;
                        _mxJog.A1_X_P = true;
                        _mxJog.OnCycle();
                    }
                    else
                    {
                        _mxJog.AXISGROUPIDX = _axisGroupIdx;
                        if (radioButton1.Checked == true) { _moveType = 1; } else { _moveType = 0; }
                        _mxJog.MOVETYPE = _moveType;
                        _mxJog.VELOCITY = 0;
                        _mxJog.ACCELERATION = 0;
                        _mxJog.COORDINATESYSTEM = cORS_COR;
                        _mxJog.INCREMENT = 0;
                        _mxJog.A1_X_P = false;
                        _mxJog.OnCycle();
                    }

                    */





                    if (_mxAMoveAxisAbsolute2.DONE)
                    {
                        _moveToPos2 = false;
                    }
                }

                WriteAxisGroup();

                if (!_reset && _resetCount == 0) return;

                if (_mxAKrcAutomaticExternal.RC_RDY1 && _mxAKrcAutomaticExternal.PRO_ACT)
                {
                    _reset = false;
                    _resetCount = 0;
                    bReset.BackColor = Color.Empty;
                }
                else
                {
                    _confirmMessages = true;

                    _resetCount++;

                    if (_resetCount > 50)
                    {
                        _resetCount = -50;
                        _reset = false;
                        bReset.BackColor = Color.FromArgb(225, 31, 37);
                    }
                    if (_resetCount == -1)
                    {
                        _resetCount = 1;
                        _reset = true;
                        bReset.BackColor = Color.FromArgb(123, 182, 84);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug("exception: " + ex.Message);
                Debug(ex.StackTrace);
            }
        }

        private void ReadAxisGroup()
        {
            byte[] buffer = null;

            try
            {
                if (_receiveSocket.Available > 0)
                {
                    buffer = _receiveSocket.Receive(ref _receiveEndPoint);
                    _recvTimeout = 0;

                    //if (Icon != Resources.Robgreen) Icon = Resources.Robgreen;
                }
                else
                {
                    _recvTimeout++;
                    if (_recvTimeout > 50)
                    {
                        Debug("don't receive data from robot");
                        _recvTimeout = 0;
                        _myCycle = 0;
                        //Icon = Resources.Robyellow;
                    }
                }
            }
            catch (SocketException se)
            {
                Debug("Socket exception in readAxisGroup with the message: " + se.Message);
            }

            if (buffer != null && buffer.Length >= 256)
            {
                _krc4Input = buffer;
            }

            _mxARag.KRC4_INPUT = _krc4Input;
            _mxARag.AXISGROUPIDX = _axisGroupIdx;
            _mxARag.OnCycle();

            _mxAKrcReadactualaxisposition.AXISGROUPIDX = _axisGroupIdx;
            _mxAKrcReadactualaxisposition.OnCycle();

            if (_mxARag.ERROR)
            {
                Debug("Error by Read Axis Group function block: " + _mxARag.ERRORID);
                tbAxisGroupRef.Clear();
                return;
            }

            //KRC_DIAG
            _mxAKrcDiag.AXISGROUPIDX = _axisGroupIdx;
            _mxAKrcDiag.OnCycle();
            bool diag_err = _mxAKrcDiag.ERROR;
            int diag_err_id = _mxAKrcDiag.ERRORID;
            int diag_err_pcos = _mxAKrcDiag.ERRORID_PCOS;
            int diag_err_plc = _mxAKrcDiag.ERRORID_PLC;
            int diag_err_ri = _mxAKrcDiag.ERRORID_RI;
            int diag_err_si = _mxAKrcDiag.ERRORID_SI;

            var myStr = "";
            myStr = myStr + "Initialized        :" +
                    GLOBAL.KRC_AXISGROUPREFARR[_axisGroupIdx].INITIALIZED.ToString(CultureInfo.InvariantCulture) +
                    Environment.NewLine;
            myStr = myStr + "Online             :" +
                    GLOBAL.KRC_AXISGROUPREFARR[_axisGroupIdx].ONLINE.ToString(CultureInfo.InvariantCulture) +
                    Environment.NewLine;
            myStr = myStr + "Last orderID       :" +
                    GLOBAL.KRC_AXISGROUPREFARR[_axisGroupIdx].LASTORDERID.ToString(CultureInfo.InvariantCulture) +
                    Environment.NewLine;
            myStr = myStr + "Read axisGroupInit :" +
                    GLOBAL.KRC_AXISGROUPREFARR[_axisGroupIdx].READAXISGROUPINIT.ToString(CultureInfo.InvariantCulture) +
                    Environment.NewLine;
            myStr = myStr + "Read done          :" +
                    GLOBAL.KRC_AXISGROUPREFARR[_axisGroupIdx].READDONE.ToString(CultureInfo.InvariantCulture) +
                    Environment.NewLine + Environment.NewLine;

            myStr = myStr + "error              :" +
                    diag_err.ToString(CultureInfo.InvariantCulture) +
                    Environment.NewLine;
            myStr = myStr + "int_error ID       :" +
                    diag_err_id.ToString(CultureInfo.InvariantCulture) +
                    Environment.NewLine;
            myStr = myStr + "int_FBerror ID     :" +
                    diag_err_plc.ToString(CultureInfo.InvariantCulture) +
                    Environment.NewLine;
            myStr = myStr + "KRC_PLC error ID   :" +
                    diag_err_pcos.ToString(CultureInfo.InvariantCulture) +
                    Environment.NewLine;
            myStr = myStr + "KRC_ROB error ID   :" +
                    diag_err_ri.ToString(CultureInfo.InvariantCulture) +
                    Environment.NewLine;
            myStr = myStr + "KRC_SUB error ID   :" +
                    diag_err_si.ToString(CultureInfo.InvariantCulture) +
                    Environment.NewLine + Environment.NewLine;

            myStr = myStr + "Recv.  Timeout[ms] :" +
                    GLOBAL.KRC_AXISGROUPREFARR[_axisGroupIdx].HEARTBEATTO.ToString(CultureInfo.InvariantCulture) +
                    Environment.NewLine;
            myStr = myStr + "PLC Version        :" +
                    GLOBAL.KRC_AXISGROUPREFARR[_axisGroupIdx].PLC_MAJOR.ToString(CultureInfo.InvariantCulture);
            myStr = myStr + "." +
                    GLOBAL.KRC_AXISGROUPREFARR[_axisGroupIdx].PLC_MINOR.ToString(CultureInfo.InvariantCulture) +
                    Environment.NewLine;
            myStr = myStr + "DEF VEL CP         :" +
                    GLOBAL.KRC_AXISGROUPREFARR[_axisGroupIdx].DEF_VEL_CP.ToString(CultureInfo.InvariantCulture) +
                    Environment.NewLine;
            myStr = myStr + "Axis Position A1   :" +
                    _mxAKrcReadactualaxisposition.A1.ToString("0.0", CultureInfo.InvariantCulture) +
                    Environment.NewLine;
            myStr = myStr + "Axis Position A2   :" +
                    _mxAKrcReadactualaxisposition.A2.ToString("0.0", CultureInfo.InvariantCulture) +
                    Environment.NewLine;
            myStr = myStr + "Axis Position A3   :" +
                    _mxAKrcReadactualaxisposition.A3.ToString("0.0", CultureInfo.InvariantCulture) +
                    Environment.NewLine;
            myStr = myStr + "Axis Position A4   :" +
                    _mxAKrcReadactualaxisposition.A4.ToString("0.0", CultureInfo.InvariantCulture) +
                    Environment.NewLine;
            myStr = myStr + "Axis Position A5   :" +
                    _mxAKrcReadactualaxisposition.A5.ToString("0.0", CultureInfo.InvariantCulture) +
                    Environment.NewLine;
            myStr = myStr + "Axis Position A6   :" +
                    _mxAKrcReadactualaxisposition.A6.ToString("0.0", CultureInfo.InvariantCulture) +
                    Environment.NewLine + Environment.NewLine;

            myStr = myStr + "Lifecounter (" + _myCycle++ + ")";
            tbAxisGroupRef.Text = myStr;
        }

        private void WriteAxisGroup()
        {
            _mxAWag.AXISGROUPIDX = _axisGroupIdx;
            _mxAWag.KRC4_OUTPUT = _krc4Output;
            _mxAWag.OnCycle();

            _sendSocket.Send(_krc4Output, _krc4Output.Length);
        }
        #endregion

        #region Click handler to windows forms
        private void bStart_Click(object sender, EventArgs e)
        {
            //Icon = Resources.Robyellow;
            _myCycle = 0;

            //Initialize communication
            try
            {
                IPAddress ipAddress;
                if (IPAddress.TryParse(textBox13.Text, out ipAddress))
                {
                    var sendEndPoint = new IPEndPoint(ipAddress, 1336);
                    _sendSocket.Connect(sendEndPoint);
                    cycle.Enabled = true;
                }
                else
                {
                    Debug("IP " + textBox13.Text + " not valid");
                }
            }
            catch (Exception expt)
            {
                Debug(expt.Message);
                _sendSocket.Close();
                _sendSocket = new UdpClient();
            }
        }

        private void bStop_Click(object sender, EventArgs e)
        {
            cycle.Enabled = false;
            //Icon = Resources.Rob;
        }

        private void tbOverride_Scroll(object sender, EventArgs e)
        {
            _overrideToSet = (short)tbOverride.Value;
        }

        private void bMoveToPos1_Click(object sender, EventArgs e)
        {
            try
            {
                _position1.A1 = (float)Convert.ToDouble(textBox1.Text, CultureInfo.InvariantCulture);
                _position1.A2 = (float)Convert.ToDouble(textBox2.Text, CultureInfo.InvariantCulture);
                _position1.A3 = (float)Convert.ToDouble(textBox3.Text, CultureInfo.InvariantCulture);
                _position1.A4 = (float)Convert.ToDouble(textBox4.Text, CultureInfo.InvariantCulture);
                _position1.A5 = (float)Convert.ToDouble(textBox5.Text, CultureInfo.InvariantCulture);
                _position1.A6 = (float)Convert.ToDouble(textBox6.Text, CultureInfo.InvariantCulture);
                
            }
            catch (Exception se)
            {
                Debug("Text conversation: " + se.Message);
            }
            _moveToPos1 = true;
        }

        private void bMoveToPos2_Click(object sender, EventArgs e)
        {
            try
            {
                _position2.A1 = (float)Convert.ToDouble(textBox12.Text, CultureInfo.InvariantCulture);
                _position2.A2 = (float)Convert.ToDouble(textBox11.Text, CultureInfo.InvariantCulture);
                _position2.A3 = (float)Convert.ToDouble(textBox10.Text, CultureInfo.InvariantCulture);
                _position2.A4 = (float)Convert.ToDouble(textBox9.Text, CultureInfo.InvariantCulture);
                _position2.A5 = (float)Convert.ToDouble(textBox8.Text, CultureInfo.InvariantCulture);
                _position2.A6 = (float)Convert.ToDouble(textBox7.Text, CultureInfo.InvariantCulture);
            }
            catch (Exception se)
            {
                Debug("Text conversation: " + se.Message);
            }
            _moveToPos2 = true;
        }



        // Click on button "RESET...."
        private void bReset_Click(object sender, EventArgs e)
        {
            _reset = true;
        }
        #endregion


        #region justin test form

        #region Justin Jog test


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

                #region JOG (A1_X_+)
                case "A1_X_+":

                    try
                    {
                        _jogAxis = "A1_X_P";
                    }
                    catch (Exception x) { MessageBox.Show(x.ToString(), "systen error!!!"); }

                    break;
                #endregion

                #region JOG (A1_X_-)
                case "A1_X_-":

                    try
                    {
                        _jogAxis = "A1_X_M";
                        

                    }
                    catch (Exception x) { MessageBox.Show(x.ToString(), "systen error!!!"); }

                    break;
                #endregion

                #region JOG (A2_Y_+)
                case "A2_Y_+":

                    try
                    {
                        _jogAxis = "A2_Y_P";
                    }
                    catch (Exception x) { MessageBox.Show(x.ToString(), "systen error!!!"); }

                    break;
                #endregion

                #region JOG (A2_Y_-)
                case "A2_Y_-":

                    try
                    {
                        _jogAxis = "A2_Y_M";
                    }
                    catch (Exception x) { MessageBox.Show(x.ToString(), "systen error!!!"); }

                    break;
                #endregion

                #region JOG (A3_Z_+)
                case "A3_Z_+":

                    try
                    {
                        _jogAxis = "A3_Z_P";
                    }
                    catch (Exception x) { MessageBox.Show(x.ToString(), "systen error!!!"); }

                    break;
                #endregion

                #region JOG (A3_Z_-)
                case "A3_Z_-":

                    try
                    {
                        _jogAxis = "A3_Z_M";
                    }
                    catch (Exception x) { MessageBox.Show(x.ToString(), "systen error!!!"); }

                    break;
                #endregion

                #region JOG (A4_RX_+)
                case "A4_RX_+":

                    try
                    {
                        _jogAxis = "A4_A_P";
                    }
                    catch (Exception x) { MessageBox.Show(x.ToString(), "systen error!!!"); }

                    break;
                #endregion

                #region JOG (A4_RX_-)
                case "A4_RX_-":

                    try
                    {
                        _jogAxis = "A4_A_M";
                    }
                    catch (Exception x) { MessageBox.Show(x.ToString(), "systen error!!!"); }

                    break;
                #endregion

                #region JOG (A5_RY_+)
                case "A5_RY_+":

                    try
                    {
                        _jogAxis = "A5_B_P";
                    }
                    catch (Exception x) { MessageBox.Show(x.ToString(), "systen error!!!"); }

                    break;
                #endregion

                #region JOG (A5_RY_-)
                case "A5_RY_-":

                    try
                    {
                        _jogAxis = "A5_B_M";
                    }
                    catch (Exception x) { MessageBox.Show(x.ToString(), "systen error!!!"); }

                    break;
                #endregion


                #region JOG (A6_RZ_+)
                case "A6_RZ_+":

                    try
                    {
                        _jogAxis = "A6_C_P";
                    }
                    catch (Exception x) { MessageBox.Show(x.ToString(), "systen error!!!"); }

                    break;
                #endregion

                #region JOG (A3_Z_-)
                case "A6_RZ_-":

                    try
                    {
                        _jogAxis = "A6_C_M";
                    }
                    catch (Exception x) { MessageBox.Show(x.ToString(), "systen error!!!"); }

                    break;
                #endregion

                #region 測試按鈕
                case "test_btn":

                    try
                    {

                    }
                    catch (Exception x) { MessageBox.Show(x.ToString(), "systen error!!!"); }


                    break;
                    #endregion

            }
            _jogState = true;
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

                #region JOG (A1_X_+)
                case "A1_X_+":

                    try
                    {
                        _jogAxis = "A1_X_P";
                    }
                    catch (Exception x) { MessageBox.Show(x.ToString(), "systen error!!!"); }

                    break;
                #endregion

                #region JOG (A1_X_-)
                case "A1_X_-":

                    try
                    {
                        _jogAxis = "A1_X_M";
                    }
                    catch (Exception x) { MessageBox.Show(x.ToString(), "systen error!!!"); }

                    break;
                #endregion

                #region JOG (A2_Y_+)
                case "A2_Y_+":

                    try
                    {
                        _jogAxis = "A2_Y_P";
                    }
                    catch (Exception x) { MessageBox.Show(x.ToString(), "systen error!!!"); }

                    break;
                #endregion

                #region JOG (A2_Y_-)
                case "A2_Y_-":

                    try
                    {
                        _jogAxis = "A2_Y_M";
                    }
                    catch (Exception x) { MessageBox.Show(x.ToString(), "systen error!!!"); }

                    break;
                #endregion


                #region JOG (A3_Z_+)
                case "A3_Z_+":

                    try
                    {
                        _jogAxis = "A3_Z_P";
                    }
                    catch (Exception x) { MessageBox.Show(x.ToString(), "systen error!!!"); }

                    break;
                #endregion

                #region JOG (A3_Z_-)
                case "A3_Z_-":

                    try
                    {
                        _jogAxis = "A3_Z_M";
                    }
                    catch (Exception x) { MessageBox.Show(x.ToString(), "systen error!!!"); }

                    break;
                #endregion

                #region JOG (A4_RX_+)
                case "A4_RX_+":

                    try
                    {
                        _jogAxis = "A4_A_P";
                    }
                    catch (Exception x) { MessageBox.Show(x.ToString(), "systen error!!!"); }

                    break;
                #endregion

                #region JOG (A4_RX_-)
                case "A4_RX_-":

                    try
                    {
                        _jogAxis = "A4_A_M";
                    }
                    catch (Exception x) { MessageBox.Show(x.ToString(), "systen error!!!"); }

                    break;
                #endregion

                #region JOG (A5_RY_+)
                case "A5_RY_+":

                    try
                    {
                        _jogAxis = "A5_B_P";
                    }
                    catch (Exception x) { MessageBox.Show(x.ToString(), "systen error!!!"); }

                    break;
                #endregion

                #region JOG (A5_RY_-)
                case "A5_RY_-":

                    try
                    {
                        _jogAxis = "A5_B_M";
                    }
                    catch (Exception x) { MessageBox.Show(x.ToString(), "systen error!!!"); }

                    break;
                #endregion


                #region JOG (A6_RZ_+)
                case "A6_RZ_+":

                    try
                    {
                        _jogAxis = "A6_C_P";
                    }
                    catch (Exception x) { MessageBox.Show(x.ToString(), "systen error!!!"); }

                    break;
                #endregion

                #region JOG (A6_RZ_-)
                case "A6_RZ_-":

                    try
                    {
                        _jogAxis = "A6_C_M";
                    }
                    catch (Exception x) { MessageBox.Show(x.ToString(), "systen error!!!"); }

                    break;
                #endregion

                #region 測試按鈕
                case "test_btn":

                    try
                    {

                    }
                    catch (Exception x) { MessageBox.Show(x.ToString(), "systen error!!!"); }


                    break;
                    #endregion

            }
            _jogState = false;
        }


        private void jogStart(string _Anum )
        {
            _mxJog.AXISGROUPIDX = _axisGroupIdx;
            if (radioButton1.Checked == true){_moveType = 1;}else{_moveType = 0;}
            _mxJog.MOVETYPE = _moveType;
            _mxJog.VELOCITY = 0;
            _mxJog.ACCELERATION = 0;
            _mxJog.COORDINATESYSTEM = cORS_COR;
            _mxJog.INCREMENT = 0;

            switch (_Anum)
            {
                case "A1_X_P":

                    _mxJog.A1_X_P = true;

                    break;

                case "A1_X_M":
                    _mxJog.A1_X_M = true;
                    break;

                case "A2_Y_P":
                    _mxJog.A2_Y_P = true;
                    break;

                case "A2_Y_M":
                    _mxJog.A2_Y_M = true;
                    break;

                case "A3_Z_P":
                    _mxJog.A3_Z_P = true;
                    break;

                case "A3_Z_M":
                    _mxJog.A3_Z_M = true;
                    break;

                case "A4_A_P":
                    _mxJog.A4_A_P = true;
                    break;

                case "A4_A_M":
                    _mxJog.A4_A_M = true;
                    break;


                case "A5_B_P":
                    _mxJog.A5_B_P = true;
                    break;

                case "A5_B_M":
                    _mxJog.A5_B_M = true;
                    break;


                case "A6_C_P":
                    _mxJog.A6_C_P = true;
                    break;

                case "A6_C_M":
                    _mxJog.A6_C_M = true;
                    break;

            }

            _mxJog.OnCycle();

        }

        private void jogStop(string _Anum)
        {
            _mxJog.AXISGROUPIDX = _axisGroupIdx;
            if (radioButton1.Checked == true) { _moveType = 1; } else { _moveType = 0; }
            _mxJog.MOVETYPE = _moveType;
            _mxJog.VELOCITY = 0;
            _mxJog.ACCELERATION = 0;
            _mxJog.COORDINATESYSTEM = cORS_COR;
            _mxJog.INCREMENT = 0;

            switch (_Anum)
            {
                case "A1_X_P":

                    _mxJog.A1_X_P = false;

                    break;

                case "A1_X_M":
                    _mxJog.A1_X_M = false;
                    break;

                case "A2_Y_P":
                    _mxJog.A2_Y_P = false;
                    break;

                case "A2_Y_M":
                    _mxJog.A2_Y_M = false;
                    break;

                case "A3_Z_P":
                    _mxJog.A3_Z_P = false;
                    break;

                case "A3_Z_M":
                    _mxJog.A3_Z_M = false;
                    break;

                case "A4_A_P":
                    _mxJog.A4_A_P = false;
                    break;

                case "A4_A_M":
                    _mxJog.A4_A_M = false;
                    break;


                case "A5_B_P":
                    _mxJog.A5_B_P = false;
                    break;

                case "A5_B_M":
                    _mxJog.A5_B_M = false;
                    break;


                case "A6_C_P":
                    _mxJog.A6_C_P = false;
                    break;

                case "A6_C_M":
                    _mxJog.A6_C_M = false;
                    break;

            }


            _mxJog.OnCycle();
        }


        #endregion

        /// <summary>
        /// read jog state (justin)
        /// </summary>
        public void ReadJogState()
        {
            label20.Text = "Busy : " + _mxJog.BUSY.ToString();
            label19.Text = "Active : " + _mxJog.ACTIVE.ToString();
            label18.Text = "Done : " + _mxJog.DONE.ToString();
            label17.Text = "Aborted : " + _mxJog.ABORTED.ToString();
            label16.Text = "Error : " + _mxJog.ERROR.ToString();
            label15.Text = "ErrorID : " + _mxJog.ERRORID.ToString();
        }

        #endregion

        #region internal
        // internal frame move
        private void MainView_MouseDown(object sender, MouseEventArgs e)
        {
            _mouseposition = new Point(e.X, e.Y);
        }
        // internal frame move
        private void MainView_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                var mousePos = Location;
                var newFramePos = new Point(mousePos.X + (e.X - _mouseposition.X), mousePos.Y + (e.Y - _mouseposition.Y));
                Location = newFramePos;
            }
        }

        private void Debug(string p)
        {
            tbDebug.AppendText("->" + p + Environment.NewLine);
            _counter++;
            if (_counter > 1000)
            {
                tbDebug.Clear();
                _counter = 0;
                tbDebug.AppendText("->Message Box cleared." + Environment.NewLine);
            }
        }
        #endregion

        private void timer1_Tick(object sender, EventArgs e)
        {
            //jog (Justin)
            ReadJogState();
        }
    }
}