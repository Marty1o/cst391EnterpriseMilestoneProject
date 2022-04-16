using Catalog.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Catalog.Entities;
using System;
using System.Linq;
using Catalog.Dtos;

namespace Catalog.Controllers
{
    [ApiController]
    [Route("items")]
    public class Itemscontroller : ControllerBase
    {
        private readonly IItemsRepository repository;

        public Itemscontroller(IItemsRepository repository)
        {
            this.repository = repository;
        }

        // GET via /items this will give you the entire list
        [HttpGet]
        public IEnumerable<ItemDto> GetItems()
        {
            // here we are also converting the items into item DTos using the method in Extensions
            var items = repository.GetItems().Select(item => item.AsDto());

            return items;

        }

        // GET via /items/{id}   this will give you the single item request
        [HttpGet("{id}")]
        public ActionResult<ItemDto> GetItem(Guid id)
        {
            // here the item is not conversted as DTO yet since we want to check if it is null below
            var item = repository.GetItem(id);

            // sends a response when Item is null-not found
            if (item is null)
            {
                return NotFound();
            }

            // finally item is converted to item DTO before it is returned
            return item.AsDto();
        }

        // Post via /items    this will pass the info to create the new item
        [HttpPost]
        public ActionResult<ItemDto> CreateItem(CreateItemDto itemDto)
        {
            Item item = new()
            {
                Id = Guid.NewGuid(),
                Name = itemDto.Name,
                Price = itemDto.Price,
                Color = itemDto.Color,
                Size = itemDto.Size,
                Description = itemDto.Description

            };

            repository.CreateItem(item);

            return CreatedAtAction(nameof(GetItem), new { id = item.Id }, item.AsDto());
        }

        // PUT via /items/{id}
        [HttpPut("{id}")] // lets the put know it needs an ID since this is an item udpate, else use item create/POST
        public ActionResult UpdateItem(Guid id, UpdateItemDto itemDto) // unlike above here we are also taking in item ID as a value
        {
            // item must fist be found
            var existingItem = repository.GetItem(id);

            // need to verify if this item really exist
            if (existingItem is null)
            {
                return NotFound();
            }

            // if item is found and passes test above we must now create a new item that will replace the item found
            Item updatedItem = existingItem with // the WITH expression here tell the program we are making a copy of the item with the following changes below
            {
                Name = itemDto.Name,
                Price = itemDto.Price,
                Color = itemDto.Color,
                Size = itemDto.Size,
                Description = itemDto.Description
        };
            // this will udpate the exist item with the updated version
            repository.UpdateItem(updatedItem);
            // no item is returned since its just an update and item is created or deleted
            return NoContent();
        }

        // DELETE via /items/{id}
        [HttpDelete("{id}")]
        public ActionResult DeleteItem(Guid id)
        {
            // verify the item you wish to delete exist
            var existingItem = repository.GetItem(id);

            //if not send error not found
            if (existingItem is null)
            {
                return NotFound();
            }

            //delete item from repo list
            repository.DeleteItem(id);

            // item is just being deleted so nothing need to be returned
            return NoContent();
        }
    }
}