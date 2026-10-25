using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    public class Technician
    {
        public string TechnicianID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Username { get; set; }
        public string Specialisation { get; set; }
        public string Status { get; set; }
        public string Provider { get; set; }

        public string TechnicianName
        {
            get
            {
                return FirstName + " " + LastName;
            }
        }
    }
}
