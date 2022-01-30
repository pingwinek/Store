using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MVC.Data;
using MVC.Models.IRepository;

namespace MVC.Models.Repository
{
    public class StoreRepository : IStoreRepository
    {
        private StoreDbContext _context;
        public StoreRepository(StoreDbContext context)
        {
            _context = context;
        }
        
        public IQueryable<Product> Products { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}