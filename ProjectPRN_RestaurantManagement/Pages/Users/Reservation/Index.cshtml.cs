using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ProjectPRN_RestaurantManagement.Models;

namespace ProjectPRN_RestaurantManagement.Pages.Users.Reservation
{
    public class IndexModel : PageModel
    {
        private readonly ProjectPRN_RestaurantManagement.Models.RestaurantManagementContext _context;

        public IndexModel(ProjectPRN_RestaurantManagement.Models.RestaurantManagementContext context)
        {
            _context = context;
        }

        public List<Restaurant> Restaurants { get; set; }

        public async Task OnGetAsync()
        {
            Restaurants = await _context.Restaurants.Distinct().ToListAsync();
        }
    }
}
