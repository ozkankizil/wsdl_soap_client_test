using CountryInfoService;
using Microsoft.AspNetCore.Mvc;

public class CountryController : Controller
{
    private readonly CountryInfoServiceSoapTypeClient _client;

    public CountryController()
    {
        _client = new CountryInfoServiceSoapTypeClient(
            CountryInfoServiceSoapTypeClient.EndpointConfiguration.CountryInfoServiceSoap);
    }

    public async Task<IActionResult> Index()
    {
        var countries = await _client.ListOfCountryNamesByCodeAsync();
        var list = countries.Body.ListOfCountryNamesByCodeResult;

        ViewBag.Countries = list;
        return View();
    }

    [HttpPost]
    [Route("Country/GetCapital")]
    public async Task<IActionResult> GetCapital(string code)
    {
        var capital = await _client.CapitalCityAsync(code);
        ViewBag.Capital = capital.Body.CapitalCityResult;

        // Yeniden ülke listesini yollayalım
        var countries = await _client.ListOfCountryNamesByCodeAsync();
        ViewBag.Countries = countries.Body.ListOfCountryNamesByCodeResult;

        return View("Index");
    }
}

