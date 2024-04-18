using AutoMapper;
using MediatR;
using Project.Domain.Abstractions;


namespace Project.Application.Features.ChapterFeatures.Commands
{
    public class UpdateChapterCommand : IRequest<string>
    {
        public string title { get; set; }
        public UpdateChapterCommand(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; private set; }

    }
    public class UpdateChapterHandler : IRequestHandler<UpdateChapterCommand, string>
    {
        private readonly IUnitOfWorkDb _unitOfWorkDb;
        private readonly IMapper _mapper;
        public UpdateChapterHandler(IUnitOfWorkDb unitOfWorkDb, IMapper mapper)
        {
            _unitOfWorkDb = unitOfWorkDb;
            _mapper = mapper;
        }


        public async Task<string> Handle(UpdateChapterCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var data = await _unitOfWorkDb.chapterQueryRepository.GetByIdAsync(request.Id);
                if (data == null) return default;
                else
                {
                    data.title = request.title;
                }
                await _unitOfWorkDb.chapterCommandRepository.UpdateAsync(data);
                await _unitOfWorkDb.SaveAsync();
                return "Successfully upddate";
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
