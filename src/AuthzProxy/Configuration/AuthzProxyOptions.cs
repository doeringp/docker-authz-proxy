namespace AuthzProxy.Configuration
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
    }
}