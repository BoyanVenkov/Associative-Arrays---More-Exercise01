using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main()
    {
        Dictionary<string, Dictionary<string, int>> players =
            new Dictionary<string, Dictionary<string, int>>();

        string input;
        while ((input = Console.ReadLine()) != "Season end")
        {
            if (input.Contains(" -> "))
            {
                string[] parts = input.Split(" -> ");
                string player = parts[0];
                string position = parts[1];
                int skill = int.Parse(parts[2]);

                if (!players.ContainsKey(player))
                {
                    players[player] = new Dictionary<string, int>();
                }

                if (!players[player].ContainsKey(position))
                {
                    players[player][position] = skill;
                }
                else
                {
                    if (skill > players[player][position])
                    {
                        players[player][position] = skill;
                    }
                }
            }
            else if (input.Contains(" vs "))
            {
                string[] parts = input.Split(" vs ");
                string playerOne = parts[0];
                string playerTwo = parts[1];

                if (!players.ContainsKey(playerOne) || !players.ContainsKey(playerTwo))
                {
                    continue;
                }

                bool hasCommonPosition = players[playerOne]
                                            .Keys
                                            .Intersect(players[playerTwo].Keys)
                                            .Any();

                if (!hasCommonPosition)
                {
                    continue;
                }

                int totalSkillOne = players[playerOne].Values.Sum();
                int totalSkillTwo = players[playerTwo].Values.Sum();

                if (totalSkillOne > totalSkillTwo)
                {
                    players.Remove(playerTwo);
                }
                else if (totalSkillTwo > totalSkillOne)
                {
                    players.Remove(playerOne);
                }
            }
        }

        foreach (var player in players
                              .OrderByDescending(p => p.Value.Values.Sum())
                              .ThenBy(p => p.Key))
        {
            int totalSkill = player.Value.Values.Sum();
            Console.WriteLine($"{player.Key}: {totalSkill} skill");

            foreach (var position in player.Value
                                           .OrderByDescending(p => p.Value)
                                           .ThenBy(p => p.Key))
            {
                Console.WriteLine($"- {position.Key} <::> {position.Value}");
            }
        }
    }
}