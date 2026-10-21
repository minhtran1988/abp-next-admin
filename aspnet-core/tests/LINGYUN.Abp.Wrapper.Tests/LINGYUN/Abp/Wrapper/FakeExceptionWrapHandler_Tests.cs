using Shouldly;
using Volo.Abp;
using Volo.Abp.Http;
using Xunit;

namespace LINGYUN.Abp.Wrapper.Tests
{
    public class FakeExceptionWrapHandler_Tests: AbpWrapperTestsBase
    {
        private readonly IExceptionWrapHandlerFactory _exceptionWrapHandlerFactory;

        public FakeExceptionWrapHandler_Tests()
        {
            _exceptionWrapHandlerFactory = GetRequiredService<IExceptionWrapHandlerFactory>();
        }

        [Fact]
        public void Should_Return_Wraped_Result_With_Fake_Exception()
        {
            var exception = new FakeException();
            var exceptionWrapContext = new ExceptionWrapContext(
                exception, 
                new RemoteServiceErrorInfo(),
                ServiceProvider);

            var handler = _exceptionWrapHandlerFactory.CreateFor(exceptionWrapContext);
            handler.Wrap(exceptionWrapContext);

            exceptionWrapContext.ErrorInfo.Code.ShouldBe("1001");
            exceptionWrapContext.ErrorInfo.Message.ShouldBe("Custom exception handling message");
            exceptionWrapContext.ErrorInfo.Details.ShouldBe("Custom exception handling message details");
        }

        [Fact]
        public void Should_Return_Wraped_Result_With_Default_Exception()
        {
            var exception = new AbpException();
            var errorInfo = new RemoteServiceErrorInfo(
                "Default exception handling message",
                "Default exception handling message details",
                "1000");
            var exceptionWrapContext = new ExceptionWrapContext(
                exception,
                errorInfo,
                ServiceProvider);

            var handler = _exceptionWrapHandlerFactory.CreateFor(exceptionWrapContext);
            handler.Wrap(exceptionWrapContext);

            exceptionWrapContext.ErrorInfo.Code.ShouldBe("1000");
            exceptionWrapContext.ErrorInfo.Message.ShouldBe("Default exception handling message");
            exceptionWrapContext.ErrorInfo.Details.ShouldBe("Default exception handling message details");
        }

        [Fact]
        public void Should_Return_Wraped_Result_Code_500_With_Unhandled_Exception()
        {
            var exception = new AbpException();
            var errorInfo = new RemoteServiceErrorInfo(
                "Default exception handling message",
                "Default exception handling message details");
            var exceptionWrapContext = new ExceptionWrapContext(
                exception,
                errorInfo,
                ServiceProvider);

            var handler = _exceptionWrapHandlerFactory.CreateFor(exceptionWrapContext);
            handler.Wrap(exceptionWrapContext);

            exceptionWrapContext.ErrorInfo.Code.ShouldBe("500");
            exceptionWrapContext.ErrorInfo.Message.ShouldBe("Default exception handling message");
            exceptionWrapContext.ErrorInfo.Details.ShouldBe("Default exception handling message details");
        }
    }
}
