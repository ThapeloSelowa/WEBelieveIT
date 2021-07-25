using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SharedModels.DataBaseEntities
{
    public class ModelLibrary.Models 
    {
        public int Id { get; set; }
        public string EntryType { get; set; }
        public int Emp_Id { get; set; }
        public virtual CasualEmployee CasualEmployee { get; set; }
        public string Employee_No { get; set; }
        public int Task_Id { get; set; }
        public string Task_Description { get; set; }
        public string Emp_Role { get; set; }
        public string Role_Rate_PH { get; set; }
        public int Hours_Worked { get; set; }
        public DateTime Work_Date { get; set; }
        public Nullable<decimal> Day_Earnings { get; set; }
        public Nullable<decimal> Total_Earnings { get; set; }
        public Nullable<decimal> Payment { get; set; }
        public Nullable<decimal> Total_Due_To_Employee { get; set; }
    }
}
