using Absher.Domain.ResponseModel;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Absher.Application.Features.EditProfile.Queries
{
    public class EditProfileHandler : IRequestHandler<EditProfileCommand, ResponseResult<bool>>
    {
        public async Task<ResponseResult<bool>> Handle(EditProfileCommand request, CancellationToken cancellationToken)
        {
            ResponseResult<bool> responseResult = new ResponseResult<bool>();
            responseResult.IsSuccess = true;
            responseResult.Status = System.Net.HttpStatusCode.OK;
            responseResult.Entity = true;
            return await Task.FromResult(responseResult);
        }
    }
}
