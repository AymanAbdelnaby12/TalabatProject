namespace Talabat.Api.Error
{
    public class ApiResponce
    {
        // This class To Get Api DefultResponse 
        public int StatusCode { set; get; }
        public string? Message { set; get; }

        public ApiResponce(int statusCode, string? message=null)
        {
            StatusCode = statusCode;
            Message = message ?? GetDefultMessageForStatusCode(StatusCode);
        }

        public string GetDefultMessageForStatusCode(int statusCode)
        {
            return StatusCode switch
            {
                400 => "Bad Request",
                401 => "you are Not Authorized",
                404 => "Resource Not Found",
                500 => "Internal Server Error",
                _ => string.Empty

            };
        }
    }
}
