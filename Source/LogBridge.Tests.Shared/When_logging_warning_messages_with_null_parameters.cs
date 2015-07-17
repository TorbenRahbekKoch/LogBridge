using System;
using FluentAssertions;
using Xunit;

namespace SoftwarePassion.LogBridge.Tests.Shared
{
    public abstract class When_logging_warning_messages_with_null_parameters : LogTestBase
    {
        public When_logging_warning_messages_with_null_parameters(ILogDataVerifier verifier)
            : base(Level.Warning, verifier)
        {}

        [Fact]
        public void Verify_that_null_message_can_be_logged_without_failures()
        {
            Action action = () => Log.Warning((string)null);

            action.ShouldNotThrow();            
            VerifyOneEventLogged();
        }

        [Fact]
        public void Verify_that_correlationid_and_null_message_and_null_parameter_can_be_logged_without_failures()
        {
            Action action = () => Log.Warning(Guid.NewGuid(), (string)null, (object)null);
            action.ShouldNotThrow();            
            VerifyOneEventLogged();
        }

        [Fact]
        public void Verify_that_correlationid_and_null_message_and_null_parameters_can_be_logged_without_failures()
        {
            Action action = () => Log.Warning(Guid.NewGuid(), (string)null, (string)null, null);
            action.ShouldNotThrow();            
            VerifyOneEventLogged();
        }

        [Fact]
        public void Verify_that_null_message_and_null_parameters_can_be_logged_without_failures()
        {
            Action action = () => Log.Warning((string)null, null, null);
            action.ShouldNotThrow();            
            VerifyOneEventLogged();
        }

        [Fact]
        public void Verify_that_null_exception_value_can_be_logged_without_failures()
        {
            Action action = () => Log.Warning((Exception) null);
            action.ShouldNotThrow();            
            VerifyOneEventLogged();
        }

        [Fact]
        public void Verify_that_null_extended_properties_can_be_logged_without_failures()
        {
            Action action = () => Log.Warning((object) null);
            action.ShouldNotThrow();            
            VerifyOneEventLogged();
        }

        [Fact]
        public void Verify_that_null_extended_properties_and_null_message_and_null_parameter_can_be_logged_without_failures()
        {
            Action action = () => Log.Warning((object) null, (string)null, null);
            action.ShouldNotThrow();            
            VerifyOneEventLogged();
        }

        [Fact]
        public void Verify_that_correlationid_and_null_extended_properties_and_null_message_and_null_parameter_can_be_logged_without_failures()
        {
            Action action = () => Log.Warning(Guid.NewGuid(), (object) null, (string)null, null);
            action.ShouldNotThrow();            
            VerifyOneEventLogged();
        }

        [Fact]
        public void Verify_that_correlationid_and_null_extended_properties_can_be_logged_without_failures()
        {
            Action action = () => Log.Warning(Guid.NewGuid(), (object) null);
            action.ShouldNotThrow();            
            VerifyOneEventLogged();
        }

        [Fact]
        public void Verify_that_correlationid_and_null_exception_value_can_be_logged_without_failures()
        {
            Action action = () => Log.Warning(Guid.NewGuid(), (Exception) null);
            action.ShouldNotThrow();            
            VerifyOneEventLogged();
        }

        [Fact]
        public void Verify_that_null_exception_and_null_extended_properties_can_be_logged_without_failures()
        {
            Action action = () => Log.Warning((Exception)null, (object)null);

            action.ShouldNotThrow();            
            VerifyOneEventLogged();
        }

        [Fact]
        public void Verify_that_null_exception_and_null_message_can_be_logged_without_failures()
        {
            Action action = () => Log.Warning((Exception)null, (string)null);

            action.ShouldNotThrow();            
            VerifyOneEventLogged();
        }

        [Fact]
        public void Verify_that_null_exception_and_null_message_and_null_parameters_can_be_logged_without_failures()
        {
            Action action = () => Log.Warning((Exception)null, (string)null, (string)null);

            action.ShouldNotThrow();            
            VerifyOneEventLogged();
        }

        [Fact]
        public void Verify_that_correlationid_and_null_exception_and_null_message_and_null_parameters_can_be_logged_without_failures()
        {
            Action action = () => Log.Warning(Guid.NewGuid(), (Exception)null, (string)null, (string)null);

            action.ShouldNotThrow();            
            VerifyOneEventLogged();
        }

        [Fact]
        public void Verify_that_correlationid_and_null_exception_and_null_message_can_be_logged_without_failures()
        {
            Action action = () => Log.Warning(Guid.NewGuid(), (Exception)null, (string)null);

            action.ShouldNotThrow();            
            VerifyOneEventLogged();
        }

        [Fact]
        public void Verify_that_correlationid_and_null_exception_and_null_extended_properties_can_be_logged_without_failures()
        {
            Action action = () => Log.Warning(Guid.NewGuid(), (Exception)null, (object)null);

            action.ShouldNotThrow();            
            VerifyOneEventLogged();
        }

        [Fact]
        public void Verify_that_null_exception_and_null_extended_properties_and_null_message_and_null_formatting_parameter_can_be_logged_without_failures()
        {
            Action action = () => Log.Warning((Exception)null, (object)null, (string)null, (string)null);

            action.ShouldNotThrow();            
            VerifyOneEventLogged();
        }

        [Fact]
        public void Verify_that_correlationid_and_null_exception_and_null_extended_properties_and_null_message_and_null_formatting_parameter_can_be_logged_without_failures()
        {
            Action action = () => Log.Warning(Guid.NewGuid(), null, (object)null, (string)null, null);

            action.ShouldNotThrow();            
            VerifyOneEventLogged();
        }
    }
}