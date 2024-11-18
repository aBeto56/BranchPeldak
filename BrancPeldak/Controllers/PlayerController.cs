using BrancPeldak.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BrancPeldak.Controllers
{
    [Route("players")]
    [ApiController]
    public class PlayerController : ControllerBase
    {
        [HttpPost]
        public ActionResult<Player> Post(CreatePlayerDto createPlayer)
        {
            var player = new Player
            {
                Id = Convert.ToString(Guid.NewGuid()),
                Name = createPlayer.Name,
                Weight = createPlayer.Weight,
                Height = createPlayer.Height,
                CreatedTime = DateTime.Now,
            };
            if (player != null)
            {
                using (var context = new BasketteamContext())
                {
                    context.Players.Add(player);
                    context.SaveChanges();
                    return StatusCode(201, player);
                }
            }
            return BadRequest();
        }
        [HttpGet]

        public ActionResult<Player> Get()
        {
            using (var context = new BasketteamContext())
            {
                return Ok(context.Players.ToList());
            }
        }
        [HttpGet("{id}")]
        public ActionResult<Player> GetById(string id)
        {
            using (var context = new BasketteamContext())
            {
                var player = context.Players.FirstOrDefault(x => x.Id == id);
                if (player != null)
                {
                    return Ok(player);
                }
                return NotFound();
            }
        }
        [HttpPut("{id}")]
        public ActionResult<Player> Put(UpdatePlayerDto updatePlayerDto, string id)
        {
            using (var context = new BasketteamContext())
            {
                var existingPlayer = context.Players.FirstOrDefault(player => player.Id == id);

                if (existingPlayer != null)
                {
                    existingPlayer.Id = updatePlayerDto.Id;
                    existingPlayer.Name = updatePlayerDto.Name;
                    existingPlayer.Height = updatePlayerDto.Height;
                    existingPlayer.Weight = updatePlayerDto.Weight;

                    context.Players.Update(existingPlayer);
                    context.SaveChanges();
                    return Ok(existingPlayer);
                }
                return NotFound();
            }
        }
        [HttpDelete("{id}")]
        public ActionResult Delete(string id)
        {
            using (var context = new BasketteamContext())
            {
                var player = context.Players.FirstOrDefault(player => player.Id == id);
                if (player != null)
                {
                    context.Players.Remove(player);
                    context.SaveChanges();
                    return Ok(new { message = "Sikeres törlés" });
                }
                return NotFound();
            }
        }
        [HttpGet("playerData/{id}")]
        public ActionResult<Player> Get(string id)
        {
            using (var contex = new BasketteamContext())
            {
                var player = contex.Players.Include(x => x.Matchdata).FirstOrDefault(player => player.Id == id);
                if (player != null)
                {
                    return Ok(player);
                }
                return NotFound();
            }
        }

    }
}
