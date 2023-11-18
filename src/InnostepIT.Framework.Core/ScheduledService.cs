using InnostepIT.Framework.Core.Contract;
using InnostepIT.Framework.Core.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace InnostepIT.Framework.Core
{
    /// <summary>
    ///     Scheduled service, which is executing certain Subtasks in a cycle.
    /// </summary>
    /// <typeparam name="THosted">The type of ExecutorTask that needs to be executed in cycles.</typeparam>
    public class ScheduledService<THosted> : IHostedService, IDisposable
    {
        private readonly bool _autoStart;
        private readonly string _hostedTypeName;

        private readonly ILogger<ScheduledService<THosted>>? _logger;
        private readonly IScheduledService? _scheduledService;
        private bool _booting = true;
        private Timer _timer;

        public ScheduledService(ILogger<ScheduledService<THosted>>? logger, IScheduledService? scheduledService, int intervalInMilliseconds = Timeout.Infinite, bool autoStart = true)
        {
            _hostedTypeName = typeof(THosted).ToLogFriendlyName();
            _scheduledService = scheduledService;
            _autoStart = autoStart;
            CurrentInterval = autoStart ? intervalInMilliseconds : Timeout.Infinite;
            ConfiguredInterval = intervalInMilliseconds;
            _logger = logger;
        }

        public int ConfiguredInterval { get; private set; }
        public int CurrentInterval { get; private set; }

        public void Dispose()
        {
            _timer?.Dispose();
        }

        /// <summary>
        ///     Starts the execution of the cycle.
        /// </summary>
        /// <param name="stoppingToken"></param>
        /// <returns></returns>
        public Task StartAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("ScheduledService<{TypeName}>: Starting scheduler", _hostedTypeName);

            // prevent service to start on boot up if autostart is disabled.
            if (_booting && _autoStart == false)
            {
                _booting = false;
                return Task.CompletedTask;
            }

            // change current interval to configured interval value and start timer.
            CurrentInterval = ConfiguredInterval;
            if (_timer == null)
                _timer = new Timer(DoWork, null, 0, CurrentInterval);
            else
                _timer.Change(0, CurrentInterval);

            return Task.CompletedTask;
        }

        /// <summary>
        ///     Stopping the execution of the cycle.
        /// </summary>
        /// <param name="stoppingToken"></param>
        /// <returns></returns>
        public Task StopAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("ScheduledService<{HostedTypeName}>: Stopping scheduler", _hostedTypeName);
            CurrentInterval = Timeout.Infinite;
            _timer?.Change(Timeout.Infinite, Timeout.Infinite);

            return Task.CompletedTask;
        }

        /// <summary>
        /// </summary>
        /// <param name="interval">interval in milliseconds</param>
        public (int oldValue, int newValue) ChangeInterval(int interval)
        {
            var oldConfiguredInterval = ConfiguredInterval;
            ConfiguredInterval = interval;
            var newConfiguredInterval = ConfiguredInterval;

            return (oldConfiguredInterval, newConfiguredInterval);
        }

        private async void DoWork(object state)
        {
            if (CurrentInterval != Timeout.Infinite)
                await _scheduledService.ExecuteAsync();
        }
    }
}
