using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MVC.Models;
using MVC.Models.IRepository;

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

    public IActionResult Index(int productPage = 2)
    {
        return View(_repo.Products.OrderBy(p => p.Name).Skip((productPage - 1) * PageSize).Take(PageSize));
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
