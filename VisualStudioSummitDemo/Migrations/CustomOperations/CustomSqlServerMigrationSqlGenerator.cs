using System.Data.Entity.Migrations.Model;
using System.Data.Entity.SqlServer;

namespace VisualStudioSummitDemo.Migrations.CustomOperations
{
    public class CustomSqlServerMigrationSqlGenerator : SqlServerMigrationSqlGenerator
    {
        protected override void Generate(MigrationOperation migrationOperation)
        {
            var dropAllForeignKeyOperation = migrationOperation as DropAllForeignKeysOperation;
            if (dropAllForeignKeyOperation != null)
            {
                using (var write = Writer())
                {
                    write.WriteLine(dropAllForeignKeyOperation.GetSql());

                    Statement(write.InnerWriter.ToString(), suppressTransaction: true);
                }
            }
            else
                base.Generate(migrationOperation);
        }
    }
}