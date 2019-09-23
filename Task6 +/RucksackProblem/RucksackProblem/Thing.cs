namespace RucksackProblem
{
    public class Thing
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Weight { get; set; }
        public int Cost { get; set; }
        public bool Put { get; set; }

        public int RucksackId { get; set; }
        public Rucksack Rucksack { get; set; }  //рюкзак которому принадлежит вещь
    }
}
