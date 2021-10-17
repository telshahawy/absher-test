using Absher.Domain.Entities;
using Absher.Domain.ResponseModel;
using Absher.Interfaces.Repositories;
using Absher.Interfaces.Services;
using Absher.Resource;
using Absher.Utility.Exceptions;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Absher.Application.Features.PostFeature.Commands.CreatePost
{
   public class CreatePostCommandHandler : IRequestHandler<CreatePostCommand, ResponseResult<bool>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWriteService<Post> _writeService;

        public CreatePostCommandHandler(IWriteService<Post> writeService, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _writeService = writeService ?? throw new ArgumentNullException(nameof(writeService));
        }

        public async Task<ResponseResult<bool>> Handle(CreatePostCommand request, CancellationToken cancellationToken)
        {
            //var post = _mapper.Map<Post>(request);
            //_writeRepository.Add(post);
            /* bool result = (await _unitOfWork.CommitAsync()) > 0;

             if (result)
                 return new ResponseResult<bool>(result);
             else
                 throw new SaveFailureException(Message_Resource.SaveField);*/

            ResponseResult<bool> responseResult = new ResponseResult<bool>();
            responseResult.IsSuccess = true;
            responseResult.Status = System.Net.HttpStatusCode.OK;
            responseResult.Entity = true;

             return await Task.FromResult(responseResult);
        }
    }
}
