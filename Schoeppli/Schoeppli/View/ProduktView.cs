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
                            ConsoleUtils.PrintContinueMessage();
                            Console.ReadKey();
                            break;
                        case 2:
                            NewProduct();
                            break;
                        case 3:
                            ShowAllProducts(controller.GetAllProducts());
                            EditProduct(GetUserInputAsInt("Welches Produkt soll bearbeitet werden? [ID]"));
                            break;
                        case 4:
                            ShowAllProducts(controller.GetAllProducts());
                            DeleteProduct(GetUserInputAsInt("Welches Produkt soll gelöscht werden? [ID]"));
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
            Console.WriteLine();

            neuesProdukt.Preis = GetUserInputAsInt("Preis in Rappen: ");
            Console.WriteLine();

            neuesProdukt.Bestand = GetUserInputAsInt("Bestand: ");
            Console.WriteLine();

            neuesProdukt.MinAnzahl = GetUserInputAsInt("Min. Anzahl: ");
            Console.WriteLine();

            neuesProdukt.MaxAnzahl = GetUserInputAsInt("Max. Anzahl: ");
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
            Console.Clear();
            ConsoleUtils.PrintTitle();

            Produkt originalProdukt = controller.GetAllProducts().Where(x => x.ID == productID).SingleOrDefault();

            if (originalProdukt != null)
            {
                Produkt bearbeitetesProdukt = new Produkt(originalProdukt.ID, originalProdukt.Beschreibung,
                    originalProdukt.Kategorie, originalProdukt.Preis, originalProdukt.Bestand, originalProdukt.MinAnzahl, originalProdukt.MaxAnzahl);
                string userInput;

                do
                {
                    Console.WriteLine("1) Beschreibung");
                    Console.WriteLine("2) Kategorie");
                    Console.WriteLine("3) Preis");
                    Console.WriteLine("4) Bestand");
                    Console.WriteLine("5) Min. Anzahl");
                    Console.WriteLine("6) Max. Anzahl");
                    Console.WriteLine();
                    Console.WriteLine("8) Speichern");
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
                            bearbeitetesProdukt.Preis = GetUserInputAsInt("Neuer Wert: ");
                            break;
                        case "4":
                            Console.WriteLine($"Alter Wert: {bearbeitetesProdukt.Bestand}");
                            bearbeitetesProdukt.Bestand = GetUserInputAsInt("Neuer Wert: ");
                            break;
                        case "5":
                            Console.WriteLine($"Alter Wert: {bearbeitetesProdukt.MinAnzahl}");
                            bearbeitetesProdukt.MinAnzahl = GetUserInputAsInt("Neuer Wert: ");
                            break;
                        case "6":
                            Console.WriteLine($"Alter Wert: {bearbeitetesProdukt.MaxAnzahl}");
                            bearbeitetesProdukt.MaxAnzahl = GetUserInputAsInt("Neuer Wert: ");
                            break;
                        case "8":
                            controller.GetAllProducts()[controller.GetAllProducts().FindIndex(ind => ind.ID == bearbeitetesProdukt.ID)] = bearbeitetesProdukt;
                            break;
                        default:
                            break;
                    }
                    Console.Clear();
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
            Console.Clear();
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

        private int GetUserInputAsInt(string userText)
        {
            return ConsoleUtils.GetUserInputAsInt(userText);
        }
    }
}
