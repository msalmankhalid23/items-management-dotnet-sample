using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ItemsManagementBusinessLayer.DTO;

namespace WebAPI.Mapping
{
    /// <summary>
    /// AutoMapper profile for mapping between different types related to items.
    /// </summary>
    public class ItemProfile : Profile
    {
        /// <summary>
        /// Configures mappings between models.
        /// </summary>
        public ItemProfile()
        {
            CreateMap<ItemsManagementDataAccess.Models.Item, ItemDTO>().ReverseMap();
            CreateMap<WebAPI.Models.Request.Item, ItemDTO>().ReverseMap();
        }
    }
}
