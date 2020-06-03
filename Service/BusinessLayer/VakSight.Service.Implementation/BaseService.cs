using System;
using System.Threading.Tasks;
using VakSight.Repository.Contracts.UnitOfWork;

namespace VakSight.Service.Implementation
{
    public class BaseService
    {
        private readonly IUnitOfWorkFactory unitOfWorkFactory;

        public BaseService(IUnitOfWorkFactory unitOfWorkFactory)
        {
            this.unitOfWorkFactory = unitOfWorkFactory;
        }

        protected async Task<T> Run<T>(Func<IUnitOfWork, Task<T>> action)
        {
            using (var unitOfWork = unitOfWorkFactory.Create())
            {
                var result = await action(unitOfWork);
                await unitOfWork.CommitAsync();
                return result;
            }
        }

        protected async Task<T> RunWithoutCommit<T>(Func<IUnitOfWork, Task<T>> action)
        {
            using (var unitOfWork = unitOfWorkFactory.Create())
            {
                return await action(unitOfWork);
            }
        }

        protected async Task Run(Func<IUnitOfWork, Task> action)
        {
            using (var unitOfWork = unitOfWorkFactory.Create())
            {
                await action(unitOfWork);
                await unitOfWork.CommitAsync();
            }
        }

        protected async Task RunWithoutCommit(Func<IUnitOfWork, Task> action)
        {
            using (var unitOfWork = unitOfWorkFactory.Create())
            {
                await action(unitOfWork);
            }
        }
    }
}
