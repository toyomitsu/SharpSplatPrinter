using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SharpSplatPrinter.Util
{
    /// <summary>
    /// This class will run processes. I couldn't find a better name.
    /// </summary>
    public static class ProcessRunner
    {
        public static void RunBatch(string Instructions, out string Data, out string Error, string WorkingDirectory = "")
        {
            string DataToReturn = "";
            string ErrorToReturn = "";

            ProcessStartInfo ProcessInfo = new ProcessStartInfo("cmd.exe", "/c " + Instructions);
            if (!string.IsNullOrWhiteSpace(WorkingDirectory))
            {
                ProcessInfo.WorkingDirectory = WorkingDirectory;
            }
            ProcessInfo.CreateNoWindow = true;
            ProcessInfo.UseShellExecute = false;
            ProcessInfo.RedirectStandardError = true;
            ProcessInfo.RedirectStandardOutput = true;

            Process Batch = Process.Start(ProcessInfo);

            DataToReturn = Batch.StandardOutput.ReadToEnd();
            ErrorToReturn = Batch.StandardError.ReadToEnd();

            //Batch.OutputDataReceived += (object sender, DataReceivedEventArgs e) =>
            //    DataToReturn = e.Data;
            //Batch.BeginOutputReadLine();

            //Batch.ErrorDataReceived += (object sender, DataReceivedEventArgs e) =>
            //    ErrorToReturn = e.Data;
            //Batch.BeginErrorReadLine();

            Batch.WaitForExit();
            Batch.Close();

            Data = DataToReturn;
            Error = ErrorToReturn;
        }

        public static void RunAvrDude(string Instructions, out string Data, out string Error, string WorkingDirectory)
        {
            string DataToReturn = "";
            string ErrorToReturn = "";

            ProcessStartInfo ProcessInfo = new ProcessStartInfo(@"C:\Program Files (x86)\Arduino\hardware\tools\avr\bin\avrdude.exe", Instructions);
            ProcessInfo.WorkingDirectory = WorkingDirectory;
            ProcessInfo.CreateNoWindow = true;
            ProcessInfo.UseShellExecute = false;
            ProcessInfo.RedirectStandardError = true;
            ProcessInfo.RedirectStandardOutput = true;

            Process Batch = Process.Start(ProcessInfo);

            DataToReturn = Batch.StandardOutput.ReadToEnd();
            ErrorToReturn = Batch.StandardError.ReadToEnd();

            Batch.WaitForExit();
            Batch.Close();

            Data = DataToReturn;
            Error = ErrorToReturn;
        }
    }
}
