using backend.Data.DataModels;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace JwtBackend.Data
{
    public class IdentityDbContext: DbContext
    {
        public IdentityDbContext(DbContextOptions<IdentityDbContext> options):base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRoles> UsersRoles { get; set; }
        public DbSet<UserProfile> UsersProfile { get; set; }
        public DbSet<RefreshSession> RefreshSessions { get; set; }
        public DbSet<TeacherLesson> TeacherLessons { get; set; }
        public DbSet<StudentLesson> StudentLessons { get; set; }
        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<LessonType> LessonTypes { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
