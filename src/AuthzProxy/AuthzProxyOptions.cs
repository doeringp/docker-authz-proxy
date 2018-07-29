using System.Collections.Generic;
using System.Linq;

namespace AuthzProxy
{
    public class AuthzProxyOptions
    {
        /// <summary>
        /// The destination URL you want to proxy.
        /// </summary>
        public string TargetUrl { get; set; }

        /// <summary>
        /// Username to log in.
        /// </summary>
        public string User { get; set; }

        /// <summary>
        /// Password to log in.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// List of Regular Expressions matching the URL path.
        /// Paths which match one of these Regular Expressions will
        /// be permitted for anonymous access.
        /// </summary>
        public string Whitelist { get; set; }
    }
}