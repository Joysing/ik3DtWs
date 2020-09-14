using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Linq;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Timers;
using System.Web;
using System.Web.Services;
using System.Xml;
using System.Xml.Linq;
using Jason.ComponentModel;

namespace linkTimer
{
    class timer
    {
        [STAThread]
        static void Main(string[] args)
        {
            System.Timers.Timer aTimer = new System.Timers.Timer();
            aTimer.Elapsed += new ElapsedEventHandler(TimeEvent);
            // 设置引发时间的时间间隔 此处设置为１秒（１０００毫秒）
            aTimer.Interval = 1000;
            aTimer.Enabled = true;
            //aTimer.Start();
            //aTimer.Elapsed += new ElapsedEventHandler(TimeEvent);
            Console.WriteLine("按回车键结束程序");
            Console.WriteLine("等待程序的执行......");
            Console.WriteLine("正在同步......");
            //DateTime now = DateTime.Now;
            //string Sync = DeptSync(now.ToString("yyyy-MM-dd"));
            //if (!string.IsNullOrEmpty(Sync))
            //{

            //    ConsoleHelper.resultWrite("同步程序已执行：" + Sync + "\r\n" + now.ToString() + "\r\n");
            //    Console.WriteLine("同步程序已执行：" + now.ToString() + "\r\n");
            //    //ConsoleHelper.hideConsole();
            //}
            Console.ReadLine();
        }

        // 当时间发生的时候需要进行的逻辑处理等
        //     在这里仅仅是一种方式，可以实现这样的方式很多．
        private static void TimeEvent(object source, ElapsedEventArgs e)
        {
            // 得到 hour minute second   如果等于某个值就开始执行某个程序。
            DateTime date1 = e.SignalTime; //.Date.ToString()+" "+e.SignalTime.Hour.ToString()+":"+e.SignalTime.Minute.ToString()+":"+e.SignalTime.Second.ToString());
            int intHour = e.SignalTime.Hour;
            int intMinute = e.SignalTime.Minute;
            int intSecond = e.SignalTime.Second;
            //DayOfWeek WeekDate = e.SignalTime.DayOfWeek;
            // 定制时间； 比如 在10：30 ：00 的时候执行某个函数
            int iHour = 21;
            int iMinute = 30;
            int iSecond = 1;
            ////设置 每秒钟的开始执行一次
            //if (intSecond == iSecond)
            //{
            //    ConsoleHelper.showConsole();
            //    Console.WriteLine("每秒钟的开始执行一次！");
            //    Console.WriteLine("正在同步......");
            //    DateTime now = DateTime.Now;
            //    ConsoleHelper.resultWrite("同步成功！" + now.ToString() + "\r\n");
            //    Console.WriteLine("同步完成！");
            //    ConsoleHelper.hideConsole();

            //    //test
            //    ConsoleHelper.showConsole();
            //    DataSet OADept = new DataSet();
            //    string Sync1 = DeptSync(now.ToString("yyyy-MM-dd"));
            //    if (!string.IsNullOrEmpty(Sync1))
            //    {
            //        //DateTime now = DateTime.Now;
            //        ConsoleHelper.resultWrite(Sync1 + now.ToString() + "\r\n");
            //        Console.WriteLine("同步成功！" + now.ToString());
            //        ConsoleHelper.hideConsole();
            //    }
            //}
            ////设置 每个小时的３０分钟开始执行
            //if (intMinute == iMinute && intSecond == iSecond)
            //{
            //    Console.WriteLine("每个小时的３０分钟开始执行一次！");
            //}

            // 设置 每天的21：３０：００开始执行程序
            if (intHour == iHour && intMinute == iMinute && intSecond == iSecond)
            {
                ConsoleHelper.showConsole();
                Console.WriteLine("正在同步......");
                //DataSet OADept = new DataSet();
                //DateTime now = DateTime.Now;

              //  string Sync = DeptSync(now.ToString("yyyy-MM-dd"));
                //if (!string.IsNullOrEmpty(Sync))
                //{

                    //ConsoleHelper.resultWrite("同步程序已执行：" + "\r\n" + now.ToString() + "\r\n");
                    //now.ToShortDateString();
                    //Console.WriteLine("同步程序已执行：" + now.ToString() + "\r\n");
                    ConsoleHelper.hideConsole();
                //}

            }
        }

        //private void btnExecute_Click(object sender, EventArgs e)
        //{
        //    //tbResult.Text = "";
        //    ProcessStartInfo start = new ProcessStartInfo("Ping.exe");//设置运行的命令行文件问ping.exe文件，这个文件系统会自己找到
        //    //如果是其它exe文件，则有可能需要指定详细路径，如运行winRar.exe
        //    //start.Arguments = txtCommand.Text;//设置命令参数
        //    start.CreateNoWindow = false;//不显示dos命令行窗口
        //    start.RedirectStandardOutput = true;//
        //    start.RedirectStandardInput = true;//
        //    start.UseShellExecute = false;//是否指定操作系统外壳进程启动程序
        //    Process p = Process.Start(start);
        //    StreamReader reader = p.StandardOutput;//截取输出流
        //    string line = reader.ReadLine();//每次读取一行
        //    while (!reader.EndOfStream)
        //    {
        //        //tbResult.AppendText(line + " ");
        //        line = reader.ReadLine();
        //    }
        //    p.WaitForExit();//等待程序执行完退出进程
        //    p.Close();//关闭进程
        //    reader.Close();//关闭流
        //}

        /// <summary>
        ///调用oa和k3接口进行部门同步
        /// </summary>
        private static string DeptSync(string time)
        {

            string sResult;
            string oaAllDept = null;
            //尝试调用oa接口，调用失败，返回相关信息
            try
            {
                string oaUrl = "http://192.168.0.29:8080/axis/ysxt_jk.jws?wsdl";
                string oaName = "Bm_Info";
                string syncDate="kssj:2011-10-03,jssj:"+time;
                string[] oaParam = { syncDate, "ysjkyh", "ysjkyh_123456" };
                WebServiceProxy oaWsd = new WebServiceProxy(oaUrl, oaName);
                oaAllDept = (string)oaWsd.ExecuteQuery(oaName, oaParam);
            }
            catch (Exception ex)
            {
                return "oa接口调用失败，无法同步。" + ex.ToString();
            }

            string k3Url = "http://192.168.0.60:8787/IDepartment.asmx?wsdl";
            string k3Name = "T_Dept";
            string[] k3Param = {oaAllDept };
            WebServiceProxy wsd1 = new WebServiceProxy(k3Url, k3Name);
            sResult = (string)wsd1.ExecuteQuery(k3Name, k3Param);

            return sResult;  
           
        }



    }

}

