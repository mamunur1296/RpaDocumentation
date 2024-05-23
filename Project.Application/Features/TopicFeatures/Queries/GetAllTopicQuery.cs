using AutoMapper;
using MediatR;
using Project.Application.DTOs;
using Project.Application.Response;
using Project.Domain.Abstractions;
using System.Net;


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
                // Retrieve all Topic asynchronously
                var TopicList = await _unitOfWorkDb.topicQueryRepository.GetAllAsync();
                // Map Topic to DTOs
                var TopicDtos = TopicList.Select(department => _mapper.Map<TopicDTO>(department)).ToList();

                // Create a successful response
                return TopicDtos;
            }
            catch (Exception ex)
            {
                // Return a failure response with detailed error message
                throw new Exception($"An error occurred while retrieving Topics: {ex.Message}", ex);
            }
        }
    }
}
