using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Opt;

class Program
{
    static void Main(string[] args)
    {
        var maybeYear = TryToGetYear();

        foreach (var year in maybeYear)
        {
            Console.WriteLine($"This year is {year}");
        }

        var maybeSeconds = new Maybe<int>[10];
        for (var i = 0; i < maybeSeconds.Length; i++)
        {
            Thread.Sleep(1000);
            maybeSeconds[i] = TryToGetSecond();
        }

        foreach (var second in Maybe.Flatten(maybeSeconds))
        {
            Console.WriteLine($"This second is {second}");
        }
    }

    private static Maybe<int> TryToGetYear()
    {
        var year = DateTime.Now.Year;

        if (year < 0) return Maybe.Nothing<int>();

        return DateTime.Now.Year;
    }

    private static Maybe<int> TryToGetSecond()
    {
        var second = DateTime.Now.Second;

        if (second < 0) return Maybe.Nothing<int>();

        return DateTime.Now.Second;
    }
}
