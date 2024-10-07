using PcInfoMultiPlatform.src;

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
    
    /// <summary>
    /// コンストラクタ
    /// </summary>
    public PCInfoMultiPlatform()
    {
        OsInfo = new OsInfo();
    }

}
