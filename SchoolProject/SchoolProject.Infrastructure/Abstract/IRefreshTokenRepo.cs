using SchoolProject.Data.Entities.Identity;
using SchoolProject.Infrastructure.InfrastructureBases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Infrastructure.Abstract
{
    public interface IRefreshTokenRepo:IGenericRepositoryAsync<UserRefreshToken>
    {
    }
}
