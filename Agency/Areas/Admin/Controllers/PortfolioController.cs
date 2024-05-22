using Agency.Business.Exceptions;
using Agency.Business.Services.Abstracts;
using Agency.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace Agency.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PortfolioController : Controller
    {
        private readonly IPortfolioService _portfolioService;
        public PortfolioController(IPortfolioService portfolioService)
        {
            _portfolioService = portfolioService;
        }

        public IActionResult Index()
        {
            var portfolios = _portfolioService.GetPortfolios();
            return View(portfolios);
        }
        public IActionResult Create()
        {
            return View();
        }
        public IActionResult Delete(int id)
        {
            var existportfolio=_portfolioService.GetPortfolio(x=>x.Id == id);
            if (existportfolio == null) return NotFound();
            return View(existportfolio);
        }
        public IActionResult Update(int id)
        {
            var portfolio = _portfolioService.GetPortfolio(x => x.Id == id);
            if (portfolio == null) return NotFound();
            return View(portfolio);
        }
        [HttpPost]
        public IActionResult Create(Portfolio portfolio)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            try
            {
                _portfolioService.AddPortfolio(portfolio);
            }
            catch(ImageNullException ex)
            {
                ModelState.AddModelError(ex.Propertname, ex.Message);
                return View();
            }
            catch (FileContentTypeException ex)
            {
                ModelState.AddModelError(ex.Propertyname, ex.Message);
                return View();
            }
            catch(FileSizeException ex)
            {
                ModelState.AddModelError(ex.Propertyname, ex.Message);
                return View();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public  IActionResult DeletePortfolio(int id)
        {
            try
            {
                _portfolioService.DeletePortfolio(id);
            }
            catch(EntityNotFoundException ex)
            {
                return NotFound();
            }
            catch(Business.Exceptions.FileNotFoundException ex)
            {
                return NotFound();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return RedirectToAction(nameof(Index));
        }
        [HttpPost  ]
        public IActionResult Update(Portfolio portfolio)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            try
            {
                _portfolioService.UpdatePortfolio(portfolio.Id, portfolio);
            }
            catch(EntityNotFoundException ex)
            {
                return NotFound();
            }
            catch(FileSizeException ex)
            {
                ModelState.AddModelError(ex.Propertyname, ex.Message);
                return View();
            }
            catch(FileContentTypeException ex)
            {
                ModelState.AddModelError(ex.Propertyname, ex.Message);
                return View();
            }
            catch(Business.Exceptions.FileNotFoundException ex)
            {
                ModelState.AddModelError(ex.PropertyName, ex.Message);
                return View();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return RedirectToAction(nameof(Index));
        }

    }
}
