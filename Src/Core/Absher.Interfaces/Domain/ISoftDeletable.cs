using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Absher.Interfaces.Domain
{
    public interface ISoftDeletable<TId> : IEntity<TId> where TId : struct
    {
        bool IsDeleted { get; set; }
        DateTime? DeletedDate { get; set; }
    }
}
