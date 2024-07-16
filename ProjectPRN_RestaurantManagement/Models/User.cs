using System;
using System.Collections.Generic;

namespace ProjectPRN_RestaurantManagement.Models
{
    public partial class User
    {
        public User()
        {
            Carts = new HashSet<Cart>();
            Orders = new HashSet<Order>();
            Reservations = new HashSet<Reservation>();
        }

        public int UserId { get; set; }
        public int? RoleId { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;
        public string? Phone { get; set; }
        public string? Address { get; set; }

        public virtual Role? Role { get; set; }
        public virtual ICollection<Cart> Carts { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<Reservation> Reservations { get; set; }
    }
}
