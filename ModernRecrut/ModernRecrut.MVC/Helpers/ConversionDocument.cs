namespace ModernRecrut.MVC.Helpers
{
    public class ConversionDocument
    {
        public static async Task<string> ConvertirDocumentEnString(IFormFile fichier)
        {
            using (var ms = new MemoryStream())
            {
               await fichier.CopyToAsync(ms);
                var fileBytes = ms.ToArray();
                string str = Convert.ToBase64String(fileBytes);
                // act on the Base64 data

                return str;
            }
        }
    }
}
