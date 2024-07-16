using System;
using System.Collections.Generic;

namespace ProjectPRN_RestaurantManagement.Models
{
    public partial class CartItem
    {
        public int CartItemId { get; set; }
        public int? CartId { get; set; }
        public int? MenuItemId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }

        public virtual Cart? Cart { get; set; }
        public virtual MenuItem? MenuItem { get; set; }
    }
}
