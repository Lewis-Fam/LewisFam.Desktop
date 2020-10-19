using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace LewisFam.Desktop.Core.Helpers.ThirdParty
{
    public class Syncfusion
    {
        //public static string LicenseKey => GetLicenseKey();
        

        public static string GetLicenseKey()
        {
            //MDAxQDMxMzgyZTMyMmUzMEE0RVBCcmwvWU1PMTdaMG9sZTI0VnkvQWh4ajlESVp3ajdCVyt0TnZyMmc9
            var key = File.ReadAllText("SyncfusionLicense.txt");

            return key;
        }
    }
}
