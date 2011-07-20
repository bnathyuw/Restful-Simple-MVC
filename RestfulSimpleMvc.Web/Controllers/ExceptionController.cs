using System.Net;
using System.Web.Mvc;
using RestfulSimpleMvc.Core.Exceptions;

namespace RestfulSimpleMvc.Web.Controllers
{
    public class ExceptionController : Controller
    {
        public object Get(HttpStatusCode httpStatusCode) {
        	throw new RestfulException(httpStatusCode);
        }
    }
}
