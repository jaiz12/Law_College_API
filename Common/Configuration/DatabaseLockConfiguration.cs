using Microsoft.Extensions.Configuration;

namespace Common.Configuration
{
    public class DatabaseLockConfiguration
    {
        private readonly IConfiguration _configuration;

        //for SqlServer locking
        public object connectionStringLockObj = new object();

        public DatabaseLockConfiguration(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GetConnectionString()
        {
            return _configuration.GetConnectionString("DefaultConnection");
        }
    }
}
