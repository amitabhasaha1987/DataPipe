using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SolsticeDataServices.DAL.Context;
using SolsticeDataServices.DAL.Entities;
using System.Data.Entity;
using System.Transactions;

namespace SolsticeDataServices.DAL.Implementation
{
    public class Register
    {
        SolsticeDataServicesContext _context;

        public Register()
        {
            _context = new SolsticeDataServicesContext();
        }

        public async Task<bool> UserSignUp(Users user)
        {
            var newUser = new Users
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Contact = user.Contact,
                EmailId = user.EmailId,
                Company = user.Company,
                AddTime = DateTime.Now,
                IsVerified = false,
                IsBlocked = false,
            };
            _context.Users.Add(newUser);
            int affectedRows = 0;
            try
            {
                affectedRows = await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return false;
            }

            return affectedRows > 0;
        }

        public async Task<IEnumerable<Users>> GetSignUps()
        {
            List<Users> signups = new List<Users>();
            signups = await _context.Users.Where(u => u.IsVerified == false).ToListAsync<Users>();
            return signups;
        }

        public async Task<int> Acceptence(Users user)
        {
            try
            {
                Users acceptUser = new Users();
                acceptUser = await _context.Users.FirstOrDefaultAsync(s => s.EmailId == user.EmailId && s.IsVerified);

                if (acceptUser == null)
                {
                    var myuser = await _context.Users.FirstOrDefaultAsync(s => s.UserId == user.UserId);
                    myuser.IsVerified = true;
                    myuser.Password = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(user.Password));
                    myuser.LinkedAccessKey = Guid.NewGuid();
                    myuser.LastUsedLinkedAccessKey = myuser.LinkedAccessKey;

                    var affectedRows = await _context.SaveChangesAsync();

                    return affectedRows > 0 ? 1 : 0;
                }
                else
                    return 2;
            }
            catch (Exception ex) { return 0; }
        }

        public async Task<bool> Rejection(Users user)
        {
            try
            {
                //bool IsExist = _context.Users.Any(s => s.UserId == user.UserId);
                //if (IsExist)
                //{
                Users acceptUser = new Users();
                acceptUser = await _context.Users.FirstOrDefaultAsync(s => s.UserId == user.UserId);
                _context.Users.Remove(acceptUser);

                int _affectedRows = await _context.SaveChangesAsync();

                if (_affectedRows > 0)
                    return true;
                else
                    return false;
                //}
                //else
                //    return false;
            }
            catch (Exception ex) { return false; }
        }
    }
}
