using System;
using System.Collections.Generic;
using System.Linq;

static class DbConsole
{
    public static TelegramUser RequestedUser
    {
        get
        {
            Console.Write("Telegram ID: ");
            if (Int32.TryParse(Console.ReadLine(), out var id))
            {
                return TelegramUser.With(id);
            }
            Console.WriteLine("Invalid ID, please enter ID again");
            return RequestedUser;
        }
    }

    public static TelegramUser Print(this TelegramUser user)
    {
        Console.WriteLine(user);
        return user;
    }

    public static TelegramUser SetMoney(this TelegramUser user)
    {
        Console.Write("Money: ");
        if (UInt32.TryParse(Console.ReadLine(), out var m))
        {
            user.Money = m;
        }
        else
        {
            Console.WriteLine("Error: It was necessary to enter number of money.");
        }
        return user;
    }

    public static TelegramUser SetGuns(this TelegramUser user)
    {
        IEnumerable<uint> readUints() => Console.ReadLine().Split(":").Select(new Func<string, uint>(Convert.ToUInt32));
        Console.Write("Restrict power [days:hours:minutes]: ");
        TimeSpan span;
        try
        {
            var arr = readUints().ToList();
            span = new TimeSpan((int)arr[0], (int)arr[1], (int)arr[2], 0);
        }
        catch
        {
            Console.WriteLine("Error: It was necessary to enter days:hours:mnutes, like 1:23:59");
            return user;
        }
        Console.Write("Number of bullets and guns [guns:bullets]: ");
        uint guns, bullets;
        try
        {
            var GunsAndBullets = readUints();
            guns = GunsAndBullets.First();
            bullets = GunsAndBullets.Last();
        }
        catch
        {
            Console.WriteLine("Error: It was necessary to enter guns:bullets, like 1:5");
            return user;
        }
        user.Property[span] = (guns, bullets);
        return user;
    }
}

