namespace PcInfoMultiPlatform.Tests;
using System.Runtime.InteropServices;

public class OSNameTest
{
    readonly private string _osName;

    private PcInfoMultiPlatform _pcInfoMultiPlatform;

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
        _pcInfoMultiPlatform = new PcInfoMultiPlatform();
    }
    /// <summary>
    /// OS名称/バージョンの取得テスト
    /// </summary>
    [Fact]
    public void GetNameWindows()
    {
        Assert.Equal(_osName, _pcInfoMultiPlatform.OsName);
        Console.WriteLine("TestOS:"+_pcInfoMultiPlatform.OsName);
        Assert.NotEqual(Property.SystemInfo.GetResourceValue("Unknown").ToString(),_pcInfoMultiPlatform.OsVersion);
        Console.WriteLine("TestOSVersion:"+_pcInfoMultiPlatform.OsVersion);
    }
}