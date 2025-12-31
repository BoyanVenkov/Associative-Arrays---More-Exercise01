using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main()
    {
        int n = int.Parse(Console.ReadLine());

        Dictionary<string, Dictionary<string, Dragon>> dragons =
            new Dictionary<string, Dictionary<string, Dragon>>();

        for (int i = 0; i < n; i++)
        {
            string[] parts = Console.ReadLine().Split(' ');

            string type = parts[0];
            string name = parts[1];

            int damage = parts[2] == "null" ? 45 : int.Parse(parts[2]);
            int health = parts[3] == "null" ? 250 : int.Parse(parts[3]);
            int armor = parts[4] == "null" ? 10 : int.Parse(parts[4]);

            if (!dragons.ContainsKey(type))
            {
                dragons[type] = new Dictionary<string, Dragon>();
            }

            dragons[type][name] = new Dragon(damage, health, armor);
        }

        foreach (var type in dragons)
        {
            double avgDamage = type.Value.Values.Average(d => d.Damage);
            double avgHealth = type.Value.Values.Average(d => d.Health);
            double avgArmor = type.Value.Values.Average(d => d.Armor);

            Console.WriteLine(
                $"{type.Key}::({avgDamage:F2}/{avgHealth:F2}/{avgArmor:F2})");

            foreach (var dragon in type.Value.OrderBy(d => d.Key))
            {
                Console.WriteLine(
                    $"-{dragon.Key} -> damage: {dragon.Value.Damage}, " +
                    $"health: {dragon.Value.Health}, armor: {dragon.Value.Armor}");
            }
        }
    }
}

class Dragon
{
    public int Damage { get; }
    public int Health { get; }
    public int Armor { get; }

    public Dragon(int damage, int health, int armor)
    {
        Damage = damage;
        Health = health;
        Armor = armor;
    }
}