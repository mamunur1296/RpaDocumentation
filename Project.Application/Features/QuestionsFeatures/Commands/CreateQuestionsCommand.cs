
using MediatR;

using Project.Domain.Abstractions;
using Project.Domain.Entities;


namespace Project.Application.Features.QuestionsFeatures.Commands
{
    public class CreateQuestionsCommand : IRequest<string>
    {
        public string title { get; set; }
        public string answers { get; set; }
        public Guid TopicId { get; set; }
    }
    public class CreateQuestionsHandler : IRequestHandler<CreateQuestionsCommand, string>
    {
        private readonly IUnitOfWorkDb _unitOfWorkDb;

        public CreateQuestionsHandler(IUnitOfWorkDb unitOfWorkDb)
        {
            _unitOfWorkDb = unitOfWorkDb;
            
        }
        public async Task<string> Handle(CreateQuestionsCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var questions = new Questions()
                {
                    title = request.title,
                    answers = request.answers,
                    TopicId=request.TopicId,
                    Id = new Guid()
                    
                    
                };
                await _unitOfWorkDb.questionCommandRepository.AddAsync(questions);
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
