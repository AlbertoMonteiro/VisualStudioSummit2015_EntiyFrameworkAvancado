using System;
using System.Data.Entity.Infrastructure.Interception;
using System.Linq;
using System.Text.RegularExpressions;

namespace VisualStudioSummitDemo.Interceptors.Auditoria.CommandHandlers
{
    public abstract class CommandHandler<T> : ICommandHandler
    {
        const string FORMAT = "Field:{0} - Value: {1}";

        protected readonly DbCommandInterceptionContext<T> Context;
        protected ICommandHandler NextHandler;
        protected AuditEntryKind Kind;
        protected string Pattern;
        private const string FIELD_REGEX_GROUP = "field";
        private const string TABLE_REGEX_GROUP = "table";
        private const string VALUE_REGEX_GROUP = "value";

        protected CommandHandler(DbCommandInterceptionContext<T> context, AuditEntryKind kind, string pattern)
        {
            Context = context;
            Kind = kind;
            Pattern = pattern;
        }

        protected virtual long GetEntityId(IDbCommandWraper command, Match match)
        {
            return Convert.ToInt64(command[match.Groups["id"].Value].Value);
        }

        public void SetNext(ICommandHandler nextCommandHandler)
        {
            NextHandler = nextCommandHandler;
        }

        public virtual AuditEntry HandleCommand(IDbCommandWraper command)
        {
            var match = Regex.Match(command.CommandText, Pattern);
            if (match.Success && Context.Exception == null)
            {
                var auditEntry = CreateAuditEntry(command, Kind, match.Groups, GetEntityId(command, match));

                return auditEntry;
            }
            return NextHandler != null ? NextHandler.HandleCommand(command) : null;
        }

        protected static AuditEntry CreateAuditEntry(IDbCommandWraper command, AuditEntryKind kind, GroupCollection groups, long entityId)
        {
            var fields = groups[FIELD_REGEX_GROUP].Captures.Cast<Capture>().Select(x => x.Value.StartsWith("[") && x.Value.EndsWith("]") ? x.Value : "[" + x.Value + "]");
            var valuesCommand = groups[VALUE_REGEX_GROUP].Captures.Cast<Capture>().ToList();
            var commandValues = from capture in valuesCommand
                                join parameter in command.Parameters on capture.Value equals parameter.ParameterName into dbParameters
                                from dbParameter in dbParameters.DefaultIfEmpty()
                                select dbParameter != null ? dbParameter.Value : capture.Value;

            var values = fields.Zip(commandValues, (t, p) => string.Format(FORMAT, t, p)).ToList();
            
            var audit = new AuditEntry(kind)
            {
                NewValue = string.Join(";", values),
                Table = groups[TABLE_REGEX_GROUP].Value,
                Transaction = command.Transaction.GetHashCode(),
                EntityId = entityId
            };
            return audit;
        }
    }
}