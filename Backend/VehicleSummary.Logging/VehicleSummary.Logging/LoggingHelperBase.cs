using System;
using System.Collections.Generic;
using System.Text;
using VehicleSummary.Logging.Extensions;
using VehicleSummary.Logging.Interface;

namespace VehicleSummary.Logging
{
    public abstract class LoggingHelperBase : ILoggingHelper
    {
        private readonly IList<string> _failureMessages;
        protected abstract void Log(string message);
        protected abstract void LogError(string message);

        protected LoggingHelperBase()
        {
            _failureMessages = new List<string>();
        }

        public IList<string> FailureMessages
        {
            get { return _failureMessages; }
        }


        public void LogError(String Message, Exception ex)
        {
            var message = Message + "Exception: {0}".FormatWith(ex);
            FailureMessages.Add(message);
            LogError(message);
        }

        public void Log(string format, params object[] args)
        {
            Log(format.FormatWith(args));
        }

        protected virtual void AddFailureMessage(StringBuilder stringToWrite)
        {
            _failureMessages.Add(stringToWrite.ToString());
        }

        public void LogAction(string action)
        {
            var stringToWrite = new StringBuilder();
            stringToWrite.AppendFormattedLine("Date: {0}", DateTime.Now);
            stringToWrite.AppendFormattedLine("Action: {0}", action);
            Log(stringToWrite.ToString(), "Information");
        }
    }
}
