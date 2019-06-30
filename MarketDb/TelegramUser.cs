using System;
using System.Collections.Generic;
using System.Text;

class TelegramUser
{
    static TelegramUser() => Db.Init();

    private TelegramUser(
        in int i, in uint m, in Dictionary<TimeSpan, (uint Guns, uint Bullets)> p) => (Money, Id, Property) = (m, i, p);

    public readonly int Id;
    public uint Money;
    public Dictionary<TimeSpan, (uint Guns, uint Bullets)> Property;

    public void Save() => Db.SaveData(this);

    public override string ToString()
    {
        var res = new StringBuilder($"User #{Id} with {Money}$ has");
        if (Property.Count == 0)
        {
            res.Append(" nothing");
        }
        res.AppendLine();
        foreach (var p in Property)
        {
            res.AppendLine($"    {p.Value.Guns} banhammers and {p.Value.Bullets} bullets with ban`s power {p.Key.Days}d {p.Key.Hours}h {p.Key.Minutes}m");
        }
        return res.ToString();
    }

    public static TelegramUser With(in int id)
    {
        var (money, property) = Db.ReadData(id);
        return new TelegramUser(id, money, property);
    }
}

