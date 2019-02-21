using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tournamentapi.Models;

namespace tournamentapi.Interfaces
{
   
    public interface IMatchesRepository
    {
        Task<IEnumerable<Match>> GetMatchAsync(int id);
        void AddMatch(Match item);
        void Save();
        void UpdateItem(Match item);
        bool MatchExists(int id);


    }
    public class MatchesRepository : IMatchesRepository
    {
        private readonly ApplicationContext _context;

        public MatchesRepository(ApplicationContext context)
        {
            _context = context;
        }

        public void AddMatch(Match item)
        {
            try
            {
                _context.Match.Add(item);
               

            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<Match>> GetMatchAsync(int id)
        {
            try
            {
                
                    return await _context.Match.Where(s => s.TournamentID == id).ToListAsync();

            }
            catch(Exception ex)
            {
                throw ex;
            }
          
        }

        public void UpdateItem(Match item)
        {
            _context.Entry(item).State = EntityState.Modified;
        }
        public void Save()
        {
            _context.SaveChanges();
        }

        public bool MatchExists(int id)
        {
            return _context.Match.Any(e => e.MatchID == id);
        }
    }
}
