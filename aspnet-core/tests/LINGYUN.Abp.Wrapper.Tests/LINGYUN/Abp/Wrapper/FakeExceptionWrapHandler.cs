namespace LINGYUN.Abp.Wrapper
{
    public class FakeExceptionWrapHandler : IExceptionWrapHandler
    {
        public void Wrap(ExceptionWrapContext context)
        {
            context.WithCode("1001")
                   .WithMessage("Custom exception handling message")
                   .WithDetails("Customized exception handling message details");
        }
    }
}
