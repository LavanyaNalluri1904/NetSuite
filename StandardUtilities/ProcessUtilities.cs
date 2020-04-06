using System;
using System.Management;
using System.Diagnostics;
namespace StandardUtilities
{
    /// <summary>
    /// @Author - Pavan Parmar
    /// </summary>
    public class ProcessUtilities
    {
        public static void KillProcessByNameAndUser(string ProcessName, string ProcessUserName)
        {
            Process[] foundProcesses = Process.GetProcessesByName(ProcessName);
            Console.WriteLine(foundProcesses.Length.ToString() + " processes found.");
            string strMessage = string.Empty;
            foreach (Process p in foundProcesses)
            {
                string UserName = GetProcessOwner(p.Id);
                strMessage = string.Format("Process Name: {0} | Process ID: {1} | User Name : {2} | StartTime {3}",
                                                 p.ProcessName, p.Id.ToString(), UserName, p.StartTime.ToString());
               

                if (UserName.Equals(ProcessUserName, StringComparison.OrdinalIgnoreCase))
                {
                    p.Kill();
                    Console.WriteLine("Process ID " + p.Id.ToString() + " is killed.");
                }
            }
        }

        public static string GetProcessOwner(int processId)
        {
            string query = "Select * From Win32_Process Where ProcessID = " + processId;
            ManagementObjectSearcher searcher = new ManagementObjectSearcher(query);
            ManagementObjectCollection processList = searcher.Get();

            foreach (ManagementObject obj in processList)
            {
                string[] argList = new string[] { string.Empty, string.Empty };
                int returnVal = Convert.ToInt32(obj.InvokeMethod("GetOwner", argList));
                if (returnVal == 0)
                {
                    return argList[1] + "\\" + argList[0];   // return DOMAIN\user
                }
            }
            return "NO OWNER";
        }
    }
}
