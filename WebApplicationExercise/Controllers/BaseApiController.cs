using System.Web.Http;
using WebApplicationExercise.Filters;

namespace WebApplicationExercise.Controllers
{
    [LogFilter]
    //[ExceptionFilter]
    public class BaseApiController : ApiController
    {
    }
}
