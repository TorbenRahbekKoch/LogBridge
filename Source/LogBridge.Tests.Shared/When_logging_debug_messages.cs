using System;
using System.Runtime.CompilerServices;
using SoftwarePassion.Common.Core;
using Xunit;

namespace SoftwarePassion.LogBridge.Tests.Shared
{
    public abstract class When_logging_debug_messages : LogTestBase
    {
        public When_logging_debug_messages(ILogDataVerifier verifier)
            : base(Level.Debug, verifier)
        {
            LogContext.ThreadLogContext.CorrelationId = Option.None<Guid>();
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private Guid RunLambda(Func<Guid> code)
        {
            return code();
        }

        [Fact]
        public void Verify_that_level_is_enabled()
        {
            const string message = "Message";
            Guid eventId = Guid.Empty;
            if (Log.IsDebugLevelEnabled)
                eventId = Log.Debug(message);
            LogData expected = CreateExpectedLogData(eventId, message);

            VerifyLogData(expected);
        }

        [Fact]
        public void Verify_that_message_is_logged_correctly()
        {
            const string message = "Message";
            var eventId = Log.Debug(message);
            LogData expected = CreateExpectedLogData(eventId, message);

            VerifyLogData(expected);
        }

        [Fact]
        public void Verify_that_correlationid_and_message_is_logged_correctly()
        {
            var correlationId = Guid.NewGuid();
            const string message = "Message";
            var eventId = Log.Debug(correlationId, message);
            LogData expected = CreateExpectedLogData(eventId, correlationId, message);

            VerifyLogData(expected);
        }

        [Fact]
        public void Verify_that_extended_properties_and_message_is_logged_correctly()
        {
            var extended = new ExtendedProperties();
            const string message = "Message";
            var eventId = Log.Debug(extended, message);
            LogData expected = CreateExpectedLogData(eventId, message, extended.AsProperties);

            VerifyLogData(expected);
        }

        [Fact]
        public void Verify_that_extended_properties_and_message_with_non_string_parameters_is_logged_correctly()
        {
            var extended = new ExtendedProperties();
            const string message = "Message {0} {1}";
            const string formattedMessage = "Message 42 87";
            var eventId = Log.Debug(extended, message, 42, 87);
            LogData expected = CreateExpectedLogData(eventId, formattedMessage, extended.AsProperties);

            VerifyLogData(expected);
        }

        [Fact]
        public void Verify_that_extended_properties_and_message_with_string_parameters_is_logged_correctly()
        {
            var extended = new ExtendedProperties();
            const string message = "Message {0} {1}";
            const string formattedMessage = "Message 42 87";
            var eventId = Log.Debug(extended, message, "42", "87");
            LogData expected = CreateExpectedLogData(eventId, formattedMessage, extended.AsProperties);

            VerifyLogData(expected);
        }

        [Fact]
        public void Verify_that_extended_properties_and_message_with_null_parameters_is_logged_correctly()
        {
            var extended = new ExtendedProperties();
            const string message = "Message {0} {1}";
            const string formattedMessage = "Message [null] [null]";
            var eventId = Log.Debug(extended, message, null, null);
            LogData expected = CreateExpectedLogData(eventId, formattedMessage, extended.AsProperties);

            VerifyLogData(expected);
        }

        [Fact]
        public void Verify_that_extended_properties_and_message_with_too_few_parameters_is_logged_correctly()
        {
            var extended = new ExtendedProperties();
            const string message = "Message {0} {1}";
            const string formattedMessage = "Message {0} {1}";
            var eventId = Log.Debug(extended, message, 42);
            LogData expected = CreateExpectedLogData(eventId, formattedMessage, extended.AsProperties);

            VerifyLogData(expected);
        }

        [Fact]
        public void Verify_that_extended_properties_and_message_with_too_many_parameters_is_logged_correctly()
        {
            var extended = new ExtendedProperties();
            const string message = "Message {0} {1}";
            const string formattedMessage = "Message 42 87";
            var eventId = Log.Debug(extended, message, 42, 87, 119);
            LogData expected = CreateExpectedLogData(eventId, formattedMessage, extended.AsProperties);

            VerifyLogData(expected);
        }

        [Fact]
        public void Verify_that_correlationid_and_extended_properties_and_message_is_logged_correctly()
        {
            var correlationId = Guid.NewGuid();
            var extended = new ExtendedProperties();
            const string message = "Message";
            var eventId = Log.Debug(correlationId, extended, message);
            LogData expected = CreateExpectedLogData(eventId, correlationId, message, extended.AsProperties);

            VerifyLogData(expected);
        }

        [Fact]
        public void Verify_that_correlationid_and_extended_properties_and_message_with_string_parameters_is_logged_correctly()
        {
            var correlationId = Guid.NewGuid();
            var extended = new ExtendedProperties();
            const string message = "Message {0} {1}";
            const string formattedMessage = "Message 42 87";
            var eventId = Log.Debug(correlationId, extended, message, "42", "87");
            LogData expected = CreateExpectedLogData(eventId, correlationId, formattedMessage, extended.AsProperties);

            VerifyLogData(expected);
        }

        [Fact]
        public void Verify_that_correlationid_and_extended_properties_and_message_with_too_few_parameters_is_logged_correctly()
        {
            var correlationId = Guid.NewGuid();
            var extended = new ExtendedProperties();
            const string message = "Message {0} {1}";
            const string formattedMessage = "Message {0} {1}";
            var eventId = Log.Debug(correlationId, extended, message, "42");
            LogData expected = CreateExpectedLogData(eventId, correlationId, formattedMessage, extended.AsProperties);

            VerifyLogData(expected);
        }

        [Fact]
        public void Verify_that_correlationid_and_extended_properties_and_message_with_too_many_parameters_is_logged_correctly()
        {
            var correlationId = Guid.NewGuid();
            var extended = new ExtendedProperties();
            const string message = "Message {0} {1}";
            const string formattedMessage = "Message 42 87";
            var eventId = Log.Debug(correlationId, extended, message, "42", "87", 199);
            LogData expected = CreateExpectedLogData(eventId, correlationId, formattedMessage, extended.AsProperties);

            VerifyLogData(expected);
        }

        [Fact]
        public void Verify_that_correlationid_and_extended_properties_and_message_with_non_string_parameters_is_logged_correctly()
        {
            var correlationId = Guid.NewGuid();
            var extended = new ExtendedProperties();
            const string message = "Message {0} {1}";
            const string formattedMessage = "Message 42 87";
            var eventId = Log.Debug(correlationId, extended, message, 42, 87);
            LogData expected = CreateExpectedLogData(eventId, correlationId, formattedMessage, extended.AsProperties);

            VerifyLogData(expected);
        }

        [Fact]
        public void Verify_that_correlationid_and_extended_properties_and_message_with_null_parameters_is_logged_correctly()
        {
            var correlationId = Guid.NewGuid();
            var extended = new ExtendedProperties();
            const string message = "Message {0} {1}";
            const string formattedMessage = "Message [null] [null]";
            var eventId = Log.Debug(correlationId, extended, message, null, null);
            LogData expected = CreateExpectedLogData(eventId, correlationId, formattedMessage, extended.AsProperties);

            VerifyLogData(expected);
        }

        [Fact]
        public void Verify_that_message_with_string_parameters_is_logged_correctly()
        {
            const string message = "Message {0} {1}";
            const string formattedMessage = "Message 42 87";
            var eventId = Log.Debug(message, "42", "87");
            LogData expected = CreateExpectedLogData(eventId, formattedMessage);

            VerifyLogData(expected);
        }

        [Fact]
        public void Verify_that_message_with_non_string_parameters_is_logged_correctly()
        {
            const string message = "Message {0} {1}";
            const string formattedMessage = "Message 42 87";
            var eventId = Log.Debug(message, 42, 87);
            LogData expected = CreateExpectedLogData(eventId, formattedMessage);

            VerifyLogData(expected);
        }

        [Fact]
        public void Verify_that_correlationid_and_message_with_non_string_parameters_is_logged_correctly()
        {
            var correlationId = Guid.NewGuid();
            const string message = "Message {0} {1}";
            const string formattedMessage = "Message 42 87";
            var eventId = Log.Debug(correlationId, message, 42, 87);
            LogData expected = CreateExpectedLogData(eventId, correlationId, formattedMessage);

            VerifyLogData(expected);
        }

        [Fact]
        public void Verify_that_correlationid_and_message_with_string_parameters_is_logged_correctly()
        {
            var correlationId = Guid.NewGuid();
            const string message = "Message {0} {1}";
            const string formattedMessage = "Message 42 87";
            var eventId = Log.Debug(correlationId, message, "42", "87");
            LogData expected = CreateExpectedLogData(eventId, correlationId, formattedMessage);

            VerifyLogData(expected);
        }

        [Fact]
        public void Verify_that_message_with_too_few_parameters_is_logged_correctly()
        {
            const string message = "Message {0} {1}";
            const string formattedMessage = "Message {0} {1}";
            var eventId = Log.Debug(message, 17);
            LogData expected = CreateExpectedLogData(eventId, formattedMessage);

            VerifyLogData(expected);
        }

        [Fact]
        public void Verify_that_correlationid_and_message_with_too_few_parameters_is_logged_correctly()
        {
            var correlationId = Guid.NewGuid();
            const string message = "Message {0} {1}";
            const string formattedMessage = "Message {0} {1}";
            var eventId = Log.Debug(correlationId, message, 17);
            LogData expected = CreateExpectedLogData(eventId, correlationId, formattedMessage);

            VerifyLogData(expected);
        }

        [Fact]
        public void Verify_that_message_with_too_many_parameters_is_logged_correctly()
        {
            const string message = "Message {0} {1}";
            const string formattedMessage = "Message 17 42";
            var eventId = Log.Debug(message, 17, 42, 87);
            LogData expected = CreateExpectedLogData(eventId, formattedMessage);

            VerifyLogData(expected);
        }

        [Fact]
        public void Verify_that_correlationid_and_message_with_too_many_parameters_is_logged_correctly()
        {
            var correlationId = Guid.NewGuid();
            const string message = "Message {0} {1}";
            const string formattedMessage = "Message 17 42";
            var eventId = Log.Debug(correlationId, message, 17, 42, 87);
            LogData expected = CreateExpectedLogData(eventId, correlationId, formattedMessage);

            VerifyLogData(expected);
        }

        [Fact]
        public void Verify_that_message_with_null_parameters_is_logged_correctly()
        {
            const string message = "Message {0} {1}";
            const string formattedMessage = "Message [null] [null]";
            var eventId = Log.Debug(message, null, null);
            LogData expected = CreateExpectedLogData(eventId, formattedMessage);

            VerifyLogData(expected);
        }

        [Fact]
        public void Verify_that_correlationid_and_message_with_null_parameters_is_logged_correctly()
        {
            var correlationId = Guid.NewGuid();
            const string message = "Message {0} {1}";
            const string formattedMessage = "Message [null] [null]";
            var eventId = Log.Debug(correlationId, message, null, null);
            LogData expected = CreateExpectedLogData(eventId, correlationId, formattedMessage);

            VerifyLogData(expected);
        }

        [Fact]
        public void Verify_that_exception_is_logged_correctly()
        {
            LogData expected;
            try
            {
                throw new ArgumentException("argument");
            }
            catch (Exception exception)
            {
                var eventId = Log.Debug(exception);
                expected = CreateExpectedLogData(eventId, exception, exception.Message);
            }

            VerifyLogData(expected);
        }

        [Fact]
        public void Verify_that_exception_and_extended_properties_is_logged_correctly()
        {
            var extended = new ExtendedProperties();
            LogData expected;
            try
            {
                throw new ArgumentException("argument");
            }
            catch (Exception exception)
            {
                var eventId = Log.Debug(exception, extended);
                expected = CreateExpectedLogData(eventId, exception, exception.Message, extended.AsProperties);
            }

            VerifyLogData(expected);
        }

        [Fact]
        public void Verify_that_exception_and_message_is_logged_correctly()
        {
            const string message = "Message";
            LogData expected;
            try
            {
                throw new ArgumentException("argument");
            }
            catch (Exception exception)
            {
                var eventId = Log.Debug(exception, message);
                expected = CreateExpectedLogData(eventId, exception, message);
            }

            VerifyLogData(expected);
        }

        [Fact]
        public void Verify_that_exception_and_message_with_null_parameters_is_logged_correctly()
        {
            const string message = "Message {0} {1}";
            const string formattedMessage = "Message [null] [null]";
            LogData expected;
            try
            {
                throw new ArgumentException("argument");
            }
            catch (Exception exception)
            {
                var eventId = Log.Debug(exception, message, null, null);
                expected = CreateExpectedLogData(eventId, exception, formattedMessage);
            }

            VerifyLogData(expected);
        }

        [Fact]
        public void Verify_that_exception_and_message_with_non_string_parameters_is_logged_correctly()
        {
            const string message = "Message {0} {1}";
            const string formattedMessage = "Message 42 87";
            LogData expected;
            try
            {
                throw new ArgumentException("argument");
            }
            catch (Exception exception)
            {
                var eventId = Log.Debug(exception, message, 42, 87);
                expected = CreateExpectedLogData(eventId, exception, formattedMessage);
            }

            VerifyLogData(expected);
        }

        [Fact]
        public void Verify_that_exception_and_message_with_string_parameters_is_logged_correctly()
        {
            const string message = "Message {0} {1}";
            const string formattedMessage = "Message 42 87";
            LogData expected;
            try
            {
                throw new ArgumentException("argument");
            }
            catch (Exception exception)
            {
                var eventId = Log.Debug(exception, message, "42", "87");
                expected = CreateExpectedLogData(eventId, exception, formattedMessage);
            }

            VerifyLogData(expected);
        }

        [Fact]
        public void Verify_that_exception_and_message_with_too_few_parameters_is_logged_correctly()
        {
            const string message = "Message {0} {1}";
            const string formattedMessage = "Message {0} {1}";
            LogData expected;
            try
            {
                throw new ArgumentException("argument");
            }
            catch (Exception exception)
            {
                var eventId = Log.Debug(exception, message, "42");
                expected = CreateExpectedLogData(eventId, exception, formattedMessage);
            }

            VerifyLogData(expected);
        }

        [Fact]
        public void Verify_that_exception_and_message_with_too_many_parameters_is_logged_correctly()
        {
            const string message = "Message {0} {1}";
            const string formattedMessage = "Message 42 87";
            LogData expected;
            try
            {
                throw new ArgumentException("argument");
            }
            catch (Exception exception)
            {
                var eventId = Log.Debug(exception, message, "42", 87, 119);
                expected = CreateExpectedLogData(eventId, exception, formattedMessage);
            }

            VerifyLogData(expected);
        }

        [Fact]
        public void Verify_that_correlationid_and_exception_and_message_is_logged_correctly()
        {
            var correlationId = Guid.NewGuid();
            var message = "Message";
            LogData expected;
            try
            {
                throw new ArgumentException("argument");
            }
            catch (Exception exception)
            {
                var eventId = Log.Debug(correlationId, exception, message);
                expected = CreateExpectedLogData(eventId, correlationId, exception, message);
            }

            VerifyLogData(expected);
        }

        [Fact]
        public void Verify_that_correlationid_and_exception_and_message_with_non_string_parameters_is_logged_correctly()
        {
            var correlationId = Guid.NewGuid();
            const string message = "Message {0} {1}";
            const string formattedMessage = "Message 42 87";
            LogData expected;
            try
            {
                throw new ArgumentException("argument");
            }
            catch (Exception exception)
            {
                var eventId = Log.Debug(correlationId, exception, message, 42, 87);
                expected = CreateExpectedLogData(eventId, correlationId, exception, formattedMessage);
            }

            VerifyLogData(expected);
        }

        [Fact]
        public void Verify_that_correlationid_and_exception_and_message_with_string_parameters_is_logged_correctly()
        {
            var correlationId = Guid.NewGuid();
            const string message = "Message {0} {1}";
            const string formattedMessage = "Message 42 87";
            LogData expected;
            try
            {
                throw new ArgumentException("argument");
            }
            catch (Exception exception)
            {
                var eventId = Log.Debug(correlationId, exception, message, "42", "87");
                expected = CreateExpectedLogData(eventId, correlationId, exception, formattedMessage);
            }

            VerifyLogData(expected);
        }

        [Fact]
        public void Verify_that_correlationid_and_exception_and_message_with_too_few_parameters_is_logged_correctly()
        {
            var correlationId = Guid.NewGuid();
            const string message = "Message {0} {1}";
            const string formattedMessage = "Message {0} {1}";
            LogData expected;
            try
            {
                throw new ArgumentException("argument");
            }
            catch (Exception exception)
            {
                var eventId = Log.Debug(correlationId, exception, message, 42);
                expected = CreateExpectedLogData(eventId, correlationId, exception, formattedMessage);
            }

            VerifyLogData(expected);
        }

        [Fact]
        public void Verify_that_correlationid_and_exception_and_message_with_too_many_parameters_is_logged_correctly()
        {
            var correlationId = Guid.NewGuid();
            const string message = "Message {0} {1}";
            const string formattedMessage = "Message 42 87";
            LogData expected;
            try
            {
                throw new ArgumentException("argument");
            }
            catch (Exception exception)
            {
                var eventId = Log.Debug(correlationId, exception, message, 42, 87, 117);
                expected = CreateExpectedLogData(eventId, correlationId, exception, formattedMessage);
            }

            VerifyLogData(expected);
        }

        [Fact]
        public void Verify_that_correlationid_and_exception_and_message_with_null_parameters_is_logged_correctly()
        {
            var correlationId = Guid.NewGuid();
            const string message = "Message {0} {1}";
            const string formattedMessage = "Message [null] [null]";
            LogData expected;
            try
            {
                throw new ArgumentException("argument");
            }
            catch (Exception exception)
            {
                var eventId = Log.Debug(correlationId, exception, message, null, null);
                expected = CreateExpectedLogData(eventId, correlationId, exception, formattedMessage);
            }

            VerifyLogData(expected);
        }

        [Fact]
        public void Verify_that_extended_properties_is_logged_correctly()
        {
            var extended = new ExtendedProperties();
            var eventId = Log.Debug(extended);

            var expected = CreateExpectedLogData(eventId, string.Empty);
            VerifyLogData(expected);
        }

        [Fact]
        public void Verify_that_correlationid_and_extended_properties_is_logged_correctly()
        {
            var correlationId = Guid.NewGuid();
            var extended = new ExtendedProperties();

            var eventId = Log.Debug(correlationId, extended);
            var expected = CreateExpectedLogData(eventId, correlationId, string.Empty, extended.AsProperties);

            VerifyLogData(expected);
        }

        [Fact]
        public void Verify_that_correlationid_and_exception_is_logged_correctly()
        {
            var correlationId = Guid.NewGuid();
            LogData expected;
            try
            {
                throw new ArgumentException("argument");
            }
            catch (Exception exception)
            {
                var eventId = Log.Debug(correlationId, exception);
                expected = CreateExpectedLogData(eventId, correlationId, exception, exception.Message);
            }

            VerifyLogData(expected);
        }

        [Fact]
        public void Verify_that_correlationid_and_exception_and_extended_properties_is_logged_correctly()
        {
            var correlationId = Guid.NewGuid();
            var extended = new ExtendedProperties();
            LogData expected;
            try
            {
                throw new ArgumentException("argument");
            }
            catch (Exception exception)
            {
                var eventId = Log.Debug(correlationId, exception, extended);
                expected = CreateExpectedLogData(eventId, correlationId, exception, exception.Message, extended.AsProperties);
            }

            VerifyLogData(expected);
        }

        [Fact]
        public void Verify_that_correlationid_and_exception_and_extended_properties_and_message_is_logged_correctly()
        {
            const string message = "Message";
            var correlationId = Guid.NewGuid();
            var extended = new ExtendedProperties();
            LogData expected;
            try
            {
                throw new ArgumentException("argument");
            }
            catch (Exception exception)
            {
                var eventId = Log.Debug(correlationId, exception, extended, message);
                expected = CreateExpectedLogData(eventId, correlationId, exception, message, extended.AsProperties);
            }

            VerifyLogData(expected);
        }

        [Fact]
        public void Verify_that_correlationid_and_exception_and_extended_properties_and_message_with_too_few_parameters_is_logged_correctly()
        {
            const string message = "Message {0} {1}";
            const string formattedMessage = "Message {0} {1}";
            var correlationId = Guid.NewGuid();
            var extended = new ExtendedProperties();
            LogData expected;
            try
            {
                throw new ArgumentException("argument");
            }
            catch (Exception exception)
            {
                var eventId = Log.Debug(correlationId, exception, extended, message, 42);
                expected = CreateExpectedLogData(eventId, correlationId, exception, formattedMessage, extended.AsProperties);
            }

            VerifyLogData(expected);
        }

        [Fact]
        public void Verify_that_correlationid_and_exception_and_extended_properties_and_message_with_too_many_parameters_is_logged_correctly()
        {
            const string message = "Message {0} {1}";
            const string formattedMessage = "Message 17 42";
            var correlationId = Guid.NewGuid();
            var extended = new ExtendedProperties();
            LogData expected;
            try
            {
                throw new ArgumentException("argument");
            }
            catch (Exception exception)
            {
                var eventId = Log.Debug(correlationId, exception, extended, message, "17", 42, "87");
                expected = CreateExpectedLogData(eventId, correlationId, exception, formattedMessage, extended.AsProperties);
            }

            VerifyLogData(expected);
        }

        [Fact]
        public void Verify_that_correlationid_and_exception_and_extended_properties_and_message_with_non_string_parameters_is_logged_correctly()
        {
            const string message = "Message {0} {1}";
            const string formattedMessage = "Message 42 87";
            var correlationId = Guid.NewGuid();
            var extended = new ExtendedProperties();
            LogData expected;
            try
            {
                throw new ArgumentException("argument");
            }
            catch (Exception exception)
            {
                var eventId = Log.Debug(correlationId, exception, extended, message, 42, 87);
                expected = CreateExpectedLogData(eventId, correlationId, exception, formattedMessage, extended.AsProperties);
            }

            VerifyLogData(expected);
        }

        [Fact]
        public void Verify_that_correlationid_and_exception_and_extended_properties_and_message_with_string_parameters_is_logged_correctly()
        {
            const string message = "Message {0} {1}";
            const string formattedMessage = "Message 42 87";
            var correlationId = Guid.NewGuid();
            var extended = new ExtendedProperties();
            LogData expected;
            try
            {
                throw new ArgumentException("argument");
            }
            catch (Exception exception)
            {
                var eventId = Log.Debug(correlationId, exception, extended, message, "42", "87");
                expected = CreateExpectedLogData(eventId, correlationId, exception, formattedMessage, extended.AsProperties);
            }

            VerifyLogData(expected);
        }

        [Fact]
        public void Verify_that_correlationid_and_exception_and_extended_properties_and_message_with_null_parameters_is_logged_correctly()
        {
            const string message = "Message {0} {1}";
            const string formattedMessage = "Message [null] [null]";
            var correlationId = Guid.NewGuid();
            var extended = new ExtendedProperties();
            LogData expected;
            try
            {
                throw new ArgumentException("argument");
            }
            catch (Exception exception)
            {
                var eventId = Log.Debug(correlationId, exception, extended, message, null, null);
                expected = CreateExpectedLogData(eventId, correlationId, exception, formattedMessage, extended.AsProperties);
            }

            VerifyLogData(expected);
        }
    }
}