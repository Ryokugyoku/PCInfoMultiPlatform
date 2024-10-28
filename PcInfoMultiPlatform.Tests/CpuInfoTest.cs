namespace PcInfoMultiPlatform.Tests;
using PcInfoMultiPlatform; // 追加
public class CpuInfoTest{
    PCInfoMultiPlatform _pcInfoMultiPlatform;

    /// <summary>
    /// CPU情報テスト用クラス残すトラクタ
    /// </summary>
    public CpuInfoTest(){
        _pcInfoMultiPlatform = new PCInfoMultiPlatform();
    }

    [Fact]
    public async Task CpuParamChectAsync(){
        await Task.Delay(1000);
        Assert.NotNull(_pcInfoMultiPlatform.CpuData.CpuBrand);
    }
}