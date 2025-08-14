namespace CRM.Model
{
    public class Todo_Type
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public  ICollection<Notes> Notes { get; set; }
    }

}
