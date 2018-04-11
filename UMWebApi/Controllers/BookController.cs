using System.Web.Http;

namespace UMWebApi.Controllers
{
    public class BookController : ApiController
    {
        [Authorize]
        [HttpGet]
        public IHttpActionResult GetBooks()
        {
            return Ok(new string[] { "Book1", "Book2", "Book3" });
        }
    }
}
