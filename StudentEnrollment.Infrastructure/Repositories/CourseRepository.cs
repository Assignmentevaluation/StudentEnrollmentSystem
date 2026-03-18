using Microsoft.EntityFrameworkCore;
using StudentEnrollment.Core.Entities;
using StudentEnrollment.Core.Interfaces.Repositories;
using StudentEnrollment.Infrastructure.Data;

namespace StudentEnrollment.Infrastructure.Repositories
{
    public class CourseRepository : GenericRepository<Course>, ICourseRepository
    {
        public CourseRepository(AppDbContext context) : base(context) { }

        public async Task<Course?> GetCourseWithEnrollmentsAsync(int courseId)
            => await _context.Courses
                             .Include(c => c.Enrollments)
                             .ThenInclude(e => e.Student)
                             .FirstOrDefaultAsync(c => c.CourseId == courseId);
    }
}