using System.ComponentModel.DataAnnotations;

namespace eCommerce.Models
{
    /// <summary>
    /// Represents an individual bakery item
    /// </summary>
    public class Item
    {
        /// <summary>
        /// Unique identifier for each item
        /// </summary>
        [Key]
        public int ItemId { get; set; }

        /// <summary>
        /// Name of the bakery item
        /// </summary>
        [Required]
        public string Title { get; set; }

        /// <summary>
        /// Explanation of what the bakery item is
        /// </summary>
        [Required]
        public string Description { get; set; }

        /// <summary>
        /// Sales price of the bakery item
        /// </summary>
        [Required]
        [Range(0, double.MaxValue)]
        public double Price { get; set; }
    }
}
