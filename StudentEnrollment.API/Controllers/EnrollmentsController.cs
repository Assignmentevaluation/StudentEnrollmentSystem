using Microsoft.AspNetCore.Mvc;
using StudentEnrollment.Core.DTOs;
using StudentEnrollment.Core.Interfaces.Services;

namespace StudentEnrollment.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EnrollmentsController : ControllerBase
    {
        private readonly IEnrollmentService _enrollmentService;

        public EnrollmentsController(IEnrollmentService enrollmentService)
        {
            _enrollmentService = enrollmentService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
            => Ok(await _enrollmentService.GetAllEnrollmentsAsync());

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var enrollment = await _enrollmentService.GetEnrollmentByIdAsync(id);
            return enrollment is null ? NotFound() : Ok(enrollment);
        }

        [HttpGet("student/{studentId:int}")]
        public async Task<IActionResult> GetByStudent(int studentId)
            => Ok(await _enrollmentService.GetEnrollmentsByStudentAsync(studentId));

        [HttpGet("course/{courseId:int}")]
        public async Task<IActionResult> GetByCourse(int courseId)
            => Ok(await _enrollmentService.GetEnrollmentsByCourseAsync(courseId));

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateEnrollmentDto dto)
        {
            var created = await _enrollmentService.CreateEnrollmentAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.EnrollmentId }, created);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _enrollmentService.DeleteEnrollmentAsync(id);
            return deleted ? NoContent() : NotFound();
        }
    }
}