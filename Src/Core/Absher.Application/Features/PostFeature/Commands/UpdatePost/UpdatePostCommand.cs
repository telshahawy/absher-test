using Absher.Application.Common;
using Absher.Domain.ResponseModel;
using Absher.Interfaces.Models.Dto.EditPost;

namespace Absher.Application.Features.PostFeature.Commands.UpdatePost
{
    public class UpdatePostCommand : QueryBase<ResponseResult<PagedResponseResult<EditPostDto>>>
    {
    }
}
