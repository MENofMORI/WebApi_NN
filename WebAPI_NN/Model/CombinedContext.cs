using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI_NN.Model
{
    public class CombinedContext : DbContext
    {

        public CombinedContext(DbContextOptions<CombinedContext> options) : base(options)
        {
            Database.EnsureCreated();

            InitializeBuildInData();
        }

        private void InitializeBuildInData()
        {
            if (BudgetTypes != null)
                if (BudgetTypes.Count() < 1)
                {
                    BudgetType BuildInData1 = new BudgetType();
                    BuildInData1.Id = 1;
                    BuildInData1.Type = "Incomes";
                    BuildInData1.Budgets = null;
                    BudgetTypes.Add(BuildInData1);
                    BudgetType BuildInData2 = new BudgetType();
                    BuildInData2.Id = 2;
                    BuildInData2.Type = "Expanses";
                    BuildInData2.Budgets = null;
                    BudgetTypes.Add(BuildInData2);

                    this.SaveChanges();
                }
        }

        public DbSet<Budget> Budgets { get; set; }
        public DbSet<BudgetType> BudgetTypes { get; set; }
        public DbSet<Content> Contents { get; set; }
        public DbSet<ListContent> ListContents { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Budget>()

                    .HasOne(c => c.Type)
                    .WithMany(c => c.Budgets)
                    .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<BudgetType>()
                    .HasMany(c => c.Budgets)
                    .WithOne(c => c.Type)
                    .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Content>()
                    .HasOne(c => c.ListContent)
                    .WithMany(c => c.Contents)
                    .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<ListContent>()
                    .HasMany(c => c.Contents)
                    .WithOne(c => c.ListContent)
                    .OnDelete(DeleteBehavior.Cascade);
        }//*/
    }
}
