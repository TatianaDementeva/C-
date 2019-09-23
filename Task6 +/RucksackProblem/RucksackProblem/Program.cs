using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace RucksackProblem
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
            using (ApplicationContext db = new ApplicationContext())
            {
                var RucksacksNotDone = db.Rucksacks.Where(p => p.Status == false).ToList();
                if(RucksacksNotDone != null)
                {
                    foreach(Rucksack rucksack in RucksacksNotDone)
                    {
                        MainThread.CreateThread(rucksack.Id);
                    }
                }

            }
        }
        
        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
    public class MainThread
    {
        private static Dictionary<int, Thread> tasks = new Dictionary<int, Thread>();

        public static void CreateThread( int idRucksack)
        {

            Debug.WriteLine("I start create Thread");
            tasks.Add(idRucksack, new Thread(new ParameterizedThreadStart(FillingRucksack)));
            
            tasks[idRucksack].Start(idRucksack); // запускаем поток

        }
        public static void Abort( int idRucksack)
        {
            tasks[idRucksack].Abort();
            tasks[idRucksack].Join();

        }
        public static void FillingRucksack(object x)
        {
            
            int idRucksack = (int)x;

            Debug.WriteLine("I start filling rucksack {0}", idRucksack);

            Rucksack myRucksack;
            List<Thing> Things = new List<Thing>();

            using (ApplicationContext db = new ApplicationContext())
            {
                myRucksack = db.Rucksacks.Find(idRucksack);
                Things = db.Things.Where(p => p.Rucksack.Id == idRucksack ).ToList();
            }

            var FinishTime = new DateTime();
            var StartTime = DateTime.Now;
            Thread.Sleep(10000);//after test delet
            int NumThings = Things.Count;
            int RucksackWeight = myRucksack.Weight;

            int[,] func = new int[NumThings + 1, RucksackWeight + 1];
            int Weight;

            for (Weight = 1; Weight <= RucksackWeight; Weight++)
                for (int i = 1; i <= NumThings; i++)
                    if (Things[i - 1].Weight > Weight)
                    {
                        func[i, Weight] = func[i - 1, Weight];
                    }
                    else
                    {
                        func[i, Weight] = Math.Max(func[i - 1, Weight - Things[i - 1].Weight] + Things[i - 1].Cost, func[i - 1, Weight]);
                    }

            FinishTime = DateTime.Now;

            Weight = RucksackWeight;

            using (ApplicationContext db = new ApplicationContext())
            {
                myRucksack = db.Rucksacks.Find(idRucksack);
                var curThings = db.Things.Where(p => p.Rucksack.Id == idRucksack).ToList();

                var curThing = Things[0];

                for (int i = NumThings; i > 0; i--)
                    if (func[i, Weight] != func[i - 1, Weight])
                    {
                        foreach (Thing thing in curThings)
                        {
                            if (thing.Name == Things[i - 1].Name) curThing = thing;
                        }

                        curThing.Put = true;
                        db.SaveChanges();
                        Weight -= Things[i - 1].Weight;
                    }
                myRucksack.Cost = func[NumThings, RucksackWeight];
                db.SaveChanges();

                TimeSpan interval = FinishTime - StartTime;
                myRucksack.TimeWork = interval;
                db.SaveChanges();

                myRucksack.Status = true;
                db.SaveChanges();
            }
            Debug.WriteLine("I end filling rucksack {0}", idRucksack);
        }
    }
}
