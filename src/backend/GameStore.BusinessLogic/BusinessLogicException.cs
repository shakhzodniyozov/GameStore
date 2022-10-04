namespace GameStore.BusinessLogic
{
    public class BusinessLogicException : Exception
    {
        public BusinessLogicException(string message, int statusCode = 500) : base(message)
        {
            StatusCode = statusCode;
        }

        public int StatusCode { get; set; } = 500;
    }
}
