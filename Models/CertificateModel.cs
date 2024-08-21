﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Hunarmis.Models
{
    public class CertificateModel
    {
        [AllowHtml]
        public string HrmlData { get; set; }
        public string IsCertificate { get; set; }
        public string ScorePercentage { get; set; }
        public int FormId { get; set; }
        public int BatchId { get; set; }
        public Guid ParticipantId { get; set; }
    }
}