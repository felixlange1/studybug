using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Studybug.Data;
using Studybug.Models;

namespace Studybug.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ApiService _apiService;

    public HomeController(ApiService apiService, ILogger<HomeController> logger)
    {
        _logger = logger;
        _apiService = apiService;
    }
    

    // public IActionResult Index()
    // {
    //     return View();
    // }

    public async Task<IActionResult> Index(string studyNotes)
    {
        if (string.IsNullOrEmpty(studyNotes))
        {
            return View();
        }

       
        var response = await _apiService.GetFlashcardsAsync(studyNotes);
        var flashcard = new Flashcard
        {
            Answer = response,
            Question = ""
        };
        
        return View(flashcard);
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