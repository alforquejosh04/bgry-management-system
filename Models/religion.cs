using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace ProjectBasedSystem.Models
{
    [DataContract]
    public class religion
    {
        [Key]
        public int religion_id { get; set; }

        [DataMember(Name = "religion_name")]
        public string religion_name { get; set; }
    }
}