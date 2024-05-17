using AutoMapper;
using MediatR;
using Project.Application.DTOs;
using Project.Application.Response;
using Project.Domain.Abstractions;
using System.Net;


namespace Project.Application.Features.TopicFeatures.Queries
{
    public class GetTopicByIdQuery : IRequest<Response<TopicDTO>>
    {
        public GetTopicByIdQuery(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; private set; }
    }

    public class GetTopicByIdHandler : IRequestHandler<GetTopicByIdQuery, Response<TopicDTO>>
    {
        private readonly IUnitOfWorkDb _unitOfWorkDb;
        private readonly IMapper _mapper;
        public GetTopicByIdHandler(IUnitOfWorkDb unitOfWorkDb, IMapper mapper)
        {
            _unitOfWorkDb = unitOfWorkDb;
            _mapper = mapper;
        }
        public async Task<Response<TopicDTO>> Handle(GetTopicByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                // Retrieve the Topic asynchronously by ID
                var Topic = await _unitOfWorkDb.topicQueryRepository.GetByIdAsync(request.Id);

                // Check if the Topic was found
                if (Topic == null)
                {
                    return new Response<TopicDTO>
                    {
                        Data = null,
                        Success = false,
                        StatusCode = HttpStatusCode.NotFound,
                        ErrorMessage = $"Topic with ID = {request.Id} not found"
                    };
                }

                // Map the Topic to a DTO
                var TopicDto = _mapper.Map<TopicDTO>(Topic);

                // Create a successful response
                return new Response<TopicDTO>
                {
                    Data = TopicDto,
                    Success = true,
                    StatusCode = HttpStatusCode.OK
                };
            }
            catch (Exception ex)
            {
                // Return a failure response with detailed error message
                return new Response<TopicDTO>
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
