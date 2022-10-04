using GameStore.BusinessLogic.Dtos;
using GameStore.BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GameStore.Controllers
{
    [Route("api/[controller]"), ApiController]
    public class GenreController : Controller
    {
        private readonly IGenreService service;

        public GenreController(IGenreService service)
        {
            this.service = service;
        }

        [HttpGet("{genreId}")]
        public async Task<ActionResult<GenreDto>> GetById(Guid genreId)
        {
            return Ok(await service.GetByIdAsync(genreId));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GenreDto>>> GetPagedList(int page = 1)
        {
            return Ok(await service.GetPagedListAsync(page, pageSize: 50));
        }

        [HttpGet("details")]
        public async Task<ActionResult<IEnumerable<GenreWithDetailsDto>>> GetWithDetails()
        {
            return Ok(await service.GetGenresWithDetailsAsync());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateGenreDto model)
        {
            await service.CreateAsync(model);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateGenreDto model)
        {
            await service.UpdateAsync(model);
            return NoContent();
        }

        [HttpDelete("{genreId}")]
        public async Task<IActionResult> Delete(Guid genreId)
        {
            await service.DeleteAsync(genreId);
            return NoContent();
        }
    }
}
