using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.Api.Error;
using Talabat.Repository.Data;

namespace Talabat.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BuggyController : ControllerBase
    {
        private StoreContext _dbContext;
        public BuggyController(StoreContext dbContext)
        {
            _dbContext = dbContext;
        }
        [HttpGet("NotFound")]
        public ActionResult GetFoundRequest()
        {
            var product = _dbContext.Products.Find(100);
            if (product is null) return NotFound(new ApiResponse(404));
            return Ok(product);
        }
        [HttpGet("ServerError")]
        public ActionResult GetServerError()
        {
            var product = _dbContext.Products.Find(100);
            var produtReturn = product.ToString();
            return Ok(produtReturn);
        }
        [HttpGet("BadRequest")]
        public ActionResult GetBadRequest()
        {
            return BadRequest();
        }
        [HttpGet("BadRequest/{id}")]
        public ActionResult GetBadRequest(int id)
        {
            return Ok();
        }

    }
}
