namespace CRM.Model
{
    public class Todo_Desc
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public  ICollection<Notes> Notes { get; set; }
    }

}
