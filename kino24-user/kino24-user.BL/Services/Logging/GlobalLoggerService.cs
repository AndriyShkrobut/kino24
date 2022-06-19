using kino24_user.BL.Interfaces.Logging;
using NLog;

namespace kino24_user.BL.Services.Logging
{
    public class GlobalLoggerService : IGlobalLoggerService
    {
        private readonly ILogger _logger = LogManager.GetLogger("Global Exception Handling");
        public void LogError(Exception ex)
        {
            _logger.Error($"Something went wrong: {ex}");
        }
    }
}
