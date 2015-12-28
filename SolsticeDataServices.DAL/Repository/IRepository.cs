using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolsticeDataServices.DAL.Repository
{
    // as these methods are very common. so we used T instead of any certain class and 
    // "Task" makes them prepare to call asynchronously
   public  interface IRepository<T> where T : class
    {
         Task<T> Add(T itemtoadd);
         Task<bool> Update(T itemtoupdate);
         Task<bool> Delete(string id);
         Task<IQueryable<T>> GetAll();
    }
}
