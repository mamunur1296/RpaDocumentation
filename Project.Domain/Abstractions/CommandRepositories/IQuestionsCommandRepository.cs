using Project.Domain.Abstractions.CommandRepositories.Base;
using Project.Domain.Entities;


namespace Project.Domain.Abstractions.CommandRepositories
{
    public interface IQuestionsCommandRepository : ICommandRepository<Questions>
    {
        // Extand for all if any
    }
}
