using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using static RucksackProblem.Controllers.HistoryController;

namespace RucksackProblem.Controllers
{
    [Route("api/[controller]")]
    public class RunningTasksController : Controller
    {
        // GET: api/<controller>
        [HttpGet]
        public IEnumerable<RucksackDataResponse> Get()
        {
            List<RucksackDataResponse> Rucksacks = new List<RucksackDataResponse>();

            using (ApplicationContext db = new ApplicationContext())
            {
                var RucksacksDone = db.Rucksacks.Where(p => p.Status == false).ToList();

                foreach (Rucksack rucksack in RucksacksDone)
                {
                    Rucksacks.Add(new RucksackDataResponse { Id = rucksack.Id, Name = rucksack.Name, Weight = rucksack.Weight, Cost = rucksack.Cost, Status = rucksack.Status });
                }

            }
            return Rucksacks;
        }

        // POST api/<controller>
        [HttpPost("[action]")]
        public void Abort([FromBody]int value)
        {
            if (!ModelState.IsValid)
            {
                Debug.WriteLine("POST get bad value");
            }


            MainThread.Abort(value);
        }
    }
}
