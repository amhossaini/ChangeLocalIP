using System ;
using System.Text ;
using Microsoft.Win32;
using System.Security.Permissions;



namespace ChangeIP
{
	/// <summary>
	/// Change local IP Address and SubnetMask.
	/// </summary>
	class Class1
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main(string[] args)
		{
            if (args.Length < 2)
            {
                Console.WriteLine("Please enter IP and SubnetMask as argument.");
                Console.ReadLine();
                return;
            }
			System.Security.Permissions.RegistryPermission RP = new System.Security.Permissions.RegistryPermission(System.Security.Permissions.PermissionState.Unrestricted);
            RegistryKey regKey ;
			string strServiceName = "";
			regKey = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion\NetworkCards\13");
			strServiceName = regKey.GetValue("ServiceName").ToString();
			regKey.Close();
			RegistryPermission f = new RegistryPermission(RegistryPermissionAccess.Write | RegistryPermissionAccess.Read, "SYSTEM\\CurrentControlSet\\Services\\" + strServiceName + "\\Parameters\\Tcpip");
			regKey = Registry.LocalMachine.OpenSubKey("SYSTEM\\CurrentControlSet\\Services\\" + strServiceName + "\\Parameters\\Tcpip", true);
			regKey.SetValue("EnableDHCP ", 0);
			regKey.SetValue("IPAddress", Encoding.ASCII.GetBytes(args[0]));
			regKey.SetValue("SubnetMask", Encoding.ASCII.GetBytes(args[1]));
			regKey.SetValue("DefaultGateway", Encoding.ASCII.GetBytes(""));
			regKey.Close();
			
		}
	}
}


