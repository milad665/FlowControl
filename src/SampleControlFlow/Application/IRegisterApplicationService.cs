using System;
using Milad.Utils.FlowControl;

namespace SampleFlowControl.Application
{
    public interface IRegisterApplicationService
    {
        MethodReturnValue<Guid> RegisterUser(string name, string email);
    }
}