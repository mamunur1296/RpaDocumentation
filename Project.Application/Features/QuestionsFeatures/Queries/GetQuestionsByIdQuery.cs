using AutoMapper;
using MediatR;
using Project.Application.DTOs;
using Project.Application.Interfaces;
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
            var questions = await _unitOfWorkDb.questionQueryRepository.GetByIdAsync(request.Id);
            var questionDto = _mapper.Map<QuestionsDTO>(questions);
            return questionDto;
        }
    }


}
