using Repository.Repositories;
using System.Threading.Tasks;

namespace Repository.UnitOfWork
{
    public interface IUnitOfWork
    {
        UserRepository Users { get; set; }
        Task CommitAsync();
    }
}