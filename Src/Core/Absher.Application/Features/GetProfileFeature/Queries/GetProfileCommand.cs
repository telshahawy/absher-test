using Absher.Application.Common;
using Absher.Domain.ResponseModel;
using Absher.Interfaces.Models.Dto.User.UserProfile;

namespace Absher.Application.Features.GetProfileFeature.Queries
{
    public class GetProfileCommand : QueryBase<ResponseResult<PagedResponseResult<UserProfileDto>>>
    {
    }
}
