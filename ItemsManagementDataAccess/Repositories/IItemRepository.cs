using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ItemsManagementDataAccess.Models;

namespace ItemsManagementDataAccess.Repositories
{
    public interface IItemRepository
    {
        public List<Item> GetItems();
        public Item GetItemById(int id);

        public int AddItems(List<Item> items);
        public bool UpdateItemById(Item item);
        public bool DeleteItemById(Item item);
    }
}
