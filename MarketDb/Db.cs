using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

static class Db
{
    private const string DbPath = "Db";

    private static List<uint> ReadEncodedData(in string path) => File.Exists(path) ?
            File.ReadAllText(path)
                .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                .Select(s => UInt32.TryParse(s, out var r) ? r : 0)
                .ToList()
            : new List<uint>();

    private static string GetPath(in int telegramId) => $"{DbPath}/{telegramId}.txt";

    public static void Init()
    {
        if (!Directory.Exists(DbPath))
        {
            Console.WriteLine("Old database not found, we are creating a new database . . .");
            Directory.CreateDirectory(DbPath);
            Console.WriteLine("Created a new database.");
        }
        else
        {
            Console.WriteLine("An existing database has been detected.");
        }
    }

    // Считывает, затем декодирует данные
    public static (uint Money, Dictionary<TimeSpan, (uint Guns, uint Bullets)>) ReadData(in int telegramId)
    {
        // read
        var src = ReadEncodedData(GetPath(telegramId));

        // decode
        var len = src.Count;
        if (len == 0)
        {
            return (0, new Dictionary<TimeSpan, (uint Guns, uint Bullets)>());
        }
        var property = new Dictionary<TimeSpan, (uint Guns, uint Bullets)>();
        for (int i = 1; (i + 4) < len; i += 5)
        {
            var span = new TimeSpan(days: (int)src[i], hours: (int)src[i + 1], minutes: (int)src[i + 2], seconds: 0);
            var spanGuns = src[i + 3];
            var spanBullets = src[i + 4];
            if (property.ContainsKey(span))
            {
                var currentWeapon = property[span];
                currentWeapon.Guns += spanGuns;
                currentWeapon.Bullets += spanBullets;
                property[span] = currentWeapon;
                continue;
            }
            property.Add(span, (spanGuns, spanBullets));
        }
        return (src[0], property);
    }

    // Кодирует, затем сохраняет закодированые данные
    public static void SaveData(TelegramUser user)
    {
        // encode
        StringBuilder encodedData = new StringBuilder($"{user.Money} ");
        foreach (var p in user.Property)
        {
            if ((p.Key.Days == 0 && p.Key.Hours == 0 && p.Key.Minutes == 0) || (p.Value.Bullets == 0 && p.Value.Guns == 0))
            {
                continue;
            }
            encodedData.Append($"{p.Key.Days} {p.Key.Hours} {p.Key.Minutes} {p.Value.Guns} {p.Value.Bullets} ");
        }

        // save
        File.WriteAllText(GetPath(user.Id), encodedData.ToString());
    }
}

