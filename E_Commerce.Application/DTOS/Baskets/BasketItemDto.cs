using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.DTOS.Baskets
{
    public class BasketItemDto
    {
        [Required(ErrorMessage = "Product ID Must Be Required")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Product ID Must Be Required")]
        public string ProductName { get; set; } = default!;

        public string PictureUrl { get; set; } = default!;

        [Range(1, double.MaxValue, ErrorMessage = "Price must be greater than 0")]
        public decimal Price { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Quantity must at Least 1")]
        public int Quantity { get; set; }
    }
}
