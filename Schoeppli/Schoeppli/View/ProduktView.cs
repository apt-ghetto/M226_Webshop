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
    public class ProduktView : ISubView<Produkt>
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
                ConsoleUtils.PrintTitle();
                ShowMenu();
                ConsoleUtils.PrintPrompt();
                if (byte.TryParse(Console.ReadLine(), out input))
                {
                    switch (input)
                    {
                        case 1:
                            ShowAll(controller.GetAllProducts());
                            ConsoleUtils.PrintContinueMessage();
                            Console.ReadKey();
                            break;
                        case 2:
                            NewProduct();
                            break;
                        case 3:
                            ShowAll(controller.GetAllProducts());
                            EditProduct(ConsoleUtils.GetUserInputAsInt("Welches Produkt soll bearbeitet werden? [ID]"));
                            break;
                        case 4:
                            ShowAll(controller.GetAllProducts());
                            DeleteProduct(ConsoleUtils.GetUserInputAsInt("Welches Produkt soll gelöscht werden? [ID]"));
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

        public void ShowAll(List<Produkt> produkte)
        {
            ConsoleUtils.PrintTitle();
            if (produkte != null)
            {
                produkte.ForEach(Console.WriteLine);
            }            
            Console.WriteLine();
        }

        private void NewProduct()
        {
            Produkt neuesProdukt = new Produkt();

            ConsoleUtils.PrintTitle();

            Console.Write("Beschreibung: ");
            neuesProdukt.Beschreibung = Console.ReadLine();
            Console.WriteLine();

            neuesProdukt.Kategorie = ChooseCategory();
            Console.WriteLine();

            neuesProdukt.Preis = ConsoleUtils.GetUserInputAsInt("Preis in Rappen: ");
            Console.WriteLine();

            neuesProdukt.Bestand = ConsoleUtils.GetUserInputAsInt("Bestand: ");
            Console.WriteLine();

            Console.WriteLine(neuesProdukt.GetInfoAll());
            Console.WriteLine();
            Console.WriteLine("Möchten sie dieses Produkt speichern? y/n");
            ConsoleUtils.PrintPrompt();
            if (Console.ReadLine() == "y")
            {
                controller.SaveNewProduct(neuesProdukt);
            }
        }

        private void EditProduct(int productID)
        {
            ConsoleUtils.PrintTitle();

            Produkt originalProdukt = controller.GetAllProducts().Where(x => x.ID == productID).SingleOrDefault();

            if (originalProdukt != null)
            {
                Produkt bearbeitetesProdukt = new Produkt(originalProdukt.ID, originalProdukt.Beschreibung,
                    originalProdukt.Kategorie, originalProdukt.Preis, originalProdukt.Bestand);
                string userInput;

                do
                {
                    Console.Clear();
                    Console.WriteLine("1) Beschreibung");
                    Console.WriteLine("2) Kategorie");
                    Console.WriteLine("3) Preis");
                    Console.WriteLine("4) Bestand");
                    Console.WriteLine();
                    Console.WriteLine("8) Temporär speichern");
                    Console.WriteLine();
                    Console.WriteLine("9) Zurück");
                    Console.WriteLine();
                    Console.WriteLine("Welche Eigenschaft soll bearbeitet werden?");
                    ConsoleUtils.PrintPrompt();
                    userInput = Console.ReadLine();

                    switch (userInput)
                    {
                        case "1":
                            Console.WriteLine($"Alter Wert: {bearbeitetesProdukt.Beschreibung}");
                            Console.Write("Neuer Wert: ");
                            bearbeitetesProdukt.Beschreibung = Console.ReadLine();
                            break;
                        case "2":
                            Console.WriteLine($"Alter Wert: {bearbeitetesProdukt.Kategorie}");
                            bearbeitetesProdukt.Kategorie = ChooseCategory();
                            break;
                        case "3":
                            Console.WriteLine($"Alter Wert: {bearbeitetesProdukt.Preis}");
                            bearbeitetesProdukt.Preis = ConsoleUtils.GetUserInputAsInt("Neuer Wert: ");
                            break;
                        case "4":
                            Console.WriteLine($"Alter Wert: {bearbeitetesProdukt.Bestand}");
                            bearbeitetesProdukt.Bestand = ConsoleUtils.GetUserInputAsInt("Neuer Wert: ");
                            break;
                        case "8":
                            controller.GetAllProducts()[controller.GetAllProducts().FindIndex(ind => ind.ID == bearbeitetesProdukt.ID)] = bearbeitetesProdukt;
                            userInput = "9";
                            break;
                        default:
                            break;
                    }                    
                } while (userInput != "9");

            }
            else
            {
                Console.WriteLine("Produkt existiert nicht");
                ConsoleUtils.PrintContinueMessage();
                Console.ReadKey();
            }
        }

        private void DeleteProduct(int id)
        {
            ConsoleUtils.PrintTitle();
            Produkt produkt = controller.GetAllProducts().Where(x => x.ID == id).SingleOrDefault();

            if (null != produkt)
            {
                Console.WriteLine($"Wollen Sie {produkt.GetInfoAll()} wirklich löschen? y/n");
                ConsoleUtils.PrintPrompt();
                if (Console.ReadLine() == "y")
                {
                    controller.GetAllProducts().Remove(produkt);
                }
            }
            else
            {
                Console.WriteLine("Produkt existiert nicht");
                ConsoleUtils.PrintContinueMessage();
                Console.ReadKey();
            }
        }

        private Kategorie ChooseCategory()
        {
            foreach (var cat in Enum.GetValues(typeof(Kategorie)))
            {
                Console.WriteLine($"{(int)cat}) {cat}");
            }

            do
            {
                int userCat;

                Console.Write("Kategorie #: ");

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
