using Project.Domain.Abstractions.QueryRepositories;
using Project.Domain.Entities;
using Project.Infrastructure.DataContext;
using Project.Infrastructure.Implementation.Query.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Infrastructure.Implementation.Query
{
    public class ChapterQueryRepository : QueryRepository<Chapter>, IChapterQueryRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public ChapterQueryRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        
        // Extand your method or serce 
    }
}
