using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Hunarmis.Models
{
    public class IndiParticipantModels
    {
        public IndiParticipantModels()
        {
        }

        public int ID { get; set; }
        public System.Guid UserID_Fk { get; set; }
        public string RegID { get; set; }
        [Required]
        public string EmailID { get; set; }
        [Required]
        public string Password { get; set; }
        public string RandomValue { get; set; }
    }
}