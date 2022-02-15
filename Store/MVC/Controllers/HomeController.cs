using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MVC.Models;
using MVC.Models.IRepository;
using MVC.Models.ViewModels;

namespace MVC.Controllers;

public class HomeController : Controller
{
    private IStoreRepository _repo;
    private readonly ILogger<HomeController> _logger;
    public int PageSize = 2;

    public HomeController(ILogger<HomeController> logger, IStoreRepository repo)
    {
        _logger = logger;
        _repo = repo;
    }

    public IActionResult Index(string category, int productPage = 1)
    {
        return View(new ProductsListViewModel
        {
            Products = _repo.Products
                .Where(p => category == null || p.Category == category)
                .OrderBy(p => p.ProductId)
                .Skip((productPage - 1) * PageSize)
                .Take(PageSize),
            PagingInfo = new PagingInfo
            {
                CurrentPage = productPage,
                ItemsPerPage = PageSize,
                TotalItems = _repo.Products.Count()
            }
        });
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
