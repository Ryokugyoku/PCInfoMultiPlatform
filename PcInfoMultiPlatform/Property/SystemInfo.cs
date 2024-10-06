using System;
using System.Resources;
namespace PcInfoMultiPlatform.Property
{
    /// <summary>
    /// システム情報を取得するためのクラス
    /// </summary>
    public class SystemInfo
    {
        private static ResourceManager resourceManager = new ResourceManager("PcInfoMultiPlatform.Property.SystemInfo", typeof(SystemInfo).Assembly);

        /// <summary>
        /// リソースファイルから値を取得する
        /// </summary>
        /// <param name="key">リソースキー</param>
        /// <returns>リソース値</returns>
        public static string GetResourceValue(string key)
        {
            return resourceManager.GetString(key) ?? "Unknown";
        }
    }
}