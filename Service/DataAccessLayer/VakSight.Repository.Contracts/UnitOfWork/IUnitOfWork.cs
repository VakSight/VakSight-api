using System;
using System.Threading.Tasks;

namespace VakSight.Repository.Contracts.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        void Commit();
        Task CommitAsync();
    }
}
