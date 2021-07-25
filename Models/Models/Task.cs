using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace ModelLibrary.Models
{
    public class Task
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string Instructions { get; set; }
        public TaskStatus Status  { get; set; }
        public Nullable<int> Hours_Worked_On_Task { get; set; }
        public Nullable<int> Duration_In_Hours { get; set; }
        public Nullable<int> Duration_In_Days { get; set; }
        public string Deleted { get; set; }

    }
}
