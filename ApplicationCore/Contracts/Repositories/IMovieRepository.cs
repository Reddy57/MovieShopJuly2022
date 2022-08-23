using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Contracts.Repositories
{
    public interface IMovieRepository
    {
        // CRUD methods
        // get top 30 grossing movies movies from database

        List<Movie> GetTop30GrossingMovies();
        
        // Get Movie By Id
        // Get Movie By Genre



    }
}
