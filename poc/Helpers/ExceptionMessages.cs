using System.Net;

namespace poc.Helpers
{
    public static class ExceptionMessages
    {
        public static string GetTitleMessage(this int code)
        {
            var message = code switch
            {
                (int)HttpStatusCode.BadRequest => "Your request parameters didn't validate",
                (int)HttpStatusCode.GatewayTimeout => "Gateway timeout, the upstream service was unresponsive. Please try again later",
                _ => "Server error",
            };
            return message;
        }

        public static string GetTypeMessage(this int code)
        {
            var message = code switch
            {
                (int)HttpStatusCode.BadRequest => "Bad Request",
                (int)HttpStatusCode.GatewayTimeout => "Gateway Timeout",
                _ => "Server error",
            };
            return message;
        }

        public static string GetDetailMessage(this int code, string errorMessage)
        {
            var message = code switch
            {
                (int)HttpStatusCode.InternalServerError => "An internal server error has occurred",
                _ => errorMessage,
            };
            return message;
        }
    }
}
