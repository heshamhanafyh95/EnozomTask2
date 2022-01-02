using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestSharp;
using Newtonsoft.Json;
using EnozomTask.Models;

namespace EnozomTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HolidayController : ControllerBase
    {
        [HttpGet(template: "GetCountriesFromApi")]
        public async Task<IActionResult> GetCountriesFromApi()
        {
            List<Holiday> countryList = new List<Holiday>();
            var client = new RestClient($"https://www.googleapis.com/calendar/v3/calendars/en.eg%23holiday%40group.v.calendar.google.com/events?key=AIzaSyBpSZoCr4xUGsNzmAuxVw_WT0Q4hVW9Bos");
            var request = new RestRequest(Method.GET);
            IRestResponse response = await client.ExecuteAsync(request);
            dynamic responseContent = JsonConvert.DeserializeObject(response.Content);
            foreach (dynamic i in responseContent.items)
            {
                var holidayObj = new Holiday();
                holidayObj.HolidayName = i.summary;
                holidayObj.HolidayStartDate = i.start.date;
                holidayObj.HolidayEndDate = i.end.date;
                countryList.Add(holidayObj);
            }
            //TODO: transform the response here to suit your needs

            return Ok(countryList);
        }

    }

}
