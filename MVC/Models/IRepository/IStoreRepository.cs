using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC.Models.IRepository
{
    public interface IStoreRepository
    {
        IQueryable<Product> Products { get; set; }
    }
}