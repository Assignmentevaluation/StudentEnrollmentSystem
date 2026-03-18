using StudentEnrollment.Core.DTOs;

namespace StudentEnrollment.Core.Interfaces.Services
{
    public interface ICourseService
    {
        Task<IEnumerable<CourseDto>> GetAllCoursesAsync();
        Task<CourseDto?> GetCourseByIdAsync(int id);
        Task<CourseDto> CreateCourseAsync(CreateCourseDto dto);
        Task<bool> UpdateCourseAsync(int id, UpdateCourseDto dto);
        Task<bool> DeleteCourseAsync(int id);
    }
}