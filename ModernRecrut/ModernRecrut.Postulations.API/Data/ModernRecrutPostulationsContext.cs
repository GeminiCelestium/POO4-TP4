using Microsoft.EntityFrameworkCore;
using ModernRecrut.Postulations.API.Models;

namespace ModernRecrut.Postulations.API.Data
{
    public class ModernRecrutPostulationsContext : DbContext
    {
        public ModernRecrutPostulationsContext(DbContextOptions<ModernRecrutPostulationsContext> options) : base(options)
        {

        }

        public DbSet<Postulation> Postulation { get; set; }
    }
}
