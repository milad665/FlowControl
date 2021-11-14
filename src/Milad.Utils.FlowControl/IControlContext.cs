using System;

namespace Milad.Utils.FlowControl
{
    /// <summary>
    /// Main Flow Control Context
    /// </summary>
    public interface IControlContext
    {
        /// <summary>
        /// Runs the next function which receives an input parameter of type TInput and returns a result
        /// </summary>
        /// <typeparam name="TInput">Type of the input parameter, it can be the result of the last method run.</typeparam>
        /// <typeparam name="TResult">Result type of the MethodReturnValue returned from the function to run</typeparam>
        /// <param name="func">Delegate of the function to run</param>
        /// <returns>The same instance of the IControlContext</returns>
        IControlContext Then<TInput, TResult>(Func<TInput, MethodReturnValue<TResult>> func);

        /// <summary>
        /// Runs the next function which does not receive any input but returns a result
        /// </summary>
        /// <typeparam name="TResult">Result type of the MethodReturnValue returned from the function to run</typeparam>
        /// <param name="func">Delegate of the function to run</param>
        /// <returns>The same instance of the IControlContext</returns>
        IControlContext Then<TResult>(Func<MethodReturnValue<TResult>> func);

        /// <summary>
        /// Runs the next function which receives an input parameter of type TInput but does not return a result
        /// </summary>
        /// <typeparam name="TInput">Type of the input parameter, it can be the result of the last method run.</typeparam>
        /// <param name="func">Delegate of the function to run</param>
        /// <returns>The same instance of the IControlContext</returns>
        IControlContext Then<TInput>(Func<TInput, MethodVoidReturnValue> func);

        /// <summary>
        /// Runs the next function which does not receive any input parameter and does not return a result
        /// </summary>
        /// <param name="func">Delegate of the function to run</param>
        /// <returns>The same instance of the IControlContext</returns>
        IControlContext Then(Func<MethodVoidReturnValue> func);

        /// <summary>
        /// Saves the result of the last method run which is a MethodReturnValue&lt;T&gt;
        /// </summary>
        /// <returns>The same instance of the IControlContext</returns>
        IControlContext SaveResult();

        /// <summary>
        /// Returns the saved result of a method which ran earlier by the current instance of IControlContext
        /// </summary>
        /// <typeparam name="T">Type of the expected result</typeparam>
        /// <returns>The saved result</returns>
        MethodReturnValue<T> GetSavedResult<T>();
    }
}