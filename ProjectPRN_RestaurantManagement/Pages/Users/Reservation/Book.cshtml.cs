using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjectPRN_RestaurantManagement.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectPRN_RestaurantManagement.Pages.Users.Reservation
{
    public class BookModel : PageModel
    {
        private readonly RestaurantManagementContext _context;

        public BookModel(RestaurantManagementContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Models.Reservation Reservation { get; set; }

        [BindProperty(SupportsGet = true)]
        public int RestaurantId { get; set; }

        public SelectList Tables { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            if (RestaurantId > 0)
            {
                var tables = await _context.Tables
                    .Where(t => t.RestaurantId == RestaurantId)
                    .Select(t => new
                    {
                        Value = t.TableId.ToString(),
                        Text = t.TableNumber
                    })
                    .ToListAsync();

                Tables = new SelectList(tables, "Value", "Text");
            }
            else
            {
                Tables = new SelectList(Enumerable.Empty<SelectListItem>(), "Value", "Text");
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                // Load the tables again in case of validation error
                var tables = await _context.Tables
                    .Where(t => t.RestaurantId == Reservation.RestaurantId)
                    .Select(t => new
                    {
                        Value = t.TableId.ToString(),
                        Text = t.TableNumber
                    })
                    .ToListAsync();

                Tables = new SelectList(tables, "Value", "Text");

                return Page();
            }

            // Kiểm tra giá trị ReservationTime
            if (Reservation.ReservationDate < new DateTime(1753, 1, 1) || Reservation.ReservationDate > new DateTime(9999, 12, 31))
            {
                ModelState.AddModelError("Reservation.ReservationTime", "Invalid date. Must be between 1/1/1753 and 12/31/9999.");
                return Page();
            }

            _context.Reservations.Add(Reservation);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
