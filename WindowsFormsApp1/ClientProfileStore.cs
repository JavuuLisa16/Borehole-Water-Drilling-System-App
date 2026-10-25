using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    public static class ClientProfileStore
    {
        public static ClientProfile CurrentClient { get; set; } = new ClientProfile
        {
            ClientID = "C001",
            FirstName = "John",
            LastName = "Javu",
            Email = "john@example.com",
            Phone = "0712345678",
            Username = "Client",
            Location = "Queenstown"
        };
    }
}
