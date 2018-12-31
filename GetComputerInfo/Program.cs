/*
/* CPU-C 1.0.0 by Hotlands Software Inc.
 * http://hotlands.x10host.com
 */

using System;
using System.Collections.Generic;
using System.Text;
using System.Management;
using System.Globalization;
using System.Diagnostics;
using Microsoft.VisualBasic.Devices;
using Microsoft.Win32;

namespace GetComputerInfo
{
    class Program
    {
        static void Main(string[] args)
        {
            /*
             * Use this so we can easily detect what operating system we are running on.
             * Reference: Win32Windows = Windows 95, 98, Me, Win32NT = Windows NT 3.1-Windows 10, Unix = Unix OS, MacOSX = Macintosh OS X, WinCE = Windows CE, Xbox = Xbox 360, Win32S = ? (Not sure)
             */ 
            OperatingSystem osInfo = System.Environment.OSVersion; 

            Console.WriteLine("\nCPU-C 0.01 - by Hotlands Software Inc. \nSource code: https://github.com/hotlandsoftware/cpu-c \n");
            var originalColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("Operating System\n\n");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("Operating System Properties\n");
            Console.ForegroundColor = ConsoleColor.Yellow;
            //Operating System
            Console.Write("Name: ");
            Console.ForegroundColor = originalColor;
            Console.Write(new ComputerInfo().OSFullName + "\n");
            Console.ForegroundColor = ConsoleColor.Yellow;
            //Operating System Language
            Console.Write("Language: ");
            Console.ForegroundColor = originalColor;
            Console.Write(System.Globalization.CultureInfo.CurrentCulture.EnglishName + " (" + CultureInfo.InstalledUICulture + ")\n");
            Console.ForegroundColor = ConsoleColor.Yellow;
            //Build Number
            Console.Write("Build: ");
            Console.ForegroundColor = originalColor;
            Console.Write(new ComputerInfo().OSVersion + "\n");
            Console.ForegroundColor = ConsoleColor.Yellow;
            //Architecture
            Console.Write("Architecture: ");
            Console.ForegroundColor = originalColor;
            Console.Write(GetCpuArch() + "\n");
            Console.ForegroundColor = ConsoleColor.Yellow;
            //OS Installation Date
            Console.Write("Installation Date: ");
            Console.ForegroundColor = originalColor;
            Console.Write(GetWindowsInstallationDateTime(string.Empty) + "\n");
            Console.ForegroundColor = ConsoleColor.Yellow;
            //OS Root
            /* Read value from the Registry */
            string RootPath = "";
            string Root = "";
            if (osInfo.Platform == PlatformID.Win32Windows)
            {
                RootPath = @"SOFTWARE\Microsoft\Windows\CurrentVersion";
                Root = GetRegKey(RootPath, "SystemRoot");
            }
            else if (osInfo.Platform == PlatformID.Win32NT) // Windows NT
            {
                RootPath = @"SOFTWARE\Microsoft\Windows NT\CurrentVersion";
                Root = GetRegKey(RootPath, "SystemRoot");
            }
            Console.Write("Root: ");
            Console.ForegroundColor = originalColor;
            Console.Write(Root + "\n");
            Console.ForegroundColor = ConsoleColor.Yellow;
            //License Info
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("License Information\n");
            Console.ForegroundColor = ConsoleColor.Yellow;
            //Registered Owner
            /* Read value from the Registry*/
            string RegisteredPath = "";
            string RegisteredId = "";
            if (osInfo.Platform == PlatformID.Win32Windows) // Windows 9x
            {
                RegisteredPath = @"SOFTWARE\Microsoft\Windows\CurrentVersion";
                RegisteredId = GetRegKey(RegisteredPath, "RegisteredOwner");
            }
            else if (osInfo.Platform == PlatformID.Win32NT) // Windows NT
            {
                RegisteredPath = @"SOFTWARE\Microsoft\Windows NT\CurrentVersion";
                RegisteredId = GetRegKey(RegisteredPath, "RegisteredOwner");
            }
            Console.Write("Registered Owner: ");
            Console.ForegroundColor = originalColor;
            Console.Write(RegisteredId + "\n");
            Console.ForegroundColor = ConsoleColor.Yellow;
            //Registered Organization
            /* Read value from the Registry*/
            string OrganizationPath = "";
            string OrganizationId = "";
            if (osInfo.Platform == PlatformID.Win32Windows) // Windows 9x
            {
                OrganizationPath = @"SOFTWARE\Microsoft\Windows\CurrentVersion";
                OrganizationId = GetRegKey(OrganizationPath, "RegisteredOrganization");
            }
            else if (osInfo.Platform == PlatformID.Win32NT) // Windows NT
            {
                OrganizationPath = @"SOFTWARE\Microsoft\Windows NT\CurrentVersion";
                OrganizationId = GetRegKey(OrganizationPath, "RegisteredOrganization");
            }
            Console.Write("Registered Organization: ");
            Console.ForegroundColor = originalColor;
            Console.Write(OrganizationId + "\n");
            Console.ForegroundColor = ConsoleColor.Yellow;
            //Product ID
            /* Read value from the Registry*/
            string ProductIdPath = "";
            string ProductId = "";
            if (osInfo.Platform == PlatformID.Win32Windows) // Windows 9x
            {
                ProductIdPath = @"SOFTWARE\Microsoft\Windows\CurrentVersion";
                ProductId = GetRegKey(ProductIdPath, "ProductId"); 
            }
            else if (osInfo.Platform == PlatformID.Win32NT) // Windows NT
            {
                ProductIdPath = @"SOFTWARE\Microsoft\Windows NT\CurrentVersion";
                ProductId = GetRegKey(ProductIdPath, "ProductId"); 
            }
            Console.Write("Product ID: ");
            Console.ForegroundColor = originalColor;
            Console.Write(ProductId + "\n");
            Console.ForegroundColor = ConsoleColor.Yellow;
            //Current Session
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("Current Session\n");
            Console.ForegroundColor = ConsoleColor.Yellow;
            //Computer Name
            Console.Write("Computer Name: ");
            Console.ForegroundColor = originalColor;
            Console.Write(System.Environment.MachineName + "\n");
            Console.ForegroundColor = ConsoleColor.Yellow;
            //NetBIOS Domain Name
            Console.Write("NetBIOS Name: ");
            Console.ForegroundColor = originalColor;
            Console.Write(NetUtil.GetMachineNetBiosDomain() + "\n");
            Console.ForegroundColor = ConsoleColor.Yellow;
            //Username
            Console.Write("User Name: ");
            Console.ForegroundColor = originalColor;
            Console.Write(System.Environment.UserName + "\n");
            Console.ForegroundColor = ConsoleColor.Yellow;
            //Logon Domain
            Console.Write("Logon Domain: ");
            Console.ForegroundColor = originalColor;
            Console.Write(Environment.UserDomainName + "\n");
            Console.ForegroundColor = ConsoleColor.Yellow;
            //Last Boot Time
            DateTime uptimer = DateTime.Now;
            Console.Write("Last Boot Time: ");
            Console.ForegroundColor = originalColor;
            Console.Write(uptimer - UpTime + "\n");
            Console.ForegroundColor = ConsoleColor.Yellow;
            //Current Time
            DateTime now = DateTime.Now;
            Console.Write("Current Time: ");
            Console.ForegroundColor = originalColor;
            Console.Write(now + "\n");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("\nComputer\n\n");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("CPU Properties\n");
            Console.ForegroundColor = ConsoleColor.Yellow;
            //CPU Type
            Console.Write("Name: ");
            Console.ForegroundColor = originalColor;
            Console.Write(HardwareInfo.GetProcessorInformation() + "\n");
            Console.ForegroundColor = ConsoleColor.Yellow;
            //CPU Supported Instruction Sets
            if (osInfo.Platform == PlatformID.Win32Windows) // Windows 9x
            {
                /* 
                 * Skipping because according to the documentation, IsProcessorFeaturePresent doesn't
                 * work on Windows 9x.
                */
            }
            else if (osInfo.Platform == PlatformID.Win32NT) // Windows NT 
            {
                GetCPUInstructionSets.GetInstructions();
                Console.Write("Instruction Set:");
                Console.ForegroundColor = originalColor;
                Console.Write(GetCPUInstructionSets.instructions + "\n");
                Console.ForegroundColor = ConsoleColor.Yellow;
            }
            //CPU Clockspeed
            Console.Write("Clock Speed: ");
            Console.ForegroundColor = originalColor;
            Console.Write(HardwareInfo.GetCPUCurrentClockSpeed() + " MHz " + "(" + HardwareInfo.GetCpuSpeedInGHz() + " GHz)\n");
            Console.ForegroundColor = ConsoleColor.Yellow;
            //CPU Max Clock Speed
            Console.Write("Max Clock Speed: ");
            Console.ForegroundColor = originalColor;
            Console.Write(HardwareInfo.GetCPUMaxClockSpeed() + " MHz\n");
            Console.ForegroundColor = ConsoleColor.Yellow;
            //Physical Processors
            Console.Write("No. of Physical Processors: ");
            Console.ForegroundColor = originalColor;
            Console.Write(HardwareInfo.GetCPUPhysProcessorCount() + "\n");
            Console.ForegroundColor = ConsoleColor.Yellow;
            //Logical Processors
            Console.Write("No. of Logical Processors: ");
            Console.ForegroundColor = originalColor;
            Console.Write(Environment.ProcessorCount + "\n");
            Console.ForegroundColor = ConsoleColor.Yellow;
            //Logical Processors
            Console.Write("No. of Cores: ");
            Console.ForegroundColor = originalColor;
            Console.Write(HardwareInfo.GetCPUCoreCount() + "\n");
            Console.ForegroundColor = ConsoleColor.Yellow;
            //GPU
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("GPU Properties\n");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("Name: ");
            Console.ForegroundColor = originalColor;
            Console.Write(HardwareInfo.GetGPUName() + "\n");
            Console.ForegroundColor = ConsoleColor.Yellow;
            //BIOS Properties
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("BIOS Properties\n");
            Console.ForegroundColor = ConsoleColor.Yellow;
            //BIOS Vendor
            Console.Write("Vendor: ");
            Console.ForegroundColor = originalColor;
            Console.Write(HardwareInfo.GetBIOSmaker() + "\n");
            Console.ForegroundColor = ConsoleColor.Yellow;
            //BIOS Version
            Console.Write("Version: ");
            Console.ForegroundColor = originalColor;
            Console.Write(HardwareInfo.GetBIOSversion() + "\n");
            Console.ForegroundColor = ConsoleColor.Yellow;
            //BIOS Release Date
            //Get registry value
            string ReleaseDatePath = "";
            string ReleaseDate = "";
            if (osInfo.Platform == PlatformID.Win32Windows) // Windows 9x
            {
                /* todo: find out what key this is in Windows 9x and add it */
                Console.ForegroundColor = ConsoleColor.Yellow;
            }
            else if (osInfo.Platform == PlatformID.Win32NT) // Windows NT
            {
                ReleaseDatePath = @"HARDWARE\DESCRIPTION\System\BIOS";
                ReleaseDate = GetRegKey(ReleaseDatePath, "BIOSReleaseDate");
                Console.Write("Release Date: ");
                Console.ForegroundColor = originalColor;
                Console.Write(ReleaseDate + "\n");
                Console.ForegroundColor = ConsoleColor.Yellow;
            }
            //System BIOS Version
            Console.Write("System BIOS Version: ");
            Console.ForegroundColor = originalColor;
            Console.Write(HardwareInfo.GetBIOSMajorMinversion() + "\n");
            Console.ForegroundColor = ConsoleColor.Yellow;
            //Status
            Console.Write("Status: ");
            Console.ForegroundColor = originalColor;
            Console.Write(HardwareInfo.GetBIOSstatus() + "\n");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.ForegroundColor = ConsoleColor.Green;
            //Motherboard Properties
            Console.Write("Motherboard Properties\n");
            Console.ForegroundColor = ConsoleColor.Yellow;
            //Manufacturer
            //Get value from registry
            string BoardManuPath = "";
            string BoardManu = "";
            if (osInfo.Platform == PlatformID.Win32Windows) // Windows 9x
            {
                /* todo: find out what key this is in Windows 9x and add it */
                Console.ForegroundColor = ConsoleColor.Yellow;
            }
            else if (osInfo.Platform == PlatformID.Win32NT) // Windows NT
            {
                BoardManuPath = @"HARDWARE\DESCRIPTION\System\BIOS";
                BoardManu = GetRegKey(BoardManuPath, "BaseBoardManufacturer");
                Console.Write("Manufacturer: ");
                Console.ForegroundColor = originalColor;
                Console.Write(BoardManu + "\n");
                Console.ForegroundColor = ConsoleColor.Yellow;
            }
            //Product
            //Get value from registry
            string ProductPath = "";
            string ProductVer = "";
            if (osInfo.Platform == PlatformID.Win32Windows) // Windows 9x
            {
                /* todo: find out what key this is in Windows 9x and add it */
                Console.ForegroundColor = ConsoleColor.Yellow;
            }
            else if (osInfo.Platform == PlatformID.Win32NT) // Windows NT
            {
                ProductPath = @"HARDWARE\DESCRIPTION\System\BIOS";
                ProductVer = GetRegKey(ProductPath, "BaseBoardProduct");
                Console.Write("Product: ");
                Console.ForegroundColor = originalColor;
                Console.Write(ProductVer + "\n");
                Console.ForegroundColor = ConsoleColor.Yellow;
            }
            //Version
            //Get value from registry
            string BoardVerPath = "";
            string BoardVer = "";
            if (osInfo.Platform == PlatformID.Win32Windows) // Windows 9x
            {
                /* todo: find out what key this is in Windows 9x and add it */
                Console.ForegroundColor = ConsoleColor.Yellow;
            }
            else if (osInfo.Platform == PlatformID.Win32NT) // Windows NT
            {
                BoardVerPath = @"HARDWARE\DESCRIPTION\System\BIOS";
                BoardVer = GetRegKey(BoardVerPath, "BIOSVersion");
                Console.Write("Version: ");
                Console.ForegroundColor = originalColor;
                Console.Write(BoardVer + "\n");
                Console.ForegroundColor = ConsoleColor.Yellow;
            }

            //Set the CMD color to Gray
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        /// <summary>
        /// Gets the CPU architecture of the user. 
        /// </summary>
        public static string GetCpuArch()
        {
            ManagementScope scope = new ManagementScope();
            ObjectQuery query = new ObjectQuery("SELECT Architecture FROM Win32_Processor");
            ManagementObjectSearcher search = new ManagementObjectSearcher(scope, query);
            ManagementObjectCollection results = search.Get();

            ManagementObjectCollection.ManagementObjectEnumerator e = results.GetEnumerator();
            e.MoveNext();
            ushort arch = (ushort)e.Current["Architecture"];

            switch (arch)
            {
                case 0:
                    return "32-bit (x86)";
                case 1:
                    return "MIPS";
                case 2:
                    return "Alpha";
                case 3:
                    return "PowerPC";
                case 5:
                    return "ARM";
                case 6:
                    return "Itanium";
                case 9:
                    return "64-bit (x64)";
                default:
                    return "Unknown Architecture (WMI ID " + arch.ToString() + ")";
            }
        }

        /// <summary>
        /// Gets a registry keys value, allowing it to easily be printed.
        /// </summary>
        public static string GetRegKey(string sRegKeyPath, string sRegKeyName)
        {
            RegistryKey regKey = Registry.LocalMachine.OpenSubKey(sRegKeyPath, false);
            string sRegValue = regKey.GetValue(sRegKeyName).ToString();
            regKey.Close();

            return sRegValue;
        }

        /// <summary>
        /// Gets the Windows Installation date.
        /// </summary>
        public static DateTime GetWindowsInstallationDateTime(string computerName)
        {
            Microsoft.Win32.RegistryKey key = Microsoft.Win32.RegistryKey.OpenRemoteBaseKey(Microsoft.Win32.RegistryHive.LocalMachine, computerName);
            key = key.OpenSubKey(@"SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion", false);
            if (key != null)
            {
                DateTime startDate = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
                Int64 regVal = Convert.ToInt64(key.GetValue("InstallDate").ToString());

                DateTime installDate = startDate.AddSeconds(regVal);

                return installDate;
            }

            return DateTime.MinValue;
        }
        /// <summary>
        /// Gets the system's current uptime.
        /// </summary>
        public static TimeSpan UpTime
        {
            get
            {
                using (var uptime = new PerformanceCounter("System", "System Up Time"))
                {
                    uptime.NextValue();       //Call this an extra time before reading its value
                    return TimeSpan.FromSeconds(uptime.NextValue());
                }
            }
        }


    }
}

