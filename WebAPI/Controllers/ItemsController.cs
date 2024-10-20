using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ItemsManagementBusinessLayer.DTO;
using ItemsManagementBusinessLayer.Services;
using WebAPI.Models;
using WebAPI.Models.Request;
using System;
using System.Collections.Generic;

namespace WebAPI.Controllers
{
    /// <summary>
    /// Controller for managing items.
    /// </summary>
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ItemsController : ControllerBase
    {
        private readonly IItemService _itemService;
        private readonly ILogger<ItemsController> _logger;
        private readonly IMapper _mapper;

        public ItemsController(ILogger<ItemsController> logger, IItemService itemService, IMapper mapper)
        {
            _logger = logger;
            _itemService = itemService;
            _mapper = mapper;
        }

        /// <summary>
        /// Gets a list of items.
        /// </summary>
        /// <returns>The list of items.</returns>
        [HttpGet("")]
        [ProducesResponseType(200, Type = typeof(List<ItemDTO>))]
        [ProducesResponseType(400, Type = typeof(string))]
        public ActionResult<List<ItemDTO>> GetItems()
        {
            try
            {
                var items = _itemService.GetItems();
                return items;
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception occurred", ex);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Gets an item by ID.
        /// </summary>
        /// <param name="id">The ID of the item.</param>
        /// <returns>The item with the specified ID.</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(ItemDTO))]
        [ProducesResponseType(400, Type = typeof(string))]
        [ProducesResponseType(404)]
        public ActionResult<ItemDTO> GetItem(int id)
        {
            try
            {
                if (id < 0)
                {
                    _logger.LogError("Input has a negative ID");
                    return BadRequest("Invalid ID");
                }

                var item = _itemService.GetItemById(id);
                if (item != null)
                {
                    return item;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception occurred", ex);
                return BadRequest(ex.Message);
            }

            return NotFound();
        }

        /// <summary>
        /// Adds a list of items.
        /// </summary>
        /// <param name="items">The list of items to add.</param>
        /// <returns>The number of items added.</returns>
        [HttpPost("")]
        [ProducesResponseType(200, Type = typeof(int))]
        [ProducesResponseType(404)]
        public ActionResult<int> AddItems([FromBody] List<Item> items)
        {
            if (items?.Count > 0)
            {
                var itemsDto = _mapper.Map<List<ItemDTO>>(items);
                _logger.LogInformation("Items Added");
                return _itemService.AddItems(itemsDto);
            }
            else
            {
                _logger.LogWarning("Provided items count is zero");
            }

            return NotFound();
        }

        /// <summary>
        /// Updates an item by ID.
        /// </summary>
        /// <param name="id">The ID of the item to update.</param>
        /// <param name="item">The updated item.</param>
        /// <returns>No content if the item is updated successfully.</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400, Type = typeof(string))]
        [ProducesResponseType(404)]
        public IActionResult UpdateItem(int id, [FromBody] Item item)
        {
            try
            {
                if (id < 0)
                {
                    _logger.LogError("Input has a negative ID");
                    return BadRequest("Invalid ID");
                }

                var itemDto = _mapper.Map<ItemDTO>(item);
                bool isUpdated = _itemService.UpdateItemById(id, itemDto);
                if (isUpdated)
                {
                    _logger.LogInformation($"Item {id} is updated");
                    return NoContent();
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Failed to update item {id}. {e}");
                return BadRequest(e.Message);
            }

            return NotFound();
        }

        /// <summary>
        /// Deletes an item by ID.
        /// </summary>
        /// <param name="id">The ID of the item to delete.</param>
        /// <returns>No content if the item is deleted successfully.</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400, Type = typeof(string))]
        [ProducesResponseType(404)]
        public IActionResult DeleteItem(int id)
        {
            try
            {
                if (id < 0)
                {
                    _logger.LogError("Input has a negative ID");
                    return BadRequest("Invalid ID");
                }

                bool isDeleted = _itemService.DeleteItemById(id);
                if (isDeleted)
                {
                    _logger.LogInformation($"Item {id} is deleted");
                    return NoContent();
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Failed to delete item {id}. {e}");
                return BadRequest(e.Message);
            }

            return NotFound();
        }
    }
}
