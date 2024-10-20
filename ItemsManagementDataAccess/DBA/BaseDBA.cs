using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ItemsManagementDataAccess.Data;

namespace ItemsManagementDataAccess.DBA
{
    public class BaseDBA
    {
        protected T Execute<T>(Func<ItemsDBContext, T> resultDelegate, ItemsDBContext? dbContext = null)
        {
            if (dbContext != null)
            {
                return resultDelegate(dbContext);
            }
            else
            {
                using (ItemsDBContext db = new ItemsDBContext())
                {
                    return resultDelegate(db);
                }
            }
        }

        protected int Save(Action<ItemsDBContext> action, ItemsDBContext dbContext = null)
        {
            Func<ItemsDBContext, int> operation = db =>
            {
                action(db);
                return db.SaveChanges();
            };
            return Execute(operation, dbContext);
        }
    }
}
