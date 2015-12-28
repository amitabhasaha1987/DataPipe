using System;
using System.Linq;
using System.Security.Principal;

namespace SolsticeDataServices.Security
{
    public class CustomPrincipal : IPrincipal
    {
        public IIdentity Identity { get; private set; }
        public bool IsInRole(string role)
        {
            if (Roles.Any(r => role.Contains(r)))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public CustomPrincipal(string Username)
        {
            this.Identity = new GenericIdentity(Username);
        }

        public Int64 UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public Guid LinkedAccessKey { get; set; }
        public Guid LastUsedLinkedAccessKey { get; set; }
        public string DeviceAccessKey { get; set; }

        public string[] Roles { get; set; }
    }
}
