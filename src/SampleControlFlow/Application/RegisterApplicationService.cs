using System;
using Milad.Utils.FlowControl;
using SampleFlowControl.Domain;
using SampleFlowControl.Infrastructure;

namespace SampleFlowControl.Application
{
    public class RegisterApplicationService : IRegisterApplicationService
    {
        private readonly IRegisterService _registerService;
        private readonly IEmailService _emailService;

        public RegisterApplicationService(IRegisterService registerService, 
            IEmailService emailService)
        {
            _registerService = registerService;
            _emailService = emailService;
        }

        public MethodReturnValue<Guid> RegisterUser(string name, string email)
        {
            return ControlContext
                .Run(() => _registerService.AddName(name))
                .SaveResult()
                .Then(() => _emailService.SendEmail(email, "Registered", "Congrats! You are registered successfully."))
                .GetSavedResult<Guid>();
        }
    }
}