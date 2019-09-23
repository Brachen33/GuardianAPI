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
        public DbSet<LogEntry> LogEntries { get; set; }
        public DbSet<Panel> Panels { get; set; }
        public DbSet<Participant> Participants { get; set; }
        public DbSet<ParticipantPanel> ParticipantPanels { get; set; }
        public DbSet<ParticipantSchedule> ParticipantSchedules { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Requisition> Requisitions { get; set; }
        public DbSet<Result> Results { get; set; }
        public DbSet<ResultDetail> ResultDetails { get; set; }
        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Add List of Results to a Participant
            modelBuilder.Entity<Participant>()
                .HasMany(x => x.Results)
                .WithOne(x => x.Participant)
                .HasForeignKey(x => x.ParticipantId);

            // Attach Contact to Participant (1:1)
            // TODO: Make the RecordID on Contact a composite / secondary foreign key set to "PID"
            modelBuilder.Entity<Participant>()
                .HasOne(x => x.Contact)
                .WithOne(x => x.Participant)
                .HasForeignKey<Contact>(x => x.RecordID);

            // Attach ParticipantSchedule to a participant
            // TODO: ask Vince if this is one to one
            modelBuilder.Entity<Participant>()
                .HasOne(x => x.ParticipantSchedule)
                .WithOne(x => x.Participant)
                .HasForeignKey<ParticipantSchedule>(x => x.ParticipantId);

            // Attach Participants to a User
            modelBuilder.Entity<User>()
                .HasMany(x => x.Participants)
                .WithOne(x => x.User)
                .HasForeignKey(x => x.CaseManagerID);


            // Add List of ResultDetails to Results
            modelBuilder.Entity<Result>()
                .HasMany(x => x.ResultDetails)
                .WithOne(x => x.Result)
                .HasForeignKey(x => x.ResultID);

            //Join the Participant to it's Participant Panel
            modelBuilder.Entity<Participant>()
                .HasMany(x => x.ParticipantPanels)
                .WithOne(x => x.Participant)
                .HasForeignKey(x => x.ParticipantId);
        }
    }
}
