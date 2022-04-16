using Catalog.Dtos;
using Catalog.Entities;

namespace Catalog
{
    // this method extends the definition of one type by added a method that can be executed in that type
    public static class Extensions{
        public static ItemDto AsDto(this Item item) // here you get the item and then return it as a itemDto type
        {
            return new ItemDto
            {
                Id = item.Id,
                Name = item.Name,
                Price = item.Price,
                Color = item.Color,
                Size = item.Size,
                Description = item.Description
            };
        }
    }
}