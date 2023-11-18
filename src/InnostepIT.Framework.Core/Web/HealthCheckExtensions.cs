using System.Net.Mime;
using System.Text.Json;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace InnostepIT.Framework.Core.Web
{
    public class HealthReportCustomEntry
    {
        public string Description { get; set; }
        public string Status { get; set; }
        public HealthStatus StatusCode { get; set; }
    }

    public class HealthCheckCustomReport
    {
        public string Status { get; init; }
        public IEnumerable<HealthReportCustomEntry> Checks { get; init; }
        public string Component { get; init; }
        public TimeSpan TotalDuration { get; init; }
        public HealthStatus StatusCode { get; init; }
    }

    public static class HealthCheckExtensions
    {
        public static IEndpointConventionBuilder MapCustomHealthChecks(this IEndpointRouteBuilder endpoints, string componentIdentifier)
        {
            return endpoints.MapHealthChecks("/health", new HealthCheckOptions
            {
                ResponseWriter = async (context, report) =>
                {
                    var checks = report.Entries.Values.Select(v => new HealthReportCustomEntry
                    {
                        Description = v.Description,
                        Status = v.Status.ToString(),
                        StatusCode = v.Status
                    });

                    var result = JsonSerializer.Serialize(new HealthCheckCustomReport
                    {
                        Status = report.Status.ToString(),
                        StatusCode = report.Status,
                        TotalDuration = report.TotalDuration,
                        Component = componentIdentifier,
                        Checks = checks
                    });
                    context.Response.ContentType = MediaTypeNames.Application.Json;
                    await context.Response.WriteAsync(result);
                }
            });
        }
    }
}
