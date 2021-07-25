using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ModelLibrary.Models
{
    public class Task_TeamMember
    {
        public int Id { get; set; }
        public int Member_Id { get; set; }
        public virtual TeamMember TeamMember { get; set; }
        public int Task_Id { get; set; }
        public  virtual Task Task { get; set; }
        public Nullable<int> Hours_Worked_On_Task { get; set; }

    }
}
