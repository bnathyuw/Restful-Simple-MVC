﻿using System.Net;
using System.Web.Mvc;
using Playground.Web.Exceptions;

namespace Playground.Web.Controllers
{
    public class ExceptionController : Controller
    {
        public object Get(HttpStatusCode httpStatusCode) {
        	throw ExceptionFactory.Build(httpStatusCode);
        }
    }
}
