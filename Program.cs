using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;

namespace TaskManagerMaximized
{
    class Program
    {
        [DllImport("user32.dll", EntryPoint = "FindWindow", SetLastError = true)]
        private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
        [DllImport("user32.dll", EntryPoint = "FindWindowEx", SetLastError = true)]
        private static extern IntPtr FindWindowEx(IntPtr hwndParent, uint hwndChildAfter, string lpszClass, string lpszWindow);
        [DllImport("user32.dll", EntryPoint = "SendMessage", SetLastError = true, CharSet = CharSet.Auto)]
        private static extern int SendMessage(IntPtr hwnd, uint wMsg, int wParam, int lParam);
        [DllImport("user32.dll", EntryPoint = "SetForegroundWindow", SetLastError = true)]
        private static extern void SetForegroundWindow(IntPtr hwnd);
        const int WM_SYSCOMMAND = 0x0112;
        const int SC_CLOSE = 0xF060;
        const int SC_MINIMIZE = 0xF020;
        const int SC_MAXIMIZE = 0xF030;
        static void Main(string[] args)
        {
            ProcessStartInfo MyStarInfo = new ProcessStartInfo();
            MyStarInfo.FileName = @"devmgmt.msc";
            ////窗口起始状态最大化
            MyStarInfo.WindowStyle = ProcessWindowStyle.Maximized;
            MyStarInfo.UseShellExecute = true;
            Process p = new Process();
            p.StartInfo = MyStarInfo;
            p.Start();
            Thread.Sleep(2000);

            IntPtr mainHandle = FindWindow(null, "Device Manager");//Device Manager
            if (mainHandle != IntPtr.Zero)
            {
                SetForegroundWindow(mainHandle);
                SendMessage(mainHandle, WM_SYSCOMMAND, SC_MAXIMIZE, 0); // 最大化
            }
        }

        
}
}
