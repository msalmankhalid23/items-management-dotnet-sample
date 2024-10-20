using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ItemsManagementBusinessLayer.DTO;

namespace ItemsManagementBusinessLayer.Mapping
{
    public class ItemProfile : Profile
    {
        public ItemProfile()
        {
            CreateMap<ItemsManagementDataAccess.Models.Item, ItemDTO>().ReverseMap();
            //CreateMap<ItemsManagementDataAccess.Models.Item, WebAPI.Models.Request.Item>().ReverseMap();
        }
    }
}
