using System;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using NLog;

namespace WebApplicationExercise.Filters
{
    public class LogFilter : ActionFilterAttribute
    {
        private Logger _logger = LogManager.GetCurrentClassLogger();

        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            DateTime startDate = DateTime.Now;
            _logger.Info($"{actionContext.Request.Method} request to {actionContext.Request.RequestUri} started at {startDate}.");
            base.OnActionExecuting(actionContext);

            DateTime endDate = DateTime.Now;
            TimeSpan overall = endDate - startDate;
            _logger.Info(
                $"{actionContext.Request.Method} request to {actionContext.Request.RequestUri} ended at {endDate}. Time spent on request: {overall}");
        }
    }
}