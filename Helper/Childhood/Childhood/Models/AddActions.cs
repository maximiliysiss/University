using Childhood.Extensions.Attributes;

namespace Childhood.Models
{
    /// <summary>
    /// Дополнительные занятия
    /// </summary>
    public class AddActions
    {
        [HideColumn]
        public int ID { get; set; }
        [DisplayGridName("Название")]
        public string Name { get; set; }

        public override string ToString() => Name;
    }
}
