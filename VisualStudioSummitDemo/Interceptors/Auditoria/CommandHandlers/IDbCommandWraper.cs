using System.Collections.Generic;
using System.Data.Common;

namespace VisualStudioSummitDemo.Interceptors.Auditoria.CommandHandlers
{
    public interface IDbCommandWraper
    {
        string CommandText { get; }
        string ConnectionString { get; }
        IList<DbParameter> Parameters { get; }
        DbTransaction Transaction { get; }
        DbConnection Connection { get; }
        DbParameter this[string index] { get; }
    }
}