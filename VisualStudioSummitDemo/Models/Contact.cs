using System;
using System.ComponentModel.DataAnnotations;

namespace VisualStudioSummitDemo.Models
{
    public class Contact
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public Genre Genre { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
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
