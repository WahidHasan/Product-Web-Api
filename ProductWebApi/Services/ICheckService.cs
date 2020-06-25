using ProductWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductWebApi.Services
{
    public interface ICheckService
    {
        Task<int> CategoryIdFetchByName(Product product);
        Task<int> CountryIdFetchByName(Product product);
    }
}
