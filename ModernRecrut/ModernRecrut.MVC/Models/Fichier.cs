using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ModernRecrut.MVC.Models
{
    public enum TypeDocument
    {
        [Display(Name = "Curriculum Vitae")]
        Cv,
        [Display(Name = "Lettre de motivation")]
        LettreDeMotivation,
        [Display(Name = "Diplôme")]
        Diplome

    }
    public class Fichier
    {
        public string? Id { get; set; }
        public string? DataFile { get; set; }
        public string? Name { get; set; }
        public string? FileName { get; set; }
        [DisplayFormat(NullDisplayText = "Choisir un type pour le document")]
        [Required(ErrorMessage = "Le champ est obligatoire")]
        public TypeDocument? TypeDocument { get; set; }
        [FileExtensions(Extensions = "pdf,docx,doc", ErrorMessage = "Le fichier doit être au format pdf, docx ou doc")]
       
        [Required(ErrorMessage = "Ce champ est obligatoire")]
        public IFormFile? DataFormFile { get; set; }
    }
}
