using GameStore.BusinessLogic.Interfaces;
using GameStore.BusinessLogic.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace GameStore.Controllers
{
    [Route("api/[controller]"), ApiController]
    public class GamesController : Controller
    {
        private readonly IGameService service;

        public GamesController(IGameService service)
        {
            this.service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GameDto>>> Get(int page = 1)
        {
            return Ok(await service.GetPagedListWithDetailsAsync(page));
        }

        [HttpGet("{gameId}")]
        public async Task<ActionResult<GameDto>> GetById(Guid gameId)
        {
            return Ok(await service.GetByIdWithDetailsAsync(gameId));
        }

        [HttpPost("filter")]
        public async Task<ActionResult<IEnumerable<GameDto>>> GetFiltered([FromBody] FilterGamesDto filter)
        {
            return Ok(await service.GetFilteredGamesAsync(filter));
        }


        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateGameDto model)
        {
            await service.CreateAsync(model);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateGameDto model)
        {
            await service.UpdateAsync(model);
            return NoContent();
        }

        [HttpDelete("{gameId}")]
        public async Task<IActionResult> Delete(Guid gameId)
        {
            await service.DeleteAsync(gameId);
            return NoContent();
        }
    }
}
