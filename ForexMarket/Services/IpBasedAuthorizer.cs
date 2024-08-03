namespace ForexMarket.Services
{
    public class IpBasedAuthorizer : IAuthorizer
    {
        private readonly IHttpContextAccessor _httpContext;
        private readonly IConfiguration _configuration;

        public IpBasedAuthorizer(IConfiguration configuration, IHttpContextAccessor httpContext)
        {
            _configuration = configuration;
            _httpContext = httpContext;
        }

        public bool IsAdmin()
        {
            string defaultAdmin = "Syeed";
            string admn = _configuration.GetSection("Security").GetValue<string>("Admin");
            return string.Compare(defaultAdmin, admn) == 0;
        }

        public bool IsAuthorize()
        {
            var ipV4 = _httpContext.HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
            var validIp = _configuration.GetSection("Security").GetValue<string>("ValidIp");
            return string.Compare(ipV4, validIp) == 0;
        }
    }
}
