using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RucksackProblem.Controllers
{
    [Route("api/[controller]")]
    public partial class NewTaskController : Controller
    {
        // POST api/<controller>
        [HttpPost("[action]")]
        public void NewRucksack([FromBody]RucksackDataRequest value)
        {           
            if (value == null || !ModelState.IsValid)
            {
                Debug.WriteLine("POST get bad value");
            }

            using (ApplicationContext db = new ApplicationContext())
            {
                Rucksack NewRucksack = new Rucksack { Name = value.NameRucksack, Weight = value.WeightRucksack, Cost = 0, Status = false };

                db.Rucksacks.Add(NewRucksack);
                db.SaveChanges();

                foreach(ThingDataRequest thing in value.Things)
                {
                    Thing NewThing = new Thing { Name = thing.Name, Weight = thing.Weight, Cost = thing.Cost, Put = false, Rucksack = NewRucksack};
                    db.Things.AddRange(new List<Thing> { NewThing });
                }
                db.SaveChanges();

                MainThread.CreateThread(NewRucksack.Id);
            }
        }
    }
}
