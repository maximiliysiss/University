namespace SchoolService.Models
{
    public class Mark
    {
        public int ID { get; set; }
        public short MarkReal { get; set; }
        public virtual Teacher Teacher { get; set; }
        public virtual Child Child { get; set; }
        public virtual Schedule Schedule { get; set; }
    }
}