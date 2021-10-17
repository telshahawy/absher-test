using Absher.Domain.ResponseModel;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Absher.Application.Features.PostFeature.Commands.who_liked_my_post
{
    public class WhoLikedMyPostHandler : IRequestHandler<WhoLikedMyPostCommand, ResponseResult<bool>>
    {
        public async Task<ResponseResult<bool>> Handle(WhoLikedMyPostCommand request, CancellationToken cancellationToken)
        {
            ResponseResult<bool> responseResult = new ResponseResult<bool>();
            responseResult.IsSuccess = true;
            responseResult.Status = System.Net.HttpStatusCode.OK;
            responseResult.Entity = true;

            return await Task.FromResult(responseResult);
        }
    }
}

