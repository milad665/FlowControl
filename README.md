# FlowControl
FlowControl (Milad.Utils.FlowControl) is a .NET Standard library to help controlling business rule failures in multi-layered applications when the developer wants to avoid throwing exceptions to control the flow.

# Why FlowControl?

While developing software applications, there are many cases in which you need to inform the user about how the business logic does not allow the user to take a certain action, how things cannot be done the way the user expected or how the user is simply making mistakes according to the logic of the application. 

Handling this type of control flow, gets tricky in layered applications where business rules may fail in deeper layers and the software needs to stop proceeding with the logic can return to the user with a proper error message.

A bad practice or even an Anti-pattern in developing layered software applications is to throw exceptions when such business rules fail. There are many drawbacks with this approach that I don't want to get into but one of the main ones is the performance overhead of throwing exceptions and the fact that business rule failures are simply not exceptions that happen once in a while!

A reason why this approach is liked by many developers, even myself to be honest, is the convenience of breaking the flow no matter where in the code and in which layer you are. You simply throw an exception and then use a middleware or a try-catch block high above all the layers to handle the exceptions, filter them based on their type, do proper logging and generate the proper response for the user.

Not throwing exceptions means handling this in every method call and control the propagation of method call results from lower layers up to the user.

FlowControl is a simple utility to help with this :)

# Sample

    public class RegisterApplicationService : IRegisterApplicationService
    {
		public MethodReturnValue<Guid> RegisterUser(string name, string email)
        {
            return ControlContext
                .Run(() => _registerService.AddName(name))
                .SaveResult()
                .Then(() => _emailService.SendEmail(email, "Registered", "Congrats! You are registered successfully."))
                .GetSavedResult<Guid>();
        }
    }
    
    
    public class TestController : ControllerBase
    {
        private readonly IRegisterApplicationService _registerApplicationService;

        public TestController(IRegisterApplicationService registerApplicationService)
        {
            _registerApplicationService = registerApplicationService;
        }

        [HttpPost("")]
        public IActionResult Register(string name, string email)
        {
            var result = _registerApplicationService.RegisterUser(name, email);
            return result.ToActionResult();
        }
    }

You can clone the project code and use **SampleFlowControl** project to play with the library and discover how it works.

## Install via Nuget
**Main Package**

    Install-Package Milad.Utils.FlowControl


**ASP.NET Core / .NET 5 Extension Package**
Convert to IMethodReturnValue to IActionResult

    Install-Package Milad.Utils.FlowControl.AspNetCore.Extensions

