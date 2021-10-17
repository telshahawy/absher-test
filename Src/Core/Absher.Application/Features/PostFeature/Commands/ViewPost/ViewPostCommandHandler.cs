using Absher.Domain.ResponseModel;
using Absher.Interfaces.Models.Dto.Answer;
using Absher.Interfaces.Models.Dto.Poll;
using Absher.Interfaces.Models.Dto.User.UserAttachment;
using Absher.Interfaces.Models.Dto.ViewPost;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Absher.Application.Features.PostFeature.Commands.ViewPost
{
    public class ViewPostCommandHandler : IRequestHandler<ViewPostCommand, ResponseResult<PagedResponseResult<ViewPostDto>>>
    {
        public async Task<ResponseResult<PagedResponseResult<ViewPostDto>>> Handle(ViewPostCommand request, CancellationToken cancellationToken)
        {
            var data = new List<ViewPostDto>()
            {
                new ViewPostDto()
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

            ResponseResult<PagedResponseResult<ViewPostDto>> result = new ResponseResult<PagedResponseResult<ViewPostDto>>(new PagedResponseResult<ViewPostDto>(data));
            return await Task.FromResult(result);

        }
    }
}
