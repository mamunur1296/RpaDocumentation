using Project.Domain.Abstractions.CommandRepositories.Base;
using Project.Domain.Entities;


namespace Project.Domain.Abstractions.CommandRepositories
{
    public interface IChapterCommandRepository : ICommandRepository<Chapter>
    {
        // Extand for all if any
    }
}
