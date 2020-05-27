namespace VakSight.Shared.Models
{
    public class SuccessResultModel<T>
    {
        public SuccessResultModel(T data)
        {
            Data = data;
        }

        public T Data { get; private set; }
    }
}
