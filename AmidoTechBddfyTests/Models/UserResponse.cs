namespace AmidoTechBddfyTests.Models
{
    public class UserResponse
    {
        public string name { get; set; }
        public string password { get; set; }
        public Contact[] contact { get; set; }
    }

    public class Contact
    {
        public string tel { get; set; }
        public string mob { get; set; }
    }
}