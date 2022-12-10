namespace DBRepo
{
    public class Order
    {
        public int OrderId { get; set; }
        public double Total { get; set; }

        public string? Customer { get; set; }
        public bool IsCompleted { get; set; }

    }
}
