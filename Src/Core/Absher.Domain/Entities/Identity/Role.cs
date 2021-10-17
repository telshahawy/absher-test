using Absher.Domain.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Absher.Domain.Entities.Identity
{
    public class Role : UpdateSoftDeleteEntity<Guid>
    {
        public string Name { get; set; }
        public ICollection<UserRole> UserRoles { get; set; }

    }
}
