using StudentEnrollment.Core.Entities;

namespace StudentEnrollment.Core.Interfaces.Repositories
{
    public interface IEnrollmentRepository : IGenericRepository<Enrollment>
    {
        Task<IEnumerable<Enrollment>> GetEnrollmentsByStudentAsync(int studentId);
        Task<IEnumerable<Enrollment>> GetEnrollmentsByCourseAsync(int courseId);
        Task<bool> IsAlreadyEnrolledAsync(int studentId, int courseId);
    }
}