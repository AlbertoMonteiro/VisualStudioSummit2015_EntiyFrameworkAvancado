using System.Data.Common;
using System.Data.Entity.Infrastructure.Interception;
using System.Linq;
using System.Text.RegularExpressions;

namespace VisualStudioSummitDemo.Interceptors.MultiTenant
{
    public class MultiTenantInterceptor : IDbCommandInterceptor
    {
        public static long TentantId;

        const string UPDATE_PATTERN = @"UPDATE \[dbo\]\.\[(?<table>\w+)\]\r?\n?SET (?<set>(\[(?<field>\w+)\] = (?<fieldValue>@(?<fieldParam>\d+)|NULL)),?\s?)+\r?\n?WHERE \(\[Id\] = (?<id>@\d+)\)";
        const string INSERT_PATTERN = @"INSERT \[dbo\]\.\[(?<table>\w+)\]\(((?<field>\[\w+\]),?\s?)+\)\r?\n?VALUES \((\s?(?<fieldValue>[@\w']+),?)+\)";

        public void NonQueryExecuting(DbCommand command, DbCommandInterceptionContext<int> interceptionContext)
        {
            //UPDATE
            var match = Regex.Match(command.CommandText, UPDATE_PATTERN);

            if (!match.Success) return;

            var fieldValues = match.Groups["fieldValue"].Captures.Cast<Capture>();
            var fieldsWithValues = match.Groups["field"].Captures.Cast<Capture>()
                .Zip(fieldValues, (fieldCapture, fieldValueCapture) => new { fieldCapture, fieldValueCapture })
                .ToDictionary(x => x.fieldCapture.Value, x => x.fieldValueCapture);
            var parameterName = fieldsWithValues["TenantId"].Value;

            command.Parameters[parameterName].Value = TentantId;
        }

        public void ReaderExecuting(DbCommand command, DbCommandInterceptionContext<DbDataReader> interceptionContext)
        {
            //SELECT
            if (command.Parameters.Contains("tenantId"))
                command.Parameters["tenantId"].Value = TentantId;
            
            //INSERT
            var match = Regex.Match(command.CommandText, INSERT_PATTERN);

            if (!match.Success) return;

            var fieldValues = match.Groups["fieldValue"].Captures.Cast<Capture>();
            var fieldsWithValues = match.Groups["field"].Captures.Cast<Capture>()
                .Zip(fieldValues, (fieldCapture, fieldValueCapture) => new { fieldCapture, fieldValueCapture })
                .ToDictionary(x => x.fieldCapture.Value, x => x.fieldValueCapture);
            var parameterName = fieldsWithValues["[TenantId]"].Value;

            command.Parameters[parameterName].Value = TentantId;
        }

        public void ScalarExecuting(DbCommand command, DbCommandInterceptionContext<object> interceptionContext)
        {
        }

        public void NonQueryExecuted(DbCommand command, DbCommandInterceptionContext<int> interceptionContext)
        {
        }

        public void ReaderExecuted(DbCommand command, DbCommandInterceptionContext<DbDataReader> interceptionContext)
        {
        }

        public void ScalarExecuted(DbCommand command, DbCommandInterceptionContext<object> interceptionContext)
        {
        }
    }
}
