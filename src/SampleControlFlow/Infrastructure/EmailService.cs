using Milad.Utils.FlowControl;

namespace SampleFlowControl.Infrastructure
{
    public class EmailService : IEmailService
    {
        public MethodVoidReturnValue SendEmail(string email, string title, string body)
        {
            if (email.Contains("@") && email.Contains(".") && email.Length>6)
                return MethodVoidReturnValue.Successful();

            return MethodVoidReturnValue.Unsuccessful("Invalid email address", "I-1");
        }
    }
}