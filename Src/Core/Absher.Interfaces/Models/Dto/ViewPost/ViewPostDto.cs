using Absher.Interfaces.Models.Dto.Answer;
using Absher.Interfaces.Models.Dto.Poll;
using Absher.Interfaces.Models.Dto.User.UserAttachment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Absher.Interfaces.Models.Dto.ViewPost
{
   public class ViewPostDto
    {
        public int PostId { get; set; }
        public int UserProfileId { get; set; }
        public UserAttachmentDto Picture { get; set; }
        public string Region { get; set; }
        public string CreationDate { get; set; }
        public PollDto Poll { get; set; }
        public AnswerDto Answer { get; set; }

    }
}
