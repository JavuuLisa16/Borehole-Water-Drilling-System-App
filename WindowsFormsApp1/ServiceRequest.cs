using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    public class ServiceRequest
    {
        public string RequestID { get; set; }
        public string ClientID { get; set; }
        public string ClientName { get; set; }
        public string Service { get; set; }
        public string Provider { get; set; }

        public string Addressline1 { get; set; }
        public string Addressline2 { get; set; }
        public string City { get; set; }
        public string Province { get; set; }

        public string Location
        {
            get
            {
                return Addressline1 + ", " +
                       Addressline2 + ", " +
                       City + ", " +
                       Province;
            }
        }

        public DateTime RequestDate { get; set; }

        public DateTime ServiceDate { get; set; }

        public string Description { get; set; }

        public string Status { get; set; }
    }
}
