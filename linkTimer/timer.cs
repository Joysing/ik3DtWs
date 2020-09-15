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
            string Sync = DeptSync();
            if (!string.IsNullOrEmpty(Sync))
            {
                DateTime now = DateTime.Now;
                ConsoleHelper.resultWrite(Sync + now.ToString() + "\r\n");
                Console.WriteLine("同步完成，等待下次同步......" + now.ToString());
            }
            ConsoleHelper.hideConsole("");
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
            DayOfWeek WeekDate = e.SignalTime.DayOfWeek;
            // 定制时间； 比如 在10：30 ：00 的时候执行某个函数
            int iHour = 21;
            int iHour2 = 12;
            int iMinute = 30;
            int iSecond = 1;

            // 设置 每天的 21:30:00 或 12:30:00 开始执行程序
            if (((intHour == iHour) || (intHour == iHour2)) && intMinute == iMinute && intSecond == iSecond)
            {
                ConsoleHelper.showConsole("");
                Console.WriteLine("正在同步......");
                DateTime now = DateTime.Now;
                string str = DeptSync();
                if (!string.IsNullOrEmpty(str.Trim()))
                {
                    ConsoleHelper.resultWrite("同步程序已执行：" + str + "\r\n" + now.ToString() + "\r\n");
                    now.ToShortDateString();
                    Console.WriteLine("同步程序已执行：" + now.ToString() + "\r\n");
                    ConsoleHelper.hideConsole("");
                }
                else
                {
                    ConsoleHelper.resultWrite("同步程序执行失败，请重启程序：\r\n" + now.ToString() + "\r\n");
                    now.ToShortDateString();
                    Console.WriteLine("同步程序执行失败，请重启程序：" + now.ToString() + "\r\n");
                    ConsoleHelper.hideConsole("");
                }
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
        private static string DeptSync()
        {
            DateTime now = DateTime.Now;

            string sResult;
            string oaAllDept = null;
            //尝试调用oa接口，调用失败，返回相关信息
            try
            {
                string oaUrl = "http://172.16.1.60:8080/axis/ysxt_jk.jws?wsdl";
                string oaName = "Bm_Info";
                string syncDate= "kssj:" + now.AddDays(-1.0).ToString("yyyy-MM-dd") + ",jssj:" + now.ToString("yyyy-MM-dd");
                string[] oaParam = { syncDate, "ysjkyh", "ysjkyh_123456" };
                WebServiceProxy oaWsd = new WebServiceProxy(oaUrl, oaName);
                oaAllDept = (string)oaWsd.ExecuteQuery(oaName, oaParam);
            }
            catch (Exception ex)
            {
                return "oa接口调用失败，无法同步。" + ex.ToString() + oaAllDept;
            }
            bool flag = oaCheck(oaAllDept);
            if (flag)
            {
                return "没有需要同步的部门，等待下次同步！";
            }

            string k3Url = "http://192.168.0.60:8787/IDepartment.asmx?wsdl";
            string k3Name = "T_Dept";
            string[] k3Param = {oaAllDept };
            WebServiceProxy wsd1 = new WebServiceProxy(k3Url, k3Name);
            sResult = (string)wsd1.ExecuteQuery(k3Name, k3Param);

            return sResult + flag;

        }
        private static bool oaCheck(string dept)
        {
            XElement element;
            try
            {
                element = XElement.Parse(dept.Trim());
            }
            catch (Exception)
            {
                return false;
            }
            return element.Element("resultRequest").Element("resultCode").Value.Equals("009");
        }



    }

}

