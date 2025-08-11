namespace CRM.Model
{
    public class Contact_Status
    {
        public int Id { get; set; }
        public string Status { get; set; }
        public  ICollection<Contact> Contacts { get; set; }
    }
}
