using Hot_Tours_BL.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebAppHotTours.Models;
using System.Linq;

namespace WebAppHotTours.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ToursController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };
        private static readonly IList<Tour> tours = new List<Tour>();

        private readonly ILogger<ToursController> _logger;

        public ToursController(ILogger<ToursController> logger)
        {
            _logger = logger;
        }


        [HttpGet(Name = "GetTours")]
        public IEnumerable<Tour> GetTours()
        {
            return tours;
        }

        [HttpPost(Name = "AddTour")]
        public Tour AddTour([FromBody] TourRequest tour)
        {
            tour.Check();
            Tour addtour = new Tour()
            {
                AmountOfDays = tour.AmountOfDays,
                AmountOfMan = tour.AmountOfMan,
                Date = tour.Date,
                Direction = tour.Direction,
                Guid = Guid.NewGuid(),
                PriceForMan = tour.PriceForMan,
                Surcharge = tour.Surcharge,
                WiFi = tour.WiFi
            };
            addtour.TotalPrice = tour.AmountOfDays * tour.AmountOfMan * tour.PriceForMan;
            tours.Add(addtour);
            return addtour;
        }

        [HttpPut("{guid}")]
        public Tour Update([FromRoute] Guid guid, [FromBody] TourRequest tour)
        {
            var taggetTour = tours.FirstOrDefault(x => x.Guid == guid);
            if (taggetTour != null)
            {
                tour.Check();
                taggetTour.AmountOfDays = tour.AmountOfDays;
                taggetTour.AmountOfMan = tour.AmountOfMan;
                taggetTour.Date = tour.Date;
                taggetTour.Direction = tour.Direction;
                taggetTour.PriceForMan = tour.PriceForMan;
                taggetTour.Surcharge = tour.Surcharge;
                taggetTour.TotalPrice = taggetTour.AmountOfDays * taggetTour.AmountOfMan * taggetTour.PriceForMan;
                taggetTour.WiFi = tour.WiFi;
            }
            return taggetTour;
        }

        [HttpDelete("{guid}")]
        public bool DeleteUser(Guid guid)
        {
            var taggetTour = tours.FirstOrDefault(x => x.Guid == guid);
            if (taggetTour != null)
            {
                return tours.Remove(taggetTour);
            }
            return false;
        }

        [HttpGet("Statistics")]
        public ToursStats GetStats()
        {
            var statistic = new ToursStats()
            {
                SurchargeAmount = tours.Where(x => x.Surcharge > 0).Count(),
                TotalSum = tours.Select(x => x.TotalPrice).Sum(),
                TotalSurcharge = tours.Select(x => x.Surcharge).Sum(),
                TourAmount = tours.Count(),
            };

            return statistic;
        }
    }
}