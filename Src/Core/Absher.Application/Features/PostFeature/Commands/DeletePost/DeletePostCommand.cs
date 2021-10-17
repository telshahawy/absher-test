using Absher.Application.Common;
using Absher.Domain.ResponseModel;
using System;

namespace Absher.Application.Features.PostFeature.Commands.DeletePost
{
    public class DeletePostCommand : CommandBase<ResponseResult<bool>>
    {
        public Guid PostId { get; set; }
    }
}
