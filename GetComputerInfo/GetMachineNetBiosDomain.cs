// Code written by Duncan Smart:
// https://dotsmart.wordpress.com/2009/03/11/getting-a-machine%E2%80%99s-netbios-domain-name-in-csharp/ 

using System;
using System.Runtime.InteropServices;
using System.ComponentModel;

class NetUtil
{
    [DllImport("netapi32.dll", CharSet = CharSet.Auto)]
    static extern int NetWkstaGetInfo(string server,
        int level,
        out IntPtr info);

    [DllImport("netapi32.dll")]
    static extern int NetApiBufferFree(IntPtr pBuf);

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    class WKSTA_INFO_100
    {
        public int wki100_platform_id;
        [MarshalAs(UnmanagedType.LPWStr)]
        public string wki100_computername;
        [MarshalAs(UnmanagedType.LPWStr)]
        public string wki100_langroup;
        public int wki100_ver_major;
        public int wki100_ver_minor;
    }

    public static string GetMachineNetBiosDomain()
    {
        IntPtr pBuffer = IntPtr.Zero;

        WKSTA_INFO_100 info;
        int retval = NetWkstaGetInfo(null, 100, out pBuffer);
        if (retval != 0)
            throw new Win32Exception(retval);

        info = (WKSTA_INFO_100)Marshal.PtrToStructure(pBuffer, typeof(WKSTA_INFO_100));
        string domainName = info.wki100_langroup;
        NetApiBufferFree(pBuffer);
        return domainName;
    }
}