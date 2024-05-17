using AutoMapper;
using MediatR;
using Project.Application.DTOs;
using Project.Application.Response;
using Project.Domain.Abstractions;
using System.Net;


namespace Project.Application.Features.QuestionsFeatures.Queries
{
    public class GetAllQuestionsQuery : IRequest<Response<IEnumerable<QuestionsDTO>>>
    {
    }
    public class GetAllQuestionsHandler : IRequestHandler<GetAllQuestionsQuery, Response<IEnumerable<QuestionsDTO>>>
    {
        private readonly IUnitOfWorkDb _unitOfWorkDb;
        private readonly IMapper _mapper;
        public GetAllQuestionsHandler(IUnitOfWorkDb unitOfWorkDb, IMapper mapper)
        {
            _unitOfWorkDb = unitOfWorkDb;
            _mapper = mapper;
        }
        public async Task<Response<IEnumerable<QuestionsDTO>>> Handle(GetAllQuestionsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                // Retrieve all Questions asynchronously
                var QuestionsList = await _unitOfWorkDb.questionQueryRepository.GetAllAsync();

                // Check if the Questions list is empty
                if (QuestionsList == null || !QuestionsList.Any())
                {
                    return new Response<IEnumerable<QuestionsDTO>>
                    {
                        Data = null,
                        Success = false,
                        StatusCode = HttpStatusCode.NotFound,
                        ErrorMessage = "No Questions found."
                    };
                }

                // Map Questions to DTOs
                var departmentDtos = QuestionsList.Select(Question => _mapper.Map<QuestionsDTO>(Question)).ToList();

                // Create a successful response
                return new Response<IEnumerable<QuestionsDTO>>
                {
                    Data = departmentDtos,
                    Success = true,
                    StatusCode = HttpStatusCode.OK,
                    ErrorMessage = "Question retrieved successfully."
                };
            }
            catch (Exception ex)
            {
                // Return a failure response with detailed error message
                return new Response<IEnumerable<QuestionsDTO>>
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
