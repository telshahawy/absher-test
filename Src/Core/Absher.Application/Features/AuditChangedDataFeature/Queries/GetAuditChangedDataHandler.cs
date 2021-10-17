using Absher.Domain.Entities.Audit;
using Absher.Domain.ResponseModel;
using Absher.Interfaces.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Absher.Application.Features.AuditChangedDataFeature.Queries
{
    public class GetAuditChangedDataHandler : IRequestHandler<GetAuditChangedDataCommand, ResponseResult<PagedResponseResult<AuditChangedData>>>
    {
        private readonly IReadService<AuditChangedData> _readService;

        public GetAuditChangedDataHandler(IReadService<AuditChangedData> readService)
        {
            _readService = readService ?? throw new ArgumentNullException(nameof(readService));
        }

        public async Task<ResponseResult<PagedResponseResult<AuditChangedData>>> Handle(GetAuditChangedDataCommand request, CancellationToken cancellationToken)
        {
            return new ResponseResult<PagedResponseResult<AuditChangedData>>(new PagedResponseResult<AuditChangedData>((await _readService.GetManyAsync()).ToList()));
        }
    }
}
