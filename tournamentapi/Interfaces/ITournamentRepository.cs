using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tournamentapi.Models;
using Microsoft.Extensions.Options;

namespace tournamentapi.Interfaces
{
    public interface ITournamentRepository
    {
        Task<Tournament> GetTournamentsAsync(int id);
        Task<IEnumerable<Tournament>> GetTournamentAllAsync();
        Task AddTournament(Tournament item);
        void Save();
    }
    public class TournamentRepository : ITournamentRepository
    {
        private readonly ApplicationContext _context;

        public TournamentRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task AddTournament(Tournament item)
        {
            try
            {
                await _context.Tournaments.AddAsync(item);
                
            }catch(Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<Tournament>> GetTournamentAllAsync()
        {
            try
            {
                return await _context.Tournaments.ToListAsync();
            }catch(Exception ex)
            {
                throw ex;
            }
            
        }
        public async Task<Tournament> GetTournamentsAsync(int id)
        {
            try
            {
                    return await _context.Tournaments.FindAsync(id);
               
            }
            catch(Exception ex)
            {
                throw ex;
            }
            
        }

        public void Save()
        {
            _context.SaveChangesAsync();
        }
    }
}
