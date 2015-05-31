using System.Data.Entity.Core.Common.CommandTrees;

namespace VisualStudioSummitDemo.Interceptors.CommandHandlers
{
    public interface ICommandTreeHandler<out T>
        where T : DbCommandTree
    {
        T HandleRequest(DbCommandTree command);
    }
}