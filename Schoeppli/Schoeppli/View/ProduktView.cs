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
    public class ProduktView : IView
    {
        private ProduktController controller;

        public ProduktView(ProduktController controller)
        {
            this.controller = controller;
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
                            ShowAllProducts(controller.GetAllProducts());
                            break;
                        case 2:
                            NewProduct();
                            break;
                        case 3:
                            break;
                        case 4:

                            break;
                        case 5:

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
            Console.WriteLine("1) Alle Produkte anzeigen");
            Console.WriteLine("2) Neues Produkt erstellen");
            Console.WriteLine("3) Produkt bearbeiten");
            Console.WriteLine("4) Produkt löschen");
            Console.WriteLine();
            Console.WriteLine("9) Zurück");
            Console.WriteLine();
        }

        private void ShowAllProducts(List<Produkt> produkte)
        {
            Console.Clear();
            ConsoleUtils.PrintTitle();
            produkte.ForEach(Console.WriteLine);
            Console.WriteLine();
            ConsoleUtils.PrintContinueMessage();
            Console.ReadKey();
        }

        private void NewProduct()
        {
            Produkt neuesProdukt = new Produkt();

            Console.Clear();
            ConsoleUtils.PrintTitle();

            Console.Write("Beschreibung: ");
            neuesProdukt.Beschreibung = Console.ReadLine();
            Console.WriteLine();

            neuesProdukt.Kategorie = ChooseCategory();
            Console.Write("Kategorie: ");
            neuesProdukt.Beschreibung = Console.ReadLine();
            Console.Write("Beschreibung: ");
            neuesProdukt.Beschreibung = Console.ReadLine();
            Console.Write("Beschreibung: ");
            neuesProdukt.Beschreibung = Console.ReadLine();
        }

        private Kategorie ChooseCategory()
        {
            foreach (var cat in Enum.GetValues(typeof(Kategorie)))
            {
                Console.WriteLine($"{(int)cat}) {cat}");
            }
            Console.Write("Kategorie #: ");

            do
            {
                int userCat;

                if (Int32.TryParse(Console.ReadLine(), out userCat))
                {
                    if (userCat >= 0 && userCat < Enum.GetNames(typeof(Kategorie)).Length)
                    {
                        return (Kategorie)userCat;
                    }
                }

                ConsoleUtils.PrintInvalidMessage();
            } while (true);
            
        }
    }
}
