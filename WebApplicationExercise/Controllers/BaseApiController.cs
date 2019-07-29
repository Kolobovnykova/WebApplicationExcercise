using System.Web.Http;
using WebApplicationExercise.Filters;

namespace WebApplicationExercise.Controllers
{
    [LogFilter]
    public class BaseApiController : ApiController
    {
    }
}
