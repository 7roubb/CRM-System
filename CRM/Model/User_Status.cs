namespace CRM.Model
{
    public class User_Status
    {
        public int Id { get; set; }
        public string status { get; set; }
        public IEnumerable<User> Users { get; set; }

    }
}
