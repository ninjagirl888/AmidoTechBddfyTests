namespace AmidoTechBddfyTests.Configuration
{
    public class ConfigModel
    {
        public string BaseUrl { get; set; }

        public string ExisitngUserId  { get; set; }

        public string CurrentPassword { get; set; }

        public string NewPassword { get; set; }

        public bool IsLocal => new Uri(BaseUrl).Host == "localhost";
    }
}