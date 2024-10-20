using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ItemsManagementDataAccess.DBA;
using ItemsManagementDataAccess.Models;

namespace ItemsManagementDataAccess.Repositories
{
    public class ItemRepository : IItemRepository
    {
        private readonly ItemDBA _itemDBA;
        public ItemRepository(ItemDBA itemDBA) {
            _itemDBA = itemDBA;
        }
        public List<Item> GetItems()
        {
            return _itemDBA.GetItems();
        }

        public Item GetItemById(int id)
        {
            return _itemDBA.GetItemById(id);
        }


        public bool UpdateItemById(Item item){
            return _itemDBA.UpdateItemById(item);
        }
        public bool DeleteItemById(Item item)
        {
            return _itemDBA.DeleteItemById(item);
        }

        public int AddItems(List<Item> items)
        {
            return _itemDBA.AddItems(items);
        }
    }
}
