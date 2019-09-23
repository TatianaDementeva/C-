using System.Collections.Generic;

namespace RucksackProblem.Controllers
{
          public class FullRucksack
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public int Weight { get; set; }
            public int Cost { get; set; }
            public bool Status { get; set; }
            public string TimeWork { get; set; }
            public List<SimpleThing> Things { get; set; }
        }
}
