using System.Diagnostics;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using StationControl.Services.Interfaces;


namespace StationControl.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IDeputyRepository _deputyRepo;
    public HomeController(ILogger<HomeController> logger, IDeputyRepository deputyRepo)
    {
        _logger = logger;
        _deputyRepo = deputyRepo;
    }

    public IActionResult Index()
    {
        
        return View();
    }

    
}
