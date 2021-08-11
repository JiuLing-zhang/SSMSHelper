using System;
using SSMSHelper.Model;

namespace SSMSHelper.Common
{
    public class GlobalArgs
    {
        public static readonly string AppConfigPath = $"{System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)}\\config.ini";
        public static AppConfigInfo AppConfig = new AppConfigInfo();
        public static readonly string AppVersion = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version?.ToString();
    }
}
