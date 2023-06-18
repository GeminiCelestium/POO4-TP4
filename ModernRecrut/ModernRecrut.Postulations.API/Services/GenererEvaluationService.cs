using ModernRecrut.Postulations.API.Interfaces;
using ModernRecrut.Postulations.API.Models;

namespace ModernRecrut.Postulations.API.Services
{
    public class GenererEvaluationService : IGenererEvaluationService
    {
        public Note GenererEvaluation(decimal pretentionSalariale)
        {
            string notePostulation = "";

            if (pretentionSalariale < 20000)
            {
                notePostulation = "Salaire inférieur à la norme";
            }
            else if (pretentionSalariale <= 39999)
            {
                notePostulation = "Salaire proche mais inférieur à la norme";
            }
            else if (pretentionSalariale <= 79999)
            {
                notePostulation = "Salaire dans la norme";
            }
            else if (pretentionSalariale <= 99999)
            {
                notePostulation = "Salaire proche mais supérieur à la norme";
            }
            else
            {
                notePostulation = "Salaire supérieur à la norme";
            }

            Note note = new Note()
            {
                NotePostulation = notePostulation,
                NomEmeteur = "ApplicationPostulation"
            };

            return note;
        }
    }
}
