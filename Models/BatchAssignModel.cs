using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hunarmis.Models
{
    public class BatchAssignModel
    {
        public BatchAssignModel() { }
        public System.Guid ParticipantID { get; set; }
        public int TrainingCeneterId { get; set;}
        public int BatchId { get; set;}
        public int NoofParticipant { get; set;}
        public DateTime BatchAssignDate { get; set;}
    }
}