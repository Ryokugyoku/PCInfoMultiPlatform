using System.Diagnostics;
using System.Runtime.InteropServices;

namespace PcInfoMultiPlatform.src;

/// <summary>
/// OS情報を取得するための機関クラス
/// </summary>
internal class OsInfo{
    /// <summary>
    /// OS名の取得
    /// </summary>
    /// <returns></returns>
    public static string GetOSName()
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
        {
            return "macOS";
        }
        else if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            return "Windows";
        }
        else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
        {
            return "Linux";
        }
        else
        {
            return "Unknown";
        }
    }

    /// <summary>
    /// OSバージョンの取得
    /// </summary>
    /// <returns></returns>
    public static string GetOSVersion()
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
        {
            return GetOSXVersion();
        }
        else if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            return GetWindowsVersion();
        }
        else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
        {
            return GetLinuxVersion();
        }
        else
        {
            return "Unknown";
        }
    }

    /// <summary>
    /// macOSのバージョンを取得
    /// </summary>
    /// <returns></returns>
    private static string GetOSXVersion()
    {
        var p = new Process
        {
            StartInfo = new ProcessStartInfo
            {
                FileName = "/usr/bin/sw_vers",
                Arguments = "-productVersion",
                UseShellExecute = false,
                RedirectStandardOutput = true,
                CreateNoWindow = true
            }
        };
        p.Start();
        string version = p.StandardOutput.ReadToEnd().Trim();
        p.WaitForExit();
        return version;
    }


    /// <summary>
    /// Windowsのバージョンを取得
    /// </summary>
    /// <returns></returns>
    private static string GetWindowsVersion()
    {
        return Environment.OSVersion.VersionString;
    }

    /// <summary>
    /// Linuxのバージョンを取得
    /// </summary>
    /// <returns>Linux Version</returns>
    private static string GetLinuxVersion()
    {
        var p = new Process
        {
            StartInfo = new ProcessStartInfo
            {
                FileName = "/bin/uname",
                Arguments = "-r",
                UseShellExecute = false,
                RedirectStandardOutput = true,
                CreateNoWindow = true
            }
        };
        p.Start();
        string version = p.StandardOutput.ReadToEnd().Trim();
        p.WaitForExit();
        return version;
    }
}