using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.ExceptionHandling;
using quZONE.Common.Logging;

namespace quZONE.Api.Infrastructure
{
    public class GlobalExceptionLogger : ExceptionLogger
    {
        readonly Logger _logger = new Logger();

        public override void Log(ExceptionLoggerContext context)
        {
            _logger.Error(String.Format("Unhandled exception thrown in {0} for request {1}: {2}",
                                        context.Request.Method, context.Request.RequestUri, context.Exception));
        }
    }
}