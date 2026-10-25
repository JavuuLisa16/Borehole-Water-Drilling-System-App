using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    public static class ClerkProfileStore
    {
        public static ClerkProfile CurrentClerk { get; set; } = new ClerkProfile
        {
            ClerkID = "CL001",
            FirstName = "Sarah",
            LastName = "Mbeki",
            Gender = "Female",
            Email = "sarah@example.com",
            Phone = "0712345678",
            Username = "clerk",
            Organisation = "AB Pumps",
            EmploymentDate = new DateTime(2025, 1, 15)
        };
    }
}
