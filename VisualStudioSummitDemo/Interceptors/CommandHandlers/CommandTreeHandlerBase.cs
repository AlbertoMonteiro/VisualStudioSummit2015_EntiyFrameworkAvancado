using System.Data.Entity.Core.Common.CommandTrees;

namespace VisualStudioSummitDemo.Interceptors.CommandHandlers
{
    public abstract class CommandTreeHandlerBase<T> : ICommandTreeHandler<T> 
        where T : DbCommandTree
    {
        private ICommandTreeHandler<T> nextHandler;

        public T HandleRequest(DbCommandTree command)
        {
            if (CanHandle(command))
                return Handle(command);
            return nextHandler != null ? nextHandler.HandleRequest(command) : null;
        }

        public T Cast(DbCommandTree command)
        {
            throw new System.NotImplementedException();
        }

        public ICommandTreeHandler<T> SetNextHandler(ICommandTreeHandler<T> handler)
        {
            return nextHandler = handler;
        }

        protected abstract bool CanHandle(DbCommandTree command);
        protected abstract T Handle(DbCommandTree command);
    }
}