using System.Net;

namespace SambaProject.Models
{
    public class NetworkConnectionModel
    {
        public  readonly string NetworkPath = @"\\192.168.0.102\SMBDrive";
        public NetworkCredential Credentials { get; } = new NetworkCredential("user", "user");
    }
}
