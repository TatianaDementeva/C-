namespace RucksackProblem.Controllers
{
    public partial class NewTaskController
    {
        public class ThingDataRequest
        {
            public string Name { get; set; }
            public int Weight { get; set; }
            public int Cost { get; set; }
        }
    }
}
