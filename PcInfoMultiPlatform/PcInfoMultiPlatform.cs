namespace PcInfoMultiPlatform;

using System.Diagnostics;
using System.Runtime.InteropServices;
/// <summary>
/// PC情報を取得するためのクラス
/// <Example>
/// <code>
///     var pcInfo = new PcInfoMultiPlatform();
///     //OS名
///     Console.WriteLine(pcInfo.OsName);
///     //OSバージョン
///     Console.WriteLine(pcInfo.OsVersion);
/// </code>
/// </Example>
/// </summary>
public class PcInfoMultiPlatform
{
    private string _osName;
    private string _osVersion;
    
    /// <summary>
    /// OSの名称を返す
    /// </summary>
    public string OsName
    {
        get { return _osName; }
    }

    public string OsVersion
    {
        get { return _osVersion; }
    }

    /// <summary>
    /// コンストラクタ
    /// </summary>
    public PcInfoMultiPlatform()
    {
        _osName = GetOSName();
        _osVersion = GetOSVersion();
    }

    /// <summary>
    /// OS名の取得
    /// </summary>
    /// <returns></returns>
    private string GetOSName()
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
    private string GetOSVersion()
    {
        return Environment.OSVersion.VersionString;
    }

            /// <summary>
        /// macOSのバージョン情報を取得
        /// </summary>
        /// <returns>macOSのバージョン情報</returns>
        private string GetMacOSVersion()
        {
            try
            {
                var process = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = "sw_vers",
                        Arguments = "-productVersion",
                        RedirectStandardOutput = true,
                        UseShellExecute = false,
                        CreateNoWindow = true
                    }
                };
                process.Start();
                string version = process.StandardOutput.ReadToEnd().Trim();
                process.WaitForExit();
                return version;
            }
            catch (Exception)
            {
                return "Unknown";
            }
        }
}
