using Microsoft.EntityFrameworkCore;
using SchoolProject.Data.Entities.Identity;
using SchoolProject.Infrastructure.Abstract;
using SchoolProject.Infrastructure.Data;
using SchoolProject.Infrastructure.InfrastructureBases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Infrastructure.Repositories
{
    public class RefreshTokenRepo: GenericRepositoryAsync<UserRefreshToken>, IRefreshTokenRepo
    {
        private DbSet<UserRefreshToken> userRefreshTokens;


        public RefreshTokenRepo(schoolDBContext context):base(context) 
        {
            userRefreshTokens=context.Set<UserRefreshToken>();
        }
    }
}
