using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    public class Review
    {

        public string ReviewID { get; set; }

        public string RequestID { get; set; }

        public string ClientID { get; set; }

        public string ClientName { get; set; }

        public string Description { get; set; }

        public int Rating { get; set; }

        public DateTime ReviewDate { get; set; }

        public string Status { get; set; }

        public string ClerkResponse { get; set; }

        public DateTime? ResponseDate { get; set; }
    }
}
