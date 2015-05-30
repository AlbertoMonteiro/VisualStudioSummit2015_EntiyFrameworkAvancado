using System.Data.Entity.Core.Common.CommandTrees;

namespace VisualStudioSummitDemo.Interceptors.MultiTenant.CommandHandlers
{
    public abstract class CommandTreeHandlerBase : ICommandTreeHandler<DbCommandTree>
    {
        private ICommandTreeHandler<DbCommandTree> nextHandler;

        public DbCommandTree HandleRequest(DbCommandTree command)
        {
            if (CanHandle(command))
                return Handle(command);
            return nextHandler != null ? nextHandler.HandleRequest(command) : null;
        }

        public ICommandTreeHandler<DbCommandTree> SetNextHandler(ICommandTreeHandler<DbCommandTree> handler)
        {
            return nextHandler = handler;
        }

        protected abstract bool CanHandle(DbCommandTree command);
        protected abstract DbCommandTree Handle(DbCommandTree command);
    }
}