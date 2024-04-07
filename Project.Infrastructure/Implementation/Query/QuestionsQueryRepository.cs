using Project.Domain.Abstractions.QueryRepositories;
using Project.Domain.Entities;
using Project.Infrastructure.DataContext;
using Project.Infrastructure.Implementation.Query.Base;


namespace Project.Infrastructure.Implementation.Query
{
    public class QuestionsQueryRepository : QueryRepository<Questions>, IQuestionsQueryRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public QuestionsQueryRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }


        // Extand your method or serce 
    }
}
