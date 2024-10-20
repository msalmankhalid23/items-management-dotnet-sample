using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ItemsManagementWebApp.Models
{
    public class Item
    {
        /// <summary>
        /// Item Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Item Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Item Description
        /// </summary>
        public string Description { get; set; }
    }
}