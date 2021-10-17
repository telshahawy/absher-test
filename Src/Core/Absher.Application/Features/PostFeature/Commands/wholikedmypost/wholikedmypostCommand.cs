using Absher.Application.Common;
using Absher.Domain.ResponseModel;
using System;

namespace Absher.Application.Features.PostFeature.Commands.who_liked_my_post
{
    public  class WhoLikedMyPostCommand : CommandBase<ResponseResult<bool>>
    {
        public Guid PostId { get; set; }        
    } 
}
