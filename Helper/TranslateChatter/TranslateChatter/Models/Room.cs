namespace TranslateChatter.Models
{
    public class Room
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string CreatorId { get; set; }
        public virtual User Creator { get; set; }
    }
}
