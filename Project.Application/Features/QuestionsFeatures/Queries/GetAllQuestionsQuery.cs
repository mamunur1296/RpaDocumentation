using AutoMapper;
using MediatR;
using Project.Application.DTOs;
using Project.Domain.Abstractions;


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
                var dataList = await _unitOfWorkDb.questionQueryRepository.GetAllAsync();
                var data = dataList.Select(x => _mapper.Map<QuestionsDTO>(x));
                return data;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
