using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RentalDataWarehouse
{
    /// <summary>
    /// Configuration data from the appsettings.json file. Populated in the Startup.Configure() method.
    /// </summary>
    public static class ConfigurationData
    {
        private static Dictionary<string, string> _connectionStrings;
        private static Dictionary<string, string> _appSettings;
        private static Dictionary<string, string> _businessInfo;

        public static Dictionary<string, string> ConnectionStrings { get { return _connectionStrings; } }
        public static Dictionary<string, string> AppSettings { get { return _appSettings; } }
        public static Dictionary<string, string> BusinessInfo { get { return _businessInfo; } }

        /// <summary>
        /// Add the connection strings and app settings to the ConfigurationData class. Useful elsewhere in the application.
        /// </summary>
        /// <param name="connectionStrings">The connection strings to make available.</param>
        /// <param name="appSettings">The app settings to make available.</param>
        public static void Populate(Dictionary<string, string> connectionStrings, Dictionary<string, string> appSettings, Dictionary<string, string> businessInfo)
        {
            _connectionStrings = connectionStrings;
            _appSettings = appSettings;
            _businessInfo = businessInfo;
        }
    }
}