using MediatR;
using Project.Domain.Abstractions;


namespace Project.Application.Features.QuestionsFeatures.Commands
{
    public class DeleteQuestionsCommand : IRequest<string>
    {
        public DeleteQuestionsCommand(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; private set; }
    }
    public class DeleteQuestionsHandler : IRequestHandler<DeleteQuestionsCommand, string>
    {
        private readonly IUnitOfWorkDb _unitOfWorkDb;

        public DeleteQuestionsHandler(IUnitOfWorkDb unitOfWorkDb)
        {
            _unitOfWorkDb = unitOfWorkDb;
        }


        public async Task<string> Handle(DeleteQuestionsCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var date = await _unitOfWorkDb.questionQueryRepository.GetByIdAsync(request.Id);
                if (date == null)
                {
                    return "Data not found";
                }
                await _unitOfWorkDb.questionCommandRepository.DeleteAsync(date);
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
