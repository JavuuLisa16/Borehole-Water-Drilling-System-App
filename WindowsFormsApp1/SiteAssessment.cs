using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    public class SiteAssessment
    {

        public string AssessmentID { get; set; }
        public string RequestID { get; set; }
        public string TechnicianID { get; set; }
        public DateTime AssessmentDate { get; set; }
        public string SiteCondition { get; set; }
        public string Accessibility { get; set; }
        public string Suitability { get; set; }
        public string Notes { get; set; }
    }
}
