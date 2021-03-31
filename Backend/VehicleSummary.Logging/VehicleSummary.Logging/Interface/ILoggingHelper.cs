using System;
using System.Collections.Generic;

namespace VehicleSummary.Logging.Interface
{
    public interface ILoggingHelper
    {
        void LogAction(String action);
        void Log(string format, params object[] args);
        void LogError(String message, Exception ex);
        IList<string> FailureMessages { get; }
    }
}
