namespace SchoolService.Models
{
    public class Mark
    {
        public int ID { get; set; }
        public short MarkReal { get; set; }
        public int TeacherId { get; set; }
        public virtual Teacher Teacher { get; set; }
        public int ChildId { get; set; }
        public virtual Child Child { get; set; }
        public int ScheduleId { get; set; }
        public virtual Schedule Schedule { get; set; }
    }
}