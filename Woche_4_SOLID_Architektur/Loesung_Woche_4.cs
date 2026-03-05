using System;

namespace SolidBeispiel
{
    // 1. Abstraktionen (DIP)
    public interface IPersistence { void Save(string content); }
    public interface INotifier { void Notify(string message); }

    // 2. Konkrete Implementierungen
    public class FileStore : IPersistence
    {
        public void Save(string content) { /* In Datei schreiben */ Console.WriteLine("Datei gespeichert."); }
    }

    public class EmailNotifier : INotifier
    {
        public void Notify(string message) { /* E-Mail senden */ Console.WriteLine("E-Mail gesendet."); }
    }

    // 3. Logik-Klasse (SRP: Nur für Text-Generierung)
    public class BescheidGenerator
    {
        public string Generate(string name, double betrag) => 
            $"Sehr geehrter Herr/Frau {name}, Sie müssen {betrag} EUR zahlen.";
    }

    // 4. Orchestrierung (SRP & DI)
    public class BescheidService
    {
        private readonly IPersistence _persistence;
        private readonly INotifier _notifier;
        private readonly BescheidGenerator _generator;

        public BescheidService(IPersistence persistence, INotifier notifier, BescheidGenerator generator)
        {
            _persistence = persistence;
            _notifier = notifier;
            _generator = generator;
        }

        public void Process(string name, double betrag)
        {
            var text = _generator.Generate(name, betrag);
            _persistence.Save(text);
            _notifier.Notify(text);
        }
    }

    class Program
    {
        static void Main()
        {
            // Dependency Injection "händisch" (Pure DI)
            var service = new BescheidService(new FileStore(), new EmailNotifier(), new BescheidGenerator());
            service.Process("Müller", 450.50);
        }
    }
}
