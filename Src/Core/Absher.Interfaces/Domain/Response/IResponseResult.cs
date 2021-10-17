using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Absher.Interfaces.Domain.Response
{
    public interface IResponseResult<TEntity> : IResponse
    {
        TEntity Entity { get; set; }
    }
}
