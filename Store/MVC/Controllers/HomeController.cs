using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MVC.Models;
using MVC.Models.IRepository;

namespace MVC.Controllers;

public class HomeController : Controller
{
    private IStoreRepository _repo;
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger, IStoreRepository repo)
    {
        _logger = logger;
        _repo = repo;
    }

    public IActionResult Index()
    {
        return View(_repo.Products);
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
