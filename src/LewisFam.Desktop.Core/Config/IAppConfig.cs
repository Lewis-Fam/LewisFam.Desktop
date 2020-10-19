namespace LewisFam.Desktop.Core.Config
{
    public interface IAppConfig
    {          
        string EncryptionToken { get; set; }       
        //string LastOptionsQueryFileName { get; set; }
        //string LastOptionsQueryPath { get; set; }
        string MyDefaultWatchlistFileName { get; set; }
        string MyWatchlistFullFileName { get; }
        string OptionOrderItemsFileName { get; set; }              
        string WatchlistFileName { get; set; }
        string WatchlistPath { get; }
        string OptionWatchlistPath {get;}

        #region Other
        string AppPropertiesFileName { get; set; }
        string ConfigurationsFolder { get; set; }
        string IdentityCacheDirectoryName { get; set; }
        string IdentityCacheFileName { get; set; }
        string IdentityClientId { get; set; }
        string PrivacyStatement { get; set; }
        string RootDataPath { get; set; }
        string UserFileName { get; set; }        
        #endregion
    }
}