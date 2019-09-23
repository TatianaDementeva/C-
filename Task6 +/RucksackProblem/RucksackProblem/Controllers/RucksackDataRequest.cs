namespace RucksackProblem.Controllers
{
    public partial class NewTaskController
    {
        public class RucksackDataRequest
            {
                public string NameRucksack { get; set; }
                public int WeightRucksack { get; set; }
                public ThingDataRequest[] Things;
            }
    }
}
