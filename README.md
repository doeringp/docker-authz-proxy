# docker-authz-proxy

This Docker container is a reverse proxy for other containers to enable forms authentication. \
Authorization can be done by using Regular Expressions to filter the URL path.

I've originally had this idea when I was using a filesharing webapp.
The webapp had no authentication and I didn't want everyone to upload files to my server.
So I've developed this proxy container as a reverse proxy to handle authentication and authorization.

* Reverse proxy for any destination URL
* Login form
* Simple configuration over environment variables
* One username and password
* URL path whitelisting by Regular Expressions

Image on DockerHub: [doeringp/authz-proxy](https://hub.docker.com/r/doeringp/authz-proxy)

### Environment Variables
**AuthzProxy:TargetUrl**
The destination URL you want to proxy.

**AuthzProxy:User**
Username to log in.

**AuthzProxy:Password**
Password to log in.

**AuthzProxy:Whitelist**
List of Regular Expressions matching the URL path. Paths which match one of these Regular Expressions will be permitted for anonymous access.

### Run container
docker run -dt -p 80:80 \
    -v ./keys:/root/.aspnet/DataProtection-Keys \
    -e AuthzProxy:TargetUrl='https://other-container-hostname' \
    -e AuthzProxy:User=bob \
    -e AuthzProxy:Password=secret \
    -e AuthzProxy:Whitelist='/admin' \
    doeringp/authz-proxy:latest

### Data Protection Keys
The application is using ASP.NET Core Data Protection for encrypting the session cookie.
It generates symmetric keys on startup.
I recommmend to persist the keys on a volume, if you don't want to loose the sessions after each container restart.
