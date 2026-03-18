using StudentEnrollment.Core.DTOs;
using StudentEnrollment.Core.Entities;
using StudentEnrollment.Core.Interfaces.Repositories;
using StudentEnrollment.Core.Interfaces.Services;

namespace StudentEnrollment.Infrastructure.Services
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _studentRepository;

        public StudentService(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public async Task<IEnumerable<StudentDto>> GetAllStudentsAsync()
        {
            var students = await _studentRepository.GetAllAsync();
            return students.Select(s => new StudentDto
            {
                StudentId = s.StudentId,
                Name = s.Name ?? string.Empty,
                Email = s.Email ?? string.Empty
            });
        }

        public async Task<StudentDto?> GetStudentByIdAsync(int id)
        {
            var s = await _studentRepository.GetByIdAsync(id);
            if (s is null) return null;
            return new StudentDto
            {
                StudentId = s.StudentId,
                Name = s.Name ?? string.Empty,
                Email = s.Email ?? string.Empty
            };
        }

        public async Task<StudentDto> CreateStudentAsync(CreateStudentDto dto)
        {
            var student = new Student
            {
                Name = dto.Name,
                Email = dto.Email
            };

            await _studentRepository.AddAsync(student);
            await _studentRepository.SaveChangesAsync();

            return new StudentDto
            {
                StudentId = student.StudentId,
                Name = student.Name ?? string.Empty,
                Email = student.Email ?? string.Empty
            };
        }

        public async Task<bool> UpdateStudentAsync(int id, UpdateStudentDto dto)
        {
            var student = await _studentRepository.GetByIdAsync(id);
            if (student is null) return false;

            student.Name = dto.Name;
            student.Email = dto.Email;

            _studentRepository.Update(student);
            return await _studentRepository.SaveChangesAsync();
        }

        public async Task<bool> DeleteStudentAsync(int id)
        {
            var student = await _studentRepository.GetByIdAsync(id);
            if (student is null) return false;

            _studentRepository.Delete(student);
            return await _studentRepository.SaveChangesAsync();
        }
    }
}