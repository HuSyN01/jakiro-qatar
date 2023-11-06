namespace JAKIRO_QATAR.Models
{

    public class Cart
    {
        public enum OrderStatus
        {
            Pending,
            Processing,
            Shipped,
            Delivered,
            Cancelled,
        }

        public int Id { get; set; }
        public DateTime DateTime { get; set; }
        public decimal TotalPrice { get; set; }
        public OrderStatus Status { get; set; }
        public List<CartItem> CartDetails { get; set; }
    }
}
