namespace CRM.Dto.Responses
{
    public class ContactResponse
    {
        public int Id { get; set; }
        public string Contact_Title { get; set; }
        public string Contact_First { get; set; }
        public string Contact_Middle { get; set; }
        public string Contact_Last { get; set; }
        public string Contact_Type { get; set; }
        public string Date_of_Initial_Contact { get; set; }
        public string Company { get; set; }
        public string Address_Street_1 { get; set; }
        public string Address_Street_2 { get; set; }
        public string Address_City { get; set; }
        public string Address_State { get; set; }
        public string Address_Zip { get; set; }
        public string Address_Country { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }
        public string Linkedin_Profile { get; set; }
        public int Sales_Rep_ID { get; set; }
        public int Contact_Status_ID { get; set; }
        public string Project_Type { get; set; }
        public string Proposal_Description { get; set; }
        public string Proposal_Date { get; set; }
        public float Deliverables { get; set; }
    }
}
