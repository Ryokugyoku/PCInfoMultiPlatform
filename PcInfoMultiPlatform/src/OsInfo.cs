using System.Diagnostics;
using System.Runtime.InteropServices;

namespace PcInfoMultiPlatform.src;

/// <summary>
/// OS情報を取得するための機関クラス
/// </summary>
public class OsInfo{

    public string OsName { get; private set; } = String.Empty;
    public string OsVersion { get; private set; } = String.Empty;

    public OSPlatform Os { get; private set; }
    private OsInfo()
    {
        Os = OSPlatform.Windows;
    }

    public OsInfo(OSPlatform os)
    {
        Os = os;
        OsName = GetOSName();
        OsVersion = GetOSVersion();
    }

    /// <summary>
    /// OS名の取得
    /// </summary>
    /// <returns></returns>
    private string GetOSName()
    {
        switch (Os)
        {
                case var os when os == OSPlatform.OSX:
                    return "macOS";
                case var os when os == OSPlatform.Windows:
                    return "Windows";
                case var os when os == OSPlatform.Linux:
                    return "Linux";
                default:
                    return "Unknown";
        }
    }

    /// <summary>
    /// OSバージョンの取得
    /// </summary>
    /// <returns></returns>
    private string GetOSVersion()
    {
        switch(Os)
        {
            case var os when os == OSPlatform.OSX:
                return GetOSXVersion();
            case var os when os == OSPlatform.Windows:
                return GetWindowsVersion();
            case var os when os == OSPlatform.Linux:
                return GetLinuxVersion();
            default:
                return "Unknown";
        }
    }

    /// <summary>
    /// macOSのバージョンを取得
    /// </summary>
    /// <returns></returns>
    private string GetOSXVersion()
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
    private string GetWindowsVersion()
    {
        return Environment.OSVersion.VersionString;
    }

    /// <summary>
    /// Linuxのバージョンを取得
    /// </summary>
    /// <returns>Linux Version</returns>
    private string GetLinuxVersion()
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