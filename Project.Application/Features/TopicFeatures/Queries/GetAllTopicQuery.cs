using AutoMapper;
using MediatR;
using Project.Application.DTOs;
using Project.Application.Response;
using Project.Domain.Abstractions;
using System.Net;


namespace Project.Application.Features.TopicFeatures.Queries
{
    public class GetAllTopicQuery : IRequest<Response<IEnumerable<TopicDTO>>>
    {
    }
    public class GetAllTopicHandler : IRequestHandler<GetAllTopicQuery, Response<IEnumerable<TopicDTO>>>
    {
        private readonly IUnitOfWorkDb _unitOfWorkDb;
        private readonly IMapper _mapper;
        public GetAllTopicHandler(IUnitOfWorkDb unitOfWorkDb, IMapper mapper)
        {
            _unitOfWorkDb = unitOfWorkDb;
            _mapper = mapper;
        }

        public async Task<Response<IEnumerable<TopicDTO>>> Handle(GetAllTopicQuery request, CancellationToken cancellationToken)
        {
            try
            {
                // Retrieve all Topic asynchronously
                var TopicList = await _unitOfWorkDb.topicQueryRepository.GetAllAsync();

                // Check if the Topic list is empty
                if (TopicList == null || !TopicList.Any())
                {
                    return new Response<IEnumerable<TopicDTO>>
                    {
                        Data = null,
                        Success = false,
                        StatusCode = HttpStatusCode.NotFound,
                        ErrorMessage = "No Topic found."
                    };
                }

                // Map Topic to DTOs
                var TopicDtos = TopicList.Select(department => _mapper.Map<TopicDTO>(department)).ToList();

                // Create a successful response
                return new Response<IEnumerable<TopicDTO>>
                {
                    Data = TopicDtos,
                    Success = true,
                    StatusCode = HttpStatusCode.OK,
                    ErrorMessage = "Topic retrieved successfully."
                };
            }
            catch (Exception ex)
            {
                // Return a failure response with detailed error message
                return new Response<IEnumerable<TopicDTO>>
                {
                    Data = null,
                    Success = false,
                    StatusCode = HttpStatusCode.InternalServerError,
                    ErrorMessage = $"An error occurred while retrieving the Topic. Please try again later. Error: {ex.Message}"
                };
            }
        }
    }
}
