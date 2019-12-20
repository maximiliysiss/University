using Childhood.Extensions.Attributes;

namespace Childhood.Models
{
    public class AddActions
    {
        [HideColumn]
        public int ID { get; set; }
        [DisplayGridName("Название")]
        public string Name { get; set; }

        public override string ToString() => Name;
    }
}
