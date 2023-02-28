using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace ProjectBasedSystem.Models
{
    [DataContract]
    public class residentialImages
    {
        [Key]
        public int profile_id { get; set; }

        [DataMember(Name = "residential_id")]

        public int residential_id { get; set; }

        [DataMember(Name = "FileName")]

        public string FileName { get; set; }

        [DataMember(Name = "Description")]

        public string Description { get; set; }

        [DataMember(Name = "FilePath")]

        public string FilePath { get; set; }

        [DataMember(Name = "FileSize")]

        public int FileSize { get; set; }

    }
}