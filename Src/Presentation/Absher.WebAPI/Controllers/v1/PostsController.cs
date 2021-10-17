using Absher.Application.Features.PostFeature.Commands.CreatePost;
using Absher.Application.Features.PostFeature.Commands.DeletePost;
using Absher.Application.Features.PostFeature.Commands.LikePost;
using Absher.Application.Features.PostFeature.Commands.ShareAPublicPostAsAMessage;
using Absher.Application.Features.PostFeature.Commands.ShareAPublicPostAsAPost;
using Absher.Application.Features.PostFeature.Commands.UpdatePost;
using Absher.Application.Features.PostFeature.Commands.ViewPost;
using Absher.Application.Features.PostFeature.Commands.who_liked_my_post;
using Absher.Domain.ResponseModel;
using Absher.Interfaces.Models.Dto.EditPost;
using Absher.Interfaces.Models.Dto.ViewPost;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Absher.WebAPI.Controllers.v1
{
    public class PostsController : ApiControllerBase
    {
        public PostsController(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet]
        [Route("ViewPost")]

        public async Task<ActionResult<ResponseResult<PagedResponseResult<ViewPostDto>>>> ViewPost(ViewPostCommand viewPostCommand )
        {
            return Single(await CommandAsync(viewPostCommand));
        }


        [HttpPost]
        [Route("AddPost")]

        public async Task<ActionResult<ResponseResult<bool>>> AddPost(CreatePostCommand createPostCommand)
        {
            return Single(await CommandAsync(createPostCommand));
        }


        [HttpPost]
        [Route("UpdatePost")]

        public async Task<ActionResult<ResponseResult<PagedResponseResult<EditPostDto>>>> UpdatePost(UpdatePostCommand  updatePostCommand)
        {
            return Single(await CommandAsync(updatePostCommand));
        }

        [HttpDelete]
        [Route("DeletePost")]

        public async Task<ActionResult<ResponseResult<bool>>> DeletePost(DeletePostCommand deletePostCommand)
        {
            return Single(await QueryAsync(deletePostCommand));
        }


        [HttpPost]
        [Route("LikePost")]

        public async Task<ActionResult<ResponseResult<bool>>> LikePost(LikePostCommand likePostCommand)
        {
            return Single(await QueryAsync(likePostCommand));
        }


        [HttpPost]
        [Route("WhoLikedMyPost")]

        public async Task<ActionResult<ResponseResult<bool>>> WhoLikedMyPost(WhoLikedMyPostCommand wholikedmypostCommand )
        {
            return Single(await QueryAsync(wholikedmypostCommand));
        }

        [HttpPost]
        [Route("ShareAPublicPostAsAPost")]

        public async Task<ActionResult<ResponseResult<bool>>> ShareAPublicPostAsAPost(ShareAPublicPostAsAPostCommand shareAPublicPostAsAPostCommand)
        {
            return Single(await QueryAsync(shareAPublicPostAsAPostCommand));
        }

        [HttpPost]
        [Route("ShareAPublicPostAsAMessage")]

        public async Task<ActionResult<ResponseResult<bool>>> ShareAPublicPostAsAMessage(ShareAPublicPostAsAMessageCommand shareAPublicPostAsAMessageCommand )
        {
            return Single(await QueryAsync(shareAPublicPostAsAMessageCommand));
        }



    }
}
