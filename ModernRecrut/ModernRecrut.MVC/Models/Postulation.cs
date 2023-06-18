using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModernRecrut.MVC.Models
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
