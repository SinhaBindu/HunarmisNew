using Hunarmis.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using static Hunarmis.Manager.Enums;

namespace Hunarmis.Models
{
    public class ParticipantModel
    {
        public ParticipantModel()
        {
            ID = Guid.Empty;
            partChildModel = new PartChildModel();  
        }
        public System.Guid ID { get; set; }
        /// <summary>
        /// [Required]
        /// </summary>
        [Display(Name = DplyPartName.RegID)]
        public string RegID { get; set; }
        //[Required]
        //[Display(Name = DplyPartName.FirstName)]
        //public string FirstName { get; set; }
        ////[Required]
        //[Display(Name = DplyPartName.MiddleName)]
        //public string MiddleName { get; set; }
        //[Required]
        //[Display(Name = DplyPartName.LastName)]
        //public string LastName { get; set; }
        [Display(Name = DplyPartName.FullName)]
        public string FullName { get; set; }
        [Required]
        [Display(Name = DplyPartName.Gender)]
        public string Gender { get; set; }
        public Nullable<System.DateTime> DOB { get; set; }
        [Required]
        [Display(Name = DplyPartName.Age)]
        public string Age { get; set; }
        [Required]
        [Display(Name = DplyPartName.PhoneNo)]
        public string PhoneNo { get; set; }
        //[Required]
        [Display(Name = DplyPartName.AlternatePhoneNo)]
        public string AlternatePhoneNo { get; set; }
        [Required]
        [Display(Name = DplyPartName.EmailID)]
        public string EmailID { get; set; }
        [Required]
        [Display(Name = DplyPartName.AadharCardNo)]
        public string AadharCardNo { get; set; }
        //[Required]
        [Display(Name = DplyPartName.SelfImageAttached)]
        public HttpPostedFileBase SelfImage { get; set; }
        public string SelfImagePath { get; set; }
        [Required]
        [Display(Name = DplyPartName.BatchId)]
        public Nullable<int> BatchId { get; set; }
        //[Required]
        [Display(Name = "Batch Start Date")]
        public Nullable<DateTime> BatchStartDate { get; set; }
        [Display(Name = "Batch End Date")]
        public Nullable<DateTime> BatchEndDate { get; set; }
        //[Required]
        [Display(Name = DplyPartName.AssessmentScore)]
        public string AssessmentScore { get; set; }
        [Required]
        [Display(Name = DplyPartName.EduQualificationID)]
        public Nullable<int> EduQualificationID { get; set; }
        [Display(Name = DplyPartName.EduQualOther)]
        public string EduQualOther { get; set; }
        [Required]
        [Display(Name = DplyPartName.CourseEnrolledID)]
        public Nullable<int> CourseEnrolledID { get; set; }
        [Required]
        [Display(Name = DplyPartName.StateID)]
        public Nullable<int> StateID { get; set; }
        //[Required]
        [Display(Name = DplyPartName.DistrictID)]
        public Nullable<int> DistrictID { get; set; }
        //[Required]
        [Display(Name = DplyPartName.TrainingAgencyID)]
        public Nullable<int> TrainingAgencyID { get; set; }
        [Required]
        [Display(Name = DplyPartName.TrainingCenterID)]
        public Nullable<int> TrainingCenterID { get; set; }
        //[Required]
        [Display(Name = DplyPartName.TrainerName)]
        public Guid? TrainerId { get; set; }
        public string TrainerName { get; set; }

        [Display(Name = DplyPartName.IsPlaced)]
        public bool IsPlaced { get; set; }
        [Required]
        [Display(Name = DplyPartName.IsPlaced)]
        public string Is_Placed { get; set; }
        [Display(Name = DplyPartName.IsOffered)]
        public Nullable<bool> IsOffered { get; set; }
        [Required]
        [Display(Name = DplyPartName.IsOffered)]
        public string Is_Offered { get; set; }

        [Required]
        [Display(Name = "Marital Status")]
        public string MaritalStatus { get; set; }
        //[Required]
        [Display(Name = "No of Family Members")]
        public Nullable<int> NoofFamilyMembers { get; set; }
        // [Required]
        [Display(Name = "Annual Household Income (INR)")]
        public Nullable<decimal> AnnualHouseholdincome { get; set; }
        // [Required]
        [Display(Name = "Pre-Training Status")]
        public int? PreTrainingStatus { get; set; }
        // [Required]
        [Display(Name = "Target Group")]
        public int? TargetGroup { get; set; }
        public virtual PartChildModel partChildModel { get; set; }
    }
    public static class DplyPartName
    {
        public const string RegID = "Registration ID";
        public const string FullName = "Name";
        public const string FirstName = "First Name";
        public const string MiddleName = "Middle Name";
        public const string LastName = "Last Name";
        public const string Gender = "Gender";
        public const string Age = "Age";
        public const string PhoneNo = "Phone No";
        public const string AlternatePhoneNo = "Alternate Phone No";
        public const string EmailID = "Email ID";
        public const string AadharCardNo = "Aadhar Card No";
        public const string BatchId = "Batch Name";
        public const string AssessmentScore = "Assessment Score";
        public const string EduQualificationID = "Educational Qualification";
        public const string EduQualOther = "Educational Qualification Other";
        public const string CourseEnrolledID = "Course Enrolled";
        public const string StateID = "State";
        public const string DistrictID = "District";
        public const string TrainingAgencyID = "Training Agency";
        public const string TrainingCenterID = "Training Center";
        public const string TrainerName = "Trainer Name";
        public const string IsOffered = "Offered";
        public const string IsPlaced = "Placed";
        public const string MaritalStatus = "Marital Status";
        public const string NoofFamilyMembers = "No Of Family Members";
        public const string AnnualHouseholdincome = "Annual Household Income";
        public const string PreTrainingStatus = "Pre-Training Status";
        public const string TargetGroup = "Target Group";

        //Participant Child Nomeclature
        public const string NativeLanguage = "Native Language";
        public const string NativeLanguage_Other = "Native Language (Other)";
        public const string Address = "Address";
        public const string Pincode = "Pin Code";
        public const string AddressSection = "Registered Address (Permanent)";////Header
        public const string State = " State";
        public const string District = "District";
        public const string City = "City";
        public const string Village = "Village";
        public const string EmergencySection = "Emergency Contact";////Header
        public const string EmergencyPersonName = "Name";
        public const string EmergencyContactNo = "Contact No";
        public const string EmergencyRelationship = "Relationship";
        public const string EmergencyMonthlyIncome = "Monthly Income (Emergency)";
        public const string ParticipantOtherSection  = "Other Details";//Header
        public const string SelfImageAttached = "Self Image Attached";
        public const string IDType = "ID Type";
        public const string IDNo = "ID No.";
        public const string EducationHistorySection = "Education History";//Header
        public const string EducationQualification = "Education Qualification";
        public const string SchoolPassoutYear = "School Pass Out Year";
        public const string FinancialInformationSection = "//Header";//Header
        public const string MonthlyIncome = "Monthly Income";
        public const string HouseholdAssetSection = "Household Asset Ownership, What white goods do you have? (Y/N)";//Header
        public const string HouseholdAssetOwnership = "Household Asset Ownership";
        public const string AccesstoServicesSection = "Access To Services";//Header
        public const string AccesstoServices = "Access to Services";
        public const string HaveYouWorkedEarliear = "Have You Worked Earliear";
        public const string WorkExperienceType = "Type Of Work Experience";
        public const string WorkExperience = "Work Experience";
        public const string HomeTown = "Have You Worked only in your home town";
        public const string AreyouIntrestedingettingajob = "Are you Intrested in getting a job";
        public const string Willyoubeinterestedinrelocatingforajob = "Will you be interested in relocating for a job";
        public const string Languagesknown = "Languages known";
        public const string KnowledgeofEnglishLanguage = "Knowledge of English Language";
        public const string Whichskillingcourseshaveyoudoneearlier = "Which skilling courses have you done earlier";
        public const string Whichskillingcourseshaveyoudoneearlier_Other = "Which skilling courses have you done earlier (Other)";

    }
    public class BasicDetailsForPartModel
    {
        [Display(Name = DplyPartCalName.RegID)]
        public string RegID { get; set; }
        [Display(Name = DplyPartCalName.FullName)]
        public string FullName { get; set; }
        [Display(Name = DplyPartCalName.BatchName)]
        public string BatchId { get; set; }
        [Display(Name = DplyPartCalName.BatchName)]
        public string BatchName { get; set; }
        [Display(Name = DplyPartCalName.SBatchDt)]
        public string SBatchDt { get; set; }
        [Display(Name = DplyPartCalName.EBatchDt)]
        public string EBatchDt { get; set; }
        [Display(Name = DplyPartCalName.PhoneNo)]
        public string PhoneNo { get; set; }
        [Display(Name = DplyPartCalName.TrainingAgencyName)]
        public string TrainingAgencyName { get; set; }
        [Display(Name = DplyPartCalName.TrainingCenter)]
        public string TrainingCenter { get; set; }
        [Display(Name = DplyPartCalName.CourseName)]
        public string CourseName { get; set; }
        [Display(Name = DplyPartCalName.ReportedBy)]
        public string ReportedBy { get; set; }
        [Display(Name = DplyPartCalName.ReportedOn)]
        public string ReportedOn { get; set; }
    }
    public class PartChildModel 
    {
        public PartChildModel()
        {
            Child_ParticipantId_pk = Guid.Empty;
        }
        public System.Guid Child_ParticipantId_pk { get; set; }
        public Nullable<System.Guid> ParticipantId { get; set; }
        public string NativeLanguage_hd { get; set; }
        [Required]
        [Display(Name = DplyPartName.NativeLanguage)]
        public string NativeLanguage { get; set; }
        [Display(Name = DplyPartName.NativeLanguage_Other)]
        public string NativeLanguage_Other { get; set; }
        [Required]
        [Display(Name = DplyPartName.Address)]
        public string Address { get; set; }
        [Required]
        [Display(Name = DplyPartName.Pincode)]
        public string Pincode { get; set; }
        [Required]
        [Display(Name = DplyPartName.State)]
        public Nullable<int> StateId { get; set; }
        [Required]
        [Display(Name = DplyPartName.DistrictID)]
        public Nullable<int> DistrictId { get; set; }
        [Required]
        [Display(Name = DplyPartName.City)]
        public string City { get; set; }
        [Required]
        [Display(Name = DplyPartName.Village)]
        public string Village { get; set; }
        [Required]
        [Display(Name = DplyPartName.EmergencyPersonName)]
        public string EmergencyPersonName { get; set; }
        [Required]
        [Display(Name = DplyPartName.EmergencyContactNo)]
        public string EmergencyContactNo { get; set; }
        [Required]
        [Display(Name = DplyPartName.EmergencyRelationship)]
        public string EmergencyRelationship { get; set; }
        [Required]
        [Display(Name = DplyPartName.EmergencyMonthlyIncome)]
        public Nullable<decimal> EmergencyMonthlyIncome { get; set; }
       
        [Required]
        [Display(Name = DplyPartName.IDType)]
        public Nullable<int> IDType { get; set; }
        [Required]
        [Display(Name = DplyPartName.IDNo)]
        public string IDNo { get; set; }
        // public string EducationQualification { get; set; }
        [Required]
        [Display(Name = DplyPartName.SchoolPassoutYear)]
        public string SchoolPassoutYear { get; set; }
        [Required]
        [Display(Name = DplyPartName.MonthlyIncome)]
        public Nullable<decimal> MonthlyIncome { get; set; }
        [Required]
        [Display(Name = DplyPartName.HouseholdAssetOwnership)]
        public string HouseholdAssetOwnership { get; set; }
        public string HouseholdAssetOwnership_hd { get; set; }
        [Required]
        [Display(Name = DplyPartName.AccesstoServices)]
        public string AccesstoServices { get; set; }
        public string AccesstoServices_hd { get; set; }
        [Required]
        [Display(Name = DplyPartName.HaveYouWorkedEarliear)]
        public Nullable<int> HaveYouWorkedEarliear { get; set; }
        [Required]
        [Display(Name = DplyPartName.WorkExperienceType)]
        public Nullable<int> WorkExperienceType { get; set; }
        [Required]
        [Display(Name = DplyPartName.WorkExperience)]
        public Nullable<int> WorkExperience { get; set; }
        [Required]
        [Display(Name = DplyPartName.HomeTown)]
        public Nullable<int> HomeTown { get; set; }
        [Required]
        [Display(Name = DplyPartName.AreyouIntrestedingettingajob)]
        public Nullable<int> AreyouIntrestedingettingajob { get; set; }
        [Required]
        [Display(Name = DplyPartName.Willyoubeinterestedinrelocatingforajob)]
        public Nullable<int> Willyoubeinterestedinrelocatingforajob { get; set; }
        [Required]
        [Display(Name = DplyPartName.Languagesknown)]
        public string Languagesknown { get; set; }
        public string Languagesknown_hd { get; set; }
        //[Required]
        //[Display(Name = DplyPartName.Languagesknown_Other)]
        public string Languagesknown_Other { get; set; }
        [Required]
        [Display(Name = DplyPartName.KnowledgeofEnglishLanguage)]
        public string KnowledgeofEnglishLanguage { get; set; }
        public string KnowledgeofEnglishLanguage_hd { get; set; }
        public string Whichskillingcourseshaveyoudoneearlier_hd { get; set; }
        [Required]
        [Display(Name = DplyPartName.Whichskillingcourseshaveyoudoneearlier)]
        public string Whichskillingcourseshaveyoudoneearlier { get; set; }
        [Display(Name = DplyPartName.Whichskillingcourseshaveyoudoneearlier_Other)]
        public string Whichskillingcourseshaveyoudoneearlier_Other { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
        public string DeletedBy { get; set; }
        public Nullable<System.DateTime> DeletedOn { get; set; }

        public virtual ParticipantModel Partmodel { get; set; }
    }

}