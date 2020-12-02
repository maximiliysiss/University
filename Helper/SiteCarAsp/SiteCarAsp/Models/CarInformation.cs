namespace SiteCarAsp.Models
{
    public class CarInformation
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Type { get; set; }
        public string Body { get; set; }
        public bool Used { get; set; }
        public string[] Images { get; set; }
    }
}
