using System.ComponentModel.DataAnnotations;

namespace Catalog.Dtos
{
    public record CreateItemDto
    {
        [Required]
        public string Name { get; init; }
        [Range(1, 100000)]
        public decimal Price { get; init; }
        [Required]
        public string Color { get; init; }
        [Required]
        public string Size { get; init; }
        [Required]
        public string Description { get; init; }
    }
}