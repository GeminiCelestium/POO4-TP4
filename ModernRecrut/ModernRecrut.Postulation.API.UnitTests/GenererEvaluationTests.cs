using FluentAssertions;
using ModernRecrut.Postulations.API.Interfaces;
using ModernRecrut.Postulations.API.Models;
using ModernRecrut.Postulations.API.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModernRecrut.Postulation.API.UnitTests
{   
    public class GenererEvaluationTests
    {
        [Fact]
        public void GenererEvaluation_PretSalInf20000_RetourneNotePostSalInferieur()
        {
            // Arrange
            decimal pretentionSalariale = 19999;
            Note noteAttendue = new Note
            {
                NotePostulation = "Salaire inférieur à la norme",
                NomEmeteur = "ApplicationPostulation"
            };

            Mock<IGenererEvaluationService> mockGenererEvaluationService = new Mock<IGenererEvaluationService>();
            mockGenererEvaluationService.Setup(e => e.GenererEvaluation(pretentionSalariale)).Returns(noteAttendue);

            // Act
            Note noteActuelle = mockGenererEvaluationService.Object.GenererEvaluation(pretentionSalariale);

            // Assert
            noteActuelle.NotePostulation.Should().Be(noteAttendue.NotePostulation);
            noteActuelle.NomEmeteur.Should().Be(noteActuelle.NomEmeteur);

            //Assert.Equal(noteAttendue.NotePostulation, noteActuelle.NotePostulation);
            //Assert.Equal(noteAttendue.NomEmeteur, noteActuelle.NomEmeteur);
        }

        [Fact]
        public void GenererEvaluation_PretSalEntre20000Et39999_RetourneNotePostSalProcheMaisInferieur()
        {
            // Arrange
            decimal pretentionSalariale = 39999;
            Note noteAttendue = new Note
            {
                NotePostulation = "Salaire proche mais inférieur à la norme",
                NomEmeteur = "ApplicationPostulation"
            };

            Mock<IGenererEvaluationService> mockGenererEvaluationService = new Mock<IGenererEvaluationService>();
            mockGenererEvaluationService.Setup(e => e.GenererEvaluation(pretentionSalariale)).Returns(noteAttendue);

            // Act
            Note noteActuelle = mockGenererEvaluationService.Object.GenererEvaluation(pretentionSalariale);

            // Assert
            noteActuelle.NotePostulation.Should().Be(noteAttendue.NotePostulation);
            noteActuelle.NomEmeteur.Should().Be(noteActuelle.NomEmeteur);

            //Assert.Equal(noteAttendue.NotePostulation, noteActuelle.NotePostulation);
            //Assert.Equal(noteAttendue.NomEmeteur, noteActuelle.NomEmeteur);
        }

        [Fact]
        public void GenererEvaluation_PretSalEntre40000Et79999_RetourneNotePostSalDansNorme()
        {
            // Arrange
            decimal pretentionSalariale = 79999;
            Note noteAttendue = new Note
            {
                NotePostulation = "Salaire dans la norme",
                NomEmeteur = "ApplicationPostulation"
            };

            Mock<IGenererEvaluationService> mockGenererEvaluationService = new Mock<IGenererEvaluationService>();
            mockGenererEvaluationService.Setup(e => e.GenererEvaluation(pretentionSalariale)).Returns(noteAttendue);

            // Act
            Note noteActuelle = mockGenererEvaluationService.Object.GenererEvaluation(pretentionSalariale);

            // Assert
            noteActuelle.NotePostulation.Should().Be(noteAttendue.NotePostulation);
            noteActuelle.NomEmeteur.Should().Be(noteActuelle.NomEmeteur);

            //Assert.Equal(noteAttendue.NotePostulation, noteActuelle.NotePostulation);
            //Assert.Equal(noteAttendue.NomEmeteur, noteActuelle.NomEmeteur);
        }

        [Fact]
        public void GenererEvaluation_PretSalEntre80000Et99999_RetourneNotePostSalProcheMaisSuperieur()
        {
            // Arrange
            decimal pretentionSalariale = 99999;
            Note noteAttendue = new Note
            {
                NotePostulation = "Salaire proche mais supérieur à la norme",
                NomEmeteur = "ApplicationPostulation"
            };

            var mockGenererEvaluationService = new Mock<IGenererEvaluationService>();
            mockGenererEvaluationService.Setup(e => e.GenererEvaluation(pretentionSalariale)).Returns(noteAttendue);

            // Act
            Note noteActuelle = mockGenererEvaluationService.Object.GenererEvaluation(pretentionSalariale);

            // Assert
            noteActuelle.NotePostulation.Should().Be(noteAttendue.NotePostulation);
            noteActuelle.NomEmeteur.Should().Be(noteActuelle.NomEmeteur);

            //Assert.Equal(noteAttendue.NotePostulation, noteActuelle.NotePostulation);
            //Assert.Equal(noteAttendue.NomEmeteur, noteActuelle.NomEmeteur);
        }

        [Fact]
        public void GenererEvaluation_PretSalSup99999_RetourneNotePostSalSuperieur()
        {
            // Arrange
            decimal pretentionSalariale = 100000;
            Note noteAttendue = new Note
            {
                NotePostulation = "Salaire supérieur à la norme",
                NomEmeteur = "ApplicationPostulation"
            };

            Mock<IGenererEvaluationService> mockGenererEvaluationService = new Mock<IGenererEvaluationService>();
            mockGenererEvaluationService.Setup(e => e.GenererEvaluation(pretentionSalariale)).Returns(noteAttendue);

            // Act
            Note noteActuelle = mockGenererEvaluationService.Object.GenererEvaluation(pretentionSalariale);

            // Assert
            noteActuelle.NotePostulation.Should().Be(noteAttendue.NotePostulation);
            noteActuelle.NomEmeteur.Should().Be(noteActuelle.NomEmeteur);

            //Assert.Equal(noteAttendue.NotePostulation, noteActuelle.NotePostulation);
            //Assert.Equal(noteAttendue.NomEmeteur, noteActuelle.NomEmeteur);
        }
    }
}
