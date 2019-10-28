using GuardianAPI.Models.PSIManager;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuardianAPI.Models
{
    public class AppDbContext : DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {

        }

        public DbSet<Collection> Collections { get; set; }
        public DbSet<CollectionSite> CollectionSites { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Document> Documents { get; set; }
        public DbSet<DocumentData> DocumentDatas { get; set; }
        public DbSet<Lab> Labs { get; set; }
        public DbSet<LogEntry> LogEntries { get; set; }
        public DbSet<Panel> Panels { get; set; }
        public DbSet<Participant> Participants { get; set; }
        public DbSet<ParticipantPanel> ParticipantPanels { get; set; }
        public DbSet<ParticipantSchedule> ParticipantSchedules { get; set; }
        public DbSet<PaternityRelation> PaternityRelations { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Requisition> Requisitions { get; set; }
        public DbSet<Result> Results { get; set; }
        public DbSet<ResultDetail> ResultDetails { get; set; }
        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<TestSchedule> TestSchedules { get; set; }
        public DbSet<TestPanel> TestPanels { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }

        #region PSI_Manager
        public DbSet<Client> Clients { get; set; }
        public DbSet<PSIUser> PSIUsers { get; set; }
        #endregion


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

          //  User Joins
            modelBuilder.Entity<User>()
                .HasMany(x => x.Participants)
                .WithOne(x => x.User)
                .HasForeignKey(x => x.CaseManagerId);

            modelBuilder.Entity<User>()
                .HasOne(x => x.Contact)
                .WithOne(x => x.User)
                .HasForeignKey<Contact>(x => x.RecordID);

            modelBuilder.Entity<User>()
                .HasMany(x => x.Requisitions)
                .WithOne(x => x.User)
                .HasForeignKey(x => x.CaseManagerId);

            //   Participant Joins
            modelBuilder.Entity<Participant>()
                .HasOne(x => x.Contact)
                .WithOne(x => x.Participant)
                .HasForeignKey<Contact>(x => x.RecordID);

            modelBuilder.Entity<Participant>()
                .HasMany(x => x.TestSchedules)
                .WithOne(x => x.Participant)
                .HasForeignKey(x => x.ParticipantId);

           

            modelBuilder.Entity<TestSchedule>()
               .HasMany(p => p.TestPanels)
               .WithOne(t => t.TestSchedule)
               .HasForeignKey(p => p.TestID);      
          
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<TestPanel>().HasKey(
                t => new { t.TestID }
                );         

                  

            modelBuilder.Entity<Result>()
                .HasOne(p => p.Panel)
                .WithMany(x => x.Results)
                .HasForeignKey(x => x.OBR_4_1)
                .HasPrincipalKey(x => x.LabPanelCode);

            modelBuilder.Entity<Document>()
                .HasOne(x => x.DocumentData)
                .WithOne(x => x.Document)
                .HasForeignKey<DocumentData>(x => x.MetaId);            
        }
    }
}
