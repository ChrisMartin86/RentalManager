using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RentalDataWarehouse
{
    public static class ConfigurationData
    {
        private static Dictionary<string, string> _connectionStrings;
        private static Dictionary<string, string> _appSettings;

        public static Dictionary<string, string> ConnectionStrings { get { return _connectionStrings; } }
        public static Dictionary<string, string> AppSettings { get { return _appSettings; } }

        public static void Populate(Dictionary<string, string> connectionStrings, Dictionary<string, string> appSettings)
        {
            _connectionStrings = connectionStrings;
            _appSettings = appSettings;
        }
    }
}