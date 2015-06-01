namespace VisualStudioSummitDemo.Models
{
    public class ContactDealer
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public long ContactId { get; set; }
        public Contact Contact { get; set; }
    }
}