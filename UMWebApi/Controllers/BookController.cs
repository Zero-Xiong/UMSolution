using log4net;
using System.Web.Http;

namespace UMWebApi.Controllers
{
    public class BookController : ApiController
    {
        private readonly ILog log = LogManager.GetLogger(typeof(BookController));

        [Authorize]
        [HttpGet]
        public IHttpActionResult GetBooks()
        {
            return Ok(new string[] { "Book1", "Book2", "Book3" });
        }
    }
}
