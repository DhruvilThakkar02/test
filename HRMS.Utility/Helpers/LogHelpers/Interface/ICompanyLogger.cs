﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Utility.Helpers.LogHelpers.Interface
{
    public interface ICompanyLogger
    {
        void LogDebug(string message);
        void LogInformation(string message, params object[] args);
        void LogWarning(string message, params object[] args);
        void LogError(Exception ex, string message, params object[] args);
        void LogFatal(Exception ex, string message, params object[] args);
    }
}
