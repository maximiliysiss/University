namespace ChatAPI.Models
{
    /// <summary>
    /// Комната
    /// </summary>
    public class Room
    {
        public int Id { get; set; }
        public string Name { get; set; }
        /// <summary>
        /// Создатель
        /// </summary>
        public string CreatorId { get; set; }
    }
}
