﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using static System.Net.Mime.MediaTypeNames;

namespace ModernRecrut.Documents.API.Models
{
    public enum TypeDocument
    {
        Cv,
        LettreDeMotivation,
        Diplome

    }
    public class Fichier
    {
        public string? Id { get; set; }
        public string? DataFile { get; set; }
        public string? Name { get; set; }
        public string? FileName { get; set; }
        public TypeDocument? TypeDocument { get; set; }
        public IFormFile? DataFormFile { get; set; }
    }
}
