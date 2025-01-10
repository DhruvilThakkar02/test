﻿using HRMS.Utility.Helpers.LogHelpers.Interface;
using Microsoft.Extensions.Logging;

namespace HRMS.Utility.Helpers.LogHelpers.Services
{
    public class CompanyBranchLogger : ICompanyBranchLogger
    {
        
            private readonly ILogger _logger;

            public CompanyBranchLogger(ILogger<CompanyBranchLogger> logger)
            {
                _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            }

            public void LogDebug(string message)
            {
                _logger.LogDebug(message);
            }

            public void LogInformation(string message, params object[] args)
            {
                _logger.LogInformation(message, args);
            }

            public void LogWarning(string message, params object[] args)
            {
                _logger.LogWarning(message, args);
            }

            public void LogError(Exception ex, string message, params object[] args)
            {
                _logger.LogError(ex, message, args);
            }

            public void LogFatal(Exception ex, string message, params object[] args)
            {
                _logger.LogCritical(ex, message, args);
            }
        
    }
}
