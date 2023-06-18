using System.ComponentModel.DataAnnotations;

namespace ModernRecrut.Postulations.API.Models
{
    public class Note
    {
        [Key]
        public int Id { get; set; }

        public string NotePostulation { get; set; }

        public string NomEmeteur { get; set; }
    }
}
