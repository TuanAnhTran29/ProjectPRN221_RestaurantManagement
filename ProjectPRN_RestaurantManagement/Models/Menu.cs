using System;
using System.Collections.Generic;

namespace ProjectPRN_RestaurantManagement.Models
{
    public partial class Menu
    {
        public Menu()
        {
            MenuItems = new HashSet<MenuItem>();
        }

        public int MenuId { get; set; }
        public int? RestaurantId { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }

        public virtual Restaurant? Restaurant { get; set; }
        public virtual ICollection<MenuItem> MenuItems { get; set; }
    }
}
