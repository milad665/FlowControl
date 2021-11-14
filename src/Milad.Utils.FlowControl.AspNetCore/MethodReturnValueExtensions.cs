using Microsoft.AspNetCore.Mvc;

namespace Milad.Utils.FlowControl.AspNetCore
{
    public static class MethodReturnValueExtensions
    {
        public static IActionResult ToActionResult(this IMethodReturnValue methodReturnValue)
        {
            if (!methodReturnValue.IsSuccessful)
                return new ConflictObjectResult(new
                    {ErrorMessage = methodReturnValue.ErrorMessage, ErrorCode = methodReturnValue.ErrorCode});

            var returnValue = methodReturnValue.GetResultObject();
            return returnValue == null ? (IActionResult)new OkResult() : new OkObjectResult(returnValue);
        }
    }
}
