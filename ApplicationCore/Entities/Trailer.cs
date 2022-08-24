using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ApplicationCore.Entities
{
    public class Trailer
    {
        public int Id { get; set; }

        [MaxLength(2084)]
        public string Name { get; set; }

        [MaxLength(2084)]
        public string TrailerUrl { get; set; } 

        public int MovieId { get; set; }

        // Navigation Property
        public Movie Movie { get; set; } 
    }
}
