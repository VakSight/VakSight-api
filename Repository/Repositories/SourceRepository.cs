using Microsoft.EntityFrameworkCore;
using Repository.Context;
using Repository.DatabaseModels;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class SourceRepository
    {
        private DbSet<SourceRecord> sources;

        public SourceRepository(AppDbContext context)
        {
            sources = context.Sources;
        }

        public async Task CreateSourceAsync(SourceRecord source)
        {
            await sources.AddAsync(source);
        }
    }
}
