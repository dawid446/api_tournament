using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace tournamentapi.Models
{
    public class Match
    {
        [Key]
        public int MatchID { get; set; }

        
        public int? Score { get; set; }

       
        public int? Score1 { get; set; }

       
        public int Queue { get; set; }


        public bool isPlayed { get; set; }
        public bool isBreak { get; set; }


        public string TeamName { get; set; }
      

        public string TeamName1 { get; set; }

        public int TournamentID { get; set; }
        public virtual Tournament Tournament { get; set; }


    }
}
