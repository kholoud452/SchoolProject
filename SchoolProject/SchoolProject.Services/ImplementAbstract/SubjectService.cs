using Microsoft.EntityFrameworkCore;
using SchoolProject.Data.Entities;
using SchoolProject.Infrastructure.Abstract;
using SchoolProject.Infrastructure.Data;
using SchoolProject.Services.Abstract;

namespace SchoolProject.Services.ImplementAbstract
{
    public class SubjectService : ISubjectService
    {
        private readonly ISubjrctRepo _subjrctRepo;
        private readonly schoolDBContext _context;

        public SubjectService(ISubjrctRepo subjrctRepo,
                               schoolDBContext context)
        {
            _subjrctRepo = subjrctRepo;
            _context = context;
        }

        public async Task<string> Add(Subject subject)
        {
            var addedSubject = await _subjrctRepo.AddAsync(subject);
            if (addedSubject == null)
                return "Failed to Add Subject";
            return "Success";
        }
        public async Task<string> Update(Subject subject)
        {
            await _subjrctRepo.UpdateAsync(subject);
            return "Success";
        }

        public async Task<string> Delete(int id)
        {
            var transact = await _context.Database.BeginTransactionAsync();
            try
            {
                var subjectFromDB = await GetById(id);
                if (subjectFromDB == null)
                    return "NotFound";
                var studentSubject = subjectFromDB.StudentsSubjects.Where(sb => sb.SubID == subjectFromDB.SubID).ToList();
                _context.StudentSubjects.RemoveRange(studentSubject);
                var instructorSubject = subjectFromDB.Ins_Subjects.Where(ib => ib.SubId == subjectFromDB.SubID).ToList();
                _context.Ins_Subject.RemoveRange(instructorSubject);
                var departmentSubjects = subjectFromDB.DepartmetsSubjects.Where(db => db.SubID == subjectFromDB.SubID).ToList();
                _context.RemoveRange(departmentSubjects);
                await _context.SaveChangesAsync();

                subjectFromDB.IsDeleted = true;
                await _subjrctRepo.UpdateAsync(subjectFromDB);
                await transact.CommitAsync();
                return "Success";
            }
            catch (Exception ex)
            {
                await transact.RollbackAsync();
                return "failed";
            }
        }


        public async Task<bool> IsNameExist(string name)
        {
            if (await _subjrctRepo.GetTableNoTracking()
                .Where(x => x.SubjectNameEn == name | x.SubjectNameAr.Equals(name) & !x.IsDeleted)
                .FirstOrDefaultAsync() != null)
                return true;
            return false;
        }
        public async Task<bool> IsNameExistExcludeSelf(string name, int id) //for the update operation
        {
            if (await _subjrctRepo.GetTableNoTracking().Where(s => s.SubjectNameAr.Equals(name) & !s.SubID.Equals(id) & !s.IsDeleted).FirstOrDefaultAsync() != null)
                return true;
            return false;
        }

        public async Task<Subject> GetById(int id)
        {
            return await _subjrctRepo.GetTableNoTracking().Where(s => s.SubID.Equals(id) & !s.IsDeleted).FirstOrDefaultAsync();
        }

        public async Task<List<Subject>> GetAll()
        {
            return await _subjrctRepo.GetTableNoTracking().Where(s => !s.IsDeleted).ToListAsync();
        }
    }
}
