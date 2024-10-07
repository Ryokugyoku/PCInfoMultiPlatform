using System.Diagnostics;
using System.Management;
using System.Runtime.InteropServices;
using PcInfoMultiPlatform.src.Object;

namespace PcInfoMultiPlatform.src;
public class CpuInfo{
    /// <summary>
    /// Cpuの各種データを格納している
    /// </summary>
    public CpuData CpuData { get; private set; }
    
    public CpuInfo()
    {
        CpuData = new CpuData();
    }


}