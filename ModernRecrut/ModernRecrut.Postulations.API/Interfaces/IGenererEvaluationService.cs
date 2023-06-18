using ModernRecrut.Postulations.API.Models;
using System.Runtime.CompilerServices;

namespace ModernRecrut.Postulations.API.Interfaces
{
    public interface IGenererEvaluationService
    {
        Note GenererEvaluation(decimal pretentionSalariale);
    }
}
