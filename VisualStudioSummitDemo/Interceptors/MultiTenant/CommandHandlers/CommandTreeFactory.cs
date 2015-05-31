using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees;
using VisualStudioSummitDemo.Interceptors.CommandHandlers;

namespace VisualStudioSummitDemo.Interceptors.MultiTenant.CommandHandlers
{
    public static class CommandTreeFactory
    {
        private static readonly IDictionary<DbCommandTreeKind, ICommandTreeHandler<DbCommandTree>> _dictionary;

        static CommandTreeFactory()
        {
            _dictionary = new Dictionary<DbCommandTreeKind, ICommandTreeHandler<DbCommandTree>>
            {
                {DbCommandTreeKind.Delete, new DbDeleteCommandTreeHandler()},
                {DbCommandTreeKind.Insert, new DbInsertCommandTreeHandler()},
                {DbCommandTreeKind.Query, new DbSelectCommandTreeHandler()},
                {DbCommandTreeKind.Update, new DbUpdateCommandTreeHandler()}
            };
        }

        public static ICommandTreeHandler<DbCommandTree> GetCommandTreeHandler(DbCommandTreeKind dbCommandTreeKind)
        {
            return _dictionary.ContainsKey(dbCommandTreeKind) ? _dictionary[dbCommandTreeKind] : null;
        }
    }
}
