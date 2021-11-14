using System;

namespace Milad.Utils.FlowControl
{
    public class MethodVoidReturnValue : IMethodReturnValue
    {
        protected MethodVoidReturnValue(bool isSuccessful, string errorMessage, string errorCode, string stackTrace)
        {
            IsSuccessful = isSuccessful;
            ErrorMessage = errorMessage;
            ErrorCode = errorCode;
            StackTrace = stackTrace;
        }

        public static MethodVoidReturnValue Successful()
        {
            return new MethodVoidReturnValue(true, null, null, null);
        }

        public static MethodVoidReturnValue Unsuccessful(string errorMessage, string errorCode, string stackTrace = null)
        {
            return new MethodVoidReturnValue(false, errorMessage, errorCode, stackTrace ?? Environment.StackTrace);
        }

        public bool IsSuccessful { get; }
        public string ErrorMessage { get; }
        public string ErrorCode { get; }
        public string StackTrace { get; }
        public object GetResultObject() => null;
    }
}