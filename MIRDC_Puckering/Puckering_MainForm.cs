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
                
                e.Cancel = false;//確認離開
            }
            

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

    }


}
