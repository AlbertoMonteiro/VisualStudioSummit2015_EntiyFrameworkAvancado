using System;

namespace VisualStudioSummitDemo.Interceptors.Auditoria
{
    public class AuditEntry
    {
        internal AuditEntry(AuditEntryKind kind)
        {
            Kind = kind;
            Created = DateTime.Now;
        }

        public long EntityId { get; set; }

        public DateTime Created { get; private set; }

        public string Table { get; set; }

        public int Transaction { get; set; }

        public string NewValue { get; set; }

        public AuditEntryKind Kind { get; private set; }

        public override string ToString()
        {
            return
                string.Format("Kind: {0}{5}Created: {1}{5}On table: {2}{5}In Transaction: {3}{5}EntityId: {4}{5}NewValues: {6}",
                    Kind, Created, Table, Transaction, EntityId, Environment.NewLine, NewValue);
        }
    }

    public enum AuditEntryKind
    {
        Insert,
        Update,
        Delete
    }
}