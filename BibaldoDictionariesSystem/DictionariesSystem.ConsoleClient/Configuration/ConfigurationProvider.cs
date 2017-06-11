using System;
using System.Configuration;

namespace DictionariesSystem.ConsoleClient.Configuration
{
    public class ConfigurationProvider : IConfigurationProvider
    {
        public bool IsTestEnvironment()
        {
            return bool.Parse(ConfigurationManager.AppSettings["IsTestEnvironment"]);
        }        
    }
}
