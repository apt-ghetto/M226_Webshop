using Schoeppli.Controller;
using Schoeppli.Interface;
using Schoeppli.Model;
using Schoeppli.Model.Enumerator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schoeppli.View
{
    // Klasse für die Bestellungsverwaltung
    public class BestellungView : ISubView<Bestellung>
    {
        private BestellungController bController;
        private PersonController pController;
        private ProduktController aController;

        // Konstruktor mit allen benötigten Controlern
        public BestellungView(BestellungController controller, PersonController pController, ProduktController aController)
        {
            this.bController = controller;
            this.pController = pController;
            this.aController = aController;
        }

        // Anzeigen des Menüs für den Benutzer
        public void ShowMenu()
        {
            Console.WriteLine("1) Alle Bestellungen anzeigen");
            Console.WriteLine("2) Bestellstatus ändern");
            Console.WriteLine("3) Neue Bestellung");
            Console.WriteLine("4) Bestellung löschen");
            Console.WriteLine();
            Console.WriteLine("9) Zurück");
            Console.WriteLine();
        }

        // Einstiegspunkt in die Bestellverwaltung
        public void ShowView()
        {
            byte input;
            do
            {
                ConsoleUtils.PrintTitle();
                ShowMenu();
                ConsoleUtils.PrintPrompt();

                if (byte.TryParse(Console.ReadLine(), out input))
                {
                    switch (input)
                    {
                        case 1:
                            ShowAll(bController.GetAllBestellungen());
                            ConsoleUtils.PrintContinueMessage();
                            break;
                        case 2:
                            EditStatus();
                            break;
                        case 3:
                            NewBestellung();
                            break;
                        case 4:
                            DeleteBestellung();
                            break;
                        case 9:
                            break;
                        default:
                            ConsoleUtils.PrintInvalidMessage();
                            break;
                    }
                }
                else
                {
                    ConsoleUtils.PrintInvalidMessage();
                }
            } while (input != 9);
            
        }

        // Anzeigen aller Bestellungen
        public void ShowAll(List<Bestellung> bestellungen)
        {
            ConsoleUtils.PrintTitle();
            bestellungen.ForEach(Console.WriteLine);
            Console.WriteLine();
        }

        // Bearbeiten des Status einer Bestellung
        private void EditStatus()
        {
            Bestellung bestellung = GetBestellung();
            if (null != bestellung)
            {
                Console.WriteLine($"Alter Wert: {bestellung.Bestellstatus}");
                Bestellstatus neuerSatus = ChooseStatus();
                ConsoleUtils.PrintSaveTemporary();
                if (Console.ReadLine() == "y")
                {
                    bestellung.Bestellstatus = neuerSatus;
                }
            }
            else
            {
                PrintKeineBestellung();
            }
        }

        // Neue Bestellung erstellen
        private void NewBestellung()
        {
            ConsoleUtils.PrintTitle();
            Kunde kunde;
            
            // Kunde auswählen
            do
            {
                kunde = GetKunde();
                if (kunde == null)
                {
                    ConsoleUtils.PrintInvalidMessage();
                    ConsoleUtils.PrintContinueMessage();
                }
            } while (kunde == null);

            DateTime datum = DateTime.Now;

            // Subroutine zur Auswahl der Artikel
            List<ArtikelBestellung> bestellteArtikel = GetBestellteArtikel();

            Bestellung bestellung = new Bestellung(-1, kunde, datum, Bestellstatus.Eingegangen);
            bestellung.BestellteArtikel = bestellteArtikel;
            Console.WriteLine(bestellung.GetInfoAll());
            // Dem Benutzer eine Übersicht der zu bestellenden Artikel geben
            ListAllProducts(bestellteArtikel);
            Console.WriteLine();
            ConsoleUtils.PrintSaveTemporary();
            if (Console.ReadLine() == "y")
            {
                // Bestellung speichern
                bController.SaveNewBestellung(bestellung);
                bool bestandTief = false;
                Console.WriteLine();

                // Bestand anpassen und überprüfen, ob der kritische Lagerbestand erreicht ist
                foreach (ArtikelBestellung position in bestellung.BestellteArtikel)
                {
                    Produkt artikel = aController.GetAllProducts().Find(p => p.ID == position.Artikelnummer);
                    artikel.Bestand -= position.Anzahl;
                    if (artikel.Bestand <= Produkt.WarnungBestand)
                    {
                        Console.WriteLine($"Achtung! Bestand von {artikel.Beschreibung} ist tief: {artikel.Bestand}!");
                        bestandTief = true;
                    }
                }
                if (bestandTief)
                {
                    ConsoleUtils.PrintContinueMessage();
                }
            }
        }

        // Bestellung löschen
        private void DeleteBestellung()
        {
            Bestellung bestellung = GetBestellung();

            if (null != bestellung)
            {
                Console.WriteLine($"Soll Bestellung {bestellung.Bestellnummer} von {bestellung.Besteller.Vorname} {bestellung.Besteller.Nachname} gelöscht werden? y/n");
                ConsoleUtils.PrintPrompt();
                if (Console.ReadLine() == "y")
                {
                    bController.GetAllBestellungen().Remove(bestellung);
                    foreach (ArtikelBestellung position in bestellung.BestellteArtikel)
                    {
                        aController.GetAllProducts().Find(x => x.ID == position.Artikelnummer).Bestand += position.Anzahl;
                    }
                }
            }
            else
            {
                ConsoleUtils.PrintInvalidMessage();
                ConsoleUtils.PrintContinueMessage();
            }
        }

        // Subroutine zur Auswahl eines Status
        private Bestellstatus ChooseStatus()
        {
            foreach(Bestellstatus stat in Enum.GetValues(typeof(Bestellstatus)))
            {
                Console.WriteLine($"{(int)stat}) {stat}");
            }

            do
            {
                int status;
                Console.Write("Status #: ");
                if (int.TryParse(Console.ReadLine(), out status))
                {
                    if (status >= 0 && status < Enum.GetNames(typeof(Bestellstatus)).Length)
                    {
                        return (Bestellstatus)status;
                    }
                }
                ConsoleUtils.PrintInvalidMessage();
            } while (true);
        }

        // Subroutine zur Auswahl einer Bestellung
        private Bestellung GetBestellung()
        {
            ShowAll(bController.GetAllBestellungen());
            int bestellId = ConsoleUtils.GetUserInputAsInt("Welche Bestellung? [Bestellnr.]: ");

            return bController.GetAllBestellungen().Where(x => x.Bestellnummer == bestellId).SingleOrDefault();
        }
        
        // Subroutine zur Auswahl eines Kunden
        private Kunde GetKunde()
        {
            ConsoleUtils.PrintTitle();
            pController.GetAllKunden().ForEach(Console.WriteLine);
            Console.WriteLine();
            int kundenId = ConsoleUtils.GetUserInputAsInt("Welcher Kunde? [ID]: ");

            return pController.GetAllKunden().Where(x => x.ID == kundenId).SingleOrDefault();
        }

        // Subroutine zur Auswahl der Artikel, welche bestellt werden sollen
        private List<ArtikelBestellung> GetBestellteArtikel()
        {
            List<ArtikelBestellung> bestellteArtikel = new List<ArtikelBestellung>();
            bool addArtikel = true;

            while (addArtikel)
            {
                ConsoleUtils.PrintTitle();
                aController.GetAllProducts().ForEach(Console.WriteLine);
                Console.WriteLine();

                // Artikel wählen
                int artikelId = ConsoleUtils.GetUserInputAsInt("Welcher Artikel? [ID]: ");
                Produkt artikel = aController.GetAllProducts().Where(x => x.ID == artikelId).SingleOrDefault();
                if (artikel == null)
                {
                    ConsoleUtils.PrintInvalidMessage();
                    ConsoleUtils.PrintContinueMessage();
                    continue;
                }

                // Anzahl des Artikels wählen
                int anzahl = ConsoleUtils.GetUserInputAsInt("Anzahl: ");
                if (artikel.Bestand < anzahl || anzahl < 0)
                {
                    Console.WriteLine("Nicht genügend Artikel vorhanden oder Anzahl negativ.");
                    ConsoleUtils.PrintContinueMessage();
                    continue;
                }

                Console.WriteLine("Artikel hinzufügen? y/n");
                ConsoleUtils.PrintPrompt();
                if (Console.ReadLine() == "y")
                {
                    bestellteArtikel.Add(new ArtikelBestellung(artikelId, anzahl));
                }

                // Nachfragen, ob weitere Artikel hinzugefügt werden wollen
                Console.WriteLine("Weiteren Artikel hinzufügen? y/n");
                ConsoleUtils.PrintPrompt();
                if (Console.ReadLine() == "n")
                {
                    addArtikel = false;
                }
            }

            return bestellteArtikel;
        }

        // Subroutine zum anzeigen aller Artikel, die für die Bestellung ausgewählt wurden inkl. Anzahl
        private void ListAllProducts(List<ArtikelBestellung> artikelListe)
        {
            foreach (ArtikelBestellung position in artikelListe)
            {
                string artikel = aController.GetAllProducts().Where(x => x.ID == position.Artikelnummer).SingleOrDefault().Beschreibung;
                Console.WriteLine($"\tArtikel: {artikel}".PadRight(35) + $"Anzahl: {position.Anzahl}");
            }
        }

        // Anzeigen einer Warnmeldung, falls eine ungültige Bestellung ausgewählt wurde
        private void PrintKeineBestellung()
        {
            Console.WriteLine("Bestellung existiert nicht!");
            ConsoleUtils.PrintContinueMessage();
        }
    }
}
