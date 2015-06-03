using System.Data.Entity.Migrations.Model;
using System.Data.Entity.SqlServer;

namespace VisualStudioSummitDemo.Migrations.CustomOperations
{
    public class CustomSqlServerMigrationSqlGenerator : SqlServerMigrationSqlGenerator
    {
        protected override void Generate(MigrationOperation migrationOperation)
        {
            base.Generate(migrationOperation);
        }
    }
}