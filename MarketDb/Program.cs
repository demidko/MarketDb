using System;

class Program
{
    static void Main()
    {
        while (true)
        {
            DbConsole.RequestedUser
                .Print()
                .SetMoney()
                .SetGuns()
                .Save();
            Console.WriteLine("Changes have been saved.\n");
        }
    }
}

