using NLog;
using ObiletCase.Interface;
using System;

namespace ObiletCase.Services
{
    public class LogService : ILogService
    {
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

        public void Info(string logMessage)
        {
            _logger.Info(logMessage);
        }

        public void Error(Exception ex, string logMessage)
        {
            _logger.Error(ex, logMessage);
        }
    }
}