namespace Market.Models
{
    public class Reciept
    {
        public int Id { get; set; }
        public List<CartItem> cartItems { get; set; }
        public float Total { get; set;}
        public string UserId { get; set; }
        public AppUser User { get; set; }
    }
}
