using DAL.Models;
using DAL.Models.Animal;
using DAL.Models.Authentication;
using DAL.Models.Base;
using DAL.Models.Base.History;
using DAL.Models.Farm;
using DAL.Models.Program;
using DAL.Models.Treatment;
// using DAL.Models.Base.History;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Oracle.ManagedDataAccess.Client;
using System.Configuration;

namespace DAL.Configuration.Database
{
    public class HerdsyncDBContext : DbContext
    {
        public HerdsyncDBContext(DbContextOptions<HerdsyncDBContext> options) : base(options) { }
        public DbSet<AnimalModel> Animals { get; set; }
        public DbSet<AnimalEventTypeModel> AnimalEventTypes { get; set; }
        public DbSet<AnimalTagModel> AnimalTags { get; set; }
        public DbSet<AnimalTypeModel> AnimalTypes { get; set; }
        public DbSet<ConditionModel> Conditions { get; set; }
        public DbSet<FarmModel> Farms { get; set; }
        public DbSet<FarmActivityModel> FarmActivities { get; set; }
        public DbSet<FarmActivityTypeModel> FarmActivityTypes { get; set; }
        public DbSet<FarmUserModel> FarmUsers { get; set; }
        public DbSet<PregnancyModel> Pregnancies { get; set; }
        public DbSet<ProgramRunModel> ProgramRuns { get; set; }
        public DbSet<ProgramRunAnimalModel> ProgramRunAnimals { get; set; }
        public DbSet<ProgramRunObservationModel> ProgramRunObservations { get; set; }
        public DbSet<ProgramRunTreatmentModel> ProgramRunTreatments { get; set; }
        public DbSet<ProgramTemplateModel> ProgramTemplates { get; set; }
        public DbSet<ProgramTemplateRuleModel> ProgramTemplateRules { get; set; }
        public DbSet<ProgramTemplateRuleTreatmentModel> ProgramTemplateRuleTreatments { get; set; }
        public DbSet<TreatmentModel> Treatments { get; set; }
        public DbSet<TreatmentCategoryModel> TreatmentCategories { get; set; }
        public DbSet<TreatmentProductModel> TreatmentProducts { get; set; }
        public DbSet<UserAccountModel> UserAccounts { get; set; }
        public DbSet<UserRoleModel> UserRoles { get; set; }
        public DbSet<prg_Pregnancies_Detail> Pregnanciess { get; set; }

        public DbSet<SpeciesDetailHistory> SpeciesHistoryOld { get; set; }
        public DbSet<PregnanciesDetailHistory> PregnanciesHistoryOld { get; set; }
        public DbSet<stl_Species_Tag_Lookup> SpeciesTag { get; set; }
        public DbSet<trl_Treatment_Lookup> TreatmentOld { get; set; }
        public DbSet<prg_Program> ProgramOld { get; set; }
        public DbSet<ins_Instruction_Lookup> ProgramInstructionOld { get; set; }
        public DbSet<itr_Instruction_Treatment> ProgramInstructionTreatmentOld { get; set; }
        public DbSet<ase_Active_Session> ProgramRunOld { get; set; }
        public DbSet<ast_Animal_Session_Treatment> ProgramRunAnimalOld { get; set; }
        public DbSet<atr_Animal_Treatment> ProgramRunAnimalTreatmentOld { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            // SpeciesDetailHistory
            var speciesHistory = modelBuilder.Entity<SpeciesDetailHistory>();
            speciesHistory.Property(h => h.DeletedUser).HasMaxLength(100);
            modelBuilder.Entity<SpeciesDetailHistory>()
                .Property(s => s.spd_Weight)
                .HasPrecision(18, 2);

            // prg_Pregnancies_Detail
            var pregnancies = modelBuilder.Entity<prg_Pregnancies_Detail>();
            pregnancies.Property(p => p.prg_Pregnancy_Spot_Date)
                .IsRequired();
            pregnancies.Property(p => p.prg_Pregnancy_End_Date)
                .IsRequired();

            modelBuilder.Entity<itr_Instruction_Treatment>()
                .HasOne(it => it.Treatment)
                .WithMany()
                .HasForeignKey(it => it.TreatmentId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<atr_Animal_Treatment>()
                .HasOne(at => at.Treatment)
                .WithMany()
                .HasForeignKey(at => at.TreatmentId)
                .OnDelete(DeleteBehavior.Restrict);

            // Composite unique constraint: one user per farm
            modelBuilder.Entity<FarmUserModel>()
                .HasIndex(fu => new { fu.FarmId, fu.UserId })
                .IsUnique();

            // Unique username
            modelBuilder.Entity<UserAccountModel>()
                .HasIndex(u => u.Username)
                .IsUnique();

            // Self-referencing Animal (Mother/Father) - disable cascade to avoid cycles
            modelBuilder.Entity<AnimalModel>()
                .HasOne(a => a.Mother)
                .WithMany()
                .HasForeignKey(a => a.MotherAnimalId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<AnimalModel>()
                .HasOne(a => a.Father)
                .WithMany()
                .HasForeignKey(a => a.FatherAnimalId)
                .OnDelete(DeleteBehavior.Restrict);

            // Pregnancy multi-FK to Animal - disable cascade to avoid cycles
            modelBuilder.Entity<PregnancyModel>()
                .HasOne(p => p.Mother)
                .WithMany()
                .HasForeignKey(p => p.MotherAnimalId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PregnancyModel>()
                .HasOne(p => p.Father)
                .WithMany()
                .HasForeignKey(p => p.FatherAnimalId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PregnancyModel>()
                .HasOne(p => p.Calf)
                .WithMany()
                .HasForeignKey(p => p.CalfAnimalId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}