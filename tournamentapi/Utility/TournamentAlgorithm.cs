using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tournamentapi.Interfaces;
using tournamentapi.Models;
using static tournamentapi.Controllers.TournamentsController;

namespace tournamentapi.Controllers
{
    public interface ITournamentAlgorithm
    {
        void ListMatches(List<Team> ListTeam,int tournamentID);
    }
    public class TournamentAlgorithm : ITournamentAlgorithm
    {
        private IMatchesRepository _MatchesRepo;

        public TournamentAlgorithm(IMatchesRepository matchesRepository)
        {
            _MatchesRepo = matchesRepository;

        }

        public void ListMatches(List<Team> ListTeam, int tournamentID)
        {
            if (ListTeam.Count % 2 != 0)
            {
                ListTeam.Add(new Team { TeamName = "przerwa" }); // If odd number of teams add a dummy
            }
            ListTeam = ListTeam.OrderBy(a => Guid.NewGuid()).ToList();

            int numTeams = ListTeam.Count();
            int numDays = (numTeams - 1); // Days needed to complete tournament
            int halfSize = numTeams / 2;

            List<Team> teams = new List<Team>();

            teams.AddRange(ListTeam); // Add teams to List and remove the first team
            teams.RemoveAt(0);

            int teamsSize = teams.Count;

            for (int day = 0; day < numDays; day++)
            {

                int teamIdx = day % teamsSize;
                Match match = new Match();

                match.isPlayed = false;
                match.Queue = day + 1;
                match.TeamName = ListTeam.ElementAt(0).TeamName;
                match.TeamName1 = teams.ElementAt(teamIdx).TeamName;
                match.TournamentID = tournamentID;
                if(match.TeamName.Equals("przerwa") || match.TeamName1.Equals("przerwa"))
                {
                    match.isBreak = true;
                }

                _MatchesRepo.AddMatch(match);
                _MatchesRepo.Save();
                //_context.Match.Add(match);
                //_context.SaveChanges();

                for (int idx = 1; idx < halfSize; idx++)
                {
                    int firstTeam = (day + idx) % teamsSize;
                    int secondTeam = (day + teamsSize - idx) % teamsSize;

                    Match match1 = new Match();

                    match.isPlayed = false;
                    match1.TeamName = teams.ElementAt(firstTeam).TeamName;
                    match1.TeamName1 = teams.ElementAt(secondTeam).TeamName;
                    match1.Queue = day + 1;
                    match1.TournamentID = tournamentID;
                    if (match1.TeamName.Equals("przerwa") || match1.TeamName1.Equals("przerwa"))
                    {
                        match1.isBreak = true;
                    }

                    _MatchesRepo.AddMatch(match1);
                    _MatchesRepo.Save();
                    //_context.Match.Add(match1);
                    //_context.SaveChanges();
               
                }

            }
           
        }
    }
}
