using Agency.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agency.Business.Services.Abstracts
{
    public  interface IPortfolioService
    {
        void AddPortfolio(Portfolio portfolio);
        void DeletePortfolio(int id);
        void UpdatePortfolio(int id,Portfolio portfolio);
        Portfolio GetPortfolio(Func<Portfolio,bool>? func=null);
        List<Portfolio> GetPortfolios(Func<Portfolio,bool>? func=null);
    }
}
