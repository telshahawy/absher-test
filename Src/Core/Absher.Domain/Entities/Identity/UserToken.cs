using Absher.Domain.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Absher.Domain.Entities.Identity
{
    public class UserToken : UpdateSoftDeleteEntity<Guid>
    {
        public string Token { get; set; }
        public string ConnectionId { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
        public DateTime LastLoginDate { get; set; }
    }
}
