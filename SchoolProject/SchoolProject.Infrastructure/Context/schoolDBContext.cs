using EntityFrameworkCore.EncryptColumn.Extension;
using EntityFrameworkCore.EncryptColumn.Interfaces;
using EntityFrameworkCore.EncryptColumn.Util;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SchoolProject.Data.Commans;
using SchoolProject.Data.Entities;
using SchoolProject.Data.Entities.Identity;
using System.Linq.Expressions;
using System.Reflection;

namespace SchoolProject.Infrastructure.Data
{
    public class schoolDBContext : IdentityDbContext<ApplicationUser>
    {
        private readonly IEncryptionProvider _encryptionProvider;
        public schoolDBContext() : base()
        {

        }
        public schoolDBContext(DbContextOptions<schoolDBContext> options) : base(options)
        {
            _encryptionProvider = new GenerateEncryptionProvider("8a4dcaaec64d412380fe4b02193cd26f");
        }
        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<UserRefreshToken> UserRefreshToken { get; set; }
        public virtual DbSet<DepartmentSubject> DepartmentSubjects { get; set; }
        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<StudentSubject> StudentSubjects { get; set; }
        public virtual DbSet<Subject> Subjects { get; set; }
        public virtual DbSet<Instructor> Instructor { get; set; }
        public virtual DbSet<Ins_Subject> Ins_Subject { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Ins_Subject>().HasKey(i => new { i.SubId, i.InsId });
            modelBuilder.Entity<StudentSubject>().HasKey(i => new { i.SubID, i.StudID });
            modelBuilder.Entity<DepartmentSubject>().HasKey(i => new { i.SubID, i.DID });

            //use generic soft delete
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                if (typeof(GeneralLocalizableEntities).IsAssignableFrom(entityType.ClrType))
                {
                    var method = typeof(EF).GetMethod("Property")!
                        .MakeGenericMethod(typeof(bool));
                    var parameter = Expression.Parameter(entityType.ClrType, "e");
                    var body = Expression.Equal(
                               Expression.Call(method, parameter, Expression.Constant("IsDeleted")),
                               Expression.Constant(false)
                    );
                    var lambda = Expression.Lambda(body, parameter);
                    modelBuilder.Entity(entityType.ClrType).HasQueryFilter(lambda);
                }
            }

            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            modelBuilder.UseEncryption(_encryptionProvider);
        }

    }
}
