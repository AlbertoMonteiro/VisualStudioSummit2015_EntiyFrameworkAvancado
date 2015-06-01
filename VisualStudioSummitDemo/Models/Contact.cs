using System;

namespace VisualStudioSummitDemo.Models
{
    public class Contact
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public Genre Genre { get; set; }
        public DateTime BirthDate { get; set; }
        public bool Inactive { get; set; }
        public long TenantId { get; set; }
        public virtual Tenant Tenant { get; set; }
    }

    public enum Genre
    {
        Male,
        Female
    }
}
