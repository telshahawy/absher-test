using Absher.Persistence.Configuration.TablesConfigrations.Audit;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Absher.Persistence.Configuration.TablesConfigrations
{
    public class ConfigurationDBTables
    {
        public static void ApplyConfigurations(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AuditChangedDataConfiguration).Assembly);
        }
    }
}
