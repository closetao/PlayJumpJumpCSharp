using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Threading;
using System.ComponentModel;
using System.IO;

namespace ADBTool.AdbHelp
{
    class CmdProcessHelper
    {
        public static void AysnRun(string exePath, string args)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo(exePath);
            startInfo.Arguments = args;
            startInfo.CreateNoWindow = true;//不显示Dos窗口
            startInfo.UseShellExecute = false;//是否指定操作系统外壳进程启动程序
            startInfo.RedirectStandardOutput = true;
            startInfo.RedirectStandardInput = true;

            Process p = Process.Start(startInfo);
            p.OutputDataReceived += new DataReceivedEventHandler(p_OutputDataReceived);
            p.ErrorDataReceived += new DataReceivedEventHandler(p_ErrorDataReceived);
            p.BeginOutputReadLine();
            p.WaitForExit();
        }

        //收到数据
        static void p_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            CmdHelp.PutResultThreadToUI(e.Data);
        }

        //收到错误数据
        static void p_ErrorDataReceived(object sender, DataReceivedEventArgs e)
        {
            CmdHelp.PutResultThreadErrorToUI(e.Data);
        }




        public static void Run(string exePath, string args)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo(exePath);
            startInfo.Arguments = args;
            startInfo.CreateNoWindow = true;//不显示Dos窗口
            startInfo.UseShellExecute = false;//是否指定操作系统外壳进程启动程序
            startInfo.RedirectStandardOutput = true;
            startInfo.RedirectStandardInput = true;

            Process p = Process.Start(startInfo);

            StreamReader reader = p.StandardOutput;

            do
            {
                string lineStr = reader.ReadLine();
                CmdHelp.PutResultThreadToUI(lineStr);
            }
            while (!reader.EndOfStream);

            p.WaitForExit();
            reader.Close();
            p.Close();

        }



    }
}
