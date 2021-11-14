using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Milad.Utils.FlowControl;
using Milad.Utils.FlowControl.AspNetCore;
using SampleFlowControl.Application;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SampleFlowControl.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
}
