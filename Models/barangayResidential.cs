using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace ProjectBasedSystem.Models
{
    [DataContract]
    public class barangayResidential
    {
        [Key]
        public int residential_id { get; set; }

        [DataMember(Name = "first_Name")]

        public string first_Name { get; set; }

        [DataMember(Name = "last_Name")]

        public string last_Name { get; set; }

        [DataMember(Name = "Suffix")]

        public string Suffix { get; set; }

        [DataMember(Name = "birthdate")]

        public string birthdate { get; set; }

        [DataMember(Name = "gender")]

        public string gender { get; set; }

        [DataMember(Name = "marital_status")]

        public string marital_status { get; set; }

        [DataMember(Name = "contact_no")]

        public string contact_no { get; set; }

        [DataMember(Name = "email_add")]

        public string email_add { get; set; }

        [DataMember(Name = "household_no")]

        public string household_no { get; set; }


        [DataMember(Name = "zone_no")]

        public string zone_no { get; set; }

        [DataMember(Name = "barangay_id")]

        public int barangay_id { get; set; }

        [DataMember(Name = "household_member")]

        public int household_member { get; set; }

        [DataMember(Name = "length_of_stay")]

        public int length_of_stay { get; set; }

        [DataMember(Name = "religion_id")]

        public int religion_id { get; set; }

        [DataMember(Name = "occupation")]

        public string occupation { get; set; }

        [DataMember(Name = "nationality_id")]

        public int nationality_id { get; set; }

        [DataMember(Name = "Philhealth_no")]

        public string Philhealth_no { get; set; }

        [DataMember(Name = "voters")]

        public bool voters { get; set; }

        [DataMember(Name = "residency_status")]

        public string residency_status { get; set; }

        [DataMember(Name = "is_Delete")]

        public bool is_Delete { get; set; }


    }
}