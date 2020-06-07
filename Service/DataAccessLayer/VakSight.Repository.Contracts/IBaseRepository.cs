namespace VakSight.Repository.Contracts
{
    public interface IBaseRepository<TContext, TSignInManager>
    {
        void SetContext(TContext context);

        void SetSignInManager(TSignInManager signInManager);
    }
}
