using Microsoft.EntityFrameworkCore;
using SchoolProject.Data.Entities;
using SchoolProject.Data.Helper;
using SchoolProject.Infrastructure.Abstract;
using SchoolProject.Services.Abstract;

namespace SchoolProject.Services.ImplementAbstract
{
    public class
        StudentService : IStudentService
    {
        private readonly IStudentRepo _studentRepo;

        public StudentService(IStudentRepo studentRepo)
        {
            _studentRepo = studentRepo;
        }

        public async Task<string> AddStudent(Student student)
        {
            if (await IsNameExist(student.NameEn) == true)
                return "Exist";
            await _studentRepo.AddAsync(student);
            return "Success";
        }

        public async Task<string> DeleteAsync(Student student)
        {
            var trans = _studentRepo.BeginTransaction();
            try
            {
                await _studentRepo.UpdateAsync(student);
                await _studentRepo.DeleteAsync(student);
                await trans.CommitAsync();
                return $"{student.NameAr} deleted";

            }
            catch (Exception ex)
            {
                await trans.RollbackAsync();
                return "Failed";
            }
        }

        public async Task<string> EditAsync(Student student)
        {
            await _studentRepo.UpdateAsync(student);
            return "success";
        }

        public IQueryable<Student> FilterStudentPaginationQuarable(StudentOrderingEnum orderBy, string search)
        {
            var quarable = _studentRepo.GetTableNoTracking().AsQueryable();
            if (search != null)
            {
                quarable = quarable.Where(s => s.NameEn.Contains(search) || s.Address.Contains(search));
            }
            switch (orderBy)
            {
                case StudentOrderingEnum.StudID:
                    quarable = quarable.OrderBy(s => s.StudID);
                    break;
                case StudentOrderingEnum.Name:
                    quarable = quarable.OrderBy(s => s.NameAr);
                    break;
                case StudentOrderingEnum.Address:
                    quarable = quarable.OrderBy(s => s.Address);
                    break;
                case StudentOrderingEnum.DName:
                    quarable = quarable.OrderBy(s => s.Department.DNameAr);
                    break;

            }
            return quarable;
        }

        public async Task<List<Student>> GetAllAsync()
        {
            return await _studentRepo.GetAllAsync();
        }

        public IQueryable<Student> GetAlLQuarableByDepartmentStudents(int DID)
        {
            return _studentRepo.GetTableNoTracking().Where(d=> d.DID.Equals(DID)).AsQueryable();
        }

        public IQueryable<Student> GetAlLQuarableStudents()
        {
            return _studentRepo.GetTableNoTracking().AsQueryable();
        }

        public async Task<Student> GetStudentByIDAsync(int id)
        {
            return _studentRepo.GetTableNoTracking().Where(s => s.StudID.Equals(id)).FirstOrDefault();
        }

        public async Task<bool> IsNameExist(string name)
        {
            if (await _studentRepo.GetTableNoTracking().Where(s => s.NameEn.Equals(name)).FirstOrDefaultAsync() != null)
                return true;
            return false;

        }

        public async Task<bool> IsNameExistExcludeSelf(string name, int id)
        {
            if (await _studentRepo.GetTableNoTracking().Where(s => s.NameAr.Equals(name) & !s.StudID.Equals(id)).FirstOrDefaultAsync() != null)
                return true;
            return false;
        }
    }
}
