using Absher.Interfaces.Domain.Response;
using Absher.Utility.CommomEnum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Absher.Interfaces.Intergartions
{
    public interface IIntegrationService
    {
        Task<IResponseResult<TOutput>> GetHttpResponse<TOutput, TInput>(HttpVerb verb, string url, string endPoint, TInput input, bool throwException = false);
    }
}
