using System.Data.Entity.Migrations;
using System.Data.Entity.Migrations.Infrastructure;

namespace VisualStudioSummitDemo.Migrations.CustomOperations
{
    public static class DbMigrationExtensions
    {
        public static void DropAllForeignKeys(this DbMigration migration, string tableWithFk, string columnFk,
            string tableReferenced)
        {
            var dbMigration = (IDbMigration) migration;
            dbMigration.AddOperation(new DropAllForeignKeysOperation(tableWithFk, columnFk, tableReferenced));
        }
    }
}