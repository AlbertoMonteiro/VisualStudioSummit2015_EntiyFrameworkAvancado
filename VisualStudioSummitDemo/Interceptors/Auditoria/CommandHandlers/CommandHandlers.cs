using System.Data.Common;
using System.Data.Entity.Infrastructure.Interception;
using System.Text.RegularExpressions;

namespace VisualStudioSummitDemo.Interceptors.Auditoria.CommandHandlers
{
    public class DeleteCommandHandler : CommandHandler<int>
    {
        private const string DELETE_PATTERN = @"DELETE \[dbo\]\.\[(?<table>\w+)\]\r?\n?WHERE \(\[Id\] = (?<id>@\d+)\)";

        public DeleteCommandHandler(DbCommandInterceptionContext<int> context)
            : base(context, AuditEntryKind.Delete, DELETE_PATTERN)
        {
        }
    }

    public class InsertCommandHandler : CommandHandler<DbDataReader>
    {
        public const string INSERT_PATTERN =
            @"INSERT \[dbo\]\.\[(?<table>\w+)\]\(((?<field>\[\w+\]),? ?)+\)\r?\n?VALUES \(((?<value>@\d+|NULL),?\s?)+";

        public InsertCommandHandler(DbCommandInterceptionContext<DbDataReader> context)
            : base(context, AuditEntryKind.Insert, INSERT_PATTERN)
        {
        }

        protected override long GetEntityId(IDbCommandWraper command, Match match)
        {
            long entityId;
            using (var dbCommand = command.Connection.CreateCommand())
            {
                Context.OriginalResult.Close();
                dbCommand.Transaction = command.Transaction;
                dbCommand.CommandText = string.Format(@"SELECT CAST(IDENT_CURRENT('{0}') as bigint) as Id;",
                    match.Groups["table"].Value);
                entityId = (long) dbCommand.ExecuteScalar();
                Context.Result = dbCommand.ExecuteReader();
            }
            return entityId;
        }
    }

    public class UpdateCommandHandler : CommandHandler<int>
    {
        private const string UPDATE_PATTERN =
            @"UPDATE \[dbo\]\.\[(?<table>\w+)\]\r?\n?SET (\[(?<field>\w+)\] = (?<value>(?>@)\d+|NULL),?\s?)+\r?\n?WHERE \(\[Id\] = (?<id>@\d+)\)";

        public UpdateCommandHandler(DbCommandInterceptionContext<int> context)
            : base(context, AuditEntryKind.Update, UPDATE_PATTERN)
        {
        }
    }
}