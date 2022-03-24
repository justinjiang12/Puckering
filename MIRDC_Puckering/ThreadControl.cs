using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.Threading;

namespace MIRDC_Puckering
{

    class ThreadControl
    {
        #region 欄位宣告

        /// <summary>
        /// 實作各部動作流程類別
        /// </summary>
        public GrabRobotLoop L_GrabRobot = new GrabRobotLoop();
        public PushRobotLoop L_PushRobot = new PushRobotLoop();
        public VisionLoop L_Vision = new VisionLoop();


        /// <summary>
        /// 宣告main_MIRDC_testLoop 欄位(thread)
        /// </summary>
        private Thread Thr_GrabRobot;
        private Thread Thr_PushRobot;
        private Thread Thr_Vision;

        /// <summary>
        /// 執行序狀態旗標
        /// </summary>
        private bool state_GrabRobot = false;
        private bool state_PushRobot = false;
        private bool state_Vision = false;


        #endregion

        #region 執行緒管理
        /// <summary>
        /// 建立執行緒
        /// </summary>
        public bool Run_thread()
        {
            try
            {
                //實作執行緒L_GrabRobot.LoopRun
                Thr_GrabRobot = new Thread(L_GrabRobot.LoopRun);
                //啟動Thr_GrabRobot執行緒
                Thr_GrabRobot.Start();
                state_GrabRobot = true;


                //實作執行緒L_PushRobot.LoopRun
                Thr_PushRobot = new Thread(L_PushRobot.LoopRun);
                //啟動Thr_GrabRobot執行緒
                Thr_PushRobot.Start();
                state_PushRobot = true;

                //實作執行緒L_Vision.LoopRun
                Thr_Vision = new Thread(L_Vision.LoopRun);
                //啟動Thr_GrabRobot執行緒
                Thr_Vision.Start();
                state_Vision = true;


                return true;
            }
            catch (Exception x)
            {
                MessageBox.Show(x.ToString(), "systen error!!!");
                return false;
            }

        }

        /// <summary>
        /// 關閉執行緒
        /// </summary>
        public void End_thread()
        {
            try
            {
                OtherControl.ResetData();
                CloseThread(Thr_GrabRobot, state_GrabRobot);
                CloseThread(Thr_PushRobot, state_PushRobot);
                CloseThread(Thr_Vision, state_Vision);
            }
            catch { MessageBox.Show("sys error!!"); }
        }
        #endregion

        /// <summary>
        /// 其他執行續方法
        /// </summary>
        /// <param name="Thr_name"></param>
        /// <param name="state"></param>
        private void CloseThread(Thread Thr_name,bool state )
        {
            if (state)
            {
                //關閉執行緒
                Thr_name.Abort();
                //確認關閉執行緒動作

                while (Thr_name.ThreadState != ThreadState.Aborted)
                {
                    //當調用Abort方法後，如果thread線程的狀態不為Aborted，主線程就一直在這裡做迴圈，直到thread線程的狀態變為Aborted為止
                    Thread.Sleep(100);
                }
            }
        }

    }
}
