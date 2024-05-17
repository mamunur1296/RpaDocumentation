using Project.Domain.Abstractions.CommandRepositories;
using Project.Domain.Abstractions.QueryRepositories;

namespace Project.Domain.Abstractions
{
    public interface IUnitOfWorkDb
    {
        ITopicQueryRepository topicQueryRepository { get; }
        ITopicCommandRepository topicCommandRepository { get; }
        IQuestionsCommandRepository questionCommandRepository { get; }
        IQuestionsQueryRepository questionQueryRepository { get; }
        IChapterCommandRepository chapterCommandRepository { get; } 
        IChapterQueryRepository chapterQueryRepository { get; } 
        Task SaveAsync();
    }
}
