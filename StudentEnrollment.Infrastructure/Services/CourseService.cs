using StudentEnrollment.Core.DTOs;
using StudentEnrollment.Core.Entities;
using StudentEnrollment.Core.Interfaces.Repositories;
using StudentEnrollment.Core.Interfaces.Services;

namespace StudentEnrollment.Infrastructure.Services
{
    public class CourseService : ICourseService
    {
        private readonly ICourseRepository _courseRepository;

        public CourseService(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }

        public async Task<IEnumerable<CourseDto>> GetAllCoursesAsync()
        {
            var courses = await _courseRepository.GetAllAsync();
            return courses.Select(c => new CourseDto
            {
                CourseId = c.CourseId,
                Title = c.Title ?? string.Empty,
                Credits = c.Credits ?? 0
            });
        }

        public async Task<CourseDto?> GetCourseByIdAsync(int id)
        {
            var c = await _courseRepository.GetByIdAsync(id);
            if (c is null) return null;
            return new CourseDto
            {
                CourseId = c.CourseId,
                Title = c.Title ?? string.Empty,
                Credits = c.Credits ?? 0
            };
        }

        public async Task<CourseDto> CreateCourseAsync(CreateCourseDto dto)
        {
            var course = new Course
            {
                Title = dto.Title,
                Credits = dto.Credits
            };

            await _courseRepository.AddAsync(course);
            await _courseRepository.SaveChangesAsync();

            return new CourseDto
            {
                CourseId = course.CourseId,
                Title = course.Title ?? string.Empty,
                Credits = course.Credits ?? 0
            };
        }

        public async Task<bool> UpdateCourseAsync(int id, UpdateCourseDto dto)
        {
            var course = await _courseRepository.GetByIdAsync(id);
            if (course is null) return false;

            course.Title = dto.Title;
            course.Credits = dto.Credits;

            _courseRepository.Update(course);
            return await _courseRepository.SaveChangesAsync();
        }

        public async Task<bool> DeleteCourseAsync(int id)
        {
            var course = await _courseRepository.GetByIdAsync(id);
            if (course is null) return false;

            _courseRepository.Delete(course);
            return await _courseRepository.SaveChangesAsync();
        }
    }
}