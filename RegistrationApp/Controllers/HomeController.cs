using System.Diagnostics;
using System.Threading.Tasks;
using ErrorOr;
using Microsoft.AspNetCore.Mvc;
using RegistrationApp.Models;
using RegistrationApp.Services;

namespace RegistrationApp.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IUserService userService;

    public HomeController(ILogger<HomeController> logger, IUserService userService)
    {
        _logger = logger;
        this.userService = userService;
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Index(User user)
    {
        if(!ModelState.IsValid)
        {
            return View(user);
        }

        var result = await userService.RegisterUserAsync(user);
        if (result.IsError)
        {
            foreach (var error in result.Errors)
            {
                if(error.Code == "User.DuplicateEmail")
                {
                    ModelState.AddModelError("Email", error.Description);
                }
                else
                {
                    ModelState.AddModelError("", error.Description);
                }                    
            }
            return View(user);
        }

        TempData["UserName"] = user.UserName;
        return RedirectToAction("Success");
    }

    public IActionResult Success()
    {
        return View();
    }

    //public IActionResult Privacy()
    //{
    //    return View();
    //}

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
