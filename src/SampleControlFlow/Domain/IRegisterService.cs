using System;
using Milad.Utils.FlowControl;

namespace SampleFlowControl.Domain
{
    public interface IRegisterService
    {
        /// <summary>
        /// Registers a new user
        /// </summary>
        /// <param name="name">Name of the user</param>
        /// <returns>User Id</returns>
        MethodReturnValue<Guid> AddName(string name);
    }
}