namespace SchoolProject.Services.Abstract
{
    public interface IInstructorService
    {
        public Task<bool> IsInstructorExist(int id);
    }
}
