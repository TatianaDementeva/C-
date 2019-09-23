using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Guesses
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] SupportPhrases = new string[6] { "Cheer up!",
                "You're on the right track!",
                "If at first you don't succeed... try and try again!",
                "Don’t give up!",
                "Keep pushing!",
                "Come on! You can do it!"};
            var History = new List<string>();

            Console.WriteLine("Welcome to the GUESSES GAME!");
            Console.WriteLine("The goal of the game is to guess the number conceived by the program in the range from 0 to 50\n");

            Console.WriteLine("Enter your name: ");
            var UserName = Console.ReadLine();

            Random random = new Random();
            var ConceivedNumber = random.Next(51);
            Console.WriteLine("{0}", ConceivedNumber);//delet after

            var CountOfAttempts = 0;
            var UserNumber = -1;

            var FinishTime = new DateTime();
            var StartTime = DateTime.Now;
            
            while (ConceivedNumber != UserNumber)
            {
                CountOfAttempts++;

                Console.Write("{0}, enter your version: ", UserName);
                var UserVersion = Console.ReadLine();

                if (int.TryParse(UserVersion, out UserNumber) != true)
                {
                    if (UserVersion.Equals( "q", StringComparison.InvariantCultureIgnoreCase))
                    {
                        Console.WriteLine("Sorry {0}, you didn't get the right meaning.", UserName);
                        Thread.Sleep(3000);
                        Environment.Exit(0);
                    }

                    Console.WriteLine("{0}, invalid input. Enter a number.", UserName);
                    CountOfAttempts--;
                }
                else
                {
                    StringBuilder Attempt = new StringBuilder(UserNumber.ToString());

                    if (UserNumber == ConceivedNumber)
                    {
                        FinishTime = DateTime.Now;

                        Attempt.Append(" =");
                        History.Add(Attempt.ToString());

                        Console.WriteLine("{0}, Congratulations! You guessed it!\n", UserName);
                        break;
                    }
                    if (UserNumber < ConceivedNumber)
                    {
                        Console.WriteLine("Your version < Conceived number");
                        Attempt.Append(" <");
                    }
                    else
                    {
                        Console.WriteLine("Your version > Conceived number");
                        Attempt.Append(" >");
                    }
                    History.Add(Attempt.ToString());
                }
                if(CountOfAttempts % 4 == 0)
                {  
                    try
                    {
                        var SupPhrase = SupportPhrases[random.Next(6)];
                        Console.WriteLine("{0}, {1}!\n", UserName, SupPhrase);
                    }
                    catch
                    {
                        Console.WriteLine("{0}, {1}!\n", UserName, SupportPhrases[0]);
                    }
                }
            }

            Console.WriteLine("Count of attempts = {0} ", CountOfAttempts);
            
            TimeSpan interval = FinishTime - StartTime;

            if (interval.TotalMinutes >= 1)
            {
                Console.WriteLine("You spent {0} minutes guessing!", interval.TotalMinutes);
            }
            else
            {
                Console.WriteLine("You spent {0} seconds guessing!", interval.TotalSeconds);
            }
             

            Console.WriteLine("History:");
            foreach (var a in History)
            {
                Console.WriteLine(a);
            }
            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }
    }
}
