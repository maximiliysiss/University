namespace SchoolService.Models
{
    public class Lesson
    {
        public int ID { get; set; }
        public string Name { get; set; }
    }

    public class LessonProfile
    {
        public int ID { get; set; }
        public int LessonId { get; set; }
        public virtual Lesson Lesson { get; set; }
        public int TeacherId { get; set; }
        public virtual Teacher Teacher { get; set; }
    }
}