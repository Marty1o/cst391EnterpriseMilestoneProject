using System;

namespace Catalog.Entities
{
    public record Item
    {
        public Guid Id { get; init; }
        public string Name { get; init; }
        public decimal Price { get; init; }
        public string Color { get; init; }
        public string Size { get; init; }
        public string Description { get; init; }
    }

}