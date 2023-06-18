using ModernRecrut.Documents.API.Interfaces;
using ModernRecrut.Documents.API.Models;

namespace ModernRecrut.Documents.API.Services
{
    public class GestionFichiers : IGestionFichiers
    {
        private  readonly IWebHostEnvironment _env;

        private string _directoryPath;

        public GestionFichiers(IWebHostEnvironment env)
        {
            _env = env;
            _directoryPath = Path.Combine(_env.ContentRootPath, "wwwroot\\documents");
        }

        public async Task<string> EnregistrerFichier(Fichier fichier)
        {
            string nomFichier = "";

            string codeUtilisateur = fichier.Id;

            byte[] bytes = Convert.FromBase64String(fichier.DataFile);

            using (MemoryStream stream = new MemoryStream(bytes))
            {
                
                IFormFile file = new FormFile(stream, 0, bytes.Length, fichier.Name, fichier.FileName);

         
                Dictionary<int, string> mesDocuments = new Dictionary<int, string>();
                //On genere un numéro aléatoire 
                string numAleatoire = Guid.NewGuid().ToString();

                string extention = Path.GetExtension(fichier.FileName);
                // on prepare le nouveau nom du fichier 
                nomFichier = codeUtilisateur + "_" + fichier.TypeDocument + "_" + numAleatoire + $"{extention}";

                //Chemin du fichier avec son nouveau nom
                string documentPath = Path.Combine(_directoryPath, nomFichier);

                using (FileStream fileStream = new FileStream(documentPath, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }
               
           }

            return nomFichier;
        }

        public List<string> ObtenirNomFichiersSelonId(string id)
        {
            var fichiersAvecChemin = Directory.GetFiles(_directoryPath, id + "*");
            var fichierSansChemin = new List<string>();
            foreach (string fichier in fichiersAvecChemin)
            {
                fichierSansChemin.Add(Path.GetFileName(fichier));
            }
            return fichierSansChemin;
        }
    }
}
