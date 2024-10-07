namespace PcInfoMultiPlatform.Tests;
using System.Runtime.InteropServices;
using PcInfoMultiPlatform; // 追加

public class OSNameTest
{
    readonly private string _osName;

    private PCInfoMultiPlatform _pcInfoMultiPlatform;

    /// <summary>
    /// OS名を設定するためのコンストラクタ
    /// </summary>
    public OSNameTest()
    {
        if(RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
        {
            _osName = "macOS";
        }
        else if(RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            _osName = "Windows";
        }
        else if(RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
        {
            _osName = "Linux";
        }
        else
        {
            _osName = "Unknown";
        }
        _pcInfoMultiPlatform = new PCInfoMultiPlatform();
    }
    /// <summary>
    /// OS名称/バージョンの取得テスト
    /// </summary>
    [Fact]
    public void GetNameWindows()
    {
        Assert.Equal(_osName, _pcInfoMultiPlatform.OsInfo.OsName);
        Console.WriteLine("TestOS:"+_pcInfoMultiPlatform.OsInfo.OsName);
        Assert.NotEqual(Property.SystemInfo.GetResourceValue("Unknown").ToString(),_pcInfoMultiPlatform.OsInfo.OsVersion);
        Console.WriteLine("TestOSVersion:"+_pcInfoMultiPlatform.OsInfo.OsVersion);
    }
}