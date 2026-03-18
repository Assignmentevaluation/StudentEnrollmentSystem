using Microsoft.AspNetCore.Mvc;
using StudentEnrollment.Core.DTOs;
using StudentEnrollment.Core.Interfaces.Services;

namespace StudentEnrollment.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CoursesController : ControllerBase
    {
        private readonly ICourseService _courseService;

        public CoursesController(ICourseService courseService)
        {
            _courseService = courseService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
            => Ok(await _courseService.GetAllCoursesAsync());

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var course = await _courseService.GetCourseByIdAsync(id);
            return course is null ? NotFound() : Ok(course);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCourseDto dto)
        {
            var created = await _courseService.CreateCourseAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.CourseId }, created);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateCourseDto dto)
        {
            var updated = await _courseService.UpdateCourseAsync(id, dto);
            return updated ? NoContent() : NotFound();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _courseService.DeleteCourseAsync(id);
            return deleted ? NoContent() : NotFound();
        }
    }
}