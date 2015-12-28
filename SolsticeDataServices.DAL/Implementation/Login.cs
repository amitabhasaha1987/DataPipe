using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SolsticeDataServices.DAL.Entities;
using SolsticeDataServices.DAL.Context;
using System.Data.Entity;

namespace SolsticeDataServices.DAL.Implementation
{
    public class Login
    {
        SolsticeDataServicesContext _context;
        public Login()
        {
            _context = new SolsticeDataServicesContext();
        }

        public async Task<Users> LoggingIn(Users user)
        {
            Users LoginUser = null;
            try
            {
                var password = Convert.ToBase64String(Encoding.UTF8.GetBytes(user.Password));
                LoginUser = await _context.Users.FirstOrDefaultAsync(u => u.EmailId == user.EmailId && u.Password == password && !u.IsBlocked && u.IsVerified);
            }
            catch (Exception ex) { }
            return LoginUser;
        }

        public async Task<Users> IsUserRegistered(Users user)
        {
            bool HasUser = await _context.Users.Where(u => u.EmailId == user.EmailId).AnyAsync();
            if (HasUser)
            {
                Users myuser = new Users();
                myuser = await _context.Users.Where(u => u.EmailId == user.EmailId).FirstOrDefaultAsync();
                return myuser;
            }
            else
                return null;
        }
    }
}
