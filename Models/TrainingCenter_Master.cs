//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Hunarmis.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class TrainingCenter_Master
    {
        public int Id { get; set; }
        public Nullable<int> DistrictID_fk { get; set; }
        public Nullable<int> TrainingAgencyID_fk { get; set; }
        public string TrainingCenter { get; set; }
        public string Location { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> Updated { get; set; }
        public Nullable<int> OrderBy { get; set; }
    }
}