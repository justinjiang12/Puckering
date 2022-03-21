using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.Threading;

namespace MIRDC_Puckering
{

    class ThreadContrl
    {
        #region 欄位宣告
        //實作function類別之物件
        //private function fun_mirdc = new function();


        //宣告main_MIRDC_testLoop 欄位(thread)
        private Thread main_MIRDC_testLoop;
        private bool state_MIRDC_testLoop = false;

        #endregion

        #region 執行緒管理
        /// <summary>
        /// 建立執行緒
        /// </summary>
        private bool creat_thread()
        {
            try
            {
                //實作執行緒thr_mirdc
                //Thread thr_mirdc = new Thread(fun_mirdc.LoopRun);
                //將實作之執行緒傳遞於main_MIRDC_testLoop欄位
                //main_MIRDC_testLoop = thr_mirdc;
                //啟動main_MIRDC_testLoop執行緒
                main_MIRDC_testLoop.Start();
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
        private void end_thread()
        {
            try
            {
                if (state_MIRDC_testLoop)
                {
                    //暫停執行緒旗標
                    //fun_mirdc.loopStop = true;
                    //關閉執行緒
                    main_MIRDC_testLoop.Abort();
                    //確認關閉執行緒動作

                    while (main_MIRDC_testLoop.ThreadState != ThreadState.Aborted)
                    {
                        //當調用Abort方法後，如果thread線程的狀態不為Aborted，主線程就一直在這裡做迴圈，直到thread線程的狀態變為Aborted為止
                        Thread.Sleep(100);
                    }
                }
            }
            catch { MessageBox.Show("sys error!!"); }
        }
        #endregion




    }
}
