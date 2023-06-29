using Microsoft.Extensions.Configuration;
using TestStack.BDDfy.Configuration;

namespace AmidoTechBddfyTests.Configuration
{
    public class ConfigAccessor
    {

        public ConfigAccessor(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        static IConfigurationRoot root;

        private static IConfigurationRoot GetIConfigurationRoot()
        {
            if (root == null)
            {
                root = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false) //remember to got to properties for the appsettings file and configure to Copy to Output = always
                .AddEnvironmentVariables()
                .Build();
            }

            return root;

        }

        public static ConfigModel GetApplicationConfiguration()
        {
            var configuration = new ConfigModel();

            var iConfig = GetIConfigurationRoot();

            iConfig.Bind(configuration);

            return configuration;
        }
    }
}
