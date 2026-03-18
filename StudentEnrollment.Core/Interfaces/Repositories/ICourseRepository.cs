using StudentEnrollment.Core.Entities;

namespace StudentEnrollment.Core.Interfaces.Repositories
{
    public interface ICourseRepository : IGenericRepository<Course>
    {
        Task<Course?> GetCourseWithEnrollmentsAsync(int courseId);
    }
}