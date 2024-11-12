using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domaci2
{
    class Program
    {
        static string DateValidation()
        {
            string newDate;
            while (true)
            {
                Console.Write("Unesite novi datum rođenja (yyyy/mm/dd) - ");

                DateTime isCorrectDate;
                if (!DateTime.TryParse(Console.ReadLine(), out isCorrectDate))
                {
                    Console.WriteLine("Nepravilan unos datuma rođenja molimo vas da unesete u obliku (YYYY/MM/DD).");

                    continue;
                }
                
                
                newDate = isCorrectDate.ToString("yyyy/MM/dd");                
                break;
            }
            return newDate;
        }
        static void EnterNewUser()
         {            
            Console.Clear();
            Console.WriteLine("Unos novog korisnika --->");             

            Console.Write("Unesite ime korisnika - ");
            string newName = Console.ReadLine();
            Console.Write("Unesite prezime korisnika - ");
            string newSurname = Console.ReadLine();
            string newDate = DateValidation();


            Console.WriteLine("Uspjesno ste unijeli novog korisnika.");
            
            users.Add(new Dictionary<string, string> {
                {"id", (users.Count+1).ToString() },
                {"ime", newName },
                {"prezime", newSurname },
                {"datum_rodenja", newDate }

            });
            Console.ReadKey();


        }
        static void DeleteMenu()
        {
            bool exit = false;
            while (!exit)
            {
                Console.Clear();
                Console.WriteLine("1 - Brisanje po ID-u");
                Console.WriteLine("2 - Brisanje po imenu i prezimenu");
                Console.WriteLine("0 - Vrati se nazad");
                string enteredValue = Console.ReadLine();
                switch (enteredValue)
                {
                    case "1":
                        DeleteUserByID();
                        break;

                    case "2":
                        DeleteUserByNameAndSurname();
                        break;
                    case "0":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Nevažeći unos.\nMolimo vas da unesete ponovno.");
                        Console.ReadKey();
                        continue;
                }


            }
        }
        static void DeleteUserByID()
        {
            Console.Clear();
            Console.Write("Unesite ID - ");
            string enteredId = Console.ReadLine();
            bool exisist = false;
            foreach (var user in users)
            {
                if (user["id"] == enteredId)
                {
                    exisist = true;
                    users.Remove(user);
                    Console.WriteLine($"Uklonjen je korisnik --> {user["ime"]} {user["prezime"]}");
                    Console.ReadKey();
                    break;
                } 
                   
            }
            if (!exisist)
            {
                Console.WriteLine("Ne postoji korisnik sa ID-em --> "+ enteredId);
                Console.ReadKey();
            }
            
        }
        static void DeleteUserByNameAndSurname()
        {
            Console.Clear();
            Console.Write("Unesite ime - ");
            string enteredName = Console.ReadLine();
            Console.Write("Unesite prezime - ");
            string enteredSurname = Console.ReadLine();
            bool exists = false;            
            foreach (var user in users)
            {
                if (user["ime"].ToLower() == enteredName.Trim().ToLower() 
                    && user["prezime"].ToLower() == enteredSurname.Trim().ToLower())
                {
                    exists = true;
                    users.Remove(user);
                    Console.WriteLine("Korisnik je obrisan.");
                    Console.ReadKey();
                    break;
                }
                
            }
            if (!exists)
            {
                Console.WriteLine("Traženi korisnik nije pronađen.");
                Console.ReadKey();
            }
        }
        static void EditUserByID()
        {
            bool isValid = false;
            do
            {              
                Console.Clear();
                Console.Write("Unesite ID koji zelite urediti - ");
                string enteredId = Console.ReadLine();
                foreach (var user in users)
                {
                    if (user["id"] == enteredId)
                    {
                        Console.WriteLine($"Trenutno ime {user["ime"]}");
                        Console.Write("Unesite novo ime: ");
                        string editedName = Console.ReadLine();
                        Console.WriteLine();

                        Console.WriteLine($"Trenutno prezime {user["prezime"]}");
                        Console.Write("Unestie novo prezime: ");
                        string editedSurname = Console.ReadLine();
                        Console.WriteLine();

                        Console.WriteLine($"Trenutni datum rođenja {user["datum_rodenja"]}");
                        string newDate = DateValidation();
                        Console.WriteLine();

                        user["ime"] = editedName;
                        user["prezime"] = editedSurname;
                        user["datum_rodenja"] = newDate;
                        isValid = true;
                        Console.WriteLine("Uspjesno unijeti novi podaci za korisnika.");
                        Console.ReadKey();
                    }
                }
                if (!isValid)
                {
                    Console.WriteLine("Ne postoji traženi ID, unesite ponovo");
                    Console.ReadKey();
                }
            } while (!isValid);

            
        }
        static void UsersBySurname()
        {
            Console.Clear();
            Console.WriteLine("Pregled korisnika...\n");
            foreach (var user in users.OrderBy(k => k["prezime"]))
            {
                Console.WriteLine($"{user["id"]} - {user["ime"]} - {user["prezime"]} - {user["datum_rodenja"]}");
            }
            Console.ReadKey();
        }
        static void UsersOlderThan()
        {
            Console.Clear();
            Console.WriteLine("Pregled korisnika starijih od 30 godina...\n");
            DateTime currentDate = DateTime.Now;
            foreach (var user in users)
            {
                DateTime dateOfBirth;
                if (DateTime.TryParse(user["datum_rodenja"], out dateOfBirth))
                {
                    int yearOfBirth = dateOfBirth.Year;
                    int currentYear = DateTime.Now.Year;
                    if (currentYear - yearOfBirth > 30)
                    {
                        Console.WriteLine($"{user["id"]} - {user["ime"]} - {user["prezime"]} - {user["datum_rodenja"]}");
                    }
                }
                
            }
            Console.ReadKey();
        }
        static void OverviewMenu()
        {
            bool exit = false;
            while (!exit)
            {
                Console.Clear();
                Console.WriteLine("1 - Ispis svih po prezimenu");
                Console.WriteLine("2 - Ispis svih starijih od 30 god.");
                Console.WriteLine("3 - Ispis svih koji imaju barem jedan račun u minusu");
                Console.WriteLine("0 - Vrati se nazad");
                string enteredValue = Console.ReadLine();
                switch (enteredValue)
                {
                    case "1":
                        UsersBySurname();
                        break;

                    case "2":
                        UsersOlderThan();
                        break;
                    case "3": //vracam se na ovo kada zavrsim racune
                        break;
                    case "0":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Nevažeći unos.\nMolimo vas da unesete ponovno.");
                        Console.ReadKey();
                        continue;
                }

            } 
        }
            static void UsersMenu()
        {
            bool exit = false;
            while (!exit)
            {
                Console.Clear();
                Console.WriteLine("1 - Unos novog korisnika");
                Console.WriteLine("2 - Brisanje korisnika");
                Console.WriteLine("3 - Uređivanje korisnika");
                Console.WriteLine("4 - Pregled korisnika");
                Console.WriteLine("0 - Vrati se nazad");
                string enteredValue = Console.ReadLine();
                switch (enteredValue)
                {
                    case "1":
                        EnterNewUser();
                        break;
                    case "2":
                        DeleteMenu();
                        break;
                    case "3":
                        EditUserByID();
                        break;
                    case "4":
                        OverviewMenu();
                        break;
                    case "0":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Nevažeći unos.\nMolimo vas da unesete ponovno.");
                        Console.ReadKey();
                        continue;
                }


            }
        }
        static List<Dictionary<string, string>> users = new List<Dictionary<string, string>>();

        static void Main(string[] args)
        {
            Dictionary<string, string> user1 = new Dictionary<string, string> 
            {
                {"id", (users.Count+1).ToString() },
                {"ime", "Mirko"},
                {"prezime", "Jerkovic" },
                {"datum_rodenja", "1999/05/11" }
            };
            users.Add(user1);
            Dictionary<string, string> user2 = new Dictionary<string, string>
            {
                {"id", (users.Count+1).ToString() },
                {"ime", "Slaven"},
                {"prezime", "Bilic" },
                {"datum_rodenja", "1970/05/17" }
            };
            
            users.Add(user2);
            bool closeApp = false;
            while (!closeApp)
            {
                Console.Clear();
                Console.WriteLine("1 - Korisnici");
                Console.WriteLine("2 - Računi");
                Console.WriteLine("3 - Izlaz iz aplikacije");
                string enteredValue = Console.ReadLine();
                switch (enteredValue)
                {

                    case "1":
                        UsersMenu();
                        break;
                    case "2":

                        break;
                    case "3":
                        closeApp = true;
                        break;
                    default:
                        Console.WriteLine("Nevažeći unos.\nMolimo vas da unesete ponovno.");
                        Console.ReadKey();
                        continue;
                }

            }
            

        }
    }
}
