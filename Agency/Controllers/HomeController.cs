using Agency.Data.DAL;
using Agency.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Agency.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _dbContext;
        public HomeController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            return View();
        }


    }
}