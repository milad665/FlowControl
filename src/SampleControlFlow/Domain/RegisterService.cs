using System;
using System.Collections.Generic;
using System.Linq;
using Milad.Utils.FlowControl;

namespace SampleFlowControl.Domain
{
    public class RegisterService : IRegisterService
    {
        private readonly Dictionary<Guid, string> _names;

        public RegisterService()
        {
            _names = new Dictionary<Guid, string>
            {
                {Guid.NewGuid(), "Adrian"},
                {Guid.NewGuid(), "Sam"},
                {Guid.NewGuid(), "Milad"},
                {Guid.NewGuid(), "Arezoo"},
                {Guid.NewGuid(), "Sara"}
            };
        }

        public MethodReturnValue<Guid> AddName(string name)
        {
            //Return unsuccessful result instead of throwing exception for logical errors
            if (_names.ContainsValue(name))
                return MethodReturnValue<Guid>.Unsuccessful<Guid>("Name already registered.", "D-1");

            var id = Guid.NewGuid();
            _names.Add(id, name);
            return MethodReturnValue<Guid>.FromResult(id);
        }
    }
}