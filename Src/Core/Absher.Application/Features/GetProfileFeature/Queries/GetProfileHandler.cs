using Absher.Domain.ResponseModel;
using Absher.Interfaces.Enums;
using Absher.Interfaces.Models.Dto.User.UserAttachment;
using Absher.Interfaces.Models.Dto.User.UserProfile;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Absher.Application.Features.GetProfileFeature.Queries
{
    public class GetProfileHandler : IRequestHandler<GetProfileCommand, ResponseResult<PagedResponseResult<UserProfileDto>>>
    {
        public async Task<ResponseResult<PagedResponseResult<UserProfileDto>>> Handle(GetProfileCommand request, CancellationToken cancellationToken)
        {

            var data = new List<UserProfileDto>()
            {
                new UserProfileDto()
                {
                    UserProfileId = 1,
                    FullName = "ali ahmed",
                    Picture = new UserAttachmentDto()
                    {
                        FileName = "photo",
                        FilePath = "/img.png",
                        Extention = "test"
                    },
                    Sectors = "x",
                    Rank = "ax",
                    Gender = GenderEnum.Male,
                    Region = "cairo",
                    Sector= "abc",
                    Position = "xyz",
                    BloodType = "o",
                    Email="test@test.com",
                    Education= "elshrouk school",
                    JoiningDate =DateTime.Now ,
                    IDExpirationDate = DateTime.Now,
                  }
            };

            ResponseResult<PagedResponseResult<UserProfileDto>> result = new ResponseResult<PagedResponseResult<UserProfileDto>>(new PagedResponseResult<UserProfileDto>(data));
            return (result);
        }
    }
}
