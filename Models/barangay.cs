using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace ProjectBasedSystem.Models
{
    [DataContract]
    public class barangay
    {
        [Key]
        public int barangay_id { get; set; }

        [DataMember(Name = "barangay_name")]
        public string barangay_name { get; set; }
    }
}