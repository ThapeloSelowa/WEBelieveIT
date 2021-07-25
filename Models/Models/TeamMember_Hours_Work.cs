using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ModelLibrary.Models
{
    public class TeamMember_Hours_Work
    {
        public int Id { get; set; }
        public virtual TeamMember CasualEmployee { get; set; }
        public string Member_ID { get; set; }
        public int Task_Id { get; set; }
        public string Task_Description { get; set; }
        public int Hours_Worked { get; set; }
        public DateTime Work_Date { get; set; }
        public Nullable<decimal> Day_Earnings { get; set; }
        public Nullable<decimal> Total_Earnings { get; set; }
        public Nullable<decimal> Payment { get; set; }
        public Nullable<decimal> Total_Due_To_Employee { get; set; }
    }
}
