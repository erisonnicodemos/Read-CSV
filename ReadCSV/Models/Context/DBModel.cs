using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace ReadCSV.Models.Context
{
    public class DBModel : DbContext
    {
        public DBModel()
              : base("name=DBModel")
        {
           
        }
        public DbSet<Customers> Customers { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            base.OnModelCreating(modelBuilder);

        }
    }
}