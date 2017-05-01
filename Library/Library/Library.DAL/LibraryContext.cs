
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using Library.DAL.Common;
using Library.DAL.DatabaseModels;

namespace Library.DAL
{
    public class LibraryContext : DbContext, ILibraryContext
    {
        public LibraryContext() : base("name=LibraryDBConnectionString")
        {
        }
        public DbSet<Korisnik> Korisnik { get; set; }
        public DbSet<Knjiznica> Knjiznica { get; set; }
        public DbSet<Knjiga> Knjiga { get; set; }
        public DbSet<Odjel> Odjel { get; set; }
        public DbSet<PosudenaKnjiga> PosudenaKnjiga { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}
