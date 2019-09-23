using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace RucksackProblem.Controllers
{
    [Route("api/[controller]")]
    public partial class HistoryController : Controller
    {
        // GET: api/<controller>
        [HttpGet]
        public IEnumerable<RucksackDataResponse> Get()
        {
            List<RucksackDataResponse> Rucksacks = new List<RucksackDataResponse>();

            using (ApplicationContext db = new ApplicationContext())
            {
                var RucksacksDone = db.Rucksacks.Where(p => p.Status == true).ToList();
                
                foreach (Rucksack rucksack in RucksacksDone)
                {
                    Rucksacks.Add(new RucksackDataResponse { Id = rucksack.Id, Name = rucksack.Name, Weight = rucksack.Weight, Cost = rucksack.Cost, Status = rucksack.Status });
                }
                   
            }
            return Rucksacks;
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public FullRucksack Get(int id)
        {
            Debug.WriteLine("Start get" + id);
            Rucksack Rucksack;
            

            List<Thing> Things;
            List<SimpleThing> myThings = new List<SimpleThing>();

            using (ApplicationContext db = new ApplicationContext())
            {
                Rucksack = db.Rucksacks.Find(id);
                Things = db.Things.Where(p => p.Rucksack.Id == id).ToList();
            }
            foreach (Thing thing in Things)
            {
                SimpleThing newThing = new SimpleThing
                {
                    Id = thing.Id,
                    Name = thing.Name,
                    Weight = thing.Weight,
                    Cost = thing.Cost,
                    Put = thing.Put
                };
                myThings.Add(newThing);
            }
            FullRucksack newRucksack = new FullRucksack
            {
                Things = myThings,
                Id = Rucksack.Id,
                Cost = Rucksack.Cost,
                Name = Rucksack.Name,
                Weight = Rucksack.Weight,
                Status = Rucksack.Status,
                TimeWork = Rucksack.TimeWork.TotalSeconds.ToString()
            };

            return newRucksack;
        }

        // POST api/<controller> Empty
        [HttpPost("[action]")]
        public void Post([FromBody]string value)
        {           

        }

        // PUT api/<controller>/5 Empty
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5 Empty
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
