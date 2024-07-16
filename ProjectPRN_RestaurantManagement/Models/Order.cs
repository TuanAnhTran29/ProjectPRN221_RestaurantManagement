using System;
using System.Collections.Generic;

namespace ProjectPRN_RestaurantManagement.Models
{
    public partial class Order
    {
        public Order()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

        public int OrderId { get; set; }
        public int? CustomerId { get; set; }
        public int? RestaurantId { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }

        public virtual User? Customer { get; set; }
        public virtual Restaurant? Restaurant { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
