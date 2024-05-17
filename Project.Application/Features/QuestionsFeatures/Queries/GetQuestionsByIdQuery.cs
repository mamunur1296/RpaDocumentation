using AutoMapper;
using MediatR;
using Project.Application.DTOs;
using Project.Application.Response;
using Project.Domain.Abstractions;
using System.Net;


namespace Project.Application.Features.QuestionsFeatures.Queries
{
    public class GetQuestionsByIdQuery : IRequest<Response<QuestionsDTO>>
    {
        public GetQuestionsByIdQuery(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; private set; }
    }

    public class GetQuestionsByIdHandler : IRequestHandler<GetQuestionsByIdQuery, Response<QuestionsDTO>>
    {
        private readonly IUnitOfWorkDb _unitOfWorkDb;
        private readonly IMapper _mapper;
        public GetQuestionsByIdHandler(IUnitOfWorkDb unitOfWorkDb, IMapper mapper)
        {
            _unitOfWorkDb = unitOfWorkDb;
            _mapper = mapper;
        }
        public async Task<Response<QuestionsDTO>> Handle(GetQuestionsByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                // Retrieve the Questions asynchronously by ID
                var Questions = await _unitOfWorkDb.questionQueryRepository.GetByIdAsync(request.Id);

                // Check if the Questions was found
                if (Questions == null)
                {
                    return new Response<QuestionsDTO>
                    {
                        Data = null,
                        Success = false,
                        StatusCode = HttpStatusCode.NotFound,
                        ErrorMessage = $"Questions with ID = {request.Id} not found"
                    };
                }

                // Map the Questions to a DTO
                var QuestionsDto = _mapper.Map<QuestionsDTO>(Questions);

                // Create a successful response
                return new Response<QuestionsDTO>
                {
                    Data = QuestionsDto,
                    Success = true,
                    StatusCode = HttpStatusCode.OK
                };
            }
            catch (Exception ex)
            {
                // Return a failure response with detailed error message
                return new Response<QuestionsDTO>
                {
                    Data = null,
                    Success = false,
                    StatusCode = HttpStatusCode.InternalServerError,
                    ErrorMessage = $"An error occurred while retrieving the Questions. Please try again later. Error: {ex.Message}"
                };
            }
        }
    }


}
