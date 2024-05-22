using Agency.Business.Exceptions;
using Agency.Business.Services.Abstracts;
using Agency.Core.Models;
using Agency.Core.RepositoryAbstracts;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agency.Business.Services.Concretes
{
    public class PortfolioService : IPortfolioService
    {
        private readonly IPortfolioRepository _portfolioRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public PortfolioService(IPortfolioRepository portfolioRepository, IWebHostEnvironment webHostEnvironment)
        {
            _portfolioRepository = portfolioRepository;
            _webHostEnvironment = webHostEnvironment;
        }

        public void AddPortfolio(Portfolio portfolio)
        {
            if (portfolio.ImageFile == null) throw new ImageNullException("ImageFile", "ImgeFile null ola bilmez ");
            if (!portfolio.ImageFile.ContentType.Contains("image/")) throw new FileContentTypeException("ImageFile", "File content type error");
            if (portfolio.ImageFile.Length > 2097152) throw new FileSizeException("ImageFile", "Size error");
            string path = _webHostEnvironment.WebRootPath + @"\Upload\Portfolio\" + portfolio.ImageFile.FileName;
            using (FileStream stream = new FileStream(path, FileMode.Create))
            {
                portfolio.ImageFile.CopyTo(stream);
            }
            portfolio.ImageUrl = portfolio.ImageFile.FileName;
            _portfolioRepository.Add(portfolio);
            _portfolioRepository.Commit();

        }

        public void DeletePortfolio(int id)
        {
           var existportfolio=_portfolioRepository.Get(x=>x.Id == id);
            if (existportfolio == null) throw new EntityNotFoundException("", "Entity not found ");
            string path = _webHostEnvironment.WebRootPath + @"\Upload\Portfolio\" + existportfolio.ImageUrl;
            if (!File.Exists(path)) throw new Exceptions.FileNotFoundException("", "File not found");
            File.Delete(path);
            _portfolioRepository.Delete(existportfolio);
            _portfolioRepository.Commit();
        }

        public Portfolio GetPortfolio(Func<Portfolio, bool>? func = null)
        {
          return _portfolioRepository.Get(func);
        }

        public List<Portfolio> GetPortfolios(Func<Portfolio, bool>? func = null)
        {
            return _portfolioRepository.GetAll(func);
        }

        public void UpdatePortfolio(int id, Portfolio portfolio)
        {
            var existportfolio = _portfolioRepository.Get(x => x.Id == id);
            if (existportfolio == null) throw new EntityNotFoundException("", "Entity not found ");
         
            if (portfolio.ImageFile != null)
            {
                if (!portfolio.ImageFile.ContentType.Contains("image/")) throw new FileContentTypeException("ImageFile", "File content type error");
                if (portfolio.ImageFile.Length > 2097152) throw new FileSizeException("ImageFile", "Size error");
                string path = _webHostEnvironment.WebRootPath + @"\Upload\Portfolio\" + portfolio.ImageFile.FileName;
                using (FileStream stream = new FileStream(path, FileMode.Create))
                {
                    portfolio.ImageFile.CopyTo(stream);
                }
               
                string path1 = _webHostEnvironment.WebRootPath + @"\Upload\Portfolio\" + existportfolio.ImageUrl;
                if (!File.Exists(path1)) throw new Exceptions.FileNotFoundException("", "File not found");
                File.Delete(path1);
                existportfolio.ImageUrl = portfolio.ImageFile.FileName;
            }
            existportfolio.Title = portfolio.Title;
            existportfolio.Subtitle = portfolio.Subtitle;
            _portfolioRepository.Commit();
           
        }
    }
}
