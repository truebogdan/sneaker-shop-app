namespace DBRepo
{
    public class OrderProduct
    {
        public int Id { get; set; }
        public string? ImgUrl { get; set; }
        public string? Description { get; set; }
        public string? Brand { get; set; }

        public string? Price { get; set; }

        public Order? Order { get; set; }
    }
}
