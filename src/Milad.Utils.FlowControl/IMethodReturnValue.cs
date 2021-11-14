namespace Milad.Utils.FlowControl
{
    public interface IMethodReturnValue
    {
        bool IsSuccessful { get; }
        string ErrorMessage { get; }
        string ErrorCode { get; }
        string StackTrace { get; }
        object GetResultObject();
    }
}