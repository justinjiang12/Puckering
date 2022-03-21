using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace MIRDC_Puckering
{

    delegate void ChangeSysState(SysState State);

    class ISystem
    {
        /// <summary>
        /// 當系統參數切換時觸發
        /// </summary>
        public static event ChangeSysState OnSysStateChanging;

        private static SysState _state = SysState.Standby;

        /// <summary>
        /// !!Important!! 切換參數時的觸發
        /// </summary>
        public static SysState State
        {
            get
            {
                return _state;
            }
            set
            {
                _state = value;
                OnSysStateChanging(_state);
            }
        }

    }

    public enum SysState
    {
        //===================程式開啟=======================//
        /// <summary>
        /// 系統啟動...
        /// </summary>
        [Description("大系統啟動...")]
        System_PowerOn,
        //===================系統初始化=======================//
        /// <summary>
        /// 系統初始化(開始)...
        /// </summary>
        [Description("系統初始化(開始)...")]
        Initial_Start,
        /// <summary>
        /// 系統初始化(執行中)...
        /// </summary>
        [Description("系統初始化(執行中)...")]
        Initial_Run,
        /// <summary>
        /// 系統初始化(執行失敗)...
        /// </summary>
        [Description("系統初始化(執行失敗)...")]
        Initial_RunFailed,
        /// <summary>
        /// 系統初始化(完成)...
        /// </summary>
        [Description("系統初始化(完成)...")]
        Initial_End,
        //===================系統手動(Manual)模式=======================//
        /// <summary>
        /// 系統(手動模式)...
        /// </summary>
        [Description("系統(手動模式)...")]
        Manual,
        //===================系統自動模式準備=======================//
        /// <summary>
        /// 系統自動模式準備(開始)...
        /// </summary>
        [Description("系統自動模式準備(開始)...")]
        Prepare_Start,
        /// <summary>
        /// 系統自動模式準備(執行中)...
        /// </summary>
        [Description("系統自動模式準備(執行中)...")]
        Prepare_Run,
        /// <summary>
        /// 系統自動模式準備(執行失敗)...
        /// </summary>
        [Description("系統自動模式準備(執行失敗)...")]
        Prepare_RunFailed,
        /// <summary>
        /// 系統自動模式準備(完成)...
        /// </summary>
        [Description("系統自動模式準備(完成)...")]
        Prepare_End,
        //===================系統自動(Auto)模式=======================//
        /// <summary>
        /// 系統(自動模式)...
        /// </summary>
        [Description("系統(自動模式)...")]
        Auto,
        //===================系統自動切換至手動模式準備=======================//
        /// <summary>
        /// 系統自動切換至手動(開始)...
        /// </summary>
        [Description("系統自動切換至手動(開始)...")]
        AtoM_Start,
        /// <summary>
        /// 系統自動切換至手動(執行中)...
        /// </summary>
        [Description("系統自動切換至手動(執行中)...")]
        AtoM_Run,
        /// <summary>
        /// 系統自動切換至手動(執行失敗)...
        /// </summary>
        [Description("系統自動切換至手動(執行失敗)...")]
        AtoM_RunFailed,
        /// <summary>
        /// 系統自動切換至手動(完成)...
        /// </summary>
        [Description("系統自動切換至手動(完成)...")]
        AtoM_End,
        //===================系統關閉=======================//
        /// <summary>
        /// 系統關閉...
        /// </summary>
        [Description("系統關閉...")]
        System_PowerOff,


        /// <summary>
        /// 系統待命中
        /// </summary>
        [Description("系統待命中")]
        Standby,
        
        /// <summary>
        /// (嚴重)系統發生錯誤!
        /// </summary>
        [Description("系統發生錯誤!")]
        Sys_Error = 99
    }


}
