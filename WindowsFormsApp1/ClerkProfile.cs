using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    public class ClerkProfile
    {
        public string ClerkID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Username { get; set; }
        public string Organisation { get; set; }
        public DateTime EmploymentDate { get; set; }

        public string FullName => FirstName + " " + LastName;
    }
}
