using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ItemsManagementDataAccess.Data;
using ItemsManagementDataAccess.Models;

namespace ItemsManagementDataAccess.DBA
{
    public class ItemDBA : BaseDBA
    {
        private readonly ILogger<ItemDBA> _logger;
        public ItemDBA(ILogger<ItemDBA> logger)
        {
            _logger = logger;
        }

        public List<Item> GetItems()
        {
            try
            {
                Func<ItemsDBContext, List<Item>> getItems = db =>
                {
                    return db.Items.ToList();
                };

                return Execute(getItems);

            }
            catch (SqlException ex)
            {
                _logger.LogError("Error in DB Connection", ex);
            }

            return null;
        }

        public int AddItems(List<Item> items)
        {
            try
            {
                Action<ItemsDBContext> addItems = db =>
                {
                    db.Items.AddRange(items);
                };
                return Save(addItems);

            }
            catch (SqlException ex)
            {
                _logger.LogError("Error in DB Connection", ex);
            }

            return 0;
        }

        public Item GetItemById(int id)
        {
            try
            {
                Func<ItemsDBContext, Item> getItem = db =>
                {
                    return db.Items.Find(id);
                };

                return Execute(getItem);

            }
            catch (SqlException ex)
            {
                _logger.LogError("Error in DB Connection", ex);
            }

            return null;
        }

        public bool UpdateItemById(Item item)
        {
            try
            {
                Action<ItemsDBContext> updateItem = db =>
                {
                    db.Entry(item).State = EntityState.Modified;
                    
                };
                return Save(updateItem) > 0 ? true : false ;

            }
            catch (SqlException ex)
            {
                _logger.LogError("Error in DB Connection", ex);
            }

            return false;

        }
        public bool DeleteItemById(Item item)
        {
            try
            {
                Action<ItemsDBContext> updateItem = db =>
                {
                    db.Entry(item).State = EntityState.Deleted;

                };
                return Save(updateItem) > 0 ? true : false;

            }
            catch (SqlException ex)
            {
                _logger.LogError("Error in DB Connection", ex);
            }

            return false;
        }
    }
}
