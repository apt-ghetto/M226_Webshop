﻿using Schoeppli.Controller;
using Schoeppli.Interface;
using Schoeppli.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schoeppli.View
{
    public class RechnungsView : IView
    {
        private BestellungController controller;
        private ProduktController pController;

        public RechnungsView(BestellungController controller, ProduktController pController)
        {
            this.controller = controller;
            this.pController = pController;
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
                            ShowAll();
                            ConsoleUtils.PrintContinueMessage();
                            Console.ReadKey();
                            break;
                        case 2:
                            CreateBill();
                            ConsoleUtils.PrintContinueMessage();
                            Console.ReadKey();
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

        public void ShowMenu()
        {
            Console.WriteLine("1) Rechnung anzeigen");
            Console.WriteLine("2) Rechnung erstellen");
            Console.WriteLine();
            Console.WriteLine("9) Zurück");
            Console.WriteLine();
        }

        public void ShowAll()
        {
            int counter = 0;
            foreach (string filePath in controller.GetAllBills())
            {                
                Console.Write(counter.ToString().PadLeft(10));
                Console.WriteLine($"{Path.GetFileName(filePath)}".PadLeft(30));
                counter++;
            }
        }

        private Bestellung GetBestellung()
        {
            controller.GetAllBestellungen().ForEach(Console.WriteLine);
            int bestellId = ConsoleUtils.GetUserInputAsInt("Rechnung welcher Bestellung soll erstellt werden? [Bestellnr.]: ");

            return controller.GetAllBestellungen().Where(x => x.Bestellnummer == bestellId).SingleOrDefault();
        }

        private void CreateBill()
        {
            Console.Clear();
            ConsoleUtils.PrintTitle();
            Bestellung bestellung = GetBestellung();
            if (bestellung != null)
            {
                string filePath = controller.CreateFileNewRechnung(bestellung);
                if (filePath != string.Empty)
                {
                    WriteFile(bestellung, filePath);
                    Console.WriteLine("Rechnung erfolgreich erstellt.");
                    System.Diagnostics.Process.Start(filePath);
                }
                else
                {
                    PrintRechnungVorhanden();
                }
            }
            else
            {
                PrintKeineBestellung();
            }
        }

        private void WriteFile(Bestellung bestellung, string filePath)
        {
            int totalCost = 0;

            using (StreamWriter sw = File.CreateText(filePath))
            {
                // Space on top
                for (int i = 0; i < 5; i++)
                {
                    sw.WriteLine();
                }

                // Title
                sw.Write("Schoeppli");
                sw.WriteLine($"Rechnung # {bestellung.Bestellnummer}".PadLeft(71));
                sw.WriteLine();

                // Bill info
                sw.WriteLine($"Besteller: {bestellung.Besteller.Vorname} {bestellung.Besteller.Nachname}".PadLeft(80));
                sw.WriteLine($"Kundennummer: {bestellung.Besteller.Kundennummer}".PadLeft(80));
                sw.WriteLine($"Datum: {bestellung.Bestelldatum}".PadLeft(80));
                sw.WriteLine($"Status: {bestellung.Bestellstatus}".PadLeft(80));

                // Space before articles
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine();

                // Separator
                for (int i = 0; i < 80; i++)
                {
                    sw.Write('*');
                }
                sw.WriteLine();

                // Columns
                sw.Write("\t");
                sw.Write($"#".PadRight(5));
                sw.Write($"Artikel".PadRight(25));
                sw.Write($"Preis".PadRight(15));
                sw.Write($"Anzahl".PadRight(15));
                sw.Write($"Total");
                sw.WriteLine();

                // Separator
                for (int i = 0; i < 80; i++)
                {
                    sw.Write('*');
                }
                sw.WriteLine();

                // List articles
                foreach (ArtikelBestellung position in bestellung.BestellteArtikel)
                {
                    Produkt produkt = pController.GetAllProducts().Where(x => x.ID == position.Artikelnummer).Single();
                    totalCost += produkt.Preis * position.Anzahl;
                    sw.Write("\t");
                    sw.Write($"{position.Artikelnummer}".PadRight(5));
                    sw.Write($"{produkt.Beschreibung}".PadRight(25));
                    sw.Write($"{produkt.Preis}.-".PadRight(15));
                    sw.Write($"{position.Anzahl}".PadRight(15));
                    sw.Write($"{produkt.Preis * position.Anzahl}.-");
                    sw.WriteLine();
                }

                // Separator
                for (int j = 0; j < 80; j++)
                {
                    sw.Write('*');
                }
                sw.WriteLine();

                // Total cost   
                sw.WriteLine($"{totalCost}.-".PadLeft(71));

                // Separator
                for (int j = 0; j < 80; j++)
                {
                    sw.Write('*');
                }
                sw.WriteLine();

            }
        }

        private void PrintKeineBestellung()
        {
            Console.WriteLine("Bestellung existiert nicht!");
            ConsoleUtils.PrintContinueMessage();
            Console.ReadKey();
        }

        private void PrintRechnungVorhanden()
        {
            Console.WriteLine("Rechnung existiert bereits!");
            ConsoleUtils.PrintContinueMessage();
            Console.ReadKey();
        }

    }
}