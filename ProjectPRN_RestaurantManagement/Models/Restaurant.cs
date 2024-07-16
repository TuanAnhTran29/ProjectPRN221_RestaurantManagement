using System;
using System.Collections.Generic;

namespace ProjectPRN_RestaurantManagement.Models
{
    public partial class Restaurant
    {
        public Restaurant()
        {
            Menus = new HashSet<Menu>();
            Orders = new HashSet<Order>();
            Reservations = new HashSet<Reservation>();
            Tables = new HashSet<Table>();
        }

        public int RestaurantId { get; set; }
        public string Name { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string? Email { get; set; }
        public string? OpeningHours { get; set; }

        public virtual ICollection<Menu> Menus { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<Reservation> Reservations { get; set; }
        public virtual ICollection<Table> Tables { get; set; }
    }
}
