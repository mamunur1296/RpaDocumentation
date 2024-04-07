using Project.Domain.Abstractions.CommandRepositories.Base;
using Project.Domain.Entities;

namespace Project.Domain.Abstractions.CommandRepositories
{
    public interface ITopicCommandRepository : ICommandRepository<Topic>
    {
        // Extand for all if any
    }
}
