﻿using System;
using SoftwarePassion.Common.Core;
using Xunit;

namespace SoftwarePassion.LogBridge.Tests.Shared
{
    public abstract class When_logging_error_messages : LogTestBase
    {
        public When_logging_error_messages(ILogDataVerifier verifier)
            : base(Level.Error, verifier)
        {
            LogContext.ThreadLogContext.CorrelationId = Option.None<Guid>();
        }

        [Fact]
        public void Verify_that_level_is_enabled()
        {
            const string message = "Message";
            Guid eventId = Guid.Empty;
            if (Log.IsErrorLevelEnabled)
                eventId = Log.Error(message);
            LogData expected = CreateExpectedLogData(eventId, message);

            VerifyLogData(expected);
        }

        [Fact]
        public void Verify_that_message_is_logged_correctly()
        {
            const string message = "Message";
            var eventId = Log.Error(message);
            LogData expected = CreateExpectedLogData(eventId, message);

            VerifyLogData(expected);
        }

        [Fact]
        public void Verify_that_correlationid_and_message_is_logged_correctly()
        {
            var correlationId = Guid.NewGuid();
            const string message = "Message";
            var eventId = Log.Error(correlationId, message);
            LogData expected = CreateExpectedLogData(eventId, correlationId, message);
            
            VerifyLogData(expected);
        }

        [Fact]
        public void Verify_that_extended_properties_and_message_is_logged_correctly()
        {
            var extended = new ExtendedProperties();
            const string message = "Message";
            var eventId = Log.Error(extended, message);
            LogData expected = CreateExpectedLogData(eventId, message, extended.AsProperties);

            VerifyLogData(expected);
        }

        [Fact]
        public void Verify_that_extended_properties_and_message_with_non_string_parameters_is_logged_correctly()
        {
            var extended = new ExtendedProperties();
            const string message = "Message {0} {1}";
            const string formattedMessage = "Message 42 87";
            var eventId = Log.Error(extended, message, 42, 87);
            LogData expected = CreateExpectedLogData(eventId, formattedMessage, extended.AsProperties);

            VerifyLogData(expected);
        }

        [Fact]
        public void Verify_that_extended_properties_and_message_with_string_parameters_is_logged_correctly()
        {
            var extended = new ExtendedProperties();
            const string message = "Message {0} {1}";
            const string formattedMessage = "Message 42 87";
            var eventId = Log.Error(extended, message, "42", "87");
            LogData expected = CreateExpectedLogData(eventId, formattedMessage, extended.AsProperties);

            VerifyLogData(expected);
        }

        [Fact]
        public void Verify_that_extended_properties_and_message_with_null_parameters_is_logged_correctly()
        {
            var extended = new ExtendedProperties();
            const string message = "Message {0} {1}";
            const string formattedMessage = "Message [null] [null]";
            var eventId = Log.Error(extended, message, null, null);
            LogData expected = CreateExpectedLogData(eventId, formattedMessage, extended.AsProperties);

            VerifyLogData(expected);
        }

        [Fact]
        public void Verify_that_extended_properties_and_message_with_too_few_parameters_is_logged_correctly()
        {
            var extended = new ExtendedProperties();
            const string message = "Message {0} {1}";
            const string formattedMessage = "Message {0} {1}";
            var eventId = Log.Error(extended, message, 42);
            LogData expected = CreateExpectedLogData(eventId, formattedMessage, extended.AsProperties);

            VerifyLogData(expected);
        }

        [Fact]
        public void Verify_that_extended_properties_and_message_with_too_many_parameters_is_logged_correctly()
        {
            var extended = new ExtendedProperties();
            const string message = "Message {0} {1}";
            const string formattedMessage = "Message 42 87";
            var eventId = Log.Error(extended, message, 42, 87, 119);
            LogData expected = CreateExpectedLogData(eventId, formattedMessage, extended.AsProperties);

            VerifyLogData(expected);
        }

        [Fact]
        public void Verify_that_correlationid_and_extended_properties_and_message_is_logged_correctly()
        {
            var correlationId = Guid.NewGuid();
            var extended = new ExtendedProperties();
            const string message = "Message";
            var eventId = Log.Error(correlationId, extended, message);
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
            var eventId = Log.Error(correlationId, extended, message, "42", "87");
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
            var eventId = Log.Error(correlationId, extended, message, "42");
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
            var eventId = Log.Error(correlationId, extended, message, "42", "87", 199);
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
            var eventId = Log.Error(correlationId, extended, message, 42, 87);
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
            var eventId = Log.Error(correlationId, extended, message, null, null);
            LogData expected = CreateExpectedLogData(eventId, correlationId, formattedMessage, extended.AsProperties);

            VerifyLogData(expected);
        }

        [Fact]
        public void Verify_that_message_with_string_parameters_is_logged_correctly()
        {
            const string message = "Message {0} {1}";
            const string formattedMessage = "Message 42 87";
            var eventId = Log.Error(message, "42", "87");
            LogData expected = CreateExpectedLogData(eventId, formattedMessage);

            VerifyLogData(expected);
        }

        [Fact]
        public void Verify_that_message_with_non_string_parameters_is_logged_correctly()
        {
            const string message = "Message {0} {1}";
            const string formattedMessage = "Message 42 87";
            var eventId = Log.Error(message, 42, 87);
            LogData expected = CreateExpectedLogData(eventId, formattedMessage);

            VerifyLogData(expected);
        }

        [Fact]
        public void Verify_that_correlationid_and_message_with_non_string_parameters_is_logged_correctly()
        {
            var correlationId = Guid.NewGuid();
            const string message = "Message {0} {1}";
            const string formattedMessage = "Message 42 87";
            var eventId = Log.Error(correlationId, message, 42, 87);
            LogData expected = CreateExpectedLogData(eventId, correlationId, formattedMessage);

            VerifyLogData(expected);
        }

        [Fact]
        public void Verify_that_correlationid_and_message_with_string_parameters_is_logged_correctly()
        {
            var correlationId = Guid.NewGuid();
            const string message = "Message {0} {1}";
            const string formattedMessage = "Message 42 87";
            var eventId = Log.Error(correlationId, message, "42", "87");
            LogData expected = CreateExpectedLogData(eventId, correlationId, formattedMessage);

            VerifyLogData(expected);
        }

        [Fact]
        public void Verify_that_message_with_too_few_parameters_is_logged_correctly()
        {
            const string message = "Message {0} {1}";
            const string formattedMessage = "Message {0} {1}";
            var eventId = Log.Error(message, 17);
            LogData expected = CreateExpectedLogData(eventId, formattedMessage);

            VerifyLogData(expected);
        }

        [Fact]
        public void Verify_that_correlationid_and_message_with_too_few_parameters_is_logged_correctly()
        {
            var correlationId = Guid.NewGuid();
            const string message = "Message {0} {1}";
            const string formattedMessage = "Message {0} {1}";
            var eventId = Log.Error(correlationId, message, 17);
            LogData expected = CreateExpectedLogData(eventId, correlationId, formattedMessage);

            VerifyLogData(expected);
        }

        [Fact]
        public void Verify_that_message_with_too_many_parameters_is_logged_correctly()
        {
            const string message = "Message {0} {1}";
            const string formattedMessage = "Message 17 42";
            var eventId = Log.Error(message, 17, 42, 87);
            LogData expected = CreateExpectedLogData(eventId, formattedMessage);

            VerifyLogData(expected);
        }

        [Fact]
        public void Verify_that_correlationid_and_message_with_too_many_parameters_is_logged_correctly()
        {
            var correlationId = Guid.NewGuid();
            const string message = "Message {0} {1}";
            const string formattedMessage = "Message 17 42";
            var eventId = Log.Error(correlationId, message, 17, 42, 87);
            LogData expected = CreateExpectedLogData(eventId, correlationId, formattedMessage);

            VerifyLogData(expected);
        }

        [Fact]
        public void Verify_that_message_with_null_parameters_is_logged_correctly()
        {
            const string message = "Message {0} {1}";
            const string formattedMessage = "Message [null] [null]";
            var eventId = Log.Error(message, null, null);
            LogData expected = CreateExpectedLogData(eventId, formattedMessage);

            VerifyLogData(expected);
        }

        [Fact]
        public void Verify_that_correlationid_and_message_with_null_parameters_is_logged_correctly()
        {
            var correlationId = Guid.NewGuid();
            const string message = "Message {0} {1}";
            const string formattedMessage = "Message [null] [null]";
            var eventId = Log.Error(correlationId, message, null, null);
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
                var eventId = Log.Error(exception);
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
                var eventId = Log.Error(exception, extended);
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
                var eventId = Log.Error(exception, message);
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
                var eventId = Log.Error(exception, message, null, null);
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
                var eventId = Log.Error(exception, message, 42, 87);
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
                var eventId = Log.Error(exception, message, "42", "87");
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
                var eventId = Log.Error(exception, message, "42");
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
                var eventId = Log.Error(exception, message, "42", 87, 119);
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
                var eventId = Log.Error(correlationId, exception, message);
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
                var eventId = Log.Error(correlationId, exception, message, 42, 87);
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
                var eventId = Log.Error(correlationId, exception, message, "42", "87");
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
                var eventId = Log.Error(correlationId, exception, message, 42);
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
                var eventId = Log.Error(correlationId, exception, message, 42, 87, 117);
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
                var eventId = Log.Error(correlationId, exception, message, null, null);
                expected = CreateExpectedLogData(eventId, correlationId, exception, formattedMessage);
            }

            VerifyLogData(expected);
        }

        [Fact]
        public void Verify_that_extended_properties_is_logged_correctly()
        {
            var extended = new ExtendedProperties();
            var eventId = Log.Error(extended);

            var expected = CreateExpectedLogData(eventId, string.Empty);
            VerifyLogData(expected);
        }

        [Fact]
        public void Verify_that_correlationid_and_extended_properties_is_logged_correctly()
        {
            var correlationId = Guid.NewGuid();
            var extended = new ExtendedProperties();

            var eventId = Log.Error(correlationId, extended);
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
                var eventId = Log.Error(correlationId, exception);
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
                var eventId = Log.Error(correlationId, exception, extended);
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
                var eventId = Log.Error(correlationId, exception, extended, message);
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
                var eventId = Log.Error(correlationId, exception, extended, message, 42);
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
                var eventId = Log.Error(correlationId, exception, extended, message, "17", 42, "87");
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
                var eventId = Log.Error(correlationId, exception, extended, message, 42, 87);
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
                var eventId = Log.Error(correlationId, exception, extended, message, "42", "87");
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
                var eventId = Log.Error(correlationId, exception, extended, message, null, null);
                expected = CreateExpectedLogData(eventId, correlationId, exception, formattedMessage, extended.AsProperties);
            }

            VerifyLogData(expected);
        }
    }
}