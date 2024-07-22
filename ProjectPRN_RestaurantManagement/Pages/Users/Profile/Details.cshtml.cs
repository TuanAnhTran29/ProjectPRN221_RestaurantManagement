using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ProjectPRN_RestaurantManagement.Models;

namespace ProjectPRN_RestaurantManagement.Pages.Users.Profile
{
    public class DetailsModel : PageModel
    {
        private readonly ProjectPRN_RestaurantManagement.Models.RestaurantManagementContext _context;

        public DetailsModel(ProjectPRN_RestaurantManagement.Models.RestaurantManagementContext context)
        {
            _context = context;
        }

        public Models.User User { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            User = await _context.Users
                .Include(u => u.Role).FirstOrDefaultAsync(m => m.UserId == id);

            if (User == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
