using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolService.Models
{
    public enum DocumentType
    {
        Pasport,
        Birth,
        Other
    }

    public class Document
    {
        public int ID { get; set; }
        public string Number { get; set; }
        public DocumentType DocumentType { get; set; }
        public DateTime AddDate { get; set; }
        public string DocumentPhoto { get; set; }
        [NotMapped]
        public string DocumentPhotoUrl => DocumentPhoto;
    }
}