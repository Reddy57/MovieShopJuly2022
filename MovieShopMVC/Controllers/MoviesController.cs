using Microsoft.AspNetCore.Mvc;

namespace MovieShopMVC.Controllers
{
    public class MoviesController : Controller
    {
        public IActionResult Details(int id) {

            // go to database and get the movie information by
            // movie id and send the data (Model) to the view
            // ADO.NET 
            // Dapper Stackoverflow -> Micro ORM
            // Entity Framework Core => Full ORM

            // Select * from Movies where id =12;
            // Code is Maintenable, Reusable, Readable, extensible, testable
            // layers => Layered architecture
            // Onion, Clean 

            return View();
        }
    }
}
