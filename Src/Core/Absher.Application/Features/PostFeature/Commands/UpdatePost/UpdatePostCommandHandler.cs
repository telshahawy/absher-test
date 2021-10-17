using Absher.Domain.ResponseModel;
using Absher.Interfaces.Models.Dto.Answer;
using Absher.Interfaces.Models.Dto.EditPost;
using Absher.Interfaces.Models.Dto.Poll;
using Absher.Interfaces.Models.Dto.User.UserAttachment;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Absher.Application.Features.PostFeature.Commands.UpdatePost
{
    class UpdatePostCommandHandler : IRequestHandler<UpdatePostCommand, ResponseResult<PagedResponseResult<EditPostDto>>>
    {
        public async Task<ResponseResult<PagedResponseResult<EditPostDto>>> Handle(UpdatePostCommand request, CancellationToken cancellationToken)
        {

            var data = new List<EditPostDto>()
            {
                new EditPostDto()
                {
                    PostId = 1,
                    UserProfileId = 3,
                    Picture = new UserAttachmentDto(){

                        FileName = "photo",
                        FilePath = "/img1.png",
                        Extention = "test"
                    },
                    Region = "cairo",
                   CreationDate = "22/10/2020",
                   Poll = new PollDto()
                   {
                      PollId = 1,
                      PollQuestion = "any question",
                      VotersNumber = 70
                   },
                    Answer = new AnswerDto()
                   {
                     AnswerId = 1,
                      AnswerText = "any Text",
                      VotersNumber = 70,
                      Percent = "30%"
                   },
                  }
            };

            ResponseResult<PagedResponseResult<EditPostDto>> result = new ResponseResult<PagedResponseResult<EditPostDto>>(new PagedResponseResult<EditPostDto>(data));
            return await Task.FromResult(result);

        }
    }
}
