using System;
using System.Diagnostics;
using System.Threading;

namespace MandøOpgave
{
    internal class Program
    {
        static void Main(string[] args)
        {
            bool loginKorrekt = false;

            string brugervalg;

            string[] ugedag = new string[7];
            string[] åbningstiderSommer = new string[7];
            string[] åbningstiderVinter = new string[7];

            string[,] bruger = new string[5, 2];
            string[,] aktiviteter = new string[365, 6];

            SetBruger(bruger);
            SetUgedag(ugedag);
            SetÅbningstider(åbningstiderSommer, åbningstiderVinter);
            SetAktiviteter(aktiviteter);
            
            while (true)
            {
                brugervalg = HovedMenu();

                switch (brugervalg)
                {
                    case "1":
                        PrintÅbningstider(ugedag, åbningstiderSommer, åbningstiderVinter);
                        break;

                    case "2":
                        PrintDagensAktiviteter(aktiviteter);
                        break;

                    case "3":
                        loginKorrekt = PersonaleLogin(bruger);
                        break;

                    default:
                        Fejlmeddelelse();
                        break;
                }

                while(loginKorrekt == true)
                {
                    brugervalg = PersonaleMenu();

                    switch (brugervalg)
                    {
                        case"1":
                            AdministrerÅbningstider(ugedag, åbningstiderSommer);
                            break;

                        case "2":
                            AdministrerÅbningstider(ugedag, åbningstiderVinter);
                            break;

                        case "3":
                            OpretAktiviteter(aktiviteter);
                            break;

                        case"4":
                            loginKorrekt = false;
                            break;

                        default:
                            Fejlmeddelelse();
                            break;
                    }
                }
            }
        }

        static void SetBruger(string[,] modtagetBruger)
        {
            modtagetBruger[0, 0] = "mette";
            modtagetBruger[0, 1] = "joergensen";

            modtagetBruger[1, 0] = "michael";
            modtagetBruger[1, 1] = "terkildsen";

            modtagetBruger[2, 0] = "mie";
            modtagetBruger[2, 1] = "petersen";

            modtagetBruger[3, 0] = "nicolaj";
            modtagetBruger[3, 1] = "soerensen";

            modtagetBruger[4, 0] = "sofie";
            modtagetBruger[4, 1] = "joergensen";
        }

        static void SetUgedag(string[] modtagetUgedag)
        {
            modtagetUgedag[0] = "Mandag";
            modtagetUgedag[1] = "Tirsdag";
            modtagetUgedag[2] = "Onsdag";
            modtagetUgedag[3] = "Torsdag";
            modtagetUgedag[4] = "Fredag";
            modtagetUgedag[5] = "Loerdag";
            modtagetUgedag[6] = "Soendag";
        }
        
        static void SetÅbningstider(string[] modtagetSommer, string[] modtagetVinter)
        {
            modtagetSommer[0] = "10:00 - 17:00";
            modtagetSommer[1] = "10:00 - 17:00";
            modtagetSommer[2] = "Lukket";
            modtagetSommer[3] = "Lukket";
            modtagetSommer[4] = "10:00 - 17:00";
            modtagetSommer[5] = "08:00 - 16:00";
            modtagetSommer[6] = "10:00 - 13:00";

            modtagetVinter[0] = "10:00 - 12:00";
            modtagetVinter[1] = "Lukket";
            modtagetVinter[2] = "Lukket";
            modtagetVinter[3] = "Lukket";
            modtagetVinter[4] = "10:00 - 12:00";
            modtagetVinter[5] = "08:00 - 16:00";
            modtagetVinter[6] = "10:00 - 12:00";
        }

        static void SetAktiviteter(string[,] modtagetAktiviteter)
        {
            for (int i = 0; i < modtagetAktiviteter.GetLength(0); i++)
            {
                modtagetAktiviteter[i, 0] = "x";
                modtagetAktiviteter[i, 1] = "x";
                modtagetAktiviteter[i, 2] = "x";
                modtagetAktiviteter[i, 3] = "x";
            }
        }

        static string HovedMenu()
        {
            string hoveMenuValg;

            Console.Write("Velkommen til Mandoe turistbureau\n\n");
            Console.Write("1. Se aabningstider\n2. Dagens aktiviteter\n3. Personale login\n\nValg: ");

            hoveMenuValg = Console.ReadLine();

            Console.Clear();

            return hoveMenuValg;
        }

        static string PersonaleMenu()
        {
            string personaleMenuValg;

            Console.Write("Personale menu\n\n1. Administrer aabningstider sommer\n2. Administrer aabningstider vinter\n3. Tilfoej aktiviteter\n4. Tilbage\n\nValg: ");
            
            personaleMenuValg = Console.ReadLine();

            Console.Clear();

            return personaleMenuValg;
        }

        static void PrintÅbningstider(string[] modtagetUgedag, string[] modtagetÅbningstiderSommer, string[] modtagetÅbningstiderVinter)
        {
            DateTime dagsDato = DateTime.Now;

            int season = dagsDato.Month;

            string dagPåUgen = Convert.ToString(dagsDato.DayOfWeek);

            if (season >= 4 && season <= 9)
            {
                Console.WriteLine("Sommerseason: \n");

                for (int i = 0; i <= 6 - 1; i++)
                {
                    if (UgedagTilInt(dagPåUgen) == i)
                    {
                        Console.WriteLine("{0}\t\t{1} (i dag)", modtagetUgedag[i], modtagetÅbningstiderSommer[i]);
                    }
                    else
                    {
                        Console.WriteLine("{0}\t\t{1}", modtagetUgedag[i], modtagetÅbningstiderSommer[i]);
                    }
                }
                Retur();
            }
            else
            {
                Console.WriteLine("Vinterseason: \n");

                for (int i = 0; i <= 6 - 1; i++)
                {
                    if (UgedagTilInt(dagPåUgen) == i)
                    {
                        Console.WriteLine("{0}\t\t{1}\t(i dag)", modtagetUgedag[i], modtagetÅbningstiderSommer[i]);
                    }
                    else
                    {
                        Console.WriteLine("{0}\t\t{1}", modtagetUgedag[i], modtagetÅbningstiderVinter[i]);
                    }
                }
                Retur();
            }
        }

        static int UgedagTilInt(string modtagetDagPåUgen)
        {
            int ugedagInt = 10;

            switch (modtagetDagPåUgen)
            {
                case "Monday":
                    ugedagInt = 0;
                    break;

                case "Tuesday":
                    ugedagInt = 1;
                    break;

                case "Wednesday":
                    ugedagInt = 2;
                    break;

                case "Thursday":
                    ugedagInt = 3;
                    break;

                case "Friday":
                    ugedagInt = 4;
                    break;

                case "Saturday":
                    ugedagInt = 5;
                    break;

                case "Sunday":
                    ugedagInt = 6;
                    break;
            }
            return ugedagInt;
        }

        static void PrintDagensAktiviteter(string[,] modtagetAktiviteter)
        {
            string dag, måned, år;
            bool angivetAktiviteter = false;

            DateTime dagsDato = DateTime.Now;

            dag = Convert.ToString(dagsDato.Day);
            måned = Convert.ToString(dagsDato.Month);
            år = Convert.ToString(dagsDato.Year);

            Console.WriteLine("Dagens aktiviteter\n");

            Console.Write("Dagens aktivitet(er) er:\n");

            for (int i = 0; i < 365; i++)
            {
                if (modtagetAktiviteter[i, 0] != "x" && modtagetAktiviteter[i, 0] == dag && modtagetAktiviteter[i, 1] == måned && modtagetAktiviteter[i, 2] == år)
                {
                    Console.WriteLine ("'{0}' {1} klokken {2}.", modtagetAktiviteter[i, 3], modtagetAktiviteter[i, 4], modtagetAktiviteter[i, 5]);
                    angivetAktiviteter = true;
                }
            }

            if (angivetAktiviteter == false)
                Console.WriteLine("Ingen aktiviteter angivet for i dag.");

            Retur();
        }

        static bool PersonaleLogin(string[,] modtagetBruger)
        {
            string brugernavn, kodeord;
            bool loginKontrol = false;

            for (int i = 0; i < 3; i++)
            {
                Console.Write("Personale Login");
                Console.Write("\n\nBrugernavn:\t");

                brugernavn = Console.ReadLine();

                Console.Write("Kodeord:\t");

                kodeord = Console.ReadLine();

                Console.Clear();

                for (int k = 0; k < 4; k++)
                {
                    if (brugernavn == modtagetBruger[k, 0] && kodeord == modtagetBruger[k, 1])
                    {
                        loginKontrol = true;
                        break;
                    }
                }

                if (loginKontrol == false)
                {
                    Console.WriteLine("Brugernavn eller kodeord forkert. {0} forsoeg tilbage.", 2 - i);
                    Thread.Sleep(1500);
                    Console.Clear();
                }
                else if (loginKontrol == true)
                {
                    break;
                }
            }
            return loginKontrol;
        }

        static void AdministrerÅbningstider(string[] modtagetUgedag, string[] modtagetÅbningstider)
        {
            int intBrugervalg;
            string brugervalg;

            Console.WriteLine("Administrer aabningstider sommer\n\nVælg hvilken tid der skal opdateres: \n");

            for (int i = 0; i < 7; i++)
            {
                Console.WriteLine("{0}. {1}\t\t{2}", i + 1, modtagetUgedag[i], modtagetÅbningstider[i]);
            }

            Console.Write("\nValg: ");

            intBrugervalg = Convert.ToInt32(Console.ReadLine());

            Console.Clear();
            Console.Write("Administrer aabningstider sommer\n\nNy aabningstid for {0}: ", modtagetUgedag[intBrugervalg - 1]);

            brugervalg = Console.ReadLine();

            modtagetÅbningstider[intBrugervalg - 1] = brugervalg;

            Console.Clear();
            Console.WriteLine("Administrer aabningstider sommer\n");
            Console.WriteLine("Opdateret sommerseason: \n");

            for (int i = 0; i < 8 - 1; i++)
            {
                Console.WriteLine("{0}\t\t{1}", modtagetUgedag[i], modtagetÅbningstider[i]);
            }
            Retur();
        }

        static void OpretAktiviteter(string[,] modtagetAktiviteter)
        {
            for (int i = 0; i < modtagetAktiviteter.Length; i++)
            {
                if (modtagetAktiviteter[i, 0] != "x")
                { }
                else
                {
                    string dato;
                    string[] datoAktivitetString;

                    Console.Write("Tilfoej aktiviteter\n\n");
                    Console.Write("Angiv dato (DD:MM:YYYY): ");

                    dato = Console.ReadLine();

                    datoAktivitetString = dato.Split(':');

                    modtagetAktiviteter[i, 0] = datoAktivitetString[0];
                    modtagetAktiviteter[i, 1] = datoAktivitetString[1];
                    modtagetAktiviteter[i, 2] = datoAktivitetString[2];

                    Console.Write("Angiv aktivitet: ");

                    modtagetAktiviteter[i, 3] = Console.ReadLine();

                    Console.Write("Angiv sted: ");

                    modtagetAktiviteter[i, 4] = Console.ReadLine();

                    Console.Write("Angiv tid: ");

                    modtagetAktiviteter[i, 5] = Console.ReadLine();

                    Console.Clear();

                    break;
                }
            }
        }

        static void Fejlmeddelelse()
        {
            Console.Write("Der er sket en fejl");
            Thread.Sleep(1000);
            Console.Clear();
        }

        static void Retur()
        {
            Console.Write("\nTryk enter for at gå tilbage: ");
            Console.ReadKey();
            Console.Clear();
        }
    }
}
