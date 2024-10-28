using System.Runtime.InteropServices;
using PcInfoMultiPlatform.src;
using PcInfoMultiPlatform.src.Object;

namespace PcInfoMultiPlatform;
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
public class PCInfoMultiPlatform
{
    public readonly OsInfo OsInfo;
    private readonly OSPlatform myOs;

    public readonly CpuData CpuData;
    
    /// <summary>
    /// コンストラクタ
    /// </summary>
    public PCInfoMultiPlatform()
    {

        if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
        {
            myOs = OSPlatform.OSX;
        }
        else if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            myOs = OSPlatform.Windows;
        }
        else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
        {
            myOs = OSPlatform.Linux;
        }

        OsInfo = new OsInfo(myOs);
        CpuData = new CpuData(myOs);
    }

}
