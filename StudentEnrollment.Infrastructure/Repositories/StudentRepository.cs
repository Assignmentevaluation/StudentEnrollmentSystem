using Microsoft.EntityFrameworkCore;
using StudentEnrollment.Core.Entities;
using StudentEnrollment.Core.Interfaces.Repositories;
using StudentEnrollment.Infrastructure.Data;

namespace StudentEnrollment.Infrastructure.Repositories
{
    public class StudentRepository : GenericRepository<Student>, IStudentRepository
    {
        public StudentRepository(AppDbContext context) : base(context) { }

        public async Task<Student?> GetStudentWithEnrollmentsAsync(int studentId)
            => await _context.Students
                             .Include(s => s.Enrollments)
                             .ThenInclude(e => e.Course)
                             .FirstOrDefaultAsync(s => s.StudentId == studentId);
    }
}