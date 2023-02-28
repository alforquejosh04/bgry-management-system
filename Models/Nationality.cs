using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace ProjectBasedSystem.Models
{
    [DataContract]
    public class nationality
    {
         [Key]
        public int Nationality_id { get; set; }

        [DataMember(Name = "nationality_name")]
        public string nationality_name { get; set; }
    }
}