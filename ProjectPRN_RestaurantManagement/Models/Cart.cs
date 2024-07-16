using System;
using System.Collections.Generic;

namespace ProjectPRN_RestaurantManagement.Models
{
    public partial class Cart
    {
        public Cart()
        {
            CartItems = new HashSet<CartItem>();
        }

        public int CartId { get; set; }
        public int? CustomerId { get; set; }
        public DateTime CreatedDate { get; set; }

        public virtual User? Customer { get; set; }
        public virtual ICollection<CartItem> CartItems { get; set; }
    }
}
