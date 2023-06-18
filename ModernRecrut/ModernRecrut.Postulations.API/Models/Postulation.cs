using ModernRecrut.Postulations.API.Services;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModernRecrut.Postulations.API.Models
{
    public class Postulation
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Id")]
        public string IdCandidat { get; set; }

        [ForeignKey("Id")]
        public int OffreDEmploiID { get; set; }

        public decimal PretentionSalariale { get; set; }

        public DateTime DateDisponibilite { get; set; }        
    }
}
