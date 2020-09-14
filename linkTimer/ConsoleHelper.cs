using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace linkTimer
{
    /// <summary>  
    /// 控制台帮助类  
    /// </summary>  
    public static class ConsoleHelper
    {
        /// <summary>  
        /// 获取窗口句柄  
        /// </summary>  
        /// <param name="lpClassName"></param>  
        /// <param name="lpWindowName"></param>  
        /// <returns></returns>  
        [DllImport("user32.dll", SetLastError = true)]
        private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        /// <summary>  
        /// 设置窗体的显示与隐藏  
        /// </summary>  
        /// <param name="hWnd"></param>  
        /// <param name="nCmdShow"></param>  
        /// <returns></returns>  
        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool ShowWindow(IntPtr hWnd, uint nCmdShow);

        /// <summary>  
        /// 隐藏控制台  
        /// </summary>  
        /// <param name="ConsoleTitle">控制台标题(可为空,为空则取默认值)</param>  
        public static void hideConsole(string ConsoleTitle = "")
        {
            ConsoleTitle = String.IsNullOrEmpty(ConsoleTitle) ? Console.Title : ConsoleTitle;
            IntPtr hWnd = FindWindow("ConsoleWindowClass", ConsoleTitle);
            if (hWnd != IntPtr.Zero)
            {
                ShowWindow(hWnd, 0);
            }
        }

        /// <summary>  
        /// 显示控制台  
        /// </summary>  
        /// <param name="ConsoleTitle">控制台标题(可为空,为空则去默认值)</param>  
        public static void showConsole(string ConsoleTitle = "")
        {
            ConsoleTitle = String.IsNullOrEmpty(ConsoleTitle) ? Console.Title : ConsoleTitle;
            IntPtr hWnd = FindWindow("ConsoleWindowClass", ConsoleTitle);
            if (hWnd != IntPtr.Zero)
            {
                ShowWindow(hWnd, 1);
            }
        }

        /// <summary>
        /// 把信息写入文件
        /// </summary>
        /// <param name="text">写入文件信息</param>
        public static void resultWrite(string text)
        {
            DateTime now = DateTime.Now;
            string sPath = @"D:\K3Sync_Log\Log " + now.ToString("yyyy-MM-dd") + ".txt";
            FileStream fs = new FileStream(sPath, FileMode.Append);
            StreamWriter sw = new StreamWriter(fs, Encoding.Default);
            sw.Write(text);
            sw.Close();
            fs.Close();
        }
    }  
}
