using DalilAcademy.Core.DtOs;
using DalilAcademy.Core.Entites;
using DalilAcademy.Core.Entities;
using DalilAcademy.Core.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DalilAcademy_.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
     //primary constructor
    public class CoursesController(ICourseService courseService) : ControllerBase
    {

        //Inject Service Layer 
        //private readonly ICourseService _courseService;
        //public CoursesController(ICourseService courseService)
        //{
        //    _courseService = courseService;
        //}
        
        //CRUD
        //GET All
        [HttpGet]
        [AllowAnonymous]
         //api/courses/get-all-courses
        public async Task<ActionResult <List<CourseResponseDto>>> GetAll()
        {
            return Ok(await courseService.GetAll());
        }
        [AllowAnonymous]
        [HttpGet("page")]
        public async Task<ActionResult<PageResultcs<CourseResponseDto>>> GetPage([FromQuery]int Page=1, [FromQuery] int PageSize=10, [FromQuery] decimal? minPrice=null, [FromQuery] decimal ?maxPrice=null)
        {
            Page = Math.Max(Page, 1);
            PageSize = Math.Clamp(PageSize, 1, 50);
            return Ok(await courseService.GetPage(Page, PageSize, minPrice, maxPrice));

        }
        [HttpGet("count")]
        public async Task<IActionResult> GetCount()
        {
            var count = await courseService.GetCount();
            return Ok(count);
        }

        //Getbyid
        [HttpGet]
        [Route("{id:int}")]

        public async Task<ActionResult <CourseResponseDto> >GetById(int id)
        {
            var course = await courseService.GetById(id);

            if (course is null) return NotFound();
            return Ok(course);
        }

        [HttpPost]
        [Authorize(Roles =Roles.Admin)]

        public async Task<ActionResult<CourseResponseDto>>Create(CreateCourseDto course)
        {
            
                var created = await courseService.Create(course);
                return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
            
            
        }
        //Update
        [HttpPut]
        [Route("{id:int}")]
        [Authorize(Roles = Roles.Admin)]


        public async Task<ActionResult> Update(int id, UpdateCourseDto dto)
        {
            
                await courseService.Update(id,dto);
                return NoContent();
             }


        //Delete
        [HttpDelete]
        [Route("{id:int}")]
        [Authorize(Roles = Roles.Admin)]

        public async Task<ActionResult> Delete(int id)
        {
            await courseService.Delete(id);
            return NoContent();
        }



    }
}
