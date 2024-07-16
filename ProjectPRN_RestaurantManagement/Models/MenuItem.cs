using System;
using System.Collections.Generic;

namespace ProjectPRN_RestaurantManagement.Models
{
    public partial class MenuItem
    {
        public MenuItem()
        {
            CartItems = new HashSet<CartItem>();
            OrderDetails = new HashSet<OrderDetail>();
        }

        public int MenuItemId { get; set; }
        public int? MenuId { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public decimal Price { get; set; }

        public virtual Menu? Menu { get; set; }
        public virtual ICollection<CartItem> CartItems { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
