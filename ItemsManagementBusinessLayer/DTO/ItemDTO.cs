using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemsManagementBusinessLayer.DTO
{
    public class ItemDTO
    {
        /// <summary>
        /// ItemDTO Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// ItemDTO Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// ItemDTO Description
        /// </summary>
        public string Description { get; set; }
    }
}
