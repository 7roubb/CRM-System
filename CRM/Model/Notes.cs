using System.ComponentModel.DataAnnotations.Schema;
using System.Threading.Tasks;

namespace CRM.Model
{
    public class Notes
    {
        public int Id { get; set; }
        public string Date { get; set; }
        public string Note { get; set; }
        public bool IsNew_Todo { get; set; }

        [ForeignKey("Todo_Type")]
        public int Todo_Type_ID { get; set; }
        public  Todo_Type Todo_Type { get; set; }

        [ForeignKey("Todo_Desc")]
        public int Todo_Desc_ID { get; set; }
        public  Todo_Desc Todo_Desc { get; set; }

        [ForeignKey("Task_Status")]
        public int Task_Status_ID { get; set; }
        public  Task_Status Task_Status { get; set; }

        [ForeignKey("Sales_Rep")]
        public int Sales_Rep_ID { get; set; }
        public  User Sales_Rep { get; set; }
    }
}
