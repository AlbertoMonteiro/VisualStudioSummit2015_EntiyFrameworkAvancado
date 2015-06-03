using System;
using System.Data.Common;
using System.Data.Entity.Infrastructure.Interception;
using VisualStudioSummitDemo.Interceptors.Auditoria.CommandHandlers;

namespace VisualStudioSummitDemo.Interceptors.Auditoria
{
    public class AuditingCommandInterceptor : IDbCommandInterceptor
    {
        private readonly string _connectionString;
        public Action<AuditEntry> Log { get; set; }

        public AuditingCommandInterceptor(Action<AuditEntry> log, string connectionString)
        {
            _connectionString = connectionString;
            Log = log;
        }

        #region Uneeded methods

        public void NonQueryExecuting(DbCommand command, DbCommandInterceptionContext<int> interceptionContext)
        {
        }

        public void ReaderExecuting(DbCommand command, DbCommandInterceptionContext<DbDataReader> interceptionContext)
        {
        }

        public void ScalarExecuting(DbCommand command, DbCommandInterceptionContext<object> interceptionContext)
        {
        }

        public void ScalarExecuted(DbCommand command, DbCommandInterceptionContext<object> interceptionContext)
        {
        }

        #endregion

        public void ReaderExecuted(DbCommand command, DbCommandInterceptionContext<DbDataReader> context)
        {
            ICommandHandler insert = new InsertCommandHandler(context);
            var auditEntry = insert.HandleCommand(new DbCommandWraper(command, _connectionString));

            if (auditEntry != null)
                Log(auditEntry);
        }

        public void NonQueryExecuted(DbCommand command, DbCommandInterceptionContext<int> context)
        {
            ICommandHandler updateHandler = new UpdateCommandHandler(context);
            updateHandler.SetNext(new DeleteCommandHandler(context));
            var auditEntry = updateHandler.HandleCommand(new DbCommandWraper(command, _connectionString));

            if (auditEntry == null) return;
            Log(auditEntry);
        }

    }
}