namespace ProjectPRN_RestaurantManagement.Pages.Product
{
    public class OrderListRespDTO
    {
        public int orderId;
        public DateTime orderDate;
        public string restaurantName;
        public int totalQuantity;
        public decimal totalAmount;

        public OrderListRespDTO(int id, DateTime date, string name, int totalQuantity, decimal totalAmount)
        {
            orderId = id;
            orderDate = date;
            this.restaurantName = name;
            this.totalQuantity = totalQuantity;
            this.totalAmount = totalAmount;
        }

        
    }
}
