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
    public class TournamentsController : ControllerBase
    {
        
        private ITournamentRepository _TournamentRepo;
        private IMatchesRepository _MatchesRepo;
        private ITournamentAlgorithm _tournamentAlgorithm;
        public TournamentsController(ITournamentRepository TournamentRepo, IMatchesRepository MatchesRepo, ITournamentAlgorithm tournamentAlgorithm)
        {
            _TournamentRepo = TournamentRepo;
            _MatchesRepo = MatchesRepo;
            _tournamentAlgorithm = tournamentAlgorithm;
        }

        [HttpGet]
        public async Task<IEnumerable<Tournament>> GetTournaments()
        {
            return await _TournamentRepo.GetTournamentAllAsync();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTournament([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var tournament = await _TournamentRepo.GetTournamentsAsync(id);

            if (tournament == null)
            {
                return NotFound();
            }

            return Ok(tournament);
        }
  
        // POST: api/Tournaments
        [HttpPost]
        public async Task<IActionResult> PostTournament([FromBody] List<Team> value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Tournament tournament = new Tournament();
            var temp = value[value.Count - 1];
            tournament.TournamentName = temp.TeamName;

            await _TournamentRepo.AddTournament(tournament);
            _TournamentRepo.Save();

            value.RemoveAt(value.Count - 1);
            _tournamentAlgorithm.ListMatches(value, tournament.TournamentID);
           

            return Ok(tournament);
        }
    }
}