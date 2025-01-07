using HRMS.Utility.Helpers.Enums;

namespace HRMS.Utility.Helpers.Models
{
    public class ExceptionResponse
    {
        public int StatusCode { get; set; }
        public string ExceptionMessage { get; set; }
        public string? ExceptionStackTrace { get; set; }
        public bool IsWarning { get; set; }

        public ExceptionResponse(Exception exception, bool isWarning, string exceptionMessage, StatusCode statusCode = Enums.StatusCode.BAD_REQUEST)
        {
            StatusCode = (int)statusCode;
            ExceptionMessage = $"{exceptionMessage} | Original Message: {exception?.Message ?? "No Message Available"}";
            ExceptionStackTrace = exception?.StackTrace ?? Environment.StackTrace;
            IsWarning = isWarning;
        }
    }
}
