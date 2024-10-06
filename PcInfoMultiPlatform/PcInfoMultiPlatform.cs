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
        _osName = OsInfo.GetOSName();
        _osVersion = OsInfo.GetOSVersion();
    }

}
