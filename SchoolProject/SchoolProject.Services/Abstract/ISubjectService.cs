using SchoolProject.Data.Entities;

namespace SchoolProject.Services.Abstract
{
    public interface ISubjectService
    {
        public Task<string> Add(Subject subject);
        public Task<string> Update(Subject subject);
        public Task<string> Delete(int id);
        public Task<Subject> GetById(int id);
        public Task<List<Subject>> GetAll();
        public Task<bool> IsNameExist(string name);
        public Task<bool> IsNameExistExcludeSelf(string name, int id);
    }
}
