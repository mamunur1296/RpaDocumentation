using AutoMapper;
using MediatR;
using Project.Application.DTOs;
using Project.Domain.Abstractions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;


namespace Project.Application.Features.ChapterFeatures.Queries
{
    public class GetAllChapterQuery : IRequest<IEnumerable<ChapterDTO>>
    {
    }
    public class GetAllChapterHandler : IRequestHandler<GetAllChapterQuery, IEnumerable<ChapterDTO>>
    {
        private readonly IUnitOfWorkDb _unitOfWorkDb;
        private readonly IMapper _mapper;
        public GetAllChapterHandler(IUnitOfWorkDb unitOfWorkDb, IMapper mapper)
        {
            _unitOfWorkDb = unitOfWorkDb;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ChapterDTO>> Handle(GetAllChapterQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var ChapterList = await _unitOfWorkDb.chapterQueryRepository.GetAllAsync();
                var tipicList = await _unitOfWorkDb.topicQueryRepository.GetAllAsync();
                var quesList = await _unitOfWorkDb.questionQueryRepository.GetAllAsync();
                foreach (var item in ChapterList)
                {
                    item.tipicList = tipicList.Where(x => x.Chapterid == item.Id).ToList();
                   
                }
                foreach (var item in tipicList)
                {
                    item.QuestionsList = quesList.Where(x => x.TopicId == item.Id).ToList();
                }
                var chapterDto = ChapterList.Select(item => _mapper.Map<ChapterDTO>(item));
                return chapterDto;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
