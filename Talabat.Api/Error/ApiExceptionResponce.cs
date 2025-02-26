namespace Talabat.Api.Error
{
    // This class To Get Api DefultResponse on Exception Error only in internal server error
    public class ApiExceptionResponce : ApiResponse
    {
        public string? Details { get; set; }
        public ApiExceptionResponce(int statusCode, string? message = null, string? details = null) : base(statusCode, message)
        {
            Details = details;
        }
    }
}
