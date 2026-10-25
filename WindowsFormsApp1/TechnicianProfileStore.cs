using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    internal class TechnicianProfileStore
    {
        public static TechnicianProfile CurrentTechnician { get; set; }
        = new TechnicianProfile
        {
            TechnicianID = "T001",
            FirstName = "Sipho",
            LastName = "Mokoena",
            Gender = "Male",
            Email = "sipho@example.com",
            Phone = "0712345678",
            Username = "technician",
            Specialisation = "Borehole Drilling",
            Organisation = "AB Pumps"
        };
    }
}
