using Absher.Domain.ResponseModel;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Absher.Application.Features.PostFeature.Commands.DeletePost
{
    public class DeletePostHandler : IRequestHandler<DeletePostCommand, ResponseResult<bool>>
    {
        public DeletePostHandler()
        { }

        public async Task<ResponseResult<bool>> Handle(DeletePostCommand request, CancellationToken cancellationToken)
        {
            ResponseResult<bool> responseResult = new ResponseResult<bool>();
            responseResult.IsSuccess = true;
            responseResult.Status = System.Net.HttpStatusCode.OK;
            responseResult.Entity = true;

            return await Task.FromResult(responseResult);
        }
    }
}
