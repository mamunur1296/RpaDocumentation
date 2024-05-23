using AutoMapper;
using MediatR;
using Project.Application.DTOs;
using Project.Application.Exceptions;
using Project.Domain.Abstractions;


namespace Project.Application.Features.QuestionsFeatures.Queries
{
    public class GetQuestionsByIdQuery : IRequest<QuestionsDTO>
    {
        public GetQuestionsByIdQuery(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; private set; }
    }

    public class GetQuestionsByIdHandler : IRequestHandler<GetQuestionsByIdQuery, QuestionsDTO>
    {
        private readonly IUnitOfWorkDb _unitOfWorkDb;
        private readonly IMapper _mapper;
        public GetQuestionsByIdHandler(IUnitOfWorkDb unitOfWorkDb, IMapper mapper)
        {
            _unitOfWorkDb = unitOfWorkDb;
            _mapper = mapper;
        }
        public async Task<QuestionsDTO> Handle(GetQuestionsByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                // Retrieve the Questions asynchronously by ID
                var Questions = await _unitOfWorkDb.questionQueryRepository.GetByIdAsync(request.Id);

                // Check if the Questions was found
                if (Questions == null)
                {
                    throw new NotFoundException("No Questions found.");
                }

                // Map the Questions to a DTO
                var QuestionsDto = _mapper.Map<QuestionsDTO>(Questions);

                // Create a successful response
                return QuestionsDto;
            }
            catch (Exception ex)
            {
                // Return a failure response with detailed error message
                throw new Exception($"An error occurred while retrieving Questions: {ex.Message}", ex);
            }
        }
    }


}
