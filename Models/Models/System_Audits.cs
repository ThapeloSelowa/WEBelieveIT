using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ModelLibrary.Models
{
    public class System_Audits
    {
        public int Id { get; set; }
        public string User { get; set; }
        public string Change { get; set; }
        public DateTime Change_Date { get; set; }

    }
}
