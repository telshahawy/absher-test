using Absher.Application.Common;
using Absher.Domain.ResponseModel;
using System;

namespace Absher.Application.Features.PostFeature.Commands.ShareAPublicPostAsAPost
{
    public class ShareAPublicPostAsAPostCommand : CommandBase<ResponseResult<bool>>
    {
        public Guid PostId { get; set; }
    }
}
