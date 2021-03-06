﻿using Schoeppli.Controller;
using Schoeppli.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schoeppli.View
{
    // Klasse für die Interaktion mit dem Hauptmenü der Applikation
    public class MainView : IView
    {
        private PersonController personenCtrl;
        private ProduktController produktCtrl;
        private BestellungController bestellCtrl;

        // Konstruktor
        public MainView()
        {
            personenCtrl = new PersonController();
            produktCtrl = new ProduktController();
            bestellCtrl = new BestellungController();
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
                            MitarbeiterView mitarbeiterView = new MitarbeiterView(personenCtrl);
                            mitarbeiterView.ShowView();
                            break;
                        case 2:
                            KundenView kundenView = new KundenView(personenCtrl);
                            kundenView.ShowView();
                            break;
                        case 3:
                            ProduktView produktView = new ProduktView(produktCtrl);
                            produktView.ShowView();
                            break;
                        case 4:
                            BestellungView bestellungView = new BestellungView(bestellCtrl, personenCtrl, produktCtrl);
                            bestellungView.ShowView();
                            break;
                        case 5:
                            RechnungsView rechnungsView = new RechnungsView(bestellCtrl, produktCtrl);
                            rechnungsView.ShowView();
                            break;
                        case 8:
                            break;
                        case 9:
                            personenCtrl.WriteData();
                            produktCtrl.WriteData();
                            bestellCtrl.WriteData();
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
            } while (input != 9 && input != 8);
        }

        // Anzeigen des Menüs für den Benutzer
        public void ShowMenu()
        {
            Console.WriteLine("1) Mitarbeiterverwaltung");
            Console.WriteLine("2) Kundenverwaltung");
            Console.WriteLine("3) Produktverwaltung");
            Console.WriteLine("4) Bestellungen");
            Console.WriteLine("5) Rechnungen");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("8) Beenden ohne zu Speichern");
            Console.WriteLine("9) Speichern und Beenden");
            Console.WriteLine();
        }
    }
}
