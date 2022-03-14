using System;
using System.ComponentModel.DataAnnotations;

namespace ApiBlog.Models
{
    //Model for the user data structure
    public class User 
    {
        [Key]
        public Int32 user_id { get; set; }
        [StringLength(50)]
        public string user_name { get; set; }
        [StringLength(150)]
        public string user_fullname { get; set; }
        [StringLength(100)]
        public string user_password { get; set; }
        [StringLength(1)]
        public string user_type { get; set; }
        [StringLength(1)]
        public string user_status { get; set; }
        public DateTime user_last_update { get; set; }
    }
}
