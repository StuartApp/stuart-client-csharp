namespace StuartDelivery.Models
{
    public class Result
    {
        public ErrorResponse Error { get; set; }
    }

    public class Result<T> : Result
    {
        public T Data { get; set; }
    }
}