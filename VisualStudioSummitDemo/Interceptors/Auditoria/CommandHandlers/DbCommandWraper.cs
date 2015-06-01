using System.Collections.Generic;
using System.Data.Common;
using System.Linq;

namespace VisualStudioSummitDemo.Interceptors.Auditoria.CommandHandlers
{
    public class DbCommandWraper : IDbCommandWraper
    {
        private readonly DbParameterCollection _parameters;

        public DbCommandWraper(DbCommand command, string connectionString)
        {
            CommandText = command.CommandText;
            _parameters = command.Parameters;
            Parameters = _parameters.Cast<DbParameter>().ToList();
            Transaction = command.Transaction;
            Connection = command.Connection;
            ConnectionString = connectionString;
        }

        public string CommandText { get; private set; }

        public string ConnectionString { get; private set; }

        public IList<DbParameter> Parameters { get; private set; }

        public DbTransaction Transaction { get; private set; }

        public DbConnection Connection { get; private set; }

        public DbParameter this[string index]
        {
            get { return _parameters[index]; }
        }
    }
}