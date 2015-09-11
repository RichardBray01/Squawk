namespace Squawk.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class DatabaseContext : DbContext
    {
        public DatabaseContext()
            : base("name=DatabaseContext")
        {
        }

        public virtual DbSet<HistSample> HistSamples { get; set; }
        public virtual DbSet<Host> Hosts { get; set; }
        public virtual DbSet<SampleType> SampleTypes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Host>();

            modelBuilder.Entity<SampleType>();
        }
    }
}
