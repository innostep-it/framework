using InnostepIT.Framework.Core.Contract;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace InnostepIT.Framework.Core.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddScheduledService<TScheduledService>(this IServiceCollection services,
        int scheduleIntervalInMilliseconds)
        where TScheduledService : class, IScheduledService
    {
        services.AddTransient<TScheduledService>();
        services.AddHostedService(sp => new ScheduledService<TScheduledService>(
            sp.GetService<ILogger<ScheduledService<TScheduledService>>>(),
            sp.GetService<TScheduledService>(),
            scheduleIntervalInMilliseconds));
    }
}