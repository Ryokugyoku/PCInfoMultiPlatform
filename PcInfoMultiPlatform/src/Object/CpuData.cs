using System.Diagnostics;
using System.Management;
using System.Runtime.InteropServices;
using Microsoft.VisualBasic;
using PcInfoMultiPlatform.src.ThreadsTransaction;

namespace PcInfoMultiPlatform.src.Object;

/// <summary>
/// CPU単位の情報
/// CpuBrand:CPUのブランド名
/// PhysicalCores:物理コア数
/// LogicalCores:論理コア数
/// AvgFrequency:CPU平均周波数
/// MaxFrequency:CPU最大周波数
/// CpuPower:CPU消費電力
/// InternalGpuPower:内蔵GPU消費電力
/// ANEPower:ニューアルコア消費電力 ※MacOS Only
/// Cores:コアのリスト
/// GpuCore:GPUコア
/// CpuUsage:CPU使用率
/// </summary>
public class CpuData
{
    public int CoreNumber { get; set; }
    public int CurrentFrequency { get; set; }
    public int MinFrequency { get; set; }
    public int MaxFrequency { get; set; }
    public int CpuUsage { get; set; }
    public string CpuBrand { get; private set; } = string.Empty;
    public int PhysicalCores { get; private set; }
    public int LogicalCores { get; private set; }
    public int AvgFrequency { get; protected set; }
    public int CpuPower { get;  set; }
    public int InternalGpuPower { get; set; }
    public int ANEPower { get; set; }
    public List<Core> Cores { get; set; } = new();
    public Core GpuCore { get; set; } = new Core();

    public CpuTransaction CpuTransaction;

    public CpuData(OSPlatform os)
    {
        SetCpuInfo(os);
        CpuTransaction = new CpuTransaction(this,os);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="os"></param>
    /// <param name="interval">更新感覚：ミリ秒</param>
    public CpuData(OSPlatform os, int interval)
    {
        SetCpuInfo(os);
        CpuTransaction = new CpuTransaction(this,os,interval);
    }

    private void SetCpuInfo(OSPlatform os)
    {
        switch(os)
        {
            case var ostype when ostype == OSPlatform.OSX:
                GetCpuInfoMacOS();
                break;
            case var ostype when ostype == OSPlatform.Windows:
                GetCpuInfoWindows();
                break;
            case var ostype when ostype == OSPlatform.Linux:
                GetCpuInfoLinux();
                break;
        }
    }

    /// <summary>
    /// MacOSのCPU基本情報を取得
    /// </summary>
    private void GetCpuInfoMacOS()
    {
        var sysctlCommands = new Dictionary<string, Action<string>>
        {
            { "machdep.cpu.brand_string", result => CpuBrand = result },
            { "hw.physicalcpu", result => PhysicalCores = int.Parse(result) },
            { "hw.logicalcpu", result => LogicalCores = int.Parse(result) }
        };

        foreach (var command in sysctlCommands)
        {
            var result = ExecuteSysctlCommand(command.Key);
            if (!string.IsNullOrEmpty(result))
            {
                command.Value(result);
            }
        }
    }

    /// <summary>
    /// Execute sysctl command
    /// </summary>
    /// <param name="argument"></param>
    /// <returns></returns>
    private string ExecuteSysctlCommand(string argument)
    {
        try
        {
            var process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "/usr/sbin/sysctl",
                    Arguments = $"-n {argument}",
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                }
            };
            process.Start();
            string output = process.StandardOutput.ReadToEnd().Trim();
            process.WaitForExit();
            return output;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error retrieving CPU info for {argument}: {ex.Message}");
            return string.Empty;
        }
    }

    private void GetCpuInfoWindows()
    {
        try
        {
            using (var searcher = new ManagementObjectSearcher("select * from Win32_Processor"))
            {
                foreach (var item in searcher.Get())
                {
                    CpuBrand = item["Name"]?.ToString() ?? "Unknown";
                    PhysicalCores = int.TryParse(item["NumberOfCores"]?.ToString(), out var cores) ? cores : 0;
                    LogicalCores = int.TryParse(item["NumberOfLogicalProcessors"]?.ToString(), out var logicalCores) ? logicalCores : 0;
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error retrieving CPU info: {ex.Message}");
        }
    }

    private string ParseCpuInfo(string cpuInfo, string key)
    {
        return cpuInfo.Split('\n')
                      .FirstOrDefault(line => line.StartsWith(key))?
                      .Split(':')[1].Trim() ?? string.Empty;
    }

    private void GetCpuInfoLinux()
    {
        try
        {
            var cpuInfo = File.ReadAllText("/proc/cpuinfo");
            CpuBrand = ParseCpuInfo(cpuInfo, "model name");
            PhysicalCores = int.Parse(File.ReadAllText("/sys/devices/system/cpu/cpu0/topology/core_id"));
            LogicalCores = int.Parse(File.ReadAllText("/sys/devices/system/cpu/online").Split('-')[1]) + 1;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error retrieving CPU info: {ex.Message}");
        }
    }
}

/// <summary>
/// コア単位の情報
/// </summary>
public class Core
{
    public int Frequency { get; protected set; }
    public int MaxFrequency { get; protected set; }
}