using System;
using Command.Interfaces;
using Controller.Interfaces;
using DataTransformation.Interfaces;

namespace DataCommand.Implementation
{
    /// <summary>
    /// Base class for commands performing CRUD 
    /// (Create, Read, Update, Delete) operations.
    /// It is assumed that the commands use a corresponding
    /// controller, which operates on transformed data, obtains
    /// the source object from a transformed data wrapper, 
    /// and performs the operation itself on a collection
    /// implementing the ITransformedDataCollection interface.
    /// </summary>
    public abstract class CRUDCommandBase : INotifiableCommand 
    {
        protected ITransformedDataWrapper Source;
        protected ITransformedDataCollection Target;
        protected ISimpleController Controller;
        protected Func<bool> Condition;

        protected CRUDCommandBase(ITransformedDataWrapper source, ITransformedDataCollection target, ISimpleController controller, Func<bool> condition)
        {
            Source = source;
            Target = target;
            Controller = controller;
            Condition = condition;
        }

        /// <summary>
        /// Invoke the controller corresponding to the command.
        /// </summary>
        public void Execute()
        {
            Controller.Run();
        }

        /// <summary>
        /// Evaluate if command can be executed.
        /// </summary>
        public bool CanExecute()
        {
            return Condition();
        }

        /// <summary>
        /// Currently not used - forwards to parameterless version.
        /// </summary>
        public bool CanExecute(object parameter)
        {
            return CanExecute();
        }

        /// <summary>
        /// Currently not used - forwards to parameterless version.
        /// </summary>
        public void Execute(object parameter)
        {
            Execute();
        }

        /// <summary>
        /// Invoke re-evaluation of CanExecuteChanged.
        /// </summary>
        public event EventHandler CanExecuteChanged;
        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}