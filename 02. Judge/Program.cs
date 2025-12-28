using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main()
    {
        Dictionary<string, Dictionary<string, int>> contests =
            new Dictionary<string, Dictionary<string, int>>();

        Dictionary<string, int> individualStats =
            new Dictionary<string, int>();

        string input;
        while ((input = Console.ReadLine()) != "no more time")
        {
            string[] parts = input.Split(" -> ");
            string username = parts[0];
            string contest = parts[1];
            int points = int.Parse(parts[2]);

            if (!contests.ContainsKey(contest))
            {
                contests[contest] = new Dictionary<string, int>();
            }

            if (!contests[contest].ContainsKey(username))
            {
                contests[contest][username] = points;

                if (!individualStats.ContainsKey(username))
                {
                    individualStats[username] = 0;
                }

                individualStats[username] += points;
            }
            else
            {
                if (points > contests[contest][username])
                {
                    int difference = points - contests[contest][username];
                    contests[contest][username] = points;
                    individualStats[username] += difference;
                }
            }
        }

        foreach (var contest in contests)
        {
            Console.WriteLine($"{contest.Key}: {contest.Value.Count} participants");

            int position = 1;
            foreach (var user in contest.Value
                                        .OrderByDescending(u => u.Value)
                                        .ThenBy(u => u.Key))
            {
                Console.WriteLine($"{position}. {user.Key} <::> {user.Value}");
                position++;
            }
        }

        Console.WriteLine("Individual standings:");

        int rank = 1;
        foreach (var user in individualStats
                             .OrderByDescending(u => u.Value)
                             .ThenBy(u => u.Key))
        {
            Console.WriteLine($"{rank}. {user.Key} -> {user.Value}");
            rank++;
        }
    }
}