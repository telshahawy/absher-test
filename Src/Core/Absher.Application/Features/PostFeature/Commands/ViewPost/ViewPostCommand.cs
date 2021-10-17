using Absher.Application.Common;
using Absher.Domain.ResponseModel;
using Absher.Interfaces.Models.Dto.ViewPost;

namespace Absher.Application.Features.PostFeature.Commands.ViewPost
{
    public class ViewPostCommand : QueryBase<ResponseResult<PagedResponseResult<ViewPostDto>>>
    {
    }
}
