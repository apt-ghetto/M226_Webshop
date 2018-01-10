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
        private BestellungController controller;
        private PersonController pcontroller;

        public BestellungView(BestellungController controller, PersonController pcontroller)
        {
            this.controller = controller;
            this.pcontroller = pcontroller;
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
                Console.Clear();
                ConsoleUtils.PrintTitle();
                ShowMenu();
                ConsoleUtils.PrintPrompt();

                if (byte.TryParse(Console.ReadLine(), out input))
                {
                    switch (input)
                    {
                        case 1:
                            ShowAll(controller.GetAllBestellungen());
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
            Console.Clear();
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
            Console.Clear();
            ConsoleUtils.PrintTitle();

            Kunde kunde = GetKunde();
            DateTime datum = DateTime.Now;

            Bestellung bestellung = new Bestellung(-1, kunde, datum, Bestellstatus.Eingegangen);
            Console.WriteLine(bestellung.GetInfoAll());
            Console.WriteLine();
            ConsoleUtils.PrintSaveTemporary();
            if (Console.ReadLine() == "y")
            {
                controller.SaveNewBestellung(bestellung);
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
                    controller.GetAllBestellungen().Remove(bestellung);
                }
            }
            else
            {
                ConsoleUtils.PrintInvalidMessage();
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
            ShowAll(controller.GetAllBestellungen());
            int bestellId = ConsoleUtils.GetUserInputAsInt("Welche Bestellung? [Bestellnr.]");

            return controller.GetAllBestellungen().Where(x => x.Bestellnummer == bestellId).SingleOrDefault();
        }
        
        private Kunde GetKunde()
        {
            Console.Clear();
            ConsoleUtils.PrintTitle();
            pcontroller.GetAllKunden().ForEach(Console.WriteLine);
            Console.WriteLine();
            int kundenId = ConsoleUtils.GetUserInputAsInt("Welcher Kunde? [ID]");

            return pcontroller.GetAllKunden().Where(x => x.ID == kundenId).SingleOrDefault();
        }

        private void PrintKeineBestellung()
        {
            Console.WriteLine("Bestellung existiert nicht!");
            ConsoleUtils.PrintContinueMessage();
            Console.ReadKey();
        }
    }
}
