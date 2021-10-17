using Absher.Application.Common;
using Absher.Domain.ResponseModel;
using Absher.Interfaces.Models.Dto.User.UserAttachment;

namespace Absher.Application.Features.EditProfile.Queries
{
    public class EditProfileCommand : QueryBase<ResponseResult<bool>>
    {
        public int UserID { get; set; }
        public UserAttachmentDto Picture { get; set; }
    }
}
