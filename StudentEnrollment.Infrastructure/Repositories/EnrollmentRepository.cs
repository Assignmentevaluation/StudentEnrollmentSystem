using Microsoft.EntityFrameworkCore;
using StudentEnrollment.Core.Entities;
using StudentEnrollment.Core.Interfaces.Repositories;
using StudentEnrollment.Infrastructure.Data;

namespace StudentEnrollment.Infrastructure.Repositories
{
    public class EnrollmentRepository : GenericRepository<Enrollment>, IEnrollmentRepository
    {
        public EnrollmentRepository(AppDbContext context) : base(context) { }

        public async Task<IEnumerable<Enrollment>> GetEnrollmentsByStudentAsync(int studentId)
            => await _context.Enrollments
                             .Include(e => e.Course)
                             .Where(e => e.StudentId == studentId)
                             .ToListAsync();

        public async Task<IEnumerable<Enrollment>> GetEnrollmentsByCourseAsync(int courseId)
            => await _context.Enrollments
                             .Include(e => e.Student)
                             .Where(e => e.CourseId == courseId)
                             .ToListAsync();

        public async Task<bool> IsAlreadyEnrolledAsync(int studentId, int courseId)
            => await _context.Enrollments
                             .AnyAsync(e => e.StudentId == studentId && e.CourseId == courseId);
    }
}