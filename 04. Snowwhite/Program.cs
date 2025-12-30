using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main()
    {
        Dictionary<(string name, string color), long> dwarfs =
            new Dictionary<(string, string), long>();

        Dictionary<string, int> colorCount =
            new Dictionary<string, int>();

        string input;
        while ((input = Console.ReadLine()) != "Once upon a time")
        {
            string[] parts = input.Split(" <:> ");
            string name = parts[0];
            string color = parts[1];
            long physics = long.Parse(parts[2]);

            var key = (name, color);

            if (!dwarfs.ContainsKey(key))
            {
                dwarfs[key] = physics;

                if (!colorCount.ContainsKey(color))
                {
                    colorCount[color] = 0;
                }
                colorCount[color]++;
            }
            else
            {
                if (physics > dwarfs[key])
                {
                    dwarfs[key] = physics;
                }
            }
        }

        foreach (var dwarf in dwarfs
            .OrderByDescending(d => d.Value) 
            .ThenByDescending(d => colorCount[d.Key.color])) 
        {
            Console.WriteLine(
                $"({dwarf.Key.color}) {dwarf.Key.name} <-> {dwarf.Value}");
        }
    }
}