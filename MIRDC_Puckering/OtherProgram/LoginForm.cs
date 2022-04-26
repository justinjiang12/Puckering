using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MIRDC_Puckering.OtherProgram
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void btn_Login_Click(object sender, EventArgs e)
        {
            setLevel();
            Hide();
        }

        private void btn_Leave_Click(object sender, EventArgs e)
        {
            Hide();
        }

        private void setLevel()
        {
            try
            {
                if (tex_name.Text == "gust" && tex_password.Text == "") { IPermission.Permission_Level = PermissionList.Level_0_Guest; }
                if (tex_name.Text == "op" && tex_password.Text == "op") { IPermission.Permission_Level = PermissionList.Level_1_Operator; }
                if (tex_name.Text == "eng" && tex_password.Text == "eng") { IPermission.Permission_Level = PermissionList.Level_2_Engineer; }
                if (tex_name.Text == "seng" && tex_password.Text == "seng") { IPermission.Permission_Level = PermissionList.Level_3_SeniorEngineer; }
                if (tex_name.Text == "mirdc" && tex_password.Text == "102691") { IPermission.Permission_Level = PermissionList.Level_10_Designer; }
                
            }
            catch (Exception x) { MessageBox.Show(x.ToString(), "systen error!!!"); }
        }

    }
}
