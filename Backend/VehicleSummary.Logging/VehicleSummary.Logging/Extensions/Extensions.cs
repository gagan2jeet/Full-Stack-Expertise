using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VehicleSummary.Logging.Extensions
{
    /// <summary>
    /// Enumerable Extension
    /// </summary>
    public static class EnumerableExtensions
    {
        /// <summary>
        /// Join the enumerable to one string
        /// </summary>
        /// <param name="values">Array of string</param>
        /// <param name="separator">Seperator for string join</param>
        /// <returns>string</returns>
        public static string StringJoin(this IEnumerable<string> values, string separator)
        {
            return String.Join(separator, values);
        }
    }

    /// <summary>
    /// JObject Extension
    /// </summary>
    public static class JObjectExtension
    {
        /// <summary>
        /// Add the response status
        /// </summary>
        /// <param name="jobject">JObject</param>
        public static void AddStatus(this JObject jobject)
        {
            var status = jobject.ContainsKey("errors") ? "fail" : "success";
            jobject.Add("status", status);
        }
    }

    /// <summary>
    /// String builder extension
    /// </summary>
    public static class StringBuilderExtensions
    {
        /// <summary>
        /// Append the string
        /// </summary>
        /// <param name="stringBuilder">String builder object</param>
        /// <param name="format">format to be identified</param>
        /// <param name="args">Arguments</param>
        public static void AppendFormattedLine(this StringBuilder stringBuilder, string format, params object[] args)
        {
            stringBuilder.AppendLine(format.FormatWith(args));
        }
    }

    /// <summary>
    /// String object extensions
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Fomrat extension
        /// </summary>
        /// <param name="format">Format to be used</param>
        /// <param name="args">Arguments</param>
        /// <returns>string</returns>
        public static string FormatWith(this string format, params object[] args)
        {
            return args.Any() ? String.Format(format, args) : format;
        }

        /// <summary>
        /// Chck if the string is null or empty
        /// </summary>
        /// <param name="s">string to be validated</param>
        /// <returns>bool</returns>
        public static bool IsNullOrEmpty(this string s)
        {
            return string.IsNullOrEmpty(s);
        }
    }
}
