using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolsticeDataServices.Security
{
    public class CustomPrincipalSerializeModel
    {
        public Int64 UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public Guid LinkedAccessKey { get; set; }
        public Guid LastUsedLinkedAccessKey { get; set; }
        public string[] Roles { get; set; }


    }
}
