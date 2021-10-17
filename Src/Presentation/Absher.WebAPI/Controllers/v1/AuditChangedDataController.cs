using Absher.Application.Features.AuditChangedDataFeature.Queries;
using Absher.Domain.Entities.Audit;
using Absher.Domain.ResponseModel;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Absher.WebAPI.Controllers.v1
{
    [ApiVersion("1.0")]
    [Route("api/[controller]")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class AuditChangedDataController : ApiControllerBase
    {
        public AuditChangedDataController(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet]
        [Route("GetAuditChangedDataAsync")]
        //[ProducesResponseType(typeof(AuditChangedData), 200)]
        //[ProducesResponseType(400)]
        //[ProducesResponseType(404)]
        public async Task<ActionResult<ResponseResult<PagedResponseResult<AuditChangedData>>>> GetAuditChangedDataAsync([FromQuery] GetAuditChangedDataCommand getAuditChangedDataCommand)
        {
            return Single(await QueryAsync(getAuditChangedDataCommand));
        }
    }
}
