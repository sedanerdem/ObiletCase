using NLog;
using ObiletCase.Interface;
using System;

namespace ObiletCase.Services
{
    public class LogService : ILogService
    {
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logMessage"></param>
        public void Info(string logMessage)
        {
            _logger.Info(logMessage);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="logMessage"></param>
        public void Error(Exception ex, string logMessage)
        {
            _logger.Error(ex, logMessage);
        }
    }
}