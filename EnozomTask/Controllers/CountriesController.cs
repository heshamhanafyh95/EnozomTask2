using EnozomTask.Data;
using EnozomTask.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using RestSharp;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EnozomTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    
    public class CountriesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CountriesController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetCountriesFromApi()
        {
            List<Country> countryList = new List<Country>();
            var client = new RestClient($"https://restcountries.com/v3.1/all");
            var request = new RestRequest(Method.GET);
            IRestResponse response = await client.ExecuteAsync(request);
            dynamic responseContent = JsonConvert.DeserializeObject(response.Content);
            foreach (dynamic i in responseContent)
            {
                var countryObj = new Country();
                countryObj.CountryName = i.name.common;
                countryObj.CountryCca2 = i.cca2;
                countryList.Add(countryObj);
            }
            //TODO: transform the response here to suit your needs

            return Ok(countryList);
        }

        [HttpGet(template: "GetCountriesFromDb")]
        public async Task<IActionResult> GetCountriesFromDb(int pageIndex, int pageSize)
        {
            List<Country> countryList = new List<Country>();

            countryList = _context.Countries.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            return Ok(countryList);

        }


        [HttpGet(template: "SaveCountriesToDb")]
        public async Task<IActionResult> SaveCountriesToDb()
        {
            List<Country> countryList = new List<Country>();
            var client = new RestClient($"https://restcountries.com/v3.1/all");
            var request = new RestRequest(Method.GET);
            IRestResponse response = await client.ExecuteAsync(request);
            dynamic responseContent = JsonConvert.DeserializeObject(response.Content);
            foreach (dynamic i in responseContent)
            {
                var countryObj = new Country();
                countryObj.CountryName = i.name.common;
                countryObj.CountryCca2 = i.cca2;
                _context.Add(countryObj);
                await _context.SaveChangesAsync();
            }

            return Ok("Added Successfully");
        }
        
    }
}
