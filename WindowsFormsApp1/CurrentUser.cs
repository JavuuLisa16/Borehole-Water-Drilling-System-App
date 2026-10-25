using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    public class CurrentUser
    {
        public static int PersonID { get; set; }
        public static string Username { get; set; }
        public static string Role { get; set; }

        public static void Clear()
        {
            PersonID = 0;
            Username = null;
            Role = null;
        }
    }
}
