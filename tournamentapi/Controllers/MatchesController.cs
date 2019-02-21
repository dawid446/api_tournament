using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using tournamentapi.Interfaces;
using tournamentapi.Models;

namespace tournamentapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CORS")]
    public class MatchesController : ControllerBase
    {
        

        private IMatchesRepository _MatchesRepo;

        public MatchesController(IMatchesRepository matchesRepository)
        {
            _MatchesRepo = matchesRepository;
        }

       
        [HttpGet("{id}")]
        public async Task<IActionResult> GetMatch([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var match = await _MatchesRepo.GetMatchAsync(id);

            if (match == null)
            {
                return NotFound();
            }

            return Ok(match);
        }

        // PUT: api/Matches/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMatch([FromRoute] int id, [FromBody] Match match)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != match.MatchID)
            {
                return BadRequest();
            }

            //_context.Entry(match).State = EntityState.Modified;
            _MatchesRepo.UpdateItem(match);

            try
            {
                 _MatchesRepo.Save();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_MatchesRepo.MatchExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(match);
        }

       
    }
}