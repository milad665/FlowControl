using Milad.Utils.FlowControl;

namespace SampleFlowControl.Infrastructure
{
    public interface IEmailService
    {
        MethodVoidReturnValue SendEmail(string email, string title, string body);
    }
}