using System.Diagnostics;
using System.Runtime.InteropServices;
using PcInfoMultiPlatform.src.Object;

namespace  PcInfoMultiPlatform.src.ThreadsTransaction
{
    /// <summary>
    /// Cpuの情報を更新するクラス
    /// </summary>
    public class CpuTransaction
    {
        private readonly int _interval;

        /// <summary>
        /// CPUの情報を収集するスレッド
        /// </summary>
        private Task CpuThread;

        /// <summary>
        /// CPUの情報を収集するクラスのコンストラクタ
        /// </summary>
        /// <param name="cpuData"></param>
        /// <param name="interval">データを取得する感覚(ms) デフォルト100ms</param>
        public CpuTransaction(CpuData cpuData, OSPlatform os,int interval = 100)
        {
            _interval = interval;
            CpuThread = UpdateCpuData(cpuData,os);
        }

        /// </summary>
        /// CPUの情報を更新するメソッド
        /// <param name="cpuData"></param>
        /// <param name="osPlatform"></param>
        /// <returns></returns>
        private async Task UpdateCpuData(CpuData cpuData,OSPlatform osPlatform)
        {
            do{
                switch(osPlatform)
                {
                    case var ostype when ostype == OSPlatform.OSX:
                        SetMacOsCpuData(cpuData);
                        break;
                    case var ostype when ostype == OSPlatform.Windows:
                        
                        break;
                    case var ostype when ostype == OSPlatform.Linux:
                        
                        break;
                }
                await Task.Delay(_interval);
            }while(true);
        }

        /// <summary>
        /// MacOsのCPU情報を取得し、格納するメソッド
        /// </summary>
        /// <param name="cpuData"></param>
        /// <exception cref="NotImplementedException"></exception>
        private void SetMacOsCpuData(CpuData cpuData)
        {
            try
            {
                // osascriptを使用してパスワードを取得
                var passwordScript = new ProcessStartInfo
                {
                    FileName = "/usr/bin/osascript",
                    Arguments = "-e 'Tell application \"System Events\" to display dialog \"Enter your password:\" default answer \"\" with hidden answer' -e 'text returned of result'",
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                };

                var passwordProcess = new Process
                {
                    StartInfo = passwordScript
                };

                passwordProcess.Start();
                string password = passwordProcess.StandardOutput.ReadToEnd().Trim();
                passwordProcess.WaitForExit();
                
                var process = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = "sudo",
                        Arguments = "powermetrics --samplers cpu_power -i 1000 -n 1",
                        RedirectStandardOutput = true,
                        RedirectStandardError = true,
                        UseShellExecute = false,
                        CreateNoWindow = true,
                        Verb = "runas" // 管理者権限で実行
                    }
                };
                process.Start();
                string output = process.StandardOutput.ReadToEnd();
                string error = process.StandardError.ReadToEnd();
                process.WaitForExit();

                ParsePowerMetrics(output, cpuData);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving power metrics: {ex.Message}");
            }
        }

        /// <summary>
        /// PowerMetricsの出力を解析して、CPUデータオブジェクトに値を設定するメソッド
        /// </summary>
        /// <param name="output"></param>
        /// <param name="cpuData"></param>
        private void ParsePowerMetrics(string output, CpuData cpuData)
        {
            // 出力を解析して、cpuDataオブジェクトに値を設定します。
            // ここでは簡単な例として、出力からCPU消費電力の値を抽出する方法を示します。
            foreach (var line in output.Split('\n'))
            {
                if (line.Contains("CPU Power:"))
                {
                    cpuData.CpuPower = ExtractPowerValue(line);
                }
                else if (line.Contains("GPU Power:"))
                {
                    cpuData.InternalGpuPower = ExtractPowerValue(line);
                }
                else if (line.Contains("ANE Power:"))
                {
                    cpuData.ANEPower = ExtractPowerValue(line);
                }
            }
        }

        private int ExtractPowerValue(string line)
        {
            var parts = line.Split(':');
            if (parts.Length > 1 && int.TryParse(parts[1].Trim().Split(' ')[0], out int value))
            {
                return value;
            }
            return 0;
        }
    }
}