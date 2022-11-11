using System.Net;

namespace SambaProject.Models
{
    public class NetworkSettings
    {
        public const string SectionName = "NetworkSettings";
        public string NetworkPath { get; set; } = null!;
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
