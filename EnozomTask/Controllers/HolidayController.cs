using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestSharp;
using Newtonsoft.Json;
using EnozomTask.Models;
using EnozomTask.Data;
using MySql.Data.MySqlClient;
using Microsoft.EntityFrameworkCore;

namespace EnozomTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HolidayController : ControllerBase
    {
        private readonly AppDbContext _context;

        public HolidayController(AppDbContext context)
        {
            _context = context;
        }
        [HttpGet(template: "GetHolidayFromApi")]
        public async Task<IActionResult> GetHolidayFromApi()
        {
            List<Holiday> holidayList = new List<Holiday>();
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
                holidayList.Add(holidayObj);
            }
            //TODO: transform the response here to suit your needs

            return Ok(holidayList);
        }

        [HttpGet(template: "GetHolidayFromDb")]
        public async Task<IActionResult> GetHolidayFromDb(int id)
        {
            if (id == null)
            {
                return Ok("Not Found");
            }

            var country = await _context.Countries
                .FirstOrDefaultAsync(m => m.id == id);
            if (country == null)
            {
                return Ok("Not Found");
            }
            List<Holiday> holidayList = new List<Holiday>();

            holidayList = _context.Holidays.Where(i => i.countryid == id).ToList();
            return Ok(holidayList);

        }

        [HttpGet(template: "SaveHolidaysToDb")]
        public async Task<IActionResult> SaveCountriesToDb()
        {
            List<Holiday> holidayList = new List<Holiday>();
            foreach (Country? country in await _context.Countries.ToArrayAsync())
            {
                var client = new RestClient($"https://www.googleapis.com/calendar/v3/calendars/en." + country.CountryCca2 + "%23holiday%40group.v.calendar.google.com/events?key=AIzaSyBpSZoCr4xUGsNzmAuxVw_WT0Q4hVW9Bos");
                var request = new RestRequest(Method.GET);
                IRestResponse response = await client.ExecuteAsync(request);
                dynamic responseContent = JsonConvert.DeserializeObject(response.Content);
                if (responseContent.items != null)
                {
                    foreach (dynamic i in responseContent.items)
                    {
                        var holidayObj = new Holiday();
                        holidayObj.HolidayName = i.summary;
                        holidayObj.HolidayStartDate = i.start.date;
                        holidayObj.HolidayEndDate = i.end.date;
                        holidayObj.countryid = country.id;
                        _context.Add(holidayObj);
                    }
                    await _context.SaveChangesAsync();
                }
            }


            return Ok("Added Successfully");
        }

        [HttpPost(template: "addHolidayToDb")]
        public async Task<IActionResult> addHolidayToDb([Bind("countryid,HolidayName,HolidayStartDate,HolidayEndDate")] Holiday holiday)
        {
            _context.Add(holiday);
            await _context.SaveChangesAsync();

            return Ok("Added Successfully");
        }


        [HttpPut(template: "UpdateHolidayInDb")]
        public async Task<IActionResult> Edit(int id, [Bind("countryid,HolidayName,HolidayStartDate,HolidayEndDate")] Holiday holiday)
        {
            if (id != holiday.id)
            {
                return Ok("Not Found");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(holiday);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (_context.Holidays.Any(e => e.id == id))
                    {
                        return Ok("Not Found");
                    }
                    else
                    {
                        throw;
                    }
                }
                return Ok("Updated Successfully");
            }
            return Ok("Updated Fail");
        }



        [HttpDelete, ActionName("DeleteHolidayFromDb")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var holiday = await _context.Holidays.FindAsync(id);
            _context.Holidays.Remove(holiday);
            await _context.SaveChangesAsync();
            return Ok("Delete Successfully");
        }
    }

}
