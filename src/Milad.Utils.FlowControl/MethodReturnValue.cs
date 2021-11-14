using System;

namespace Milad.Utils.FlowControl
{
    public class MethodReturnValue<T> : IMethodReturnValue
    {
        protected MethodReturnValue(bool isSuccessful, T result, string errorMessage, string errorCode, string stackTrace)
        {
            IsSuccessful = isSuccessful;
            Result = result;
            ErrorMessage = errorMessage;
            ErrorCode = errorCode;
            StackTrace = stackTrace;
        }

        public static MethodReturnValue<TResult> FromResult<TResult>(TResult result)
        {
            return new MethodReturnValue<TResult>(true, result, null, null, null);
        }

        public static MethodReturnValue<TResult> Unsuccessful<TResult>(string errorMessage, string errorCode, string stackTrace = null)
        {
            return new MethodReturnValue<TResult>(false, default, errorMessage, errorCode, stackTrace ?? Environment.StackTrace);
        }

        public bool IsSuccessful { get; }
        public T Result { get; }
        public string ErrorMessage { get; }
        public string ErrorCode { get; }
        public string StackTrace { get; }
        public object GetResultObject() => Result;
    }
}