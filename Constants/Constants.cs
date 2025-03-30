using System.Collections.Generic;

namespace Constant
{
    public static class Constants
    {
        public static List<(string, int)> BackendServers = new List<(string, int)>
    {
        ("127.0.0.1", 6025),
        ("127.0.0.1", 6026),
        ("127.0.0.1", 6027),
        ("127.0.0.1", 6028)
    };

        public static int ServerCount => BackendServers.Count;
        public static int ClientCount => 20;
    }
}
