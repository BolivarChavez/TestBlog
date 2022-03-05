using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiBlog.Models
{
    //Model for the output data structure. This model map the return codes and messages for strored procedures that only
    //insert or update data tables in the SQL database
    public class Output
    {
        [Key]
        public Int32 return_code { get; set; }
        public string return_message { get; set; }
        public Int32 record_id { get; set; }
    }
}
