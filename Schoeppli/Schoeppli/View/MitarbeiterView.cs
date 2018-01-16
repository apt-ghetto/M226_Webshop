using Schoeppli.Controller;
using Schoeppli.Interface;
using Schoeppli.Model;
using Schoeppli.Model.Enumerator;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schoeppli.View
{
    // Klasse für die Interaktion mit der Mitarbeiterverwaltung
    public class MitarbeiterView : ISubView<Mitarbeiter>
    {
        private PersonController controller;

        // Konstruktor
        public MitarbeiterView(PersonController controller)
        {
            this.controller = controller;
        }

        // Anzeigen des Menüs für den Benutzer
        public void ShowMenu()
        {
            Console.WriteLine("1) Alle Mitarbeiter anzeigen");
            Console.WriteLine("2) Status von Mitarbeiter ändern");
            Console.WriteLine("3) Abteilung von Mitarbeiter ändern");
            Console.WriteLine("4) Neuer Mitarbeiter erstellen");
            Console.WriteLine("5) Mitarbeiter löschen");
            Console.WriteLine();
            Console.WriteLine("9) Zurück");
            Console.WriteLine();
        }

        // Einstiegspunkt für die Interaktion
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
                            ShowAll(controller.GetAllMitarbeiter());
                            ShowSingle();
                            break;
                        case 2:
                            EditStatus();
                            break;
                        case 3:
                            EditAbteilung();
                            break;
                        case 4:
                            NewMitarbeiter();
                            break;
                        case 5:
                            DeleteMitarbeiter();
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

        // Anzeigen aller Mitarbeiter
        public void ShowAll(List<Mitarbeiter> mitarbeiter)
        {
            ConsoleUtils.PrintTitle();
            mitarbeiter.ForEach(Console.WriteLine);
        }

        // Anzeigen eines einzelnen Mitarbeiters
        public void ShowSingle()
        {
            do
            {
                Console.WriteLine();
                Console.WriteLine("Mitarbeiter Details einsehen? y/n");
                ConsoleUtils.PrintPrompt();
                if (Console.ReadLine() == "y")
                {
                    Console.WriteLine();
                    int mitarbeiterID = ConsoleUtils.GetUserInputAsInt("Mitarbeiter ID: ");
                    Person mitarbeiter = controller.GetAllMitarbeiter().Find(i => i.ID == mitarbeiterID);
                    if (mitarbeiter != null)
                    {
                        Console.WriteLine(mitarbeiter.GetInfoAll());
                        Console.WriteLine();
                    }
                    else
                    {
                        PrintKeinMitarbeiter();
                    }
                }
                else
                {
                    return;
                }                
            } while (true);
        }

        // Bearbeiten des Status eines Mitarbeiters
        private void EditStatus()
        {
            Mitarbeiter mitarbeiter = GetMitarbeiter();
            if (null != mitarbeiter)
            {
                Console.WriteLine($"Alter Wert: {mitarbeiter.Status}");
                Status neuerStatus = ChooseStatus();
                ConsoleUtils.PrintSaveTemporary();
                if(Console.ReadLine() == "y")
                {
                    mitarbeiter.Status = neuerStatus;
                }
            }
            else
            {
                PrintKeinMitarbeiter();
            }
        }

        // Bearbeiten der Abteilung eines Mitarbeiters
        private void EditAbteilung()
        {
            Mitarbeiter mitarbeiter = GetMitarbeiter();

            if (null != mitarbeiter)
            {
                Console.WriteLine($"Alter Wert: {mitarbeiter.Abteilung}");
                Abteilung neueAbteilung = ChooseAbteilung();
                ConsoleUtils.PrintSaveTemporary();
                if (Console.ReadLine() == "y")
                {
                    mitarbeiter.Abteilung = neueAbteilung;
                }
            }
            else
            {
                PrintKeinMitarbeiter();
            }
        }

        // Neuen Mitarbeiter erstellen
        private void NewMitarbeiter()
        {
            ConsoleUtils.PrintTitle();

            string vorname, nachname, adresse, plz;
            DateTime geburtsdatum;
            Abteilung abteilung;
            Status status;
            int lohn;

            Console.Write("Vorname: ");
            vorname = Console.ReadLine();
            Console.Write("Nachname: ");
            nachname = Console.ReadLine();
            Console.Write("Adresse: ");
            adresse = Console.ReadLine();
            Console.Write("PLZ: ");
            plz = Console.ReadLine();
            geburtsdatum = GetGeburtstag();
            abteilung = ChooseAbteilung();
            status = ChooseStatus();
            lohn = ConsoleUtils.GetUserInputAsInt("Lohn (Franken/Monat): ");

            Mitarbeiter mitarbeiter = new Mitarbeiter(-1, vorname, nachname,
                geburtsdatum, adresse, plz, abteilung, lohn, status);

            Console.WriteLine();
            // Zu erstellenden Mitarbeiter zur Übersicht noch einmal anzeigen
            Console.WriteLine(mitarbeiter.GetInfoAll());
            Console.WriteLine();
            ConsoleUtils.PrintSaveTemporary();
            if (Console.ReadLine() == "y")
            {
                controller.SaveNewMitarbeiter(mitarbeiter);
            }
        }

        // Mitarbeiter löschen
        private void DeleteMitarbeiter()
        {
            Mitarbeiter mitarbeiter = GetMitarbeiter();
            if (null != mitarbeiter)
            {
                Console.WriteLine($"Soll Mitarbeiter {mitarbeiter.Vorname} {mitarbeiter.Nachname} gelöscht werden? y/n");
                ConsoleUtils.PrintPrompt();
                if (Console.ReadLine() == "y")
                {
                    controller.GetAllMitarbeiter().Remove(mitarbeiter);
                }
            }
            else
            {
                PrintKeinMitarbeiter();
            }
        }

        // Helfermethode zur Auswahl des Geburtstags durch den Benutzer
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

        // Helfermethode zur Auswahl einer Abteilung
        private Abteilung ChooseAbteilung()
        {
            foreach(Abteilung abt in Enum.GetValues(typeof(Abteilung)))
            {
                Console.WriteLine($"{(int)abt}) {abt}");
            }

            do
            {
                int mitarbeiterAbteilung;

                Console.Write("Abteilung #: ");

                if (int.TryParse(Console.ReadLine(), out mitarbeiterAbteilung))
                {
                    if (mitarbeiterAbteilung >= 0 && mitarbeiterAbteilung < Enum.GetNames(typeof(Abteilung)).Length)
                    {
                        return (Abteilung)mitarbeiterAbteilung;
                    }
                }

                ConsoleUtils.PrintInvalidMessage();
            } while (true);
        }

        // Helfermethode zur Auswahl eines Status
        private Status ChooseStatus()
        {
            foreach (Status stat in Enum.GetValues(typeof(Status)))
            {
                Console.WriteLine($"{(int)stat}) {stat}");
            }

            do
            {
                int status;

                Console.Write("Status #: ");

                if (int.TryParse(Console.ReadLine(), out status))
                {
                    if (status >= 0 && status < Enum.GetNames(typeof(Status)).Length)
                    {
                        return (Status)status;
                    }
                }

                ConsoleUtils.PrintInvalidMessage();
            } while (true);
        }

        // Helfermethode zur Auswahl eines Mitarbeiters
        private Mitarbeiter GetMitarbeiter()
        {
            ShowAll(controller.GetAllMitarbeiter());
            int mitarbeiterId = ConsoleUtils.GetUserInputAsInt("Welcher Mitarbeiter? [ID]: ");

            return controller.GetAllMitarbeiter().Where(x => x.ID == mitarbeiterId).SingleOrDefault();
        }

        // Warnmeldung bei nicht existierendem Mitarbeiter
        private void PrintKeinMitarbeiter()
        {
            Console.WriteLine("Mitarbeiter existiert nicht!");
            ConsoleUtils.PrintContinueMessage();
        }
    }
}
