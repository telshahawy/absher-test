using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Absher.Persistence.Configuration
{
    public class DatabaseConfiguration : ConfigurationBase
    {
        private string AbsherDbContextConnectionKey = "AbsherDbContextConnection";

        private string AuthConnectionKey = "UserManagmentConnection";

        public string GetAbsherDbContextConnectionString()
        {
            return GetConfiguration().GetConnectionString(AbsherDbContextConnectionKey);
        }

        public string GetAuthConnectionString()
        {
            return GetConfiguration().GetConnectionString(AuthConnectionKey);
        }
    }
}
