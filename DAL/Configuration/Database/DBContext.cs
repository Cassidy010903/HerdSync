//using DAL.Models.Animal;
//using DAL.Models.Authentication;
//using DAL.Models.Farm;
//using DAL.Models.Program;
//using DAL.Models.Treatment;
//using Microsoft.EntityFrameworkCore;

//namespace DAL.Configuration.Database
//{
//    public class HerdSyncDbContext : DbContext
//    {
//        public HerdSyncDbContext(DbContextOptions<HerdSyncDbContext> options) : base(options)
//        {
//        }

//        public DbSet<AnimalModel> Animals { get; set; }
//        public DbSet<AnimalEventTypeModel> AnimalEventTypes { get; set; }
//        public DbSet<AnimalTagModel> AnimalTags { get; set; }
//        public DbSet<AnimalTypeModel> AnimalTypes { get; set; }
//        public DbSet<ConditionModel> Conditions { get; set; }
//        public DbSet<FarmModel> Farms { get; set; }
//        public DbSet<FarmActivityModel> FarmActivities { get; set; }
//        public DbSet<FarmActivityTypeModel> FarmActivityTypes { get; set; }
//        public DbSet<FarmUserModel> FarmUsers { get; set; }
//        public DbSet<PregnancyModel> Pregnancies { get; set; }
//        public DbSet<ProgramRunModel> ProgramRuns { get; set; }
//        public DbSet<ProgramRunAnimalModel> ProgramRunAnimals { get; set; }
//        public DbSet<ProgramRunObservationModel> ProgramRunObservations { get; set; }
//        public DbSet<ProgramRunTreatmentModel> ProgramRunTreatments { get; set; }
//        public DbSet<ProgramTemplateModel> ProgramTemplates { get; set; }
//        public DbSet<ProgramTemplateRuleModel> ProgramTemplateRules { get; set; }
//        public DbSet<ProgramTemplateRuleTreatmentModel> ProgramTemplateRuleTreatments { get; set; }
//        public DbSet<TreatmentModel> Treatments { get; set; }
//        public DbSet<TreatmentCategoryModel> TreatmentCategories { get; set; }
//        public DbSet<TreatmentProductModel> TreatmentProducts { get; set; }
//        public DbSet<UserAccountModel> UserAccounts { get; set; }
//        public DbSet<UserRoleModel> UserRoles { get; set; }

//        protected override void OnModelCreating(ModelBuilder modelBuilder)
//        {
//            base.OnModelCreating(modelBuilder);

//            // Composite unique constraint: one user per farm
//            modelBuilder.Entity<FarmUserModel>()
//                .HasIndex(fu => new { fu.FarmId, fu.UserId })
//                .IsUnique();

//            // Unique username
//            modelBuilder.Entity<UserAccountModel>()
//                .HasIndex(u => u.Username)
//                .IsUnique();

//            // Self-referencing Animal (Mother/Father) - disable cascade to avoid cycles
//            modelBuilder.Entity<AnimalModel>()
//                .HasOne(a => a.Mother)
//                .WithMany()
//                .HasForeignKey(a => a.MotherAnimalId)
//                .OnDelete(DeleteBehavior.Restrict);

//            modelBuilder.Entity<AnimalModel>()
//                .HasOne(a => a.Father)
//                .WithMany()
//                .HasForeignKey(a => a.FatherAnimalId)
//                .OnDelete(DeleteBehavior.Restrict);

//            // Pregnancy multi-FK to Animal - disable cascade to avoid cycles
//            modelBuilder.Entity<PregnancyModel>()
//                .HasOne(p => p.Mother)
//                .WithMany()
//                .HasForeignKey(p => p.MotherAnimalId)
//                .OnDelete(DeleteBehavior.Restrict);

//            modelBuilder.Entity<PregnancyModel>()
//                .HasOne(p => p.Father)
//                .WithMany()
//                .HasForeignKey(p => p.FatherAnimalId)
//                .OnDelete(DeleteBehavior.Restrict);

//            modelBuilder.Entity<PregnancyModel>()
//                .HasOne(p => p.Calf)
//                .WithMany()
//                .HasForeignKey(p => p.CalfAnimalId)
//                .OnDelete(DeleteBehavior.Restrict);
//        }
//    }
//}