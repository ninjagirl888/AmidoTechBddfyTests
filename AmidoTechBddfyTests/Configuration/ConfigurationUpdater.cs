using Microsoft.Extensions.Configuration;
using System.IO;
using System;

namespace AmidoTechBddfyTests.Configuration
{


    public class ConfigurationUpdater
    {
        public void UpdateConfigFile()
        {
            // Retrieve the value of BaseUrl from the Windows environment variables
            string baseUrl = Environment.GetEnvironmentVariable("BaseUrl");

            // Load the existing config.json file
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("config.json")
                .Build();

            // Update the BaseUrl value in the configuration
            configuration["BaseUrl"] = baseUrl;

            // Build a new configuration and write it to the config.json file
            var newConfiguration = new ConfigurationBuilder()
                .AddConfiguration(configuration)
                .Build();

            using (StreamWriter writer = new StreamWriter("config.json"))
            {
              //  newConfiguration.Save(writer);
            }
        }
    }

}