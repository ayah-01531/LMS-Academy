using DalilAcademy.Core.DtOs;
using DalilAcademy.Core.Entities;
using DalilAcademy.Core.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DalilAcademy_.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EnrollmentsController(IEnrollmentService enrollmentService) : ControllerBase
    {
        [HttpPost]
        public async Task<ActionResult<EnrollmentResponseDto>> Enroll(CreateEnrollmentDto dto)
        {
            var enrollment = await enrollmentService.EnrollAsync(dto.StudentId, dto.CourseId);

            return StatusCode(StatusCodes.Status201Created, enrollment);
        }
        [HttpGet("me")]

        [Authorize(Roles = Roles.Student)]
        public async Task<ActionResult<List<EnrollmentResponseDto>>> GetMyCourses()
        {
            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!int.TryParse(userIdClaim, out var userId))
                return Unauthorized();

            return Ok(await enrollmentService.GetMyCoursesAsync(userId));
        }
    }
}
