using Xunit;
using Moq;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ModernRecrut.MVC.Controllers;
using ModernRecrut.MVC.Interfaces;
using ModernRecrut.MVC.Models;
using System.Reflection.Metadata;
using Microsoft.AspNetCore.Hosting;
using FluentAssertions;
using AutoFixture;
using Microsoft.AspNetCore.Routing;
using ModernRecrut.MVC.Helpers;

namespace ModernRecrut.MVC.UnitTests.Controllers
{
    public class PostulationsControllerTests
    {
        [Fact]
        public async Task Edit_IdInvalide_RetourneBadRequest()
        {
            // Arrange
            int id = 1;
            Fixture fixture = new Fixture();
            Postulation postulation = fixture.Create<Postulation>();
            postulation.Id = id;

            Mock<IGestionEmploisService> gestionEmploisServiceMock = new Mock<IGestionEmploisService>();
            gestionEmploisServiceMock.Setup(s => s.Modifier(It.IsAny<OffreEmploi>()));

            Mock<IGestionDocumentsService> gestionDocumentsServiceMock = new Mock<IGestionDocumentsService>();
            gestionDocumentsServiceMock.Setup(s => s.Ajouter(It.IsAny<Fichier>()));

            Mock<IGestionPostulationsService> gestionPostulationsServiceMock = new Mock<IGestionPostulationsService>();
            gestionPostulationsServiceMock.Setup(s => s.Modifier(It.IsAny<Postulation>())).Returns(Task.CompletedTask);

            var loggerMock = new Mock<ILogger<OffreEmploisController>>();

            Mock<IWebHostEnvironment> envMock = new Mock<IWebHostEnvironment>();

            var postulationsController = new PostulationController
                (
                    gestionEmploisServiceMock.Object,
                    gestionDocumentsServiceMock.Object,
                    gestionPostulationsServiceMock.Object,
                    loggerMock.Object,
                    envMock.Object
                 );

            // Act
            var result = await postulationsController.Edit(id, postulation) as BadRequestResult;

            // Assert
            result.Should().NotBeNull();
        }

        [Fact]
        public async Task Edit_ModelValide_ModificationLogs()
        {
            // Arrange
            int id = 1;
            Fixture fixture = new Fixture();
            Postulation postulation = fixture.Create<Postulation>();
            postulation.Id = id;

            Mock<IGestionEmploisService> gestionEmploisServiceMock = new Mock<IGestionEmploisService>();
            gestionEmploisServiceMock.Setup(s => s.Modifier(It.IsAny<OffreEmploi>()));

            Mock<IGestionDocumentsService> gestionDocumentsServiceMock = new Mock<IGestionDocumentsService>();
            gestionDocumentsServiceMock.Setup(s => s.Ajouter(It.IsAny<Fichier>()));

            Mock<IGestionPostulationsService> gestionPostulationsServiceMock = new Mock<IGestionPostulationsService>();
            gestionPostulationsServiceMock.Setup(s => s.Modifier(It.IsAny<Postulation>())).Returns(Task.CompletedTask);

            var loggerMock = new Mock<ILogger<OffreEmploisController>>();

            Mock<IWebHostEnvironment> envMock = new Mock<IWebHostEnvironment>();

            var postulationsController = new PostulationController
                (
                    gestionEmploisServiceMock.Object,
                    gestionDocumentsServiceMock.Object,
                    gestionPostulationsServiceMock.Object,
                    loggerMock.Object,
                    envMock.Object
                 );

            // Act
            var result = await postulationsController.Edit(id, postulation) as RedirectToActionResult;

            // Assert
            loggerMock.Verify(
                x => x.LogInformation(
                    CustomLogEvents.Modication,
                    $"Modification de la postulation {postulation.Id}"
                ),
                Times.Once
            );
        }

        [Fact]
        public async Task Edit_IdEtModelValides_RetourneRedirectToActionResult()
        {
            // Arrange
            int id = 1;
            Fixture fixture = new Fixture();
            Postulation postulation = fixture.Create<Postulation>();
            postulation.Id = id;

            Mock<IGestionEmploisService> gestionEmploisServiceMock = new Mock<IGestionEmploisService>();
            gestionEmploisServiceMock.Setup(s => s.Modifier(It.IsAny<OffreEmploi>()));

            Mock<IGestionDocumentsService> gestionDocumentsServiceMock = new Mock<IGestionDocumentsService>();
            gestionDocumentsServiceMock.Setup(s => s.Ajouter(It.IsAny<Fichier>()));

            Mock<IGestionPostulationsService> gestionPostulationsServiceMock = new Mock<IGestionPostulationsService>();
            gestionPostulationsServiceMock.Setup(s => s.Modifier(It.IsAny<Postulation>())).Returns(Task.CompletedTask);

            var loggerMock = new Mock<ILogger<OffreEmploisController>>();

            Mock<IWebHostEnvironment> envMock = new Mock<IWebHostEnvironment>();

            var postulationsController = new PostulationController
                (
                    gestionEmploisServiceMock.Object,
                    gestionDocumentsServiceMock.Object,
                    gestionPostulationsServiceMock.Object,
                    loggerMock.Object,
                    envMock.Object
                 );

            // Act
            var redirectToActionResult = await postulationsController.Edit(id, postulation) as RedirectToActionResult;

            // Assert
            redirectToActionResult.Should().NotBeNull();
            redirectToActionResult.ActionName.Should().Be("Index");

            gestionPostulationsServiceMock.Verify(p => p.Modifier(It.IsAny<Postulation>()), Times.Once);
        }

        [Fact]
        public async Task Edit_ModelInvalide_RetourneViewResult()
        {
            // Arrange
            int id = 1;
            Fixture fixture = new Fixture();
            Postulation postulation = fixture.Create<Postulation>();
            postulation.Id = id;

            Mock<IGestionEmploisService> gestionEmploisServiceMock = new Mock<IGestionEmploisService>();
            gestionEmploisServiceMock.Setup(s => s.Modifier(It.IsAny<OffreEmploi>()));

            Mock<IGestionDocumentsService> gestionDocumentsServiceMock = new Mock<IGestionDocumentsService>();
            gestionDocumentsServiceMock.Setup(s => s.Ajouter(It.IsAny<Fichier>()));

            Mock<IGestionPostulationsService> gestionPostulationsServiceMock = new Mock<IGestionPostulationsService>();
            gestionPostulationsServiceMock.Setup(s => s.Modifier(It.IsAny<Postulation>())).Returns(Task.CompletedTask);

            var loggerMock = new Mock<ILogger<OffreEmploisController>>();

            Mock<IWebHostEnvironment> envMock = new Mock<IWebHostEnvironment>();

            var postulationsController = new PostulationController
                (
                    gestionEmploisServiceMock.Object,
                    gestionDocumentsServiceMock.Object,
                    gestionPostulationsServiceMock.Object,
                    loggerMock.Object,
                    envMock.Object
                 );

            postulationsController.ModelState.AddModelError("PropertyName", "Erreur de validation");

            // Act
            var viewResult = await postulationsController.Edit(id, postulation) as ViewResult;

            // Assert
            viewResult.Should().NotBeNull();
            var postulationResult = viewResult.Model as Postulation;
            postulationResult.Should().NotBeNull().And.Be(postulation);            
        }
    }
}
