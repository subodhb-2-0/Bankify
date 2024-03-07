using System.Net;

namespace PublicAPI.Utility
{
    public class IPAddressHelper
    {
        public string GetClientIpAddress(HttpContext httpContext)
        {
            string ipAddress = string.Empty;
            if (httpContext != null)
            {
                IPAddress remoteIpAddress = httpContext.Connection.RemoteIpAddress;
                if (remoteIpAddress != null)
                {
                    if (remoteIpAddress.AddressFamily == System.Net.Sockets.AddressFamily.InterNetworkV6)
                    {
                        remoteIpAddress = Dns.GetHostEntry(remoteIpAddress).AddressList.First(x => x.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork);
                    }
                    ipAddress = remoteIpAddress.ToString();
                }
            }
            return ipAddress;
        }
    }
}
