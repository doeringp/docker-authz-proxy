using AuthzProxy.Extensions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AuthzProxy
{
    public class AuthorizationMiddleware : IMiddleware
    {
        private readonly IAuthorizationService _authzService;
        private readonly AuthzProxyOptions _options;
        private readonly ILogger _logger;

        public AuthorizationMiddleware(IAuthorizationService authzService,
            IOptionsSnapshot<AuthzProxyOptions> options, ILogger<AuthorizationMiddleware> logger)
        {
            _authzService = authzService;
            _options = options?.Value ?? throw new ArgumentNullException(nameof(options));
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            var result = await _authzService.AuthorizeAsync(
                    context.User, "RequireAuthenticatedUser");

            if (result.Succeeded || IsInWhiteList(context.Request.Path))
                await next.Invoke(context);
            else
                await context.ChallengeAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }

        public bool IsInWhiteList(string path)
        {
            _logger.LogInformation("Whitelist check for: {path}", path);
            IList<string> whitelist = _options.Whitelist.ToList(",");
            foreach (var pattern in whitelist)
            {
                var re = new Regex(pattern);
                if (re.IsMatch(path))
                {
                    _logger.LogInformation("The url path is matching the whitelist pattern: {pattern}", pattern);
                    return true;
                }
            }
            _logger.LogInformation("The url path is not in the whitelist.");
            return false;
        }
    }
}