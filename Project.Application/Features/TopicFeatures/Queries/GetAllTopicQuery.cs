using AutoMapper;
using MediatR;
using Project.Application.DTOs;
using Project.Application.Features.QuestionsFeatures.Queries;
using Project.Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.Features.TopicFeatures.Queries
{
    public class GetAllTopicQuery : IRequest<IEnumerable<TopicDTO>>
    {
    }
    public class GetAllTopicHandler : IRequestHandler<GetAllTopicQuery, IEnumerable<TopicDTO>>
    {
        private readonly IUnitOfWorkDb _unitOfWorkDb;
        private readonly IMapper _mapper;
        public GetAllTopicHandler(IUnitOfWorkDb unitOfWorkDb, IMapper mapper)
        {
            _unitOfWorkDb = unitOfWorkDb;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TopicDTO>> Handle(GetAllTopicQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var tipicList = await _unitOfWorkDb.topicQueryRepository.GetAllAsync();
                var quesList = await _unitOfWorkDb.questionQueryRepository.GetAllAsync();
                foreach (var item in tipicList)
                {
                     item.QuestionsList = quesList.Where(x => x.TopicId == item.Id).ToList();
                }
                var tipicDto = tipicList.Select(x => _mapper.Map<TopicDTO>(x));
                return tipicDto;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
