using LewisFam.Common.Config;
using LewisFam.Common.Util;
using System.IO;

namespace LewisFam.Desktop.Core.Config
{
    /// <summary>The app config.</summary>
    public class AppConfig : Common.Config.AppConfig, IAppConfig
    {
        public virtual string EncryptionToken { get; set; } = @"<robinhood_tdl>";     

        public virtual string MyDefaultWatchlistFileName { get; set; } = "c72f1952-9df0-4fee-adb8-248e8533839c.json";

        public virtual string MyWatchlistFullFileName => WatchlistPath + MyDefaultWatchlistFileName;

        public virtual string OptionOrderItemsFileName { get; set; } = @"_option_orderitems.csv";

        public override string RootDataPath { get; set; } = @"E:\_apps\data\stocks\";

        //public virtual string SyncFusionKey { get; set; } = "MzE2NDI3QDMxMzgyZTMyMmUzMEE0RVBCcmwvWU1PMTdaMG9sZTI0VnkvQWh4ajlESVp3ajdCVyt0TnZyMmc9";

        public virtual string WatchlistFileName { get; set; }

        public virtual string WatchlistPath => @$"{RootDataPath}watchlists\";

        public virtual string OptionWatchlistPath => ".\\option-watchlist.jsons";

        /// <summary>
        /// Loads the.
        /// </summary>
        /// <param name="json">The json.</param>
        /// <returns>An AppConfig.</returns>
        public static AppConfig Load(string json = defaultFileName)
        {               
            return JsonUtil.DeserializeObject<AppConfig>(json);
        }

        /// <summary>Save</summary>
        /// <param name="config"></param>
        /// <param name="path">  </param>
        /// <param name="format"></param>
        /// <exception cref="PathTooLongException"></exception>
        /// <exception cref="DirectoryNotFoundException"></exception>
        /// <exception cref="IOException"></exception>
        /// <exception cref="System.UnauthorizedAccessException"></exception>
        /// <exception cref="System.Security.SecurityException"></exception>
        public static void Save(AppConfig config, bool format = false, string path = defaultFileName)
        {
            File.WriteAllText(path, JsonUtil.ToJson(config, format));
        }

        /// <summary>The default file name.</summary>
        private const string defaultFileName = "appconfig.json";
    }
}