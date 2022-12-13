namespace DBRepo
{
    public class Order
    {
        public int OrderId { get; set; }
        public double Total { get; set; }

        public string? Customer { get; set; }
        public bool IsCompleted { get; set; }

        public string FullName { get; set; }
        public string Address { get; set; }
        public string Phone {get; set; }

    }
}
