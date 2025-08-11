namespace CRM.Model
{
    public class Task_Status
    {
        public int Id { get; set; }
        public string Status { get; set; }
        public  ICollection<Notes> Notes { get; set; }
    }
}
