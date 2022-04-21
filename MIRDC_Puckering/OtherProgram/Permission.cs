using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace MIRDC_Puckering.OtherProgram
{
    class Permission
    {
        /// <summary>
        /// 宣告委派
        /// </summary>
        /// <param name="State"></param>
        delegate void ChangePermission(PermissionList State);

        /// <summary>
        /// 權限狀態
        /// </summary>
        class IPermission
        {
            /// <summary>
            /// 當系統參數切換時觸發(委派)
            /// </summary>
            public static event ChangePermission OnSysLevelChanging;



            /// <summary>
            /// 系統狀態列概述
            /// </summary>
            private static PermissionList _permissionLevel = PermissionList.Level_0_Guest;


            /// <summary>
            /// !!Important!! 切換參數時的觸發
            /// </summary>
            public static PermissionList Permission_Level
            {
                get
                {
                    return _permissionLevel;
                }
                set
                {
                    _permissionLevel = value;
                    OnSysLevelChanging(_permissionLevel);
                }
            }

        }


        /// <summary>
        /// 系統狀態表單
        /// </summary>
        public enum PermissionList
        {
            //===================權限等極=======================//
            /// <summary>
            /// 訪客
            /// </summary>
            [Description("訪客")]
            Level_0_Guest,
            /// <summary>
            /// 作業員
            /// </summary>
            [Description("作業員")]
            Level_1_Operator,
            /// <summary>
            /// 工程師
            /// </summary>
            [Description("工程師")]
            Level_2_Engineer,
            /// <summary>
            /// 資深工程師
            /// </summary>
            [Description("資深工程師")]
            Level_3_SeniorEngineer,
            /// <summary>
            /// 最高權限(設計者)
            /// </summary>
            [Description("最高權限")]
            Level_3_Designer,

            /// <summary>
            /// other
            /// </summary>
            [Description("other!")]
            other = 99
        }



    }
}
