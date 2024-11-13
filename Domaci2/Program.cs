using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domaci2
{
    class Program
    {
        // user part
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
            lastUserId++;
            users.Add(new Dictionary<string, string> {
                {"id", lastUserId.ToString() },
                {"ime", newName },
                {"prezime", newSurname },
                {"datum_rodenja", newDate }

            });
            accounts[lastUserId.ToString()] = new Dictionary<string, double>()
            {
                {"tekuci", 100.00 },
                {"ziro", 0.00 },
                {"prepaid", 0.00 }
            };
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
                Console.WriteLine("Ne postoji korisnik sa ID-em --> " + enteredId);
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
                    case "3": 
                        AccountInDeficit();
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

        //account part
        static void AccountInDeficit()
                {
                    bool isInMinus = false;
                    foreach (var user in users)
                    {
                        var id = user["id"];
                        if (accounts.ContainsKey(id))
                        {
                            if (accounts[id]["tekuci"] < 0 || accounts[id]["ziro"] < 0 || accounts[id]["prepaid"] < 0)
                            {
                                Console.WriteLine($"Korisnik ID-a {user["id"]} {user["ime"]} {user["prezime"]} ima neki od racuna u minusu.");
                                isInMinus = true;
                            }
                        }
                    }
                    if (!isInMinus)
                    {
                        Console.WriteLine("Nije pronađen niti jedan korisnik koji ima racun u minusu.");
                    }
                    Console.ReadKey();
                }
        static void UserValidation()
        {
            bool isFounded = false;
            string userId = null; // Dodajte inicijalizaciju za userId
            do
            {
                Console.Clear();
                Console.Write("Unesite ime i prezime: ");
                var nameAndSurname = Console.ReadLine().Trim().ToLower().Split();
                if (nameAndSurname.Length < 2)
                {
                    Console.WriteLine("Unesite puno ime i prezime");
                    Console.ReadKey();
                    continue;
                }
                var foundUser = users.FirstOrDefault(user => user["ime"].ToLower() == nameAndSurname[0]
                                                             && user["prezime"].ToLower() == nameAndSurname[1]);

                if (foundUser != null)
                {
                    userId = foundUser["id"]; 
                    isFounded = true;
                }
                else
                {
                    Console.WriteLine("Korisnik nije pronađen. Pokušajte ponovno.");
                    Console.ReadKey();
                }

            } while (!isFounded);
            
            ChooseAccount(userId);
        }
        static void ChooseAccount(string userId)
        {
            bool exit = false;
            while (!exit)
            {
                Console.Clear();
                Console.WriteLine("Uspješna prijava!\n");
                Console.WriteLine("1 - Tekući");
                Console.WriteLine("2 - Žiro");
                Console.WriteLine("3 - Prepaid");
                Console.WriteLine("0 - Vrati se nazad");

                string enteredValue = Console.ReadLine();
                switch (enteredValue)
                {

                    case "1":  
                        DisplayTransactionMenu(userId,"tekuci");
                        break;
                    case "2":
                        DisplayTransactionMenu(userId, "ziro");
                        break;
                    case "3":
                        DisplayTransactionMenu(userId, "prepaid");
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
        
        //validation for transactions
        static double GetAmount()
        {
            double amountDecimal;
            while (true)
	        {
                Console.Clear();
                Console.WriteLine("Unesite iznos transakcije:");
                string amount = Console.ReadLine();
                if (!double.TryParse(amount, out amountDecimal) || amountDecimal <= 0)
                {
                    Console.WriteLine("Neispravan iznos. Pokusaj ponovno.");
                    Console.ReadKey();
                    continue;
                }
                break;
	        }
            return amountDecimal;
            
        }
        static string GetTypeOfTransaction()
        {
            string type;
            while (true)
	        {
                Console.Clear();
                Console.WriteLine("Unesite tip transakcije (prihod/rashod):");
                type = Console.ReadLine().ToLower();

                if (type != "prihod" && type != "rashod")
                {
                    Console.WriteLine("Neispravan tip. Može biti 'prihod' ili 'rashod'.");
                    Console.ReadKey();
                    continue;
                }
                break;
	        }
            return type;
            
        }
        static string GetCategory(string type)
        {

            List<string> income = new List<string>{"placa","honorar", "bonus", "penzija","nasljedstvo","povrat poreza","ostalo"};
            List<string> expense = new List<string>{"hrana","stanarina", "zdravstvo", "obrazovanje","prijevoz","sport i rekreacija","zabava", "ostalo"};    

            string category;
            while (true)
	        {
                Console.Clear();
                Console.WriteLine("Odabreite neko od ovih kategorija: ");
                if (type == "prihod")
	            {
                    Console.Write(string.Join(", ", income));
	            }
                else
                {
                    Console.Write(string.Join(", ", expense));
                }

                Console.WriteLine("\nUnesite kategoriju:");
                category = Console.ReadLine().ToLower();
                if (type == "prihod" && !income.Contains(category) ||type == "rashod" && !expense.Contains(category))
	            {
                    Console.WriteLine("Neispravan unos kategorije.");
                    Console.ReadKey();
                    continue;
	            }

                break;
	        }
            return category;
        }
        static string GetDescription()
        {  
            Console.Clear();
            Console.WriteLine("Unesite opis transakcije (ili pritisnite Enter za standardnu):");
            string description = Console.ReadLine();
            description = string.IsNullOrEmpty(description) ? "standardna transakcija" : description;   
            return description;
        }
        static string GetDate()
        {
            string newDate;
            while (true)
            {
                Console.Clear();
                Console.Write("Unesite datum izvršene transakcije (yyyy-MM-dd HH:mm): ");

                DateTime isCorrectDate;
                if (!DateTime.TryParse(Console.ReadLine(), out isCorrectDate))
                {
                    Console.WriteLine("Nepravilan datuma transakcije molimo vas da unesete u obliku (yyyy-MM-dd HH:mm).");
                    Console.ReadKey();
                    continue;
                }


                newDate = isCorrectDate.ToString("yyyy/MM/dd HH:mm");
                break;
            }
            return newDate;
        }

        //transactions
        static void EnterPastTransaction(string userId,string accountType)
        {
            double amountDecimal = GetAmount();
            string type = GetTypeOfTransaction();
            string category = GetCategory(type);
            string description = GetDescription();
            string transactionDate = GetDate();

            string transactionId = Guid.NewGuid().ToString();
            var transaction = new Dictionary<string, string>
            {
                { "id", transactionId },
                { "amount", amountDecimal.ToString("F2") },
                { "type", type },
                { "category", category },
                { "description", description },
                { "dateTime", transactionDate }
            };

            if (!transactions.ContainsKey(userId))
	        {
                transactions[userId] = new Dictionary<string, List<Dictionary<string, string>>>();
	        }
            if (!transactions[userId].ContainsKey(accountType))
	        {
                transactions[userId][accountType] = new List<Dictionary<string, string>>();
	        }
            transactions[userId][accountType].Add(transaction);

            Console.WriteLine("Transakcija uspješno dodana");            
            Console.WriteLine($"Trenutno imate {transactions[userId][accountType].Count} transakcija na ovom računu.");
            Console.ReadKey();
        }
        static void EnterCurrentTransaction(string userId,string accountType)
        {
            double amountDecimal = GetAmount();
            string type = GetTypeOfTransaction();
            string category = GetCategory(type);
            string description = GetDescription();
            
            
            string transactionDate =DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            
            
            string transactionId = Guid.NewGuid().ToString();
            var transaction = new Dictionary<string, string>
            {
                { "id", transactionId },
                { "amount", amountDecimal.ToString("F2") },
                { "type", type },
                { "category", category },
                { "description", description },
                { "dateTime", transactionDate }
            };

            if (!transactions.ContainsKey(userId))
	        {
                transactions[userId] = new Dictionary<string, List<Dictionary<string, string>>>();
	        }
            if (!transactions[userId].ContainsKey(accountType))
	        {
                transactions[userId][accountType] = new List<Dictionary<string, string>>();
	        }
            transactions[userId][accountType].Add(transaction);

            Console.WriteLine("Transakcija uspješno dodana");            
            Console.WriteLine($"Trenutno imate {transactions[userId][accountType].Count} transakcija na ovom računu.");
            Console.ReadKey();
        }
        static void ViewTransactions(string userId, string accountType)
        {
            // Provjera postoji li korisnik i accountType
            if (transactions.ContainsKey(userId) && transactions[userId].ContainsKey(accountType))
            {
                Console.WriteLine($"Transakcije za korisnika ID: {userId}, račun: {accountType}:");       
                var transactionList = transactions[userId][accountType];
        
                if (transactionList.Count == 0)
                {
                    Console.WriteLine("Nema transakcija za ovaj račun.");
                }
                else
                {                   
                    foreach (var transaction in transactionList)
                    {
                        
                        Console.WriteLine($"Tip: {transaction["type"]}, Iznos: {transaction["amount"]}" +
                                          $" Opis: {transaction["description"]}, Kategorija: {transaction["category"]}, Datum: {transaction["dateTime"]}");
                    }
                }
            }
            else
            {
                Console.WriteLine("Nema transakcija za odabrani račun ovog korisnika.");
            }
            Console.ReadKey();
        }
        static void EnterTransactionMenu(string userId,string accountType)
        {
            bool exit = false;
            while (!exit)
            {
                Console.Clear();
                Console.WriteLine("1 - Trenutno izvršena transakcija (po defaultu trenutni datum i vrijeme)");
                Console.WriteLine("2 - Ranije izvršena transakcija (potrebno je upisati datum i vrijeme)");
                Console.WriteLine("0 - Vrati se nazad");

                string enteredValue = Console.ReadLine();

                switch (enteredValue)
                {
                    case "1":
                        EnterCurrentTransaction(userId,accountType);
                        break;
                    case "2":
                        EnterPastTransaction(userId, accountType);
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
        static void DisplayTransactionMenu(string userId,string accountType)
        {
            bool exit = false;
            while (!exit)
            {
                Console.Clear();
                Console.WriteLine("1 - Unos nove transakcije");
                Console.WriteLine("2 - Brisanje transakcije");
                Console.WriteLine("3 - Uređivanje transakcije");
                Console.WriteLine("4 - Pregled transakcija");
                Console.WriteLine("5 - Financijsko izvješće");
                Console.WriteLine("0 - Vrati se nazad");

                string enteredValue = Console.ReadLine();

                switch (enteredValue)
                {
                    case "1":
                        EnterTransactionMenu(userId,accountType);
                        break;
                    case "2":
                        //DeleteTransactionMenu();
                        break;
                    case "3":
                        //EditTransactionMenu();
                        break;
                    case "4":
                        ViewTransactions(userId, accountType);
                        break;
                    case "5":
                        //FinancialReportMenu();
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
        static int lastUserId = 0;
        static Dictionary<string, Dictionary<string, double>> accounts = new Dictionary<string, Dictionary<string, double>>();
        static Dictionary<string, Dictionary<string, List<Dictionary<string, string>>>> transactions = new Dictionary<string, Dictionary<string, List<Dictionary<string, string>>>>();

        static void Main(string[] args)
        {
            lastUserId++;
            Dictionary<string, string> user1 = new Dictionary<string, string>
            {
                {"id", lastUserId.ToString() },
                {"ime", "Mirko"},
                {"prezime", "Jerkovic" },
                {"datum_rodenja", "1999/05/11" },

            };

            users.Add(user1);
            lastUserId++;
            accounts[lastUserId.ToString()] = new Dictionary<string, double>
            {
                {"tekuci", 100.00 },
                {"ziro", 0.00 },
                {"prepaid", 0.00 }
            };
            Dictionary<string, string> user2 = new Dictionary<string, string>
            {
                {"id", lastUserId.ToString() },
                {"ime", "Slaven"},
                {"prezime", "Bilic" },
                {"datum_rodenja", "1970/05/17" }
            };
            users.Add(user2);

            accounts[lastUserId.ToString()] = new Dictionary<string, double>
            {
                {"tekuci", 100.00 },
                {"ziro", -20.00 },
                {"prepaid", 0.00 }
            };

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
                        UserValidation();
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
