using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Hunarmis.Models
{
    public class PartResumeMode
    {
        public PartResumeMode()
        {
            PartResumeId_pk = Guid.Empty;
        }
        [Key]
        public System.Guid PartResumeId_pk { get; set; }
        [Required]
        public Guid PartId { get; set; }
        [Display(Name = "Resume Format")]
        [Required]
        [AllowHtml]
        public string ResumeTemplate { get; set; }
        public bool IsActive { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
    }

}