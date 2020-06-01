using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace SoftwarePassion.LogBridge.Implementation
{
    public static class MessageFormatter
    {
        public static string FormatMessage(string message, string firstStringParam, params object[] parameters)
        {
            object[] messageParameters;
            if (parameters == null)
            {
                // When parameters is null, it means that two null parameters 
                // have been given. Beats me, why overload resolution works
                // like this.
                messageParameters = new List<object>() { firstStringParam, null }
                    .ToArray();
            }
            else
            {
                messageParameters = new List<object>() { firstStringParam }
                    .Union(parameters)
                    .ToArray();
            }

            return FormatMessage(message, messageParameters);
        }


        /// <summary>
        /// Formats the message with the given parameters.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The formatted message</returns>
        public static string FormatMessage(string message, params object[] parameters)
        {
            if (message == null)
                return null;
            if (parameters == null || parameters.Length == 0)
                return message;

            string formattedMessage;
            try
            {
                var nullFormattedParameters = parameters
                    .Select(p => p ?? "[null]")
                    .ToArray();
                formattedMessage = string.Format(CultureInfo.InvariantCulture, message, nullFormattedParameters);
            }
            catch (FormatException)
            {
                formattedMessage = message;
            }

            return formattedMessage;
        }
    }
}