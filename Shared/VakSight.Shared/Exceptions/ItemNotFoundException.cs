namespace VakSight.Shared.Exceptions
{
    public class ItemNotFoundException : BaseException
    {
        public ItemNotFoundException(string message) : base(message)
        {
            ErrorCode = ErrorCodes.NotFound;
        }
    }
}
