using Microsoft.EntityFrameworkCore;
using WebServiceCitiesApi.Models;

/*add-migration i PackageManager generates classes for managing the DB
 skapas i en Migrations-folder
 DB utvecklas i takt med C#-koden
update-database i PM bygger DB, efter detta skapas massa filer, 
installera en Extension for att kolla in 'SQLLIte and SQLServer Toolbox' är som SQL Managemment Studio
add-migration och update-database alltid parvis
 */
namespace WebServiceCitiesApi.DBContext

{
    //DBContext ser alltid till att dessa nedan inte är null, fast compilatorn varnar (CS8618 varning),
    //använd null!, null-forgiving operator, den undertrycker kompilatorn
    public class LocalSQLContex : DbContext
    {
        //confas i Program.cs i AddDbContext
        public LocalSQLContex(DbContextOptions<LocalSQLContex> context) : base(context)
        { }

        public DbSet<EFCity> Cities { get; set; } = null!;
        public DbSet<EFPointOfInterest> Interests { get; set; } = null!;


        //Gives access to the modelbuilder, for seeding
        protected override void OnModelCreating(ModelBuilder builder)
        {
            //builder.Entity<EFCity>().HasData
            base.OnModelCreating(builder);


        }

        //this is 1 way, the other is via the constructor
        /* protected override void OnConfiguring(DbContextOptionsBuilder options)
         {
             options.UseSqlServer("connectionstring");  
             base.OnConfiguring(options);
         }*/
    }
}
