using StudentEnrollment.Core.Entities;

namespace StudentEnrollment.Core.Interfaces.Repositories
{
    public interface IStudentRepository : IGenericRepository<Student>
    {
        Task<Student?> GetStudentWithEnrollmentsAsync(int studentId);
    }
}