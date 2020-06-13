using Repository.Repositories;
using System.Threading.Tasks;

namespace Repository.UnitOfWork
{
    public interface IUnitOfWork
    {
        UserRepository Users { get; set; }
        SourceRepository Sources { get; set; }
        Task CommitAsync();
    }
}