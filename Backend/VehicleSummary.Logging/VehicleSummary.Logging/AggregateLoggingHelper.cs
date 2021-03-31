using System;
using System.Collections.Generic;
using System.Linq;
using VehicleSummary.Logging.Interface;

namespace VehicleSummary.Logging
{
    public class AggregateLoggingHelper : ILoggingHelper
    {
        private readonly IList<ILoggingHelper> _loggingHelpers;

        public AggregateLoggingHelper(IEnumerable<ILoggingHelper> loggingHelpers)
        {
            _loggingHelpers = loggingHelpers.ToList();
        }

        public AggregateLoggingHelper(params ILoggingHelper[] loggingHelpers)
        {
            _loggingHelpers = loggingHelpers.ToList();
        }

        public void Log(string format, params object[] args)
        {
            foreach (var loggingHelper in _loggingHelpers)
            {
                loggingHelper.Log(format, args);
            }
        }

        public void LogError(String Message, Exception ex)
        {
            foreach (var loggingHelper in _loggingHelpers)
            {
                loggingHelper.LogError(Message, ex);
            }
        }

        public void LogAction(string action)
        {
            foreach (var loggingHelper in _loggingHelpers)
            {
                loggingHelper.LogAction(action);
            }
        }

        public IList<string> FailureMessages
        {
            get { return _loggingHelpers.SelectMany(x => x.FailureMessages).ToList(); }
        }
    }
}