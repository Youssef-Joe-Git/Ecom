using AutoMapper;
using Ecom.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Ecom.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BugsController : BaseController
    {
        public BugsController(IUnitOfWork work, IMapper mapper) : base(work, mapper)
        {
        }
        [HttpGet("not-found")]
        public async Task<IActionResult> GetNotFound()
        {
            var cat = await work.Categories.GetByIdAsync(42);
            if (cat == null)
            {
                return NotFound();
            }
            return Ok();
        }
        [HttpGet("server-error")]
        public async Task<IActionResult> GetServerError()
        {
            var cat = await work.Categories.GetByIdAsync(42);
            cat.Name = "";
            return Ok();
        }
        [HttpGet("bad-request")]
        public IActionResult GetBadRequest()
        {
            return Ok();
        }
        [HttpGet("bad-request/{id}")]
        public IActionResult GetBadRequest(int id)
        {
            return BadRequest();
        }
    }
}
