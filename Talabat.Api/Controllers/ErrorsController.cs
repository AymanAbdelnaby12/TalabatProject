using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.Api.Error;

namespace Talabat.Api.Controllers
{
    // this controller to redirect to error page hena lma ykoon el user 3aml request 3ala url msh mawgoda f hy3ml redirect lel error page
    [Route("error/{code}")]
    [ApiController]
    [ApiExplorerSettings(IgnoreApi = true)]
    
    public class ErrorsController : ControllerBase
    {
        public ActionResult Error(int code)
        {
            return NotFound(new ApiResponse(code));
        }
    }
}
