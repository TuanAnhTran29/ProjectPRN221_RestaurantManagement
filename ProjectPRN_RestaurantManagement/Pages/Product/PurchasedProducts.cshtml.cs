using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ProjectPRN_RestaurantManagement.Models;
using ProjectPRN_RestaurantManagement.Pages.Product;

namespace ProjectPRN_RestaurantManagement.Pages.Manager.Product
{
    public class PurchasedProductsModel : PageModel
    {
        private readonly RestaurantManagementContext context;

        public PurchasedProductsModel(RestaurantManagementContext _context)
        {
            context = _context;
        }
        public void OnGet(int? orderId)
        {
            List<OrderDetail> purchasedProducts = context.OrderDetails
                .Include(m => m.MenuItem)
                .Include(m => m.Order)
                .ThenInclude(m => m.Restaurant)
                .ToList();

            List<Order> orderedList = context.Orders.Include(o => o.Restaurant)
                .Include(o => o.OrderDetails).ThenInclude(o => o.MenuItem).ToList();

            List<OrderListRespDTO> orderListRespDTOs = new List<OrderListRespDTO>();

            int totalQuantity = 0;
            for (int i = 0; i < orderedList.Count; i++)
            {
                var order = purchasedProducts.FindAll(p => p.OrderId == orderedList[i].OrderId);
                for (int j = 0; j < order.Count; j++)
                {
                    totalQuantity += order[j].Quantity;
                }
                orderListRespDTOs.Add(new OrderListRespDTO(orderedList[i].OrderId, orderedList[i].OrderDate, orderedList[i].Restaurant.Name, totalQuantity, orderedList[i].TotalAmount));
                totalQuantity = 0;
            }
            ViewData["ordered_list"] = orderListRespDTOs;

            if (orderId != null)
            {
                var orderDetails= purchasedProducts.FindAll(p => p.OrderId == orderId);
                ViewData["purchased_list"]= orderDetails;
            }
        }
    }
}
