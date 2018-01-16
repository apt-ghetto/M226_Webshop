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
    public class BestellungView : ISubView<Bestellung>
    {
        private BestellungController bController;
        private PersonController pController;
        private ProduktController aController;

        public BestellungView(BestellungController controller, PersonController pController, ProduktController aController)
        {
            this.bController = controller;
            this.pController = pController;
            this.aController = aController;
        }

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
                            Console.ReadKey();
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

        public void ShowAll(List<Bestellung> bestellungen)
        {
            ConsoleUtils.PrintTitle();
            bestellungen.ForEach(Console.WriteLine);
            Console.WriteLine();
        }

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

        private void NewBestellung()
        {
            ConsoleUtils.PrintTitle();
            Kunde kunde;
            
            do
            {
                kunde = GetKunde();
                if (kunde == null)
                {
                    ConsoleUtils.PrintInvalidMessage();
                    ConsoleUtils.PrintContinueMessage();
                    Console.ReadKey();
                }
            } while (kunde == null);

            DateTime datum = DateTime.Now;
            List<ArtikelBestellung> bestellteArtikel = GetBestellteArtikel();

            Bestellung bestellung = new Bestellung(-1, kunde, datum, Bestellstatus.Eingegangen);
            bestellung.BestellteArtikel = bestellteArtikel;
            Console.WriteLine(bestellung.GetInfoAll());
            ListAllProducts(bestellteArtikel);
            Console.WriteLine();
            ConsoleUtils.PrintSaveTemporary();
            if (Console.ReadLine() == "y")
            {
                bController.SaveNewBestellung(bestellung);
                foreach (ArtikelBestellung position in bestellung.BestellteArtikel)
                {
                    aController.GetAllProducts().Find(p => p.ID == position.Artikelnummer).Bestand -= position.Anzahl;
                }
            }
        }

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
                Console.ReadKey();
            }
        }

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

        private Bestellung GetBestellung()
        {
            ShowAll(bController.GetAllBestellungen());
            int bestellId = ConsoleUtils.GetUserInputAsInt("Welche Bestellung? [Bestellnr.]: ");

            return bController.GetAllBestellungen().Where(x => x.Bestellnummer == bestellId).SingleOrDefault();
        }
        
        private Kunde GetKunde()
        {
            ConsoleUtils.PrintTitle();
            pController.GetAllKunden().ForEach(Console.WriteLine);
            Console.WriteLine();
            int kundenId = ConsoleUtils.GetUserInputAsInt("Welcher Kunde? [ID]: ");

            return pController.GetAllKunden().Where(x => x.ID == kundenId).SingleOrDefault();
        }

        private List<ArtikelBestellung> GetBestellteArtikel()
        {
            List<ArtikelBestellung> bestellteArtikel = new List<ArtikelBestellung>();
            bool addArtikel = true;

            while (addArtikel)
            {
                ConsoleUtils.PrintTitle();
                aController.GetAllProducts().ForEach(Console.WriteLine);
                Console.WriteLine();

                int artikelId = ConsoleUtils.GetUserInputAsInt("Welcher Artikel? [ID]: ");
                Produkt artikel = aController.GetAllProducts().Where(x => x.ID == artikelId).SingleOrDefault();
                if (artikel == null)
                {
                    ConsoleUtils.PrintInvalidMessage();
                    ConsoleUtils.PrintContinueMessage();
                    Console.ReadKey();
                    continue;
                }

                int anzahl = ConsoleUtils.GetUserInputAsInt("Anzahl: ");
                if (artikel.Bestand < anzahl || anzahl < 0)
                {
                    Console.WriteLine("Nicht genügend Artikel vorhanden oder Anzahl negativ.");
                    ConsoleUtils.PrintContinueMessage();
                    Console.ReadKey();
                    continue;
                }

                Console.WriteLine("Artikel hinzufügen? y/n");
                ConsoleUtils.PrintPrompt();
                if (Console.ReadLine() == "y")
                {
                    bestellteArtikel.Add(new ArtikelBestellung(artikelId, anzahl));
                }
                Console.WriteLine("Weiteren Artikel hinzufügen? y/n");
                ConsoleUtils.PrintPrompt();
                if (Console.ReadLine() == "n")
                {
                    addArtikel = false;
                }
            }

            return bestellteArtikel;
        }

        private void ListAllProducts(List<ArtikelBestellung> artikelListe)
        {
            foreach (ArtikelBestellung position in artikelListe)
            {
                string artikel = aController.GetAllProducts().Where(x => x.ID == position.Artikelnummer).SingleOrDefault().Beschreibung;
                Console.WriteLine($"\tArtikel: {artikel}".PadRight(35) + $"Anzahl: {position.Anzahl}");
            }
        }

        private void PrintKeineBestellung()
        {
            Console.WriteLine("Bestellung existiert nicht!");
            ConsoleUtils.PrintContinueMessage();
            Console.ReadKey();
        }
    }
}
