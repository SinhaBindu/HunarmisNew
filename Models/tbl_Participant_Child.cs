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
    
    public partial class tbl_Participant_Child
    {
        public System.Guid Child_ParticipantId_pk { get; set; }
        public Nullable<System.Guid> ParticipantId { get; set; }
        public string NativeLanguage { get; set; }
        public string NativeLanguage_Other { get; set; }
        public string Address { get; set; }
        public string Pincode { get; set; }
        public Nullable<int> StateId { get; set; }
        public Nullable<int> DistrictId { get; set; }
        public string City { get; set; }
        public string Village { get; set; }
        public string EmergencyPersonName { get; set; }
        public string EmergencyContactNo { get; set; }
        public string EmergencyRelationship { get; set; }
        public Nullable<decimal> EmergencyMonthlyIncome { get; set; }
        public Nullable<int> SelfImageAttached { get; set; }
        public Nullable<int> IDType { get; set; }
        public string IDNo { get; set; }
        public string EducationQualification { get; set; }
        public string SchoolPassoutYear { get; set; }
        public Nullable<decimal> MonthlyIncome { get; set; }
        public string HouseholdAssetOwnership { get; set; }
        public string AccesstoServices { get; set; }
        public Nullable<int> HaveYouWorkedEarliear { get; set; }
        public Nullable<int> WorkExperienceType { get; set; }
        public Nullable<int> WorkExperience { get; set; }
        public Nullable<int> HomeTown { get; set; }
        public Nullable<int> AreyouIntrestedingettingajob { get; set; }
        public Nullable<int> Willyoubeinterestedinrelocatingforajob { get; set; }
        public string Languagesknown { get; set; }
        public string Languagesknown_Other { get; set; }
        public string KnowledgeofEnglishLanguage { get; set; }
        public string Whichskillingcourseshaveyoudoneearlier { get; set; }
        public string Whichskillingcourseshaveyoudoneearlier_Other { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
        public string DeletedBy { get; set; }
        public Nullable<System.DateTime> DeletedOn { get; set; }
    
        public virtual tbl_Participant tbl_Participant { get; set; }
    }
}