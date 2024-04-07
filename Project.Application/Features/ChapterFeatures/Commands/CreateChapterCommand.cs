using MediatR;
using Project.Domain.Abstractions;
using Project.Domain.Entities;


namespace Project.Application.Features.ChapterFeatures.Commands
{
    public class CreateChapterCommand  : IRequest<string>
    {
        public string title { get; set; }

    }
    public class CreateChapterHandler : IRequestHandler<CreateChapterCommand, string>
    {
        private readonly IUnitOfWorkDb _unitOfWorkDb;

        public CreateChapterHandler(IUnitOfWorkDb unitOfWorkDb)
        {
            _unitOfWorkDb = unitOfWorkDb;

        }


        public async Task<string> Handle(CreateChapterCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var questions = new Chapter()
                {
                    title = request.title,
                    Id = new Guid(),
                };
                await _unitOfWorkDb.chapterCommandRepository.AddAsync(questions);
                await _unitOfWorkDb.SaveAsync();
                return "created successfully";
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
