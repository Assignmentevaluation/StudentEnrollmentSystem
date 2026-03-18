using Microsoft.AspNetCore.Mvc;
using StudentEnrollment.Core.DTOs;
using StudentEnrollment.Core.Interfaces.Services;

namespace StudentEnrollment.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentService _studentService;

        public StudentsController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
            => Ok(await _studentService.GetAllStudentsAsync());

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var student = await _studentService.GetStudentByIdAsync(id);
            return student is null ? NotFound() : Ok(student);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateStudentDto dto)
        {
            var created = await _studentService.CreateStudentAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.StudentId }, created);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateStudentDto dto)
        {
            var updated = await _studentService.UpdateStudentAsync(id, dto);
            return updated ? NoContent() : NotFound();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _studentService.DeleteStudentAsync(id);
            return deleted ? NoContent() : NotFound();
        }
    }
}