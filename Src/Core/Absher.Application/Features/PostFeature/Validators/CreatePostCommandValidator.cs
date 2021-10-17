using Absher.Application.Features.PostFeature.Commands.CreatePost;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Absher.Application.Features.PostFeature.Validators
{
    public class CreatePostCommandValidator : AbstractValidator<CreatePostCommand>
    {
    }
}
