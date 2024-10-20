using AutoMapper;
using ItemsManagementBusinessLayer.DTO;
using ItemsManagementDataAccess.Repositories;


namespace ItemsManagementBusinessLayer.Services
{
    public class ItemService : IItemService
    {
        private readonly IMapper _mapper;
        public readonly IItemRepository _itemRepository;
        public ItemService(IItemRepository itemRepository, IMapper mapper) { 
            _itemRepository = itemRepository;
            _mapper= mapper;
        }  
        public List<ItemDTO> GetItems()
        {
            var items = _itemRepository.GetItems();
            return _mapper.Map<List<ItemDTO>>(items);
        }

        public ItemDTO GetItemById(int id)
        {
            var item = _itemRepository.GetItemById(id);
            return _mapper.Map<ItemDTO>(item); 
        }

        public int AddItems(List<ItemDTO> items)
        {
            var dbEntityItems = _mapper.Map<List<ItemsManagementDataAccess.Models.Item>>(items);
            return _itemRepository.AddItems(dbEntityItems);
        }

        public bool UpdateItemById(int id, ItemDTO item)
        {
            ItemsManagementDataAccess.Models.Item dbItem = _itemRepository.GetItemById(id);
            if(dbItem == null)
            {
                return false;
            }

            var dbEntityItem = _mapper.Map<ItemsManagementDataAccess.Models.Item>(item);
            dbEntityItem.Id = id;
            return _itemRepository.UpdateItemById(dbEntityItem);
        }

        public bool DeleteItemById(int id)
        {
            ItemsManagementDataAccess.Models.Item dbItem = _itemRepository.GetItemById(id);
            if (dbItem == null)
            {
                return false;
            }
            return _itemRepository.DeleteItemById(dbItem);
        }
    }
}
