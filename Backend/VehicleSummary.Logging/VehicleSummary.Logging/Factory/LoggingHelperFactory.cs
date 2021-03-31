using System.Collections.Generic;
using VehicleSummary.Logging.Interface;

namespace VehicleSummary.Logging.Factory
{
    /// <summary>
    /// Logging factory
    /// </summary>
    public class LoggingHelperFactory
    {
        /// <summary>
        /// Manufacture method for logging with the new request Id
        /// </summary>
        /// <param name="requestId">New GUID</param>
        /// <returns>Aggregate class for log types</returns>
        public ILoggingHelper Manufacture()
        {
            var helpers = new List<ILoggingHelper>
            {
                new ConsoleLoggingHelper()
            };

            return new AggregateLoggingHelper(helpers);
        }
    }
}
