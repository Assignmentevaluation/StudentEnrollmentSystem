using StudentEnrollment.Core.DTOs;

namespace StudentEnrollment.Core.Interfaces.Services
{
    public interface IEnrollmentService
    {
        Task<IEnumerable<EnrollmentDto>> GetAllEnrollmentsAsync();
        Task<EnrollmentDto?> GetEnrollmentByIdAsync(int id);
        Task<IEnumerable<EnrollmentDto>> GetEnrollmentsByStudentAsync(int studentId);
        Task<IEnumerable<EnrollmentDto>> GetEnrollmentsByCourseAsync(int courseId);
        Task<EnrollmentDto> CreateEnrollmentAsync(CreateEnrollmentDto dto);
        Task<bool> DeleteEnrollmentAsync(int id);
    }
}