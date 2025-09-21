using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
    

    public async Task<IActionResult> Index(string studyNotes)
    {
        if (string.IsNullOrEmpty(studyNotes))
        {
            return View();
        }
        
        var response = await _apiService.GetFlashcardsAsync(studyNotes);
        
        List<Flashcard> flashcards = new();

        try
        {
            var jObj = JObject.Parse(response);
            var result = jObj["result"]?.ToString().Trim();

            if (!string.IsNullOrEmpty(result) && (result.StartsWith("[") || result.StartsWith("{")))
            {
                    flashcards = JsonConvert.DeserializeObject<List<Flashcard>>(result);
            }
            else
            {
                ViewBag.Message = result; // plain string from API
            }
        }
        catch (JsonReaderException)
        {

            ViewBag.Message = response;
        }

        return View(flashcards);

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