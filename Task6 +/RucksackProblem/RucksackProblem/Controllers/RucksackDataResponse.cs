namespace RucksackProblem.Controllers
{
    public partial class HistoryController
    {
        public class RucksackDataResponse
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public int Weight { get; set; }
            public int Cost { get; set; }
            public bool Status { get; set; }
        }
    }
}
