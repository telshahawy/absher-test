using Absher.Interfaces.Enums;
using Absher.Interfaces.Models.Dto.User.UserAttachment;
using System;
using System.Collections.Generic;
using System.Linq;

using System.Text;
using System.Threading.Tasks;

namespace Absher.Interfaces.Models.Dto.User.UserProfile
{
  public  class UserProfileDto
    {
        public int UserProfileId { get; set; }
        public string FullName { get; set; }
        public UserAttachmentDto Picture { get; set; }
        public string Sectors { get; set; }
        public string Rank { get; set; }
        public GenderEnum Gender { get; set; }
        public string Region { get; set; }
        public string Sector { get; set; }
        public string Position { get; set; }
        public string BloodType { get; set; }
        public string Email { get; set; }
        public string Education { get; set; }
        public DateTime JoiningDate { get; set; }
        public DateTime IDExpirationDate { get; set; }
    }
}


