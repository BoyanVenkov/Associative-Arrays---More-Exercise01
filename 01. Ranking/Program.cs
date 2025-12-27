using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main()
    {
        Dictionary<string, string> contests = new Dictionary<string, string>();

        string input;
        while ((input = Console.ReadLine()) != "end of contests")
        {
            string[] parts = input.Split(':');
            string contest = parts[0];
            string password = parts[1];

            contests[contest] = password;
        }

        Dictionary<string, Dictionary<string, int>> users =
            new Dictionary<string, Dictionary<string, int>>();

        while ((input = Console.ReadLine()) != "end of submissions")
        {
            string[] parts = input.Split("=>");
            string contest = parts[0];
            string password = parts[1];
            string username = parts[2];
            int points = int.Parse(parts[3]);

            if (!contests.ContainsKey(contest)) continue;
            if (contests[contest] != password) continue;

            if (!users.ContainsKey(username))
            {
                users[username] = new Dictionary<string, int>();
            }

            if (!users[username].ContainsKey(contest))
            {
                users[username][contest] = points;
            }
            else
            {
                if (points > users[username][contest])
                {
                    users[username][contest] = points;
                }
            }
        }
        string bestUser = "";
        int bestTotalPoints = 0;

        foreach (var user in users)
        {
            int totalPoints = user.Value.Values.Sum();

            if (totalPoints > bestTotalPoints)
            {
                bestTotalPoints = totalPoints;
                bestUser = user.Key;
            }
        }

        Console.WriteLine($"Best candidate is {bestUser} with total {bestTotalPoints} points.");
        Console.WriteLine("Ranking:");

        foreach (var user in users.OrderBy(u => u.Key))
        {
            Console.WriteLine(user.Key);

            foreach (var contest in user.Value
                                        .OrderByDescending(c => c.Value))
            {
                Console.WriteLine($"#  {contest.Key} -> {contest.Value}");
            }
        }
    }
}