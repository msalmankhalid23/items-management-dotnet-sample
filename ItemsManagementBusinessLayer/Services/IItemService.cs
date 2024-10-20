

using ItemsManagementBusinessLayer.DTO;

namespace ItemsManagementBusinessLayer.Services
{
    public interface IItemService
    {
        public List<ItemDTO> GetItems();
        public ItemDTO GetItemById(int id);

        public int AddItems(List<ItemDTO> items); //Item without Id
        public bool UpdateItemById(int id, ItemDTO item); //Item without id
        public bool DeleteItemById(int id);
    }
}
