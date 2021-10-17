using Absher.Application.Common;
using Absher.Domain.ResponseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Absher.Application.Features.PostFeature.Commands.CreatePost
{
    public class CreatePostCommand : CommandBase<ResponseResult<bool>>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
