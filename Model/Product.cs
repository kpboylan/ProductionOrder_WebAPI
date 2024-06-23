namespace ProductionOrder_WebAPI.Model
{
    public class Product
    {
        public int id { get; set; }
        public string description { get; set; }
        public string name { get; set; }
        public string imageName { get; set; }
        public string category { get; set; }
        public double price { get; set; }
        public double discount { get; set; }
    }
}
