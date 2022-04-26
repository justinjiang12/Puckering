using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using System.Resources;
using System.Collections;
using System.Threading;
using System.Runtime.InteropServices;
using controlArray; //namespace ControlArray
using UniDAQ_Ns;
using System.Reflection;    //namespace P100X_Ns 

namespace DIO
{
    public partial class IOForm : Form
    {
        public IOForm()
        {
            InitializeComponent();
        }

        ArrayList mpbOutputs = new ArrayList();
        ArrayList mpbInputs = new ArrayList();

        ushort wTotalBoard, wInitialCode, wBoardNo;
        ushort wDIOWidth, wOutputPort, wInputPort;

        UniDAQ.IXUD_CARD_INFO[] sCardInfo = new UniDAQ.IXUD_CARD_INFO[UniDAQ.MAX_BOARD_NUMBER];
        UniDAQ.IXUD_DEVICE_INFO[] sDeviceInfo = new UniDAQ.IXUD_DEVICE_INFO[UniDAQ.MAX_BOARD_NUMBER];

        private void Form1_Load(object sender, EventArgs e)
        {
            byte[] szModeName = new byte[20];
            //StringBuilder sCardName = new StringBuilder();

            mpbOutputs = ControlArrayUtils.getControlArray(this.gbxOutput, "pbout", ""); //CreatControlArray
            mpbInputs = ControlArrayUtils.getControlArray(this.gbxInput, "pbin", "");  //CreatControlArray
            //mlblOuts = ControlArrayUtils.getControlArray(this.gbxOutput , "lblOut", "");
            //mlblIns = ControlArrayUtils.getControlArray(this.gbxInput , "lblIn", "");

            for (int i = 0; i < 32; i++)
            {
                ((PictureBox)mpbOutputs[i]).BackColor = Color.Gray;
                ((PictureBox)mpbOutputs[i]).Enabled = false;
                ((PictureBox)mpbInputs[i]).BackColor = Color.Gray;

                ((PictureBox)mpbOutputs[i]).Click += new EventHandler(mpbOutputs_Click); //mpbOutputs Click event
            }

            //Driver Initial
            wInitialCode = UniDAQ.Ixud_DriverInit(ref wTotalBoard);
            if (wInitialCode != UniDAQ.Ixud_NoErr)
            {
                MessageBox.Show("Driver Initial Error!!Error Code:" + wInitialCode.ToString());
                Close();
                return;
            }

            for (ushort wBoardIndex = 0; wBoardIndex < wTotalBoard; wBoardIndex++)
            {
                //Get Card Information
                wInitialCode = UniDAQ.Ixud_GetCardInfo(wBoardIndex, ref sDeviceInfo[wBoardIndex], ref sCardInfo[wBoardIndex], szModeName);
                this.cbxSelBoard.Items.Add(Encoding.ASCII.GetString(szModeName));
            }

        }

        private void btnActive_Click(object sender, EventArgs e)
        {
            wBoardNo = (ushort)cbxSelBoard.SelectedIndex;
            wOutputPort = (ushort)cbxOutputPort.SelectedIndex;
            wInputPort = (ushort)cbxInputPort.SelectedIndex;

            if (btnActive.Text == "&ACTIVE")
            {
                btnActive.Text = "&STOP";
                if ((sCardInfo[wBoardNo].wDIOPorts + sCardInfo[wBoardNo].wDIPorts) > 0)
                {
                    Timer1.Enabled = true;
                }

                btnExit.Enabled = false;
                for (int i = 0; i < 32; i++)
                {
                    ((PictureBox)mpbOutputs[i]).Enabled = true;
                }
            }
            else
            {

                btnActive.Text = "&ACTIVE";
                Timer1.Enabled = false;
                btnExit.Enabled = true;
                for (int i = 0; i < 32; i++)
                {
                    ((PictureBox)mpbOutputs[i]).Enabled = false;
                }
            }

            if (sCardInfo[wBoardNo].wDIOPorts > 1)
            {
                //Set DIO mode 1:Output 0:Input
                wInitialCode = UniDAQ.Ixud_SetDIOModes32(wBoardNo, (uint)(1 << wOutputPort));
            }
        }

        private void mpbOutputs_Click(object sender, EventArgs e) //mpbOutputs Click event
        {

            int index;
            PictureBox pb = (PictureBox)sender;
            int i = Convert.ToUInt16((Convert.ToString(pb.Name)).Remove(0, 5));

            if (((PictureBox)mpbOutputs[i]).BackColor == Color.Gray)
            {
                ((PictureBox)mpbOutputs[i]).BackColor = Color.Red;
            }
            else
            {
                ((PictureBox)mpbOutputs[i]).BackColor = Color.Gray;
            }

            for (index = 0; index < 32; index++)
            {
                if (((PictureBox)mpbOutputs[index]).BackColor == Color.Red)//Set DO Bit X Value is High
                {
                    wInitialCode = UniDAQ.Ixud_WriteDOBit(wBoardNo, wOutputPort, (ushort)index, 1);
                }
                else                                                       //Set DO Bit X Value is Low
                {
                    wInitialCode = UniDAQ.Ixud_WriteDOBit(wBoardNo, wOutputPort, (ushort)index, 0);
                }
            }

        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            ushort DIVal;
            int index;
            DIVal = 0x0;



            for (index = 0; index < 32; index++)
            {
                //Read DI Bit x Value
                wInitialCode = UniDAQ.Ixud_ReadDIBit(wBoardNo, wInputPort, (ushort)index, ref DIVal);
                if (DIVal == 0)
                {
                    ((PictureBox)mpbInputs[index]).BackColor = Color.Gray;
                }
                else
                {
                    ((PictureBox)mpbInputs[index]).BackColor = Color.Red;
                }
            }
        }

        #region Justin

        /// <summary>
        /// DI_Write
        /// </summary>
        /// <param name="_num"></param>
        /// <param name="_state"></param>
        public void DO_Write(ushort _num , bool _state)
        {
            try
            {
                if (_state == true) { wInitialCode = UniDAQ.Ixud_WriteDOBit(wBoardNo, wOutputPort, _num, 1); }
                if (_state == false) { wInitialCode = UniDAQ.Ixud_WriteDOBit(wBoardNo, wOutputPort, _num, 0); }
            }
            catch (Exception x) { MessageBox.Show(x.ToString(), "systen error!!!"); }

        }
        /// <summary>
        /// DI_Read
        /// </summary>
        /// <param name="_num"></param>
        /// <returns></returns>
        public bool DI_Read(ushort _num)
        {
            try
            {
                ushort _DIVal = 0;
                wInitialCode = UniDAQ.Ixud_ReadDIBit(wBoardNo, wInputPort, _num, ref _DIVal);
                if (_DIVal == 0) { return false; } else { return true; }
            }
            catch (Exception x) { MessageBox.Show(x.ToString(), "systen error!!!"); return false; }
            
        }




        #endregion

        private void cbxSelBoard_SelectedIndexChanged(object sender, EventArgs e)
        {
            ushort wPortIndex;
            wBoardNo = (ushort)cbxSelBoard.SelectedIndex;
            //Total Digital Output Port
            this.cbxOutputPort.Items.Clear();
            this.cbxInputPort.Items.Clear();
            this.btnActive.Enabled = true;
            this.cbxOutputPort.Enabled = false;
            this.cbxInputPort.Enabled = false;
            lblOut0.Visible = false;
            lblOut1.Visible = false;
            lblOut2.Visible = false;
            lblOut3.Visible = false;
            lblIn0.Visible = false;
            lblIn1.Visible = false;
            lblIn2.Visible = false;
            lblIn3.Visible = false;

            for (int i = 0; i < 32; i++)
            {
                ((PictureBox)mpbOutputs[i]).Visible = false;
                ((PictureBox)mpbInputs[i]).Visible = false;
            }

            if (sCardInfo[wBoardNo].wDIOPorts + sCardInfo[wBoardNo].wDOPorts > 0)
            {
                for (wPortIndex = 0; wPortIndex < sCardInfo[wBoardNo].wDIOPorts + sCardInfo[wBoardNo].wDOPorts; wPortIndex++)
                    this.cbxOutputPort.Items.Add("Port " + wPortIndex.ToString());

                this.cbxOutputPort.Enabled = true;
                cbxOutputPort.SelectedIndex = 0;

                for (int i = 0; i < 32; i++)
                {
                    if (i < sCardInfo[wBoardNo].wDIOPortWidth)
                        ((PictureBox)mpbOutputs[i]).Visible = true;
                }

                if ((sCardInfo[wBoardNo].wDIOPortWidth / 8) > 0)
                    lblOut0.Visible = true;
                if ((sCardInfo[wBoardNo].wDIOPortWidth / 8) > 1)
                    lblOut1.Visible = true;
                if ((sCardInfo[wBoardNo].wDIOPortWidth / 8) > 2)
                    lblOut2.Visible = true;
                if ((sCardInfo[wBoardNo].wDIOPortWidth / 8) > 3)
                    lblOut3.Visible = true;
            }

            if (sCardInfo[wBoardNo].wDIOPorts + sCardInfo[wBoardNo].wDIPorts > 0)
            {
                for (wPortIndex = 0; wPortIndex < sCardInfo[wBoardNo].wDIOPorts + sCardInfo[wBoardNo].wDIPorts; wPortIndex++)
                    this.cbxInputPort.Items.Add("Port " + wPortIndex.ToString());

                this.cbxInputPort.Enabled = true;
                cbxInputPort.SelectedIndex = 0;

                for (int i = 0; i < 32; i++)
                {
                    if (i < sCardInfo[wBoardNo].wDIOPortWidth)
                        ((PictureBox)mpbInputs[i]).Visible = true;
                }
                if ((sCardInfo[wBoardNo].wDIOPortWidth / 8) > 0)
                    lblIn0.Visible = true;
                if ((sCardInfo[wBoardNo].wDIOPortWidth / 8) > 1)
                    lblIn1.Visible = true;
                if ((sCardInfo[wBoardNo].wDIOPortWidth / 8) > 2)
                    lblIn2.Visible = true;
                if ((sCardInfo[wBoardNo].wDIOPortWidth / 8) > 3)
                    lblIn3.Visible = true;
            }

            this.Text = this.cbxSelBoard.Text + " Digital I/O LED Demo";
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            //Release the resource
            wInitialCode = UniDAQ.Ixud_DriverClose();
            Close();
        }

        private void cbxOutputPort_SelectedIndexChanged(object sender, EventArgs e)
        {
            wOutputPort = (ushort)cbxOutputPort.SelectedIndex;

        }

        private void cbxInputPort_SelectedIndexChanged(object sender, EventArgs e)
        {
            wInputPort = (ushort)cbxInputPort.SelectedIndex;
        }
    }
}