using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ModelLibrary.Models
{
    public class ErrorLog
    {
        public int ID { get; set; }
        public string Section { get; set; }
        public string Method { get; set; }
        public string Message { get; set; }
        public DateTime Date_Stamp { get; set; }
        public string Computer { get; set; }
    }
}
