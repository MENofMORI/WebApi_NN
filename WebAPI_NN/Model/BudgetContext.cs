using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI_NN.Model
{
    public class BudgetContext : DbContext
    {
        //INPUT
        public const string Food = "Food";
        public const string Home = "Home";
        public const string Health = "Health";
        public const string Family = "Family";
        public const string Car = "Car";
        public const string Entertainment = "Entertainment";
        public const string Stimulant = "Stimulant";
        public const string Investment_S = "Investment S";
        public const string Investment_R = "Investment R";
        public const string Month_income = "Month income";
        public const string Budget = "Budget";

        public BudgetContext(DbContextOptions<BudgetContext> options) : base(options)
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
                    BuildInData1.Type = Food;
                    BuildInData1.Budgets = null;
                    BudgetTypes.Add(BuildInData1);
                    BudgetType BuildInData2 = new BudgetType();
                    BuildInData2.Id = 2;
                    BuildInData2.Type = Home;
                    BuildInData2.Budgets = null;
                    BudgetTypes.Add(BuildInData2);
                    BudgetType BuildInData3 = new BudgetType();
                    BuildInData3.Id = 3;
                    BuildInData3.Type = Health;
                    BuildInData3.Budgets = null;
                    BudgetTypes.Add(BuildInData3);
                    BudgetType BuildInData4 = new BudgetType();
                    BuildInData4.Id = 4;
                    BuildInData4.Type = Family;
                    BuildInData4.Budgets = null;
                    BudgetTypes.Add(BuildInData4);
                    BudgetType BuildInData5 = new BudgetType();
                    BuildInData5.Id = 5;
                    BuildInData5.Type = Car;
                    BuildInData5.Budgets = null;
                    BudgetTypes.Add(BuildInData5);
                    BudgetType BuildInData6 = new BudgetType();
                    BuildInData6.Id = 6;
                    BuildInData6.Type = Entertainment;
                    BuildInData6.Budgets = null;
                    BudgetTypes.Add(BuildInData6);
                    BudgetType BuildInData7 = new BudgetType();
                    BuildInData7.Id = 7;
                    BuildInData7.Type = Stimulant;
                    BuildInData7.Budgets = null;
                    BudgetTypes.Add(BuildInData7);
                    BudgetType BuildInData8 = new BudgetType();
                    BuildInData8.Id = 8;
                    BuildInData8.Type = Investment_S;
                    BuildInData8.Budgets = null;
                    BudgetTypes.Add(BuildInData8);
                    BudgetType BuildInData9 = new BudgetType();
                    BuildInData9.Id = 9;
                    BuildInData9.Type = Investment_R;
                    BuildInData9.Budgets = null;
                    BudgetTypes.Add(BuildInData9);
                    BudgetType BuildInData10 = new BudgetType();
                    BuildInData10.Id = 10;
                    BuildInData10.Type = Month_income;
                    BuildInData10.Budgets = null;
                    BudgetTypes.Add(BuildInData10);
                    BudgetType BuildInData11 = new BudgetType();
                    BuildInData11.Id = 11;
                    BuildInData11.Type = Budget;
                    BuildInData11.Budgets = null;
                    BudgetTypes.Add(BuildInData11);

                    this.SaveChanges();
                }
        }

        public DbSet<Budget> Budgets { get; set; }
        public DbSet<BudgetType> BudgetTypes { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Budget>()

            //        .HasOne(c => c.Type)
            //        .WithMany(c => c.Budgets)
            //        .OnDelete(DeleteBehavior.NoAction);

            //modelBuilder.Entity<BudgetType>()
            //        .HasMany(c => c.Budgets)
            //        .WithOne(c => c.Type)
            //        .OnDelete(DeleteBehavior.Cascade);
        }//*/
    }
}
