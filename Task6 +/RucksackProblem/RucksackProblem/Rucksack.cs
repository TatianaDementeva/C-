using System;
using System.Collections.Generic;

namespace RucksackProblem
{
    public class Rucksack
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Weight { get; set; }
        public int Cost { get; set; }
        public bool Status { get; set; }
        public TimeSpan TimeWork { get; set; }
        public List<Thing> Things { get; set; } //вещи для задачи
    }
}
