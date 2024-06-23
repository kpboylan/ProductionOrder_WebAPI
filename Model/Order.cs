namespace ProductionOrder_WebAPI.Model
{
    public class Order
    {
        public string? OrderNumber { get; set; }

        public string? Material { get; set; }

        public string? Uom { get; set; }

        public double Quantity { get; set; }

        public string Location { get; set; }

        public string Status { get; set; } = "Scheduled";
    }
}
