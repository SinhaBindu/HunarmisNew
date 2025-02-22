using DocumentFormat.OpenXml.Wordprocessing;
using Hunarmis.Models;
using SubSonic.Schema;
using System;
using System.Data;
using System.Web;
using System.Web.Security;

namespace Hunarmis.Manager
{
    public static partial class SPManager
    {
        public static DataTable GetSPMasterList(int StateId ,string RoleIds ,string TrainingCenterIds)
        {
            StoredProcedure sp = new StoredProcedure("SPMasterList");
            sp.Command.AddParameter("@StateId", StateId, DbType.Int32);
            sp.Command.AddParameter("@RoleIds", RoleIds, DbType.String);
            sp.Command.AddParameter("@TrainingCenterIds", TrainingCenterIds, DbType.String);
            var dbCommand = sp.Command.ToDbCommand();
            dbCommand.CommandTimeout = 500; // Set timeout to 180 seconds-120//300 sec-5 mints
            DataTable dt = sp.ExecuteDataSet().Tables[0];
            return dt;
        }
        public static DataTable SP_ParticipantCheckValidWise(string ID="",int Type=0 ,string PhoneNo="",string EmailId="", string AadharCardNo = "")
        {
            StoredProcedure sp = new StoredProcedure("Check_ParticipantEntryValition");
            sp.Command.AddParameter("@ID", ID, DbType.String);
            sp.Command.AddParameter("@Type", Type, DbType.Int32);
            sp.Command.AddParameter("@PhoneNo", PhoneNo, DbType.String);
            sp.Command.AddParameter("@EmailId", EmailId, DbType.String);
            sp.Command.AddParameter("@AadharCardNo", AadharCardNo, DbType.String);
            var dbCommand = sp.Command.ToDbCommand();
            dbCommand.CommandTimeout = 500; // Set timeout to 180 seconds-120//300 sec-5 mints
            DataTable dt = sp.ExecuteDataSet().Tables[0];
            return dt;
        }
        public static DataTable SP_PCIEdubridgeCoursesList()
        {
            StoredProcedure sp = new StoredProcedure("SP_PCIEdubridgeCoursesList");
            var dbCommand = sp.Command.ToDbCommand();
            dbCommand.CommandTimeout = 500; // Set timeout to 180 seconds-120//300 sec-5 mints
            DataTable dt = sp.ExecuteDataSet().Tables[0];
            return dt;
        }
        public static DataTable SPGetUserlist(int? RoleId)
        {
            StoredProcedure sp = new StoredProcedure("SPGetUserlist");
            sp.Command.AddParameter("@RoleId", RoleId, DbType.Int32);
            sp.Command.AddParameter("@User", HttpContext.Current.User.Identity.Name, DbType.String);
            var dbCommand = sp.Command.ToDbCommand();
            dbCommand.CommandTimeout = 500; // Set timeout to 180 seconds-120//300 sec-5 mints
            DataTable dt = sp.ExecuteDataSet().Tables[0];
            return dt;
        }
        public static DataTable SP_GetDTACMasterList(int DistrictId = 0, int TAgencyId = 0)
        {
            StoredProcedure sp = new StoredProcedure("SP_GetDTACMasterList");
            sp.Command.AddParameter("@DistrictId", DistrictId, DbType.Int32);
            sp.Command.AddParameter("@TAgencyId", TAgencyId, DbType.Int32);
            var dbCommand = sp.Command.ToDbCommand();
            dbCommand.CommandTimeout = 500; // Set timeout to 180 seconds-120//300 sec-5 mints
            DataTable dt = sp.ExecuteDataSet().Tables[0];
            return dt;
        }
        public static DataTable SP_GetTrainer1CenterattimeList(int TrainingCenterId = 0)
        {
            StoredProcedure sp = new StoredProcedure("SP_GetTrainer");
            sp.Command.AddParameter("@TrainingCenterId", TrainingCenterId.ToString(), DbType.String);
            var dbCommand = sp.Command.ToDbCommand();
            dbCommand.CommandTimeout = 500; // Set timeout to 180 seconds-120//300 sec-5 mints
            DataTable dt = sp.ExecuteDataSet().Tables[0];
            return dt;
        }
        public static DataTable SP_GetAllTrainerList(string TrainerId = "")
        {
            StoredProcedure sp = new StoredProcedure("SP_GetAllTrainerList");
            sp.Command.AddParameter("@TrainerId", TrainerId.ToString(), DbType.String);
            var dbCommand = sp.Command.ToDbCommand();
            dbCommand.CommandTimeout = 500; // Set timeout to 180 seconds-120//300 sec-5 mints
            DataTable dt = sp.ExecuteDataSet().Tables[0];
            return dt;
        }
        public static DataTable SP_GetBatchForPart(string TrainerId, string TCIds, int BatchId = 0)
        {
            StoredProcedure sp = new StoredProcedure("SP_GetBatchForPart");
            sp.Command.AddParameter("@TrainerId", TrainerId, DbType.String);
            sp.Command.AddParameter("@TCIds", TCIds, DbType.String);
            sp.Command.AddParameter("@BatchId", BatchId, DbType.Int32);
            var dbCommand = sp.Command.ToDbCommand();
            dbCommand.CommandTimeout = 500; // Set timeout to 180 seconds-120//300 sec-5 mints
            DataTable dt = sp.ExecuteDataSet().Tables[0];
            return dt;
        }
        public static DataTable SP_GetModuleWiseBatches(int ModuleType = 0, int CId = 0, string TrainerId = "", string TCIds = "", int BatchId = 0)
        {
            StoredProcedure sp = new StoredProcedure("SP_GetModuleWiseBatches");
            sp.Command.AddParameter("@TrainerId", TrainerId, DbType.String);
            sp.Command.AddParameter("@TCIds", TCIds, DbType.String);
            sp.Command.AddParameter("@BatchId", BatchId, DbType.Int32);
            sp.Command.AddParameter("@CourseId", CId, DbType.Int32);
            sp.Command.AddParameter("@ModuleType", ModuleType, DbType.Int32);
            var dbCommand = sp.Command.ToDbCommand();
            dbCommand.CommandTimeout = 500; // Set timeout to 180 seconds-120//300 sec-5 mints
            DataTable dt = sp.ExecuteDataSet().Tables[0];
            return dt;
        }

        public static DataTable SP_GetUserList()
        {
            StoredProcedure sp = new StoredProcedure("SP_GetUserList");
            var dbCommand = sp.Command.ToDbCommand();
            dbCommand.CommandTimeout = 500; // Set timeout to 180 seconds-120//300 sec-5 mints
            DataTable dt = sp.ExecuteDataSet().Tables[0];
            return dt;
        }
        public static DataTable SP_Batch()
        {
            StoredProcedure sp = new StoredProcedure("SP_Batch");
            var dbCommand = sp.Command.ToDbCommand();
            dbCommand.CommandTimeout = 500; // Set timeout to 180 seconds-120//300 sec-5 mints
            DataTable dt = sp.ExecuteDataSet().Tables[0];
            return dt;
        }
        public static DataTable SP_BatchList()
        {
            StoredProcedure sp = new StoredProcedure("SP_BatchList");
            var dbCommand = sp.Command.ToDbCommand();
            dbCommand.CommandTimeout = 500; // Set timeout to 180 seconds-120//300 sec-5 mints
            DataTable dt = sp.ExecuteDataSet().Tables[0];
            return dt;
        }
        public static DataTable SP_CoursesList()
        {
            StoredProcedure sp = new StoredProcedure("SP_CoursesList");
            var dbCommand = sp.Command.ToDbCommand();
            dbCommand.CommandTimeout = 500; // Set timeout to 180 seconds-120//300 sec-5 mints
            DataTable dt = sp.ExecuteDataSet().Tables[0];
            return dt;
        }
        public static DataTable SP_EducationalMList()
        {
            StoredProcedure sp = new StoredProcedure("SP_EducationalMList");
            var dbCommand = sp.Command.ToDbCommand();
            dbCommand.CommandTimeout = 500; // Set timeout to 180 seconds-120//300 sec-5 mints
            DataTable dt = sp.ExecuteDataSet().Tables[0];
            return dt;
        }
        public static DataTable SP_Training_AgencyList()
        {
            StoredProcedure sp = new StoredProcedure("SP_Training_AgencyList");
            var dbCommand = sp.Command.ToDbCommand();
            dbCommand.CommandTimeout = 500; // Set timeout to 180 seconds-120//300 sec-5 mints
            DataTable dt = sp.ExecuteDataSet().Tables[0];
            return dt;
        }
        public static DataTable SP_TrainingCentreList()
        {
            StoredProcedure sp = new StoredProcedure("SP_TrainingCentreList");
            var dbCommand = sp.Command.ToDbCommand();
            dbCommand.CommandTimeout = 500; // Set timeout to 180 seconds-120//300 sec-5 mints
            DataTable dt = sp.ExecuteDataSet().Tables[0];
            return dt;
        }
        public static DataTable SP_ParticipantList(FilterModel model)
        {
            //model.CallStatus = !string.IsNullOrEmpty(model.CallStatus) ? model.CallStatus :"";
            model.UserId = string.IsNullOrEmpty(model.UserId) || model.UserId == "-1" || model.UserId == "0" ? "" : model.UserId;
            model.CallStatus = string.IsNullOrEmpty(model.CallStatus) || model.CallStatus == "-1" ? "" : model.CallStatus;
            StoredProcedure sp = new StoredProcedure("SP_ParticipantList");
            sp.Command.AddParameter("@Type", model.Type, DbType.Int32);
            sp.Command.AddParameter("@Search", model.Search, DbType.String);
            sp.Command.AddParameter("@CallStatus", model.CallStatus, DbType.String);
            sp.Command.AddParameter("@BatchId", model.BatchId, DbType.String);
            sp.Command.AddParameter("@CourseId", model.CourseId, DbType.String);
            sp.Command.AddParameter("@UserId", model.UserId, DbType.String);
            var dbCommand = sp.Command.ToDbCommand();
            dbCommand.CommandTimeout = 500; // Set timeout to 180 seconds-120//300 sec-5 mints
            DataTable dt = sp.ExecuteDataSet().Tables[0];
            return dt;
        }
        public static DataTable SP_RawParticipantList(FilterModel model)
        {
            //model.CallStatus = !string.IsNullOrEmpty(model.CallStatus) ? model.CallStatus :"";
            model.UserId = string.IsNullOrEmpty(model.UserId) || model.UserId == "-1" || model.UserId == "0" ? "" : model.UserId;
            model.BatchId = string.IsNullOrEmpty(model.BatchId) || model.BatchId == "-1" || model.BatchId == "0" ? "" : model.BatchId;
            //model.CallStatus = string.IsNullOrEmpty(model.CallStatus) || model.CallStatus == "-1" ? "" : model.CallStatus;
            StoredProcedure sp = new StoredProcedure("SP_RawParticipantList");
            //sp.Command.AddParameter("@Type", model.Type, DbType.Int32);
            //sp.Command.AddParameter("@Search", model.Search, DbType.String);
            //sp.Command.AddParameter("@CallStatus", model.CallStatus, DbType.String);
            var tcid =!string.IsNullOrWhiteSpace(model.TrainingCenterID) && model.TrainingCenterID!="0"? model.TrainingCenterID: MvcApplication.CUser.MappedTCenterIds;
            sp.Command.AddParameter("@IsPlacementTracker", model.IsPlacementTracker, DbType.Int16);
            sp.Command.AddParameter("@BatchId", model.BatchId, DbType.String);
            sp.Command.AddParameter("@CourseId", model.CourseId, DbType.String);
            sp.Command.AddParameter("@UserId", model.UserId, DbType.String);
            sp.Command.AddParameter("@Gender", model.Gender, DbType.String);
            sp.Command.AddParameter("@FD", model.FromDt, DbType.String);
            sp.Command.AddParameter("@TD", model.ToDt, DbType.String);
            sp.Command.AddParameter("@TCIds", tcid, DbType.String);
            var dbCommand = sp.Command.ToDbCommand();
            dbCommand.CommandTimeout = 500; // Set timeout to 180 seconds-120//300 sec-5 mints
            DataTable dt = sp.ExecuteDataSet().Tables[0];
            return dt;
        }
        public static DataTable SP_RawShowParticipantList(FilterModel model)
        {
            model.UserId = string.IsNullOrEmpty(model.UserId) || model.UserId == "-1" || model.UserId == "0" ? "" : model.UserId;
            //model.CourseId = string.IsNullOrEmpty(model.CourseId) ? model.CourseId = "" : model.CourseId;
            StoredProcedure sp = new StoredProcedure("SP_RawShowParticipantList");
            sp.Command.AddParameter("@CourseId", model.CourseId, DbType.String);
            sp.Command.AddParameter("@UserId", model.UserId, DbType.String);
            sp.Command.AddParameter("@Gender", model.Gender, DbType.String);
            sp.Command.AddParameter("@FD", model.FromDt, DbType.String);
            sp.Command.AddParameter("@TD", model.ToDt, DbType.String);
            sp.Command.AddParameter("@TCIds", model.TrainingCenterID, DbType.String);
            sp.Command.AddParameter("@TypeData", model.Type, DbType.Int32);
            var dbCommand = sp.Command.ToDbCommand();
            dbCommand.CommandTimeout = 500; // Set timeout to 180 seconds-120//300 sec-5 mints
            DataTable dt = sp.ExecuteDataSet().Tables[0];
            return dt;
        }
        public static DataTable SP_PartQuesList(FilterModel model)
        {
            StoredProcedure sp = new StoredProcedure("SP_PartQuesList");
            //model.ParticipantId = !string.IsNullOrWhiteSpace(model.ParticipantId) ? model.ParticipantId : "";
            //model.ParticipantQuestionId = !string.IsNullOrWhiteSpace(model.ParticipantQuestionId) ? model.ParticipantQuestionId : "";
            sp.Command.AddParameter("@YearId", model.YearId, DbType.Int32);
            sp.Command.AddParameter("@MonthId", model.MonthId, DbType.Int32);
            sp.Command.AddParameter("@BatchId", model.BatchId, DbType.Int32);
            sp.Command.AddParameter("@ParticipantId", model.ParticipantId, DbType.String);
            sp.Command.AddParameter("@ParticipantQuestionId", model.ParticipantQuestionId, DbType.String);
            sp.Command.AddParameter("@UserId", model.UserId, DbType.String);
            sp.Command.AddParameter("@IsPlacementTracker", model.IsPlacementTracker, DbType.Int32);
            var dbCommand = sp.Command.ToDbCommand();
            dbCommand.CommandTimeout = 500; // Set timeout to 180 seconds-120//300 sec-5 mints
            DataTable dt = sp.ExecuteDataSet().Tables[0];
            return dt;
        }
        public static DataSet SP_Dashboard(FilterModel model)
        {
            StoredProcedure sp = new StoredProcedure("SP_Dashboard");
            //sp.Command.AddParameter("@YearId", model.YearId, DbType.Int32);
            //sp.Command.AddParameter("@MonthId", model.MonthId, DbType.Int32);
            //sp.Command.AddParameter("@BatchId", model.BatchId, DbType.Int32);
            //sp.Command.AddParameter("@ParticipantQuestionId", model.ParticipantQuestionId, DbType.String);
            var dbCommand = sp.Command.ToDbCommand();
            dbCommand.CommandTimeout = 500; // Set timeout to 180 seconds-120//300 sec-5 mints
            DataSet ds = sp.ExecuteDataSet();
            return ds;
        }
        public static DataTable SP_Dashboard_TopLegend()
        {
            StoredProcedure sp = new StoredProcedure("SP_Dashboard_TopLegend");
            var dbCommand = sp.Command.ToDbCommand();
            dbCommand.CommandTimeout = 500; // Set timeout to 180 seconds-120//300 sec-5 mints
            var ds = sp.ExecuteDataSet().Tables[0];
            return ds;
        }
        public static DataTable SP_Dashboard_Graphs(int mode, string startDate = "", string endDate = "")
        {
            StoredProcedure sp = new StoredProcedure("SP_Dashboard_Graphs");
            sp.Command.AddParameter("@mode", mode, DbType.Int32);
            sp.Command.AddParameter("@startDate", startDate, DbType.String);
            sp.Command.AddParameter("@endDate", endDate, DbType.String);
            var dbCommand = sp.Command.ToDbCommand();
            dbCommand.CommandTimeout = 500; // Set timeout to 180 seconds-120//300 sec-5 mints
            var ds = sp.ExecuteDataSet().Tables[0];
            return ds;
        }

        public static DataSet Sp_DashboardPartCalling(FilterModel model)
        {
            StoredProcedure sp = new StoredProcedure("Sp_PartCalling");
            var dbCommand = sp.Command.ToDbCommand();
            dbCommand.CommandTimeout = 500; // Set timeout to 180 seconds-120//300 sec-5 mints
            DataSet ds = sp.ExecuteDataSet();
            return ds;
        }
        public static DataSet SP_CallChartWiseMonth(FilterModel model)
        {
            StoredProcedure sp = new StoredProcedure("SP_CallChartWiseMonth");
            sp.Command.AddParameter("@YearId", model.YearId, DbType.Int32);
            sp.Command.AddParameter("@MonthId", model.MonthId, DbType.Int32);
            sp.Command.AddParameter("@UserBy", model.CutUser, DbType.String);
            var dbCommand = sp.Command.ToDbCommand();
            dbCommand.CommandTimeout = 500; // Set timeout to 180 seconds-120//300 sec-5 mints
            DataSet ds = sp.ExecuteDataSet();
            return ds;
        }
        public static DataTable SP_PartTempStatus()
        {
            StoredProcedure sp = new StoredProcedure("SP_PartTempStatus");
            var dbCommand = sp.Command.ToDbCommand();
            dbCommand.CommandTimeout = 500; // Set timeout to 180 seconds-120//300 sec-5 mints
            DataTable dt = sp.ExecuteDataSet().Tables[0];
            return dt;
        }
        public static DataTable SP_GetFileUpload()
        {
            StoredProcedure sp = new StoredProcedure("SP_GetFileUpload");
            var dbCommand = sp.Command.ToDbCommand();
            dbCommand.CommandTimeout = 500; // Set timeout to 180 seconds-120//300 sec-5 mints
            DataTable dt = sp.ExecuteDataSet().Tables[0];
            return dt;
        }
        #region Start 7 june 2024  Assessment Controller 
        public static DataSet GetSPScoreMarkAnswer(string User, int FormId, int BatchId)
        {
            StoredProcedure sp = new StoredProcedure("SP_ScoreMarkAnswer");
            sp.Command.AddParameter("@FormId", FormId, DbType.Int32);
            sp.Command.AddParameter("@BatchId", BatchId, DbType.Int32);
            sp.Command.AddParameter("@User", User, DbType.String);
            var dbCommand = sp.Command.ToDbCommand();
            dbCommand.CommandTimeout = 500; // Set timeout to 180 seconds-120//300 sec-5 mints
            DataSet ds = sp.ExecuteDataSet();
            return ds;
        }
        public static DataSet GetQuestionSummaryMarks(string User, int FormId, int BatchId)
        {
            StoredProcedure sp = new StoredProcedure("SP_QuestionSummaryMarks");
            sp.Command.AddParameter("@FormId", FormId, DbType.Int32);
            sp.Command.AddParameter("@BatchId", BatchId, DbType.Int32);
            sp.Command.AddParameter("@User", User, DbType.String);
            var dbCommand = sp.Command.ToDbCommand();
            dbCommand.CommandTimeout = 500; // Set timeout to 180 seconds-120//300 sec-5 mints
            DataSet ds = sp.ExecuteDataSet();
            return ds;
        }
        public static DataSet GetSP_ScorersSummaryMarks(string User, int FormId, int BatchId)
        {
            StoredProcedure sp = new StoredProcedure("SP_ScorersSummary");
            sp.Command.AddParameter("@FormId", FormId, DbType.Int32);
            sp.Command.AddParameter("@BatchId", BatchId, DbType.Int32);
            sp.Command.AddParameter("@User", User, DbType.String);
            var dbCommand = sp.Command.ToDbCommand();
            dbCommand.CommandTimeout = 500; // Set timeout to 180 seconds-120//300 sec-5 mints
            DataSet ds = sp.ExecuteDataSet();
            return ds;
        }
        public static DataSet GetSP_ScorersSummaryBatchWise(string User, int FormId, int BatchId)
        {
            StoredProcedure sp = new StoredProcedure("SP_ScorersSummaryBatchWise");
            sp.Command.AddParameter("@FormId", FormId, DbType.Int32);
            sp.Command.AddParameter("@BatchId", BatchId, DbType.Int32);
            sp.Command.AddParameter("@User", User, DbType.String);
            // Access the underlying DbCommand
            var dbCommand = sp.Command.ToDbCommand();
            dbCommand.CommandTimeout = 500; // Set timeout to 180 seconds-120//300 sec-5 mints
            // Log the timeout value to verify it's set correctly
            //System.Diagnostics.Debug.WriteLine($"CommandTimeout: {dbCommand.CommandTimeout}");
            DataSet ds = sp.ExecuteDataSet();
            return ds;
        }
        public static DataSet GetSP_ParticipantCallMonthWisematrix(string PrtId, string BatchId,string Course,string CallStatus ,string ReportedBy)
        {
            StoredProcedure sp = new StoredProcedure("Usp_ParticipantCallMonthWisematrix");
            sp.Command.AddParameter("@BatchId", BatchId, DbType.String);
            sp.Command.AddParameter("@CourseEnrolledID", Course, DbType.String);
            sp.Command.AddParameter("@AtPresentCallStatus", CallStatus, DbType.String);
            sp.Command.AddParameter("@PrtId", PrtId, DbType.String);
            sp.Command.AddParameter("@ReportedBy", ReportedBy, DbType.String);
            var dbCommand = sp.Command.ToDbCommand();
            dbCommand.CommandTimeout = 500; // Set timeout to 180 seconds-120//300 sec-5 mints
            DataSet ds = sp.ExecuteDataSet();
            return ds;
        }
        #endregion
        #region Attendance && Assessment Module
        public static DataTable SP_GetParticipant(FilterModel model)
        {
            StoredProcedure sp = new StoredProcedure("SP_GetParticipant");
            sp.Command.AddParameter("@BatchId", Convert.ToInt32(model.BatchId), DbType.Int32);
            var dbCommand = sp.Command.ToDbCommand();
            dbCommand.CommandTimeout = 500; // Set timeout to 180 seconds-120//300 sec-5 mints
            DataTable dt = sp.ExecuteDataSet().Tables[0];
            return dt;
        }
        public static DataTable SP_AttendanceParticipantList(FilterModel model)
        {
            StoredProcedure sp = new StoredProcedure("SP_AttendanceParticipantList");
            sp.Command.AddParameter("@BatchId", Convert.ToInt32(model.BatchId), DbType.Int32);
            sp.Command.AddParameter("@FD", model.FromDt, DbType.String);
            sp.Command.AddParameter("@TD", model.ToDt, DbType.String);
            var dbCommand = sp.Command.ToDbCommand();
            dbCommand.CommandTimeout = 500; // Set timeout to 180 seconds-120//300 sec-5 mints
            DataTable dt = sp.ExecuteDataSet().Tables[0];
            return dt;
        }
        public static DataTable SP_AttendancePartSummary(FilterModel model)
        {
            StoredProcedure sp = new StoredProcedure("SP_AttendanceParticipantSummary");
            sp.Command.AddParameter("@PartId", model.ParticipantId, DbType.String);
            sp.Command.AddParameter("@BatchId", model.BatchId, DbType.String);
            sp.Command.AddParameter("@FD", model.FromDt, DbType.String);
            sp.Command.AddParameter("@TD", model.ToDt, DbType.String);
            var dbCommand = sp.Command.ToDbCommand();
            dbCommand.CommandTimeout = 500; // Set timeout to 180 seconds-120//300 sec-5 mints
            DataTable dt = sp.ExecuteDataSet().Tables[0];
            return dt;
        }
        public static DataTable SP_GetAssessmentScheduleList(FilterModel model)
        {
            StoredProcedure sp = new StoredProcedure("SP_GetAssessmentScheduleList");
            sp.Command.AddParameter("@BatchId", Convert.ToInt32(model.BatchId), DbType.Int32);
            sp.Command.AddParameter("@AssessmentScheduleId", model.AssessmentScheduleId, DbType.String);
            sp.Command.AddParameter("@TrainingCenterIds", MvcApplication.CUser.MappedTCenterIds, DbType.String);
            var dbCommand = sp.Command.ToDbCommand();
            dbCommand.CommandTimeout = 500; // Set timeout to 180 seconds-120//300 sec-5 mints
            DataTable dt = sp.ExecuteDataSet().Tables[0];
            return dt;
        }
        #endregion
        #region Batch Assign
        public static DataTable SP_GetPartlistBatchNoAssign(FilterModel model)
        {
            StoredProcedure sp = new StoredProcedure("SP_GetPartlistBatchNoAssign");
            sp.Command.AddParameter("@CourseId", Convert.ToInt32(model.CourseId), DbType.Int32);
            sp.Command.AddParameter("@TrainingCenterId",Convert.ToInt32(model.TrainingCenterID), DbType.Int32);
            var dbCommand = sp.Command.ToDbCommand();
            dbCommand.CommandTimeout = 500; 
            DataTable dt = sp.ExecuteDataSet().Tables[0];
            return dt;
        }
        #endregion

        #region Login For Participant
        public static DataTable SP_LoginForParticipantCheck(ParticipantLoginModel model)
        {
            var ParticipantId_fk = model.ParticipantId_fk != Guid.Empty ? model.ParticipantId_fk.ToString() : "";
            StoredProcedure sp = new StoredProcedure("SP_LoginForParticipantCheck");
            sp.Command.AddParameter("@EmailID", model.EmailID, DbType.String);
            sp.Command.AddParameter("@Password", model.Password, DbType.String);
            sp.Command.AddParameter("@RandomValue", model.RandomValue, DbType.String);
            sp.Command.AddParameter("@ParticiPantId", ParticipantId_fk, DbType.String);
            var dbCommand = sp.Command.ToDbCommand();
            dbCommand.CommandTimeout = 500;
            DataTable dt = sp.ExecuteDataSet().Tables[0];
            return dt;
        }
        #endregion
        #region Mail send For Participant Email Id
        public static DataTable SP_GetAssessmentParticipant(FilterModel model)
        {
            StoredProcedure sp = new StoredProcedure("SP_GetAssessmentParticipant");
            sp.Command.AddParameter("@BatchId", Convert.ToInt32(model.BatchId), DbType.Int32);
            var dbCommand = sp.Command.ToDbCommand();
            dbCommand.CommandTimeout = 500;
            DataTable dt = sp.ExecuteDataSet().Tables[0];
            return dt;
        }
        public static DataTable SP_MailSendParticipantWise(string BatchId, string ParticipantIds)
        {
            StoredProcedure sp = new StoredProcedure("SP_MailSendParticipantWise");
            sp.Command.AddParameter("@BatchId", BatchId, DbType.String);
            sp.Command.AddParameter("@ParticipantIds", ParticipantIds, DbType.String);
            var dbCommand = sp.Command.ToDbCommand();
            dbCommand.CommandTimeout = 500;
            DataTable dt = sp.ExecuteDataSet().Tables[0];
            return dt;
        }

        #endregion

        #region Report
        public static DataSet sp_callstatus(FilterModel model)
        {
            //FromDt = !string.IsNullOrWhiteSpace(FromDt) ? FromDt:"";
            StoredProcedure sp = new StoredProcedure("SP_CallStatusUserWiseList");
            sp.Command.AddParameter("@BatchId", model.BatchId, DbType.String);
            sp.Command.AddParameter("@ReportedBy", model.UserId, DbType.String);
            sp.Command.AddParameter("@SD", model.FromDt, DbType.String);
            sp.Command.AddParameter("@ED", model.ToDt, DbType.String);
            var dbCommand = sp.Command.ToDbCommand();
            dbCommand.CommandTimeout = 500;
            DataSet ds = sp.ExecuteDataSet();
            return ds;
        }
        public static DataTable SP_CourseWiseTopices(FilterModel model)
        {
            StoredProcedure sp = new StoredProcedure("SP_CourseWiseTopices");
            sp.Command.AddParameter("@CourseId", model.CourseId, DbType.String);
            var dbCommand = sp.Command.ToDbCommand();
            dbCommand.CommandTimeout = 500;
            DataTable dt = sp.ExecuteDataSet().Tables[0];
            return dt;
        }
        public static DataTable SP_BatchWiseCourse(FilterModel model)
        {
            StoredProcedure sp = new StoredProcedure("SP_CourseBatch");
            sp.Command.AddParameter("@BatchId", model.BatchId, DbType.String);
            var dbCommand = sp.Command.ToDbCommand();
            dbCommand.CommandTimeout = 500;
            DataTable dt = sp.ExecuteDataSet().Tables[0];
            return dt;
        }
        //Training Center Wise For Participant List 
        public static DataTable SP_RawPartList_TrainCentchart(FilterModel model)
        {
            StoredProcedure sp = new StoredProcedure("SP_RawPartList_TrainCentchart");
            sp.Command.AddParameter("@StateId", model.StateId, DbType.String);
            sp.Command.AddParameter("@DistrictId", model.DistrictId, DbType.String);
            sp.Command.AddParameter("@TrainingCenterId", model.TrainingCenterID, DbType.String);
            var dbCommand = sp.Command.ToDbCommand();
            dbCommand.CommandTimeout = 500;
            DataTable dt = sp.ExecuteDataSet().Tables[0];
            return dt;
        }
        #endregion

        #region Placement Tracker 
        public static DataTable SP_PlacementTrackerDetail(FilterModel model)
        {
            StoredProcedure sp = new StoredProcedure("SP_PlacementTrackerDetails");
            sp.Command.AddParameter("@PlacementTrackerId", model.PlacementTrackerId, DbType.String);
            sp.Command.AddParameter("@PartId", model.ParticipantId, DbType.String);
            var dbCommand = sp.Command.ToDbCommand();
            dbCommand.CommandTimeout = 500;
            DataTable dt = sp.ExecuteDataSet().Tables[0];
            return dt;
        }
        #endregion

        #region Login For User Profile
        public static DataTable SP_LoginForIndiParticipantCheck(string ASPNetPartId)
        {
            StoredProcedure sp = new StoredProcedure("SP_LoginForIndiParticipantCheck");
            sp.Command.AddParameter("@ASPNetPartId", ASPNetPartId, DbType.String);
            var dbCommand = sp.Command.ToDbCommand();
            dbCommand.CommandTimeout = 500;
            DataTable dt = sp.ExecuteDataSet().Tables[0];
            return dt;
        }
        public static DataSet GetIndiParticipantDetailsByID(string UserID)
        {
            StoredProcedure sp = new StoredProcedure("Usp_GetIndiParticipantDetails");
            sp.Command.AddParameter("@UserID", UserID, DbType.String);
            var dbCommand = sp.Command.ToDbCommand();
            dbCommand.CommandTimeout = 500;
            DataSet dt = sp.ExecuteDataSet();
            return dt;
        }
        public static DataSet GetSP_DownloadCertificate(string PartId, int CId, int BId)
        {
            StoredProcedure sp = new StoredProcedure("SP_DownloadCertificate");
            sp.Command.AddParameter("@CId", CId, DbType.Int32);
            sp.Command.AddParameter("@BId", BId, DbType.Int32);
            sp.Command.AddParameter("@PartId", PartId, DbType.String);
            var dbCommand = sp.Command.ToDbCommand();
            dbCommand.CommandTimeout = 500;
            DataSet ds = sp.ExecuteDataSet();
            return ds;
        }
        #endregion

        #region Trainer Dashboard
        public static DataSet SP_TainerDashoard()
        {
            StoredProcedure sp = new StoredProcedure("SP_TainerDashoard");
            sp.Command.AddParameter("@TrainingCenterIds",MvcApplication.CUser.MappedTCenterIds, DbType.String);
            var dbCommand = sp.Command.ToDbCommand();
            dbCommand.CommandTimeout = 500;
            DataSet ds = sp.ExecuteDataSet();
            return ds;
        }
        public static DataSet SP_TainerDashoard_List(int CId,int BId)
        {
            StoredProcedure sp = new StoredProcedure("SP_TainerDashoard_List");
            sp.Command.AddParameter("@TrainingCenterIds", MvcApplication.CUser.MappedTCenterIds, DbType.String);
            sp.Command.AddParameter("@CourseId", CId, DbType.Int32);
            sp.Command.AddParameter("@BatchId", BId, DbType.Int64);
            var dbCommand = sp.Command.ToDbCommand();
            dbCommand.CommandTimeout = 500;
            DataSet ds = sp.ExecuteDataSet();
            return ds;
        }
        #endregion
        public static DataTable SP_GetCallStatusDetails(string ReportedBy = "", int Flag = 0,string MaxDate="",string SD="",string ED="")
        {
            StoredProcedure sp = new StoredProcedure("Usp_CallStatusDetails");
            sp.Command.AddParameter("@RepBy", ReportedBy, DbType.String);
            sp.Command.AddParameter("@Flg", Flag, DbType.String);
            sp.Command.AddParameter("@MaxDate", MaxDate, DbType.String);
            sp.Command.AddParameter("@SD", SD, DbType.String);
            sp.Command.AddParameter("@ED", ED, DbType.String);
            var dbCommand = sp.Command.ToDbCommand();
            dbCommand.CommandTimeout = 500;
            DataTable dt = sp.ExecuteDataSet().Tables[0];
            return dt;
        }
        public static DataTable SP_GetBulkDataParticipantLoginCreated(string TypeInsertUpdate = "",string PartId="")
        {
            StoredProcedure sp = new StoredProcedure("BulkDataParticipantLoginCreated");
            sp.Command.AddParameter("@TypeInsertUpdate", TypeInsertUpdate, DbType.String);
            sp.Command.AddParameter("@PartId", PartId, DbType.String);
            var dbCommand = sp.Command.ToDbCommand();
            dbCommand.CommandTimeout = 500;
            DataTable dt = sp.ExecuteDataSet().Tables[0];
            return dt;
        }
    }
}
