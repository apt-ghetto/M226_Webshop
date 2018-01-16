using Schoeppli.Controller;
using Schoeppli.Interface;
using Schoeppli.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schoeppli.View
{
    public class KundenView : ISubView<Kunde>
    {
        private PersonController controller;

        public KundenView(PersonController controller)
        {
            this.controller = controller;
        }
               
        public void ShowMenu()
        {
            Console.WriteLine("1) Alle Kunden anzeigen");
            Console.WriteLine("2) Kunde bearbeiten");
            Console.WriteLine("3) Neuer Kunde");
            Console.WriteLine("4) Kunde löschen");
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
                            ShowAll(controller.GetAllKunden());
                            ShowSingle();
                            break;
                        case 2:
                            EditKunde();
                            break;
                        case 3:
                            NewKunde();
                            break;
                        case 4:
                            DeleteKunde();
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

        public void ShowAll(List<Kunde> kunden)
        {
            ConsoleUtils.PrintTitle();
            kunden.ForEach(Console.WriteLine);
        }

        public void ShowSingle()
        {
            do
            {
                Console.WriteLine();
                Console.WriteLine("Kundendetails einsehen? y/n");
                ConsoleUtils.PrintPrompt();
                if (Console.ReadLine() == "y")
                {
                    Console.WriteLine();
                    int kundenID = ConsoleUtils.GetUserInputAsInt("Kunden ID: ");
                    Person kunde = controller.GetAllKunden().Find(i => i.ID == kundenID);
                    if (kunde != null)
                    {
                        Console.WriteLine(kunde.GetInfoAll());
                        Console.WriteLine();
                    }
                    else
                    {
                        Console.WriteLine("Kunde existiert nicht!");
                    }
                }
                else
                {
                    return;
                }
            } while (true);
        }

        public void EditKunde()
        {
            Kunde kunde = GetKunde();            

            if (null != kunde)
            {
                Kunde bearbeiteterKunde = new Kunde(kunde.ID, kunde.Vorname, kunde.Nachname,
                    kunde.Geburtsdatum, kunde.Adresse, kunde.PLZ, kunde.Kundennummer);
                int input;

                do
                {
                    Console.Clear();
                    Console.WriteLine();
                    Console.WriteLine("1) Vorname");
                    Console.WriteLine("2) Nachname");
                    Console.WriteLine("3) Adresse");
                    Console.WriteLine("4) Plz");
                    Console.WriteLine("5) Geburtsdatum");
                    Console.WriteLine("6) Kundennummer");
                    Console.WriteLine();
                    Console.WriteLine("8) Temporär speichern");
                    Console.WriteLine();
                    Console.WriteLine("9) Zurück");
                    Console.WriteLine();


                    input = ConsoleUtils.GetUserInputAsInt("Welche Eigenschaft soll bearbeitet werden?: ");
                    switch (input)
                    {
                        case 1:
                            Console.WriteLine($"Alter Wert: {bearbeiteterKunde.Vorname}");
                            Console.Write("Neuer Wert: ");
                            bearbeiteterKunde.Vorname = Console.ReadLine();
                            break;
                        case 2:
                            Console.WriteLine($"Alter Wert: {bearbeiteterKunde.Nachname}");
                            Console.Write("Neuer Wert: ");
                            bearbeiteterKunde.Nachname = Console.ReadLine();
                            break;
                        case 3:
                            Console.WriteLine($"Alter Wert: {bearbeiteterKunde.Adresse}");
                            Console.Write("Neuer Wert: ");
                            bearbeiteterKunde.Adresse = Console.ReadLine();
                            break;
                        case 4:
                            Console.WriteLine($"Alter Wert: {bearbeiteterKunde.PLZ}");
                            Console.Write("Neuer Wert: ");
                            bearbeiteterKunde.PLZ = Console.ReadLine();
                            break;
                        case 5:
                            Console.WriteLine($"Alter Wert: {bearbeiteterKunde.Geburtsdatum.ToShortDateString()}");
                            bearbeiteterKunde.Geburtsdatum = GetGeburtstag();
                            break;
                        case 6:
                            Console.WriteLine($"Alter Wert: {bearbeiteterKunde.Kundennummer}");
                            Console.Write("Neuer Wert: ");
                            bearbeiteterKunde.Kundennummer = Console.ReadLine();
                            break;
                        case 8:
                            controller.GetAllKunden()[controller.GetAllKunden().FindIndex(ind => ind.ID == bearbeiteterKunde.ID)] = bearbeiteterKunde; // override old infos
                            input = 9; //leave function
                            break;
                        case 9:
                            break;
                        default:
                            ConsoleUtils.PrintInvalidMessage();
                            break;
                    }
                } while (input != 9);
            }
            else
            {
                PrintKeinKunde();
            }
        }

        private void NewKunde()
        {
            ConsoleUtils.PrintTitle();

            string vorname, nachname, adresse, plz, kundennummer;
            DateTime geburtsdatum;

            Console.Write("Vorname: ");
            vorname = Console.ReadLine();
            Console.Write("Nachname: ");
            nachname = Console.ReadLine();
            Console.Write("Adresse: ");
            adresse = Console.ReadLine();
            Console.Write("PLZ: ");
            plz = Console.ReadLine();
            geburtsdatum = GetGeburtstag();
            Console.Write("Kundennummer: ");
            kundennummer = Console.ReadLine();

            Kunde kunde = new Kunde(-1, vorname, nachname, geburtsdatum,
                adresse, plz, kundennummer);

            Console.WriteLine(kunde.GetInfoAll());
            Console.WriteLine();
            ConsoleUtils.PrintSaveTemporary();
            if (Console.ReadLine() == "y")
            {
                controller.SaveNewKunde(kunde);
            }
        }

        private void DeleteKunde()
        {
            Kunde kunde = GetKunde();
            if (null != kunde)
            {
                Console.WriteLine($"Soll Kunde {kunde.Vorname} {kunde.Nachname} gelöscht werden? y/n");
                ConsoleUtils.PrintPrompt();
                if (Console.ReadLine() == "y")
                {
                    controller.GetAllKunden().Remove(kunde);
                }
            }
            else
            {
                PrintKeinKunde();
            }
        }

        private DateTime GetGeburtstag()
        {
            while (true)
            {
                Console.Write("Geburtsdatum (dd.mm.yyyy): ");
                string datum = Console.ReadLine();
                try
                {
                    DateTime geburtsdatum = DateTime.ParseExact(datum, "dd.MM.yyyy", CultureInfo.InvariantCulture);
                    if (geburtsdatum < DateTime.Now)
                    {
                        return geburtsdatum;
                    }
                    else
                    {
                        throw new FormatException();
                    }
                }
                catch (FormatException)
                {
                    ConsoleUtils.PrintInvalidMessage();
                }
                catch (ArgumentNullException)
                {
                    ConsoleUtils.PrintInvalidMessage();
                }
            }
        }

        private Kunde GetKunde()
        {
            ShowAll(controller.GetAllKunden());
            int kundenId = ConsoleUtils.GetUserInputAsInt("Welcher Kunde? [ID]: ");

            return controller.GetAllKunden().Where(x => x.ID == kundenId).SingleOrDefault();
        }

        private void PrintKeinKunde()
        {
            Console.WriteLine("Kunde existiert nicht!");
            ConsoleUtils.PrintContinueMessage();
        }
    }
}
