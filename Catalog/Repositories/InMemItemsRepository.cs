using System.Collections.Generic;
using Catalog.Entities;
using System;
using System.Linq;

namespace Catalog.Repositories
{
    public class InMemItemsRepository : IItemsRepository
    {
        private readonly List<Item> items = new()
        {
            new Item { Id= Guid.NewGuid(), Name = "Shirt", Price = 9, Size = "Large", Color = "White" , Description = "A plain shirt"},
            new Item { Id= Guid.NewGuid(), Name = "Pants", Price = 20, Size = "Medium", Color = "Black" , Description = "A plain pair of pants"},
            new Item { Id= Guid.NewGuid(), Name = "Short", Price = 15, Size = "Small", Color = "Grey" , Description = "A plain pair of shorts"}

        };

        public IEnumerable<Item> GetItems()
        {
            return items;
        }

        // this will get the item and return
        public Item GetItem(Guid id)
        {
            return items.Where(item => item.Id == id).SingleOrDefault();
        }

        // new item created and added to the list
        public void CreateItem(Item item)
        {
            items.Add(item);
        }

        public void UpdateItem(Item item)
        {
            // this will use the Id of the current item to find that item and assign its value to index
            var index = items.FindIndex(existingItem => existingItem.Id == item.Id);
            //uses the index to them place that updated item in the same index overriding the old info
            items[index] = item;
        }

        public void DeleteItem(Guid id)
        {
            // before we can delete the item it must first be found
            var index = items.FindIndex(existingItem => existingItem.Id == id);
            // remove/delete the item at the index found 
            items.RemoveAt(index);
        }
    }
}