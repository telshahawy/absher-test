using Absher.Application.Common;
using Absher.Domain.ResponseModel;
using System;

namespace Absher.Application.Features.PostFeature.Commands.LikePost
{
    public class LikePostCommand : CommandBase<ResponseResult<bool>>
    {
        public Guid PostId { get; set; }
        public Guid userId { get; set; }
    }
}
