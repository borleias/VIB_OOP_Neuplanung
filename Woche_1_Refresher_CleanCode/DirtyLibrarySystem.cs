using System;
using System.Collections.Generic;

namespace LibrarySystem
{
    // Hier ist alles in einer Klasse und sehr unübersichtlich
    class Program
    {
        static void Main(string[] args)
        {
            // Liste der User
            List<string[]> u = new List<string[]>();
            u.Add(new string[] { "Peter", "1" }); // 1 heißt aktiv
            u.Add(new string[] { "Hans", "0" });  // 0 heißt inaktiv

            // List der Bücher
            List<string[]> b = new List<string[]>();
            b.Add(new string[] { "C# Profi", "Verfügbar" });
            b.Add(new string[] { "Python Skripte", "Ausgeliehen" });

            // Peter leiht das C# Profi Buch aus - sollte klappen, da Peter aktiv ist und das Buch verfügbar ist
            if (u[0][1] == "1")
            {
                if (b[0][1] == "Verfügbar")
                {
                    b[0][1] = "Ausgeliehen";
                    Console.WriteLine("Buch erfolgreich ausgeliehen an " + u[0][0]);
                }
                else
                {
                    Console.WriteLine("Nicht verfügbar.");
                }
            }
            else
            {
                Console.WriteLine("User inaktiv.");
            }

            // Hans leiht das C# Profi Buch aus - sollte nicht klappen, da Hans inaktiv ist
            if (u[1][1] == "1")
            {
                if (b[0][1] == "Verfügbar")
                {
                    b[0][1] = "Ausgeliehen";
                    Console.WriteLine("Buch erfolgreich ausgeliehen an " + u[1][0]);
                }
                else
                {
                    Console.WriteLine("Nicht verfügbar.");
                }
            }
            else
            {
                Console.WriteLine("User inaktiv.");
            }

            // Alles anzeigen
            Console.WriteLine("--- Status ---");
            foreach (var item in b)
            {
                Console.WriteLine(item[0] + ": " + item[1]);
            }
        }
    }
}
