using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using System.IO;
using System;

namespace AmidoTechBddfyTests.Configuration
{


    public class ConfigurationUpdater
    {
        public void UpdateConfigFile()
        {
            // Load the existing config file
            var configBuilder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            // Update the "BaseUrl" setting with the value from my system
            string baseUrl = Environment.GetEnvironmentVariable("BaseUrl");
            string userId = Environment.GetEnvironmentVariable("ExisitngUserId");

            configBuilder["BaseUrl"] = baseUrl;
            configBuilder["ExistingUserId"] = userId;

            // Create a new JsonConfigurationProvider with the updated configuration
            var jsonProvider = new JsonConfigurationProvider(new JsonConfigurationSource
            {
                Path = "appsettings.json",
                Optional = true,
                ReloadOnChange = true
            });
            jsonProvider.Set("BaseUrl", baseUrl);
            jsonProvider.Set("ExistingUserId", baseUrl);

            // Save the updated configuration to the file
            //jsonProvider.Save(); - Save doesn't work.. 

            // Optional: Force reload the configuration to reflect the changes
           // configBuilder.Reload();


        }
    }

}