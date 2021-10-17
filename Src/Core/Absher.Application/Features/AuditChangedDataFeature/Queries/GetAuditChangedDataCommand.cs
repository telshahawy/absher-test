using Absher.Application.Common;
using Absher.Domain.Entities.Audit;
using Absher.Domain.ResponseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Absher.Application.Features.AuditChangedDataFeature.Queries
{
    public class GetAuditChangedDataCommand : QueryBase<ResponseResult<PagedResponseResult<AuditChangedData>>>
    {
        public GetAuditChangedDataCommand()
        { }

        public GetAuditChangedDataCommand(string tableName)
        {
            TableName = tableName;
        }

        public string TableName { get; set; }
    }
}
