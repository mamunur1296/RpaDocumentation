using Project.Domain.Abstractions.CommandRepositories;
using Project.Domain.Entities;
using Project.Infrastructure.DataContext;
using Project.Infrastructure.Implementation.Command.Base;


namespace Project.Infrastructure.Implementation.Command
{
    public class QuestionsCommandRepository : CommandRepository<Questions>, IQuestionsCommandRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public QuestionsCommandRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        // Extaind your code 
    }
}
