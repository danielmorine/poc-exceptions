namespace poc.CustomExceptions
{
    public class CustomTimeoutException : Exception
    {
        public CustomTimeoutException()
        {
        }

        public CustomTimeoutException(string message)
            : base(message)
        {
        }

        public CustomTimeoutException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
