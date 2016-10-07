using System.Net;
using System.Threading.Tasks;
using App.Metrics;
using App.Metrics.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace AspNet.Metrics.Middleware
{
    public class HealthCheckEndpointMiddleware : AppMetricsMiddleware<AspNetMetricsOptions>
    {
        public HealthCheckEndpointMiddleware(RequestDelegate next,
            IOptions<AspNetMetricsOptions> options,
            ILoggerFactory loggerFactory,
            IMetricsContext metricsContext)
            : base(next, options, loggerFactory, metricsContext)
        {
        }

        public Task Invoke(HttpContext context)
        {
            if (Options.HealthEnabled && Options.HealthEndpoint.HasValue && Options.HealthEndpoint == context.Request.Path)
            {
                var healthStatus = MetricsContext.HealthStatus();
                var responseStatusCode = healthStatus.IsHealthy ? HttpStatusCode.OK : HttpStatusCode.InternalServerError;
                return WriteResponse(context, JsonHealthChecks.BuildJson(healthStatus), "application/json", responseStatusCode);
            }

            return Next(context);
        }
    }
}