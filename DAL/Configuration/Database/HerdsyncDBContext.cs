using DAL.Models;
using DAL.Models.Base;
using DAL.Models.Base.History;

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

        public DbSet<spd_Species_Detail> Species { get; set; }
        public DbSet<prg_Pregnancies_Detail> Pregnancies { get; set; }

        public DbSet<SpeciesDetailHistory> SpeciesHistory { get; set; }
        public DbSet<PregnanciesDetailHistory> PregnanciesHistory { get; set; }
        public DbSet<stl_Species_Tag_Lookup> SpeciesTag { get; set; }
        public DbSet<trl_Treatment_Lookup> Treatment { get; set; }
        public DbSet<prg_Program> Program { get; set; }
        public DbSet<ins_Instruction_Lookup> ProgramInstruction { get; set; }
        public DbSet<itr_Instruction_Treatment> ProgramInstructionTreatment { get; set; }
        public DbSet<ase_Active_Session> ProgramRun { get; set; }
        public DbSet<ast_Animal_Session_Treatment> ProgramRunAnimal { get; set; }
        public DbSet<atr_Animal_Treatment> ProgramRunAnimalTreatment { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            // SpeciesDetailHistory
            var speciesHistory = modelBuilder.Entity<SpeciesDetailHistory>();
            speciesHistory.Property(h => h.DeletedUser).HasMaxLength(100);
            modelBuilder.Entity<SpeciesDetailHistory>()
                .Property(s => s.spd_Weight)
                .HasPrecision(18, 2);

            // spd_Species_Detail
            var species = modelBuilder.Entity<spd_Species_Detail>();
            species.Property(s => s.spd_Species)
                .HasMaxLength(50);

            modelBuilder.Entity<spd_Species_Detail>()
                .HasMany(c => c.Pregnancies) 
                .WithOne(p => p.Species)
                .HasForeignKey(p => p.spd_Id)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<spd_Species_Detail>()
                .Property(s => s.spd_Weight)
                .HasPrecision(18, 2);

            // prg_Pregnancies_Detail
            var pregnancies = modelBuilder.Entity<prg_Pregnancies_Detail>();
            pregnancies.Property(p => p.prg_Pregnancy_Spot_Date)
                .IsRequired();
            pregnancies.Property(p => p.prg_Pregnancy_End_Date)
                .IsRequired();

            pregnancies.HasOne(p => p.Species)
                .WithMany(s => s.Pregnancies)
                .HasForeignKey(p => p.spd_Id)
                .OnDelete(DeleteBehavior.Restrict);

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
        }
    }
}