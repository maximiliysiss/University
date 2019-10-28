using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolService.Models
{
    public class Schedule
    {
        public int ID { get; set; }
        public int ClassId { get; set; }
        public virtual Class Class { get; set; }
        public int LessonNumber { get; set; }
        public int LessonId { get; set; }
        public virtual Lesson Lesson { get; set; }
        public int TeacherId { get; set; }
        public virtual Teacher Teacher { get; set; }
        public DayOfWeek DayOfWeek { get; set; }
        public bool IsFacultative { get; set; }
    }
}
