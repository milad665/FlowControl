using System;

namespace Milad.Utils.FlowControl
{
    /// <summary>
    /// Implementation of the flow control context
    /// </summary>
    public class ControlContext : IControlContext
    {
        private IMethodReturnValue _lastReturnValue;
        private IMethodReturnValue _savedReturnValue;

        private ControlContext()
        {}

        protected ControlContext(IMethodReturnValue lastReturnValue)
        {
            _lastReturnValue = lastReturnValue;
        }

        /// <summary>
        /// Creates an instance of the IControlContext by running a function which returns a result
        /// </summary>
        /// <typeparam name="TResult">Result type of the MethodReturnValue returned from the function to run</typeparam>
        /// <param name="func">Delegate of the function to run</param>
        /// <returns>A new instance of the IControlContext</returns>
        public static IControlContext Run<TResult>(Func<MethodReturnValue<TResult>> func)
        {
            var result = func();
            return new ControlContext(result);
        }

        /// <summary>
        /// Creates an instance of the IControlContext by running a function which does not a result
        /// </summary>
        /// <param name="func">Delegate of the function to run</param>
        /// <returns>A new instance of the IControlContext</returns>
        public static IControlContext Run(Func<MethodVoidReturnValue> func)
        {
            var result = func();
            return new ControlContext(result);
        }

        public IControlContext Then<TResult>(Func<MethodReturnValue<TResult>> func)
        {
            _lastReturnValue = _lastReturnValue.IsSuccessful
                ? func()
                : MethodReturnValue<TResult>.Unsuccessful<TResult>(_lastReturnValue.ErrorMessage, _lastReturnValue.ErrorCode, _lastReturnValue.StackTrace);

            return this;
        }

        public IControlContext Then<TInput, TResult>(Func<TInput, MethodReturnValue<TResult>> func)
        {
            if (!(_lastReturnValue is MethodReturnValue<TInput> previous))
                throw new ArgumentException(
                    "Input type does not match the output type of the previous method run.");

            _lastReturnValue = previous.IsSuccessful
                ? func(previous.Result)
                : MethodReturnValue<TResult>.Unsuccessful<TResult>(previous.ErrorMessage, previous.ErrorCode, previous.StackTrace);

            return this;
        }

        public IControlContext Then<TInput>(Func<TInput, MethodVoidReturnValue> func)
        {
            if (!(_lastReturnValue is MethodReturnValue<TInput> previous))
                throw new ArgumentException(
                    "Input type does not match the output type of the previous method run.");

            _lastReturnValue = previous.IsSuccessful
                ? func(previous.Result)
                : MethodVoidReturnValue.Unsuccessful(previous.ErrorMessage, previous.ErrorCode, previous.StackTrace);

            return this;
        }

        public IControlContext Then(Func<MethodVoidReturnValue> func)
        {
            _lastReturnValue = _lastReturnValue.IsSuccessful
                ? func()
                : MethodVoidReturnValue.Unsuccessful(_lastReturnValue.ErrorMessage, _lastReturnValue.ErrorCode, _lastReturnValue.StackTrace);

            return this;
        }

        public IControlContext SaveResult()
        {
            _savedReturnValue = _lastReturnValue;
            return this;
        }

        public MethodReturnValue<T> GetSavedResult<T>()
        {
            if (!(_savedReturnValue is MethodReturnValue<T> saved))
                throw new TypeAccessException("Type 'T' does not match the type of the saved result.");

            if (!_lastReturnValue.IsSuccessful)
                return MethodReturnValue<T>.Unsuccessful<T>(_lastReturnValue.ErrorMessage, _lastReturnValue.ErrorCode,
                    _lastReturnValue.StackTrace);

            return saved;
        }
    }
}