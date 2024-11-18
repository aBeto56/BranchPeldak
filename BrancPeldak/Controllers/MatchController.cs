using BrancPeldak.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BrancPeldak.Controllers
{
    [Route("matchdata")]
    [ApiController]
    public class MatchController : ControllerBase
    {
        [HttpPost]
        public ActionResult<Matchdatum> Post(CreateDatDto createDatDto)
        {
            var data = new Matchdatum()
            {
                Id = Convert.ToString(Guid.NewGuid()),
                SubbedIn = createDatDto.Subbed_In,
                Try = createDatDto.Try,
                Goal = createDatDto.Goal,
                Fault = createDatDto.Fault,
                PlayerId = createDatDto.PlayerId,
            };
            if (data != null)
            {
                using (var context = new BasketteamContext())
                {
                    context.Matchdata.Add(data);
                    context.SaveChanges();
                    return StatusCode(201, data);
                }
            }
            return BadRequest();
        }
        [HttpGet]

        public ActionResult<Matchdatum> Get()
        {
            using (var context = new BasketteamContext())
            {
                return Ok(context.Matchdata.ToList());
            }
        }
        [HttpGet("{id}")]
        public ActionResult<Matchdatum> GetById(string id)
        {
            using (var context = new BasketteamContext())
            {
                var data = context.Matchdata.FirstOrDefault(x => x.Id == Convert.ToString(id));
                if (data != null)
                {
                    return Ok(data);
                }
                return NotFound();
            }
        }
        [HttpPut("{id}")]

        public ActionResult<Matchdatum> Put(UpdateDatDto updateDatDto, string id)
        {
            using (var context = new BasketteamContext())
            {
                var existingData = context.Matchdata.FirstOrDefault(data => data.Id == Convert.ToString(id));
                if (existingData != null)
                {
                    existingData.Try = updateDatDto.Try;
                    existingData.Goal = updateDatDto.Goal;
                    existingData.Fault = updateDatDto.Fault;
                    existingData.SubbedOut = updateDatDto.Subb_Out;

                    context.Matchdata.Update(existingData);
                    context.SaveChanges();

                    return Ok(existingData);
                }
                return NotFound();
            }
        }
        [HttpDelete("{id}")]
        public ActionResult Delete(string id)
        {
            using (var context = new BasketteamContext())
            {
                var data = context.Matchdata.FirstOrDefault(data => data.Id == Convert.ToString(id));
                if (data != null)
                {
                    context.Matchdata.Remove(data);
                    context.SaveChanges();
                    return Ok(new { message = "Sikeres törlés" });
                }
                return NotFound();
            }
        }

    }
}
