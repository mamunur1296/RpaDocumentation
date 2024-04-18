using MediatR;
using Project.Domain.Abstractions;

namespace Project.Application.Features.ChapterFeatures.Commands
{
    public class DeleteChapterCommand : IRequest<string>
    {
        public DeleteChapterCommand(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; private set; }
    }
    public class DeleteChapterHandler : IRequestHandler<DeleteChapterCommand, string>
    {
        private readonly IUnitOfWorkDb _unitOfWorkDb;

        public DeleteChapterHandler(IUnitOfWorkDb unitOfWorkDb)
        {
            _unitOfWorkDb = unitOfWorkDb;
        }

        public async Task<string> Handle(DeleteChapterCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var date = await _unitOfWorkDb.chapterQueryRepository.GetByIdAsync(request.Id);
                if (date == null)
                {
                    return "Data not found";
                }
                await _unitOfWorkDb.chapterCommandRepository.DeleteAsync(date);
                await _unitOfWorkDb.SaveAsync();
                return "Completed";
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
