
using System.Data.Entity.Migrations.Model;

namespace VisualStudioSummitDemo.Migrations.CustomOperations
{
    public class DropAllForeignKeysOperation : MigrationOperation
    {
        #region SQL

        private const string DROP_FOREIGN_KEY = @"
DECLARE @tableWithFK varchar(255), @fkName varchar(255), @command varchar(255);
DECLARE DeleteForeignKeys CURSOR FOR
SELECT t.name  AS 'Tabela que contém FK', 
       fk.name AS 'Nome FK'       
FROM   sys.foreign_key_columns fkc 
       INNER JOIN sys.tables t 
               ON t.object_id = fkc.parent_object_id 
       INNER JOIN sys.tables t1 
       ON t1.object_id = fkc.referenced_object_id
       INNER JOIN sys.columns c1 
               ON c1.object_id = fkc.parent_object_id 
                  AND c1.column_id = fkc.parent_column_id 
       INNER JOIN sys.foreign_keys fk 
               ON fk.object_id = fkc.constraint_object_id 
	   INNER JOIN sys.schemas sc 
               ON t.schema_id = sc.schema_id
WHERE  (sc.name + '.' +t.name) = '{0}'
       AND c1.name = '{1}'
       AND (sc.name + '.' +t1.name) = '{2}'
OPEN DeleteForeignKeys;
FETCH NEXT FROM DeleteForeignKeys INTO @tableWithFK, @fkName;
WHILE @@FETCH_STATUS = 0
   BEGIN
        SET @command = 'ALTER TABLE [dbo].[' + @tableWithFK + '] DROP CONSTRAINT [' + @fkName + ']'
        PRINT @command;
        EXEC (@command);
        FETCH NEXT FROM DeleteForeignKeys INTO @tableWithFK, @fkName;
   END;
CLOSE DeleteForeignKeys;
DEALLOCATE DeleteForeignKeys;";

        #endregion

        private readonly Configuration _configuration;

        public DropAllForeignKeysOperation(string tableWithFk, string columnFk, string tableReferenced)
            : base(new Configuration(tableWithFk, columnFk, tableReferenced))
        {
            _configuration = new Configuration(tableWithFk, columnFk, tableReferenced);
        }

        public override bool IsDestructiveChange
        {
            get { return false; }
        }

        public string GetSql()
        {
            return string.Format(DROP_FOREIGN_KEY, _configuration.TableWithFk, _configuration.ColumnFk,
                _configuration.TableReferenced);
        }

        internal class Configuration
        {
            public string TableWithFk { get; private set; }
            public string ColumnFk { get; private set; }
            public string TableReferenced { get; private set; }

            public Configuration(string tableWithFk, string columnFk, string tableReferenced)
            {
                TableWithFk = tableWithFk;
                ColumnFk = columnFk;
                TableReferenced = tableReferenced;
            }
        }

    }
}