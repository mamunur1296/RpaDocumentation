using AutoMapper;
using MediatR;
using Project.Application.DTOs;
using Project.Application.Response;
using Project.Domain.Abstractions;
using System.Net;


namespace Project.Application.Features.QuestionsFeatures.Queries
{
    public class GetAllQuestionsQuery : IRequest<IEnumerable<QuestionsDTO>>
    {
    }
    public class GetAllQuestionsHandler : IRequestHandler<GetAllQuestionsQuery, IEnumerable<QuestionsDTO>>
    {
        private readonly IUnitOfWorkDb _unitOfWorkDb;
        private readonly IMapper _mapper;
        public GetAllQuestionsHandler(IUnitOfWorkDb unitOfWorkDb, IMapper mapper)
        {
            _unitOfWorkDb = unitOfWorkDb;
            _mapper = mapper;
        }
        public async Task<IEnumerable<QuestionsDTO>> Handle(GetAllQuestionsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                // Retrieve all Questions asynchronously
                var QuestionsList = await _unitOfWorkDb.questionQueryRepository.GetAllAsync();

                // Map Questions to DTOs
                var departmentDtos = QuestionsList.Select(Question => _mapper.Map<QuestionsDTO>(Question)).ToList();

                // Create a successful response
                return departmentDtos;
            }
            catch (Exception ex)
            {
                // Return a failure response with detailed error message
                throw new Exception($"An error occurred while retrieving Questions: {ex.Message}", ex);
            }
        }
    }
}
