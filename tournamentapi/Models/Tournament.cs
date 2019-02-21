using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace tournamentapi.Models
{
    public class Tournament
    {
        [Key]
        public int TournamentID { get; set; }
        public String TournamentName { get; set; }

    }
}
