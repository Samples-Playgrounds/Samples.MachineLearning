using System;
using System.Threading.Tasks;

namespace Ph4ct3x.App.TimerStopWatch
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] intervals =
            {
                60,
                15,
                60,
                10
            };

            for(int i = 0; i < intervals.Length; i++)
            {
                Console.WriteLine($"Time = {DateTime.Now}");

                System.Threading.Thread.Sleep(TimeSpan.FromSeconds(intervals[i]));
                //Task.Delay(TimeSpan.FromSeconds(intervals[i]));
            }

        }
    }
}
