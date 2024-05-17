using Project.Domain.Abstractions;
using Project.Domain.Abstractions.CommandRepositories;
using Project.Domain.Abstractions.QueryRepositories;
using Project.Infrastructure.DataContext;
using Project.Infrastructure.Implementation.Command;
using Project.Infrastructure.Implementation.Query;

namespace Project.Infrastructure.Implementation
{
    public class UnitOfWorkDb : IUnitOfWorkDb
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public ITopicQueryRepository topicQueryRepository { get; private set; }

        public ITopicCommandRepository topicCommandRepository { get; private set; }

        public IQuestionsCommandRepository questionCommandRepository { get; private set; }

        public IQuestionsQueryRepository questionQueryRepository { get; private set; }

        public IChapterCommandRepository chapterCommandRepository { get; private set; }

        public IChapterQueryRepository chapterQueryRepository { get; private set; }

        public UnitOfWorkDb(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
            topicCommandRepository = new TopicCommandRepository(applicationDbContext);
            topicQueryRepository = new TopicQueryRepository(applicationDbContext);
            questionCommandRepository = new QuestionsCommandRepository(applicationDbContext);
            questionQueryRepository = new QuestionsQueryRepository(applicationDbContext);
            chapterCommandRepository = new ChapterCommandRepository(applicationDbContext);
            chapterQueryRepository = new ChapterQueryRepository(applicationDbContext);
            
        }



        public async Task SaveAsync()
        {
            await _applicationDbContext.SaveChangesAsync();
        }
    }
}
