using StudentEnrollment.Core.DTOs;
using StudentEnrollment.Core.Entities;
using StudentEnrollment.Core.Interfaces.Repositories;
using StudentEnrollment.Core.Interfaces.Services;

namespace StudentEnrollment.Infrastructure.Services
{
    public class EnrollmentService : IEnrollmentService
    {
        private readonly IEnrollmentRepository _enrollmentRepository;

        public EnrollmentService(IEnrollmentRepository enrollmentRepository)
        {
            _enrollmentRepository = enrollmentRepository;
        }

        public async Task<IEnumerable<EnrollmentDto>> GetAllEnrollmentsAsync()
        {
            var enrollments = await _enrollmentRepository.GetAllAsync();
            return enrollments.Select(MapToDto);
        }

        public async Task<EnrollmentDto?> GetEnrollmentByIdAsync(int id)
        {
            var e = await _enrollmentRepository.GetByIdAsync(id);
            return e is null ? null : MapToDto(e);
        }

        public async Task<IEnumerable<EnrollmentDto>> GetEnrollmentsByStudentAsync(int studentId)
        {
            var enrollments = await _enrollmentRepository.GetEnrollmentsByStudentAsync(studentId);
            return enrollments.Select(MapToDto);
        }

        public async Task<IEnumerable<EnrollmentDto>> GetEnrollmentsByCourseAsync(int courseId)
        {
            var enrollments = await _enrollmentRepository.GetEnrollmentsByCourseAsync(courseId);
            return enrollments.Select(MapToDto);
        }

        public async Task<EnrollmentDto> CreateEnrollmentAsync(CreateEnrollmentDto dto)
        {
            if (await _enrollmentRepository.IsAlreadyEnrolledAsync(dto.StudentId, dto.CourseId))
                throw new InvalidOperationException("Student is already enrolled in this course.");

            var enrollment = new Enrollment
            {
                StudentId = dto.StudentId,
                CourseId = dto.CourseId
            };

            await _enrollmentRepository.AddAsync(enrollment);
            await _enrollmentRepository.SaveChangesAsync();
            return MapToDto(enrollment);
        }

        public async Task<bool> DeleteEnrollmentAsync(int id)
        {
            var enrollment = await _enrollmentRepository.GetByIdAsync(id);
            if (enrollment is null) return false;

            _enrollmentRepository.Delete(enrollment);
            return await _enrollmentRepository.SaveChangesAsync();
        }

        private static EnrollmentDto MapToDto(Enrollment e) => new EnrollmentDto
        {
            EnrollmentId = e.EnrollmentId,
            StudentId = e.StudentId ?? 0,
            CourseId = e.CourseId ?? 0,
            StudentName = e.Student?.Name ?? string.Empty,
            CourseTitle = e.Course?.Title ?? string.Empty
        };
    }
}