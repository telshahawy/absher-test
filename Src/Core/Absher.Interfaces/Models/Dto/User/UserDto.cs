using Absher.Interfaces.Models.Dto.User.UserAttachment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Absher.Interfaces.Models.Dto.User
{
    public class UserDto
    {

        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public UserAttachmentDto Picture { get; set; }
        public string Email { get; set; }

    }
}
