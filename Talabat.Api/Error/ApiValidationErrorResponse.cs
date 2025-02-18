namespace Talabat.Api.Error
{
    // This class To Get Api DefultResponse on Validation Error only
    public class ApiValidationErrorResponse : ApiResponce
    {
        public IEnumerable<string> Errors { get; set; }
        public ApiValidationErrorResponse() : base(400)
        {
            Errors = new List<string>();
        }

      
    }
}
