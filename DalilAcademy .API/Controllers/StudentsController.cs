using DalilAcademy.Core.DtOs;
using DalilAcademy.Core.Entities;
using DalilAcademy.Core.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DalilAcademy_.API.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class StudentsController(IStudentService studentService, ICourseService courseService)
        : ControllerBase
    {
        [Authorize(Roles =Roles.Admin)]
        [HttpGet]
        public async Task<ActionResult<List<StudentResponseDto>>> GetAll()
            => Ok(await studentService.GetAll());
        [HttpGet("page")]
        [Authorize(Roles = Roles.Admin)]
        public async Task<ActionResult<PageResultcs<StudentResponseDto>>> GetPage([FromQuery] int Page = 1, [FromQuery] int PageSize = 10)
        {
            Page = Math.Max(Page, 1);
            PageSize = Math.Clamp(PageSize, 1, 50);
            return Ok(await studentService.GetPage(Page, PageSize));

        }
        
        [HttpGet("{id:int}")]
        [Authorize(Roles = Roles.Admin)]
        public async Task<ActionResult<StudentResponseDto>> GetById(int id)
            => Ok(await studentService.GetById(id));


        [HttpPost]
        [Authorize(Roles = Roles.Admin)]
        public async Task<ActionResult<StudentResponseDto>> Create(CreateStudentDto dto)
        {
            var created = await studentService.Create(dto);

            return CreatedAtAction(
                nameof(GetById),
                new { id = created.Id },
                created);
        }

        [HttpPut("{id:int}")]
        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> Update(int id, UpdateStudentDto dto)
        {
            await studentService.Update(id, dto);
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> Delete(int id)
        {
            await studentService.Delete(id);
            return NoContent();
        }
        //7
        [Authorize(Roles = Roles.Admin)]
        [HttpGet("{id:int}/courses")]
        public async Task<ActionResult<List<CourseResponseDto>>> GetCourses(int id)
            => Ok(await studentService.GetCourses(id));
    }
}
