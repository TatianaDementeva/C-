using System;

namespace Calendar
{
    class Program
    {
        static void Main(string[] args) {

            Console.WriteLine("Enter the date:");

            if (DateTime.TryParse(Console.ReadLine(), out DateTime UserDate) == false)
            {
                Console.WriteLine("Your date is not correct");
                Console.WriteLine("Press any key to exit.");
                Console.ReadKey();
                Environment.Exit(0);
            }

             var CountDays = DateTime.DaysInMonth(UserDate.Year, UserDate.Month);
            DateTime FirstDay = new DateTime(UserDate.Year, UserDate.Month, 1);

            Console.WriteLine("");

            ConsoleColor CurrentBackground = Console.BackgroundColor;
            ConsoleColor CurrentForeground = Console.ForegroundColor;

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("ПН  ВТ  СР  ЧТ  ПТ  СБ  ВС");
            Console.ForegroundColor = CurrentForeground;

            int OffsetFirstDay = ((int) FirstDay.DayOfWeek) * 4 - 4;
            if (OffsetFirstDay == -4) OffsetFirstDay = 24;
 
            int CurrentDay = 0;
            int CurrentOffset = 0;

            //печатаем сдвиг первой строки
            while (CurrentOffset < OffsetFirstDay)
            {
                Console.Write(" ");
                CurrentOffset++;
            }
            //печатаем календарь
            CurrentOffset = OffsetFirstDay;

            while (CurrentDay < CountDays )
            {    
                while (CurrentOffset < 26 && CurrentDay < CountDays)
                {
                    CurrentDay++;
                    if (CurrentDay == UserDate.Day) Console.BackgroundColor = ConsoleColor.Red;
                    if (CurrentDay < 10)
                        Console.Write(" {0}  ", CurrentDay);
                    else Console.Write("{0}  ", CurrentDay);
                    CurrentOffset += 4;
                    Console.BackgroundColor = CurrentBackground;
                }
                Console.WriteLine("");
                CurrentOffset = 0;
            }
            Console.WriteLine("");

            //считаем количество рабочих дней без учёта праздничных
            int WorkingDays = CountDays - 2 * 3;
            if (OffsetFirstDay <= 20) WorkingDays -= 2;
            if (OffsetFirstDay == 24) WorkingDays -= 1;

            int countDayInLastWeek = CountDays - 3 * 7 - (7 - OffsetFirstDay / 4);

            if (countDayInLastWeek >= 7) WorkingDays -= 2;
            if (countDayInLastWeek == 6) WorkingDays -= 1;

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Number of working days = {0}", WorkingDays);
            Console.WriteLine("");
            Console.ForegroundColor = CurrentForeground;

            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }
    }
}
