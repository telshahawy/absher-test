using Absher.Application.Common;
using Absher.Domain.ResponseModel;
using System;

namespace Absher.Application.Features.PostFeature.Commands.ShareAPublicPostAsAMessage
{
    public  class ShareAPublicPostAsAMessageCommand : CommandBase<ResponseResult<bool>>
    {
        public Guid PostId { get; set; }

   
    }
}
