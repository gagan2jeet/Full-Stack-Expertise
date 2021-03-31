namespace VehicleSummary.Logging
{
    /// <summary>
    /// Console logging helper
    /// </summary>
    public class ConsoleLoggingHelper : LoggingHelperBase
    {
        /// <summary>
        /// Log the information
        /// </summary>
        /// <param name="message">Message to log</param>
        protected override void Log(string message)
        {
            System.Diagnostics.Trace.TraceInformation(message);
        }

        /// <summary>
        /// Log error
        /// </summary>
        /// <param name="message">Message to log</param>
        protected override void LogError(string message)
        {
            System.Diagnostics.Trace.TraceError(message);
        }
    }
}
