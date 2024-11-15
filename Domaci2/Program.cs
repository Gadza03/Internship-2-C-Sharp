using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domaci2
{
    class Program
    {
        //create user to use program
        static void AddTransactionToUser(string userId, string accountType, double amount,string type, string category, string description, string dateTime)
        {
            uniqueTransactionId++;
            var transaction = new Dictionary<string, string>
            {
                { "id", uniqueTransactionId.ToString() },
                { "amount", amount.ToString("F2") },
                { "type", type },
                { "category", category },
                { "description", description },
                { "dateTime", dateTime }
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

            if (type == "prihod")               
                accounts[userId][accountType] += amount;
            else if (type == "rashod")        
                accounts[userId][accountType] -= amount;

        }
        static void AddUsersAndAcconts(string name, string surname, string dateOfBirth)
        {
            lastUserId++;
            var user = new Dictionary<string, string>()
            {
                {"id",lastUserId.ToString()},
                {"ime", name},
                {"prezime", surname},
                {"datum_rodenja", dateOfBirth}

            };
            users.Add(user);

            accounts[lastUserId.ToString()] = new Dictionary<string, double>
            {
                {"tekuci", 100.00 },
                {"ziro", 0.00 },
                {"prepaid", 0.00 }
            };
            
            AddTransactionToUser(lastUserId.ToString(), "tekuci", 23.55, "prihod","placa","Isplacena placa", "2024/07/31 08:30" );
            AddTransactionToUser(lastUserId.ToString(), "tekuci", 1500.00, "prihod", "placa", "Mjesecna plata za listopad", "2024/10/31 08:30");
            AddTransactionToUser(lastUserId.ToString(), "tekuci", 550.00, "rashod", "stanarina", "Stanarina za studeni", "2024/11/01 10:00");
            AddTransactionToUser(lastUserId.ToString(), "tekuci", 25.50, "rashod", "hrana", "Kupovina namirnica", "2024/06/15 09:30");
            AddTransactionToUser(lastUserId.ToString(), "tekuci", 15.50, "rashod", "hrana", "Kupovina mesa", "2024/06/10 12:30");
            AddTransactionToUser(lastUserId.ToString(), "tekuci", 60.00, "rashod", "sport i rekreacija", "Članarina za teretanu", "2024/04/10 18:30");

            AddTransactionToUser(lastUserId.ToString(), "ziro", 200.35, "prihod","honorar","Honorar za 11. mjesec", "2023/10/31 18:30" );
            AddTransactionToUser(lastUserId.ToString(), "ziro", 300.00, "prihod", "bonus", "Godisnji bonus", "2024/09/15 14:45");
            AddTransactionToUser(lastUserId.ToString(), "ziro", 20.00, "rashod", "zabava", "Ulaznice za koncert", "2024/09/05 19:30");
            AddTransactionToUser(lastUserId.ToString(), "ziro", 5000.00, "prihod", "nasljedstvo", "Nasljedstvo od bake", "2024/05/20 16:00");

            AddTransactionToUser(lastUserId.ToString(), "prepaid", 6.99, "rashod","hrana","Kupovina u FastFood-u", "2024/10/08 15:30" );
            AddTransactionToUser(lastUserId.ToString(), "prepaid", 45.00, "rashod", "prijevoz", "Trosak goriva", "2024/08/20 17:00");
            AddTransactionToUser(lastUserId.ToString(), "prepaid", 120.00, "prihod", "povrat poreza", "Povrat poreza za 2023. godinu", "2024/07/01 12:00");
            AddTransactionToUser(lastUserId.ToString(), "prepaid", 700.00, "prihod", "penzija", "Mjesečna penzija", "2024/03/25 08:45");


           
        }
        static void UsersAccountsAndTransactions()
        {
            AddUsersAndAcconts("Mirko","Jerkovic","1997-12-03");
            AddUsersAndAcconts("Slaven","Bilic","1977-05-22");
            AddUsersAndAcconts("John","Cage","1988-01-12");
            AddUsersAndAcconts("Jana","Milicic","2003-03-18");        
        }
        // user part
        static string DateValidation()
        {
            string newDate;
            while (true)
            {
                Console.Clear();
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
                Console.Write("Unesite ID koji zelite urediti (Enter - korak nazad) - ");
                string enteredId = Console.ReadLine();
                if (enteredId == "")
                    return;

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

        //account part -userId -accountType67
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
            
            string name;
            string surname;
            string userId;
            while (true)
	        {
                Console.Clear();
                Console.Write("Unesite ime: ");
                name = Console.ReadLine().Trim().ToLower(); 
                Console.Write("Unesite prezime: ");
                surname = Console.ReadLine().Trim().ToLower(); 
                var foundUser = users.FirstOrDefault(t => t["ime"].ToLower() == name &&  t["prezime"].ToLower() == surname);
                if(foundUser != null)
                {
                    userId = foundUser["id"];
                    break;
                }
                else
                {
                    Console.WriteLine($"Korisnik imena {name} {surname} nije pronađen. Pokusajte ponovno.");
                    Console.ReadKey();
                    continue;
                }
	        }
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

            

            string category;
            while (true)
	        {
                Console.Clear();
                Console.WriteLine("Odabreite neku od ovih kategorija: ");
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
        //confirmation 
        static bool ConfirmationDialog(string message)
        {
            bool isValid = false;
            do
	        {
                Console.Clear();
                Console.Write($"Želite li zaista {message} ovu transakciju (Da/Ne): ");
                string enteredChoice = Console.ReadLine().ToLower();
                if (enteredChoice == "da")
                {
                    isValid = true;
                    return true;
                }   
                    
                else if(enteredChoice == "ne")
                {
                    Console.WriteLine("odgovor je ne i treeba returnati false");
                    Console.ReadKey();
                    isValid = true;
                    return false;
                }
                    
                else
                {
                    Console.WriteLine("Neispravan unos. Unesite 'Da' ili 'Ne'.");
                    Console.ReadKey();
                    continue;
                }
	            
	            
	        } while (!isValid);
            
            return true;


        }
        //bonus -intern and -extern transactions
        static double CheckAmoutOfTransaction(double currentAccountAmount)
        {
            double amount = 0;
            bool isValid = false;
            do
	        {
                Console.Clear();
                amount = GetAmount();
                if (amount > currentAccountAmount)
                {
                    Console.WriteLine("Transackiju je nemoguće izvršiti unijeli ste veći iznos nego što je vaš saldo.");
                    Console.ReadKey();
                    continue;
                }
                isValid = true;    
	        } while (!isValid);
            return amount;
        }
        static void AddTransctionToList(string userId, string accountType, double amount, string type, string description)
        {
            uniqueTransactionId++;
            var transactionIntern = new Dictionary<string, string>()
            {
                { "id", uniqueTransactionId.ToString() },
                { "amount", amount.ToString("F2") },
                { "type", type },
                { "category", description },
                { "description", "prijenos" },
                { "dateTime", DateTime.Now.ToString() }
            };
                    
            transactions[userId][accountType].Add(transactionIntern);    
            Console.ReadKey();
        }
        static void InternTransaction(string userId, string accountType, double amount)
        {
            
            Console.Clear();            
            Console.WriteLine($"Stanje prije transakcije: {accounts[userId][accountType]}$");
            accounts[userId][accountType] += amount;
            Console.WriteLine($"Transackija od {amount}$ je uspješno izvršena na vaš {accountType} racun.\nTrenutno stanje: {accounts[userId][accountType]}$");
            AddTransctionToList(userId, accountType, amount, "prihod", "interni prihod");
            
        }
        static void ExternTransactionMenu(string userId, string accountType)
        {
            bool isValid = false;
            string enteredId;
            
            do
	        {
                Console.Clear();
                Console.Write("Unesite ID korisnika kojem želite napraviti transakciju: ");
                enteredId = Console.ReadLine().Trim();
                var userFounded = users.Any(user => user["id"] == enteredId);
                if(userFounded)
                    isValid = true;
                else
                {
                    Console.WriteLine("Ne postoji uneseni ID. Pokusajte ponvno.");
                    Console.ReadKey();
                    continue;
                }
	        } while (!isValid);
            double amount = CheckAmoutOfTransaction(accounts[userId][accountType]);

            Console.WriteLine($"Stanje vašeg racuna prije slanja transakcije: {accounts[userId][accountType]}$");
            accounts[userId][accountType] -= amount;
            AddTransctionToList(userId, accountType, amount, "rashod", "eksterni rashod");
            Console.WriteLine($"Stanje vašeg racuna nakon slanja transakcije: {accounts[userId][accountType]}$");     

            accounts[enteredId]["tekuci"] += amount;
            AddTransctionToList(enteredId, "tekuci", amount, "prihod", "eksterni prihod");                   
            

        }    
        static void InternTransactionMenu(string userId, string accountType)
        {
            var userAccounts = accounts[userId];
            var currentAccountAmount = accounts[userId][accountType];
            bool isValidAcount = false;
            string enteredAcountType, firstAvailableAcc = "", secondAvailableAcc = "";
            
            do
	        {
                Console.Clear();
                Console.WriteLine($"Nalazite se na računu {accountType} i vaš saldo na njemu je {currentAccountAmount}$\n" +
                                    $"Dostupni računi za internu transakciju: \n");
                                    
                int counter = 0;
                foreach (var account in userAccounts.Keys)
	            {
                    if (account != accountType)
                    {
                        counter++;
                        if (counter == 1)
                            firstAvailableAcc = account;
                        else if (counter == 2)
                            secondAvailableAcc = account;

                        Console.WriteLine($"{counter} - {account}");
                    }
	            }
                Console.WriteLine("0 - Izlaz");
                Console.Write("Odaberite na koji račun zelite isvršiti transakciju: ");   
                enteredAcountType = Console.ReadLine().Trim().ToLower();
                double amountOfTransaction;
                switch (enteredAcountType)
	            {
                    case "1":                        
                        amountOfTransaction = CheckAmoutOfTransaction(currentAccountAmount);
                        accounts[userId][accountType] -= amountOfTransaction;                        
                        InternTransaction(userId, firstAvailableAcc, amountOfTransaction);
                        AddTransctionToList(userId, accountType, amountOfTransaction, "rashod", "interni rashod");
                        break;
                    case "2":
                        amountOfTransaction = CheckAmoutOfTransaction(currentAccountAmount);
                        accounts[userId][accountType] -= amountOfTransaction;                        
                        InternTransaction(userId, secondAvailableAcc, amountOfTransaction);
                        AddTransctionToList(userId, accountType, amountOfTransaction, "rashod", "interni rashod");
                        break;
                    case "0":
                        break;
		            default:
                        Console.WriteLine("Pogrešan unos. Unestie neki od dostupnih racuna brojkom (1-2).");
                        Console.ReadKey();
                        continue;
	            }            
                isValidAcount = true;
	        } while (!isValidAcount);           
            
            
        }
       


        //display financials
        static DateTime ChooseMonthAndYear()
        {
            Console.Clear();            
            DateTime isCorrectDate;
            bool isValid = false;
            do
            {
                Console.Clear();
                Console.Write("Unesite godinu i mjesec u formatu yyyy/MM: ");                
                if (!DateTime.TryParse(Console.ReadLine(), out isCorrectDate))
                {
                    Console.WriteLine("Nepravilan unos datuma rođenja molimo vas da unesete u obliku (YYYY/MM).");
                    Console.ReadKey();
                    continue;
                }
                isValid = true;        
            } while (!isValid);

            return isCorrectDate;
        }
        static double SumAmountsOfTransactions(List<Dictionary<string,string>> transactionList)
        {
            double sum = 0;
            foreach (var transaction in transactionList)
	        {
                sum += double.Parse(transaction["amount"]);
	        }
            return sum;
        }
        static void DisplayAverageTransactionByCategory(string userId, string accountType)
        {
            var transactionList = transactions[userId][accountType];
            bool isValidCategory = false;
            string enteredCat;
            do
	        {
                Console.Clear();
                Console.Write("Unesite kategoriju koju zelite prikazati: ");
                enteredCat = Console.ReadLine().ToLower();
                if (!income.Contains(enteredCat) && !expense.Contains(enteredCat))
	            {
                    Console.WriteLine("Unesena kategorija ne postoji. Pokusajte ponovno.");
                    Console.ReadKey();
                    continue;
	            }
                isValidCategory = true;
	        } while (!isValidCategory);
            var transactionsWithCategory = transactionList.Where(t => t["category"] == enteredCat).ToList();
            if (transactionsWithCategory.Count == 0)
                 Console.WriteLine($"Nema zabilježenjih transakcija u {enteredCat} kategoriji.");
            else
            {
                var transactionsCategorySum = SumAmountsOfTransactions(transactionsWithCategory);

                var averageTransactionAmount = transactionsCategorySum / transactionsWithCategory.Count;
                Console.WriteLine($"Prosjecni iznos transakcije za {enteredCat} kategoriju"+
                                    $"iznosi {averageTransactionAmount:F2}$");
            }
            Console.ReadKey();

        }
        static void DisplayAverageTransactionByMonthYear(string userId, string accountType)
        {
            var enteredDate = ChooseMonthAndYear();
            var transactionList = transactions[userId][accountType];
            Console.Clear();

            var transactionListForMonthAndYear = transactionList.Where(t => 
            {
                var dateTime = DateTime.Parse(t["dateTime"]);
                return dateTime.Year == enteredDate.Year && dateTime.Month == enteredDate.Month;
            }).ToList();

            if (transactionListForMonthAndYear.Count == 0)
                 Console.WriteLine($"Nema zabilježenjih transakcija u {enteredDate.Month}. mjesec {enteredDate.Year}. godine.");
            else
            {
                var transactionSum = SumAmountsOfTransactions(transactionListForMonthAndYear);
                var averageTransactionAmount = transactionSum / transactionListForMonthAndYear.Count;
            
                Console.WriteLine($"Prosjecni iznos transakcije za {enteredDate.Month}. mjesec u {enteredDate.Year}. godini"+
                                    $"iznosi {averageTransactionAmount:F2}$");
            }
            Console.ReadKey();
        }
        static void DisplayExpensePercentageForCategory(string userId, string accountType)
        {
            string category = GetCategory("rashod");
            var transactionList = transactions[userId][accountType];
            var transactionListCategory = transactionList.Where(t => t["type"] == "rashod" && t["category"] == category).ToList();
            var transactionListExpense = transactionList.Where(t => t["type"] == "rashod").ToList();
            Console.Clear();
            
            if (transactionListExpense.Count == 0)	      
                Console.WriteLine("Korisnik nema nikakve rashode.");
            else
            {
                double categorySum = SumAmountsOfTransactions(transactionListCategory);
                double expenseSum = SumAmountsOfTransactions(transactionListExpense);
                var percentage = (categorySum / expenseSum) * 100;
                Console.WriteLine($"Postotak udjela rashoda za kategoriju {category}: {percentage:F2}%");
            }
            Console.ReadKey(); 
        }
        static void DisplayTotalIncomeExpenseByMonthYear(string userId, string accountType)
        {
            
            var transactionList = transactions[userId][accountType];
            var enteredDate = ChooseMonthAndYear();
            
            var transactionListIncome = transactionList.Where(t => 
            {
                var dateTime = DateTime.Parse(t["dateTime"]);
                return dateTime.Year == enteredDate.Year && dateTime.Month == enteredDate.Month && t["type"] == "prihod";
            }).ToList();

            var transactionListExpense = transactionList.Where(t => 
            {
                var dateTime = DateTime.Parse(t["dateTime"]);
                return dateTime.Year == enteredDate.Year && dateTime.Month == enteredDate.Month && t["type"] == "rashod";
            }).ToList();           

            if (transactionListIncome.Count == 0 && transactionListExpense.Count == 0)
                Console.WriteLine($"Nema zabilježenjih transakcija u {enteredDate.Month}. mjesec {enteredDate.Year}. godine.");
            else
            {
                double incomeSum = SumAmountsOfTransactions(transactionListIncome);
                double expenseSum = SumAmountsOfTransactions(transactionListExpense);
                Console.Clear();
                Console.WriteLine($"Ukupan iznos prihoda i rashoda za {enteredDate.Month}. mjesec {enteredDate.Year}. godine -");
                Console.WriteLine($"Prihodi - {incomeSum.ToString()}$");
                Console.WriteLine($"Rashodi - {expenseSum.ToString()}$");
            }          
            Console.ReadKey();

        }
        static void DisplayTotalTransactionCount(string userId, string accountType)
        {
            Console.Clear();
            var totalTransactions = transactions[userId][accountType].Count;
            Console.WriteLine($"Broj ukupnih transakcija - {totalTransactions}");
            Console.ReadKey();
        }
        static void DisplayAccountBalance(string userId, string accountType)
        {
            Console.Clear();
            var balance = accounts[userId][accountType];
            Console.WriteLine($"Trenutno stanje {accountType} računa - {balance}$");
            if (balance < 0)
                Console.WriteLine("Vaš trenutni saldo je negativan. Molimo poduzmite potrebne akcije.");
            Console.ReadKey();
        }
        static void FinancialReportMenu(string userId, string accountType)
        {
            bool exit = false;
            while (!exit)
            {
                Console.Clear();
                Console.WriteLine("Financijsko izvješće:");
                Console.WriteLine("1 - Trenutno stanje računa (ukupni zbroj prihoda i rashoda)");
                Console.WriteLine("2 - Broj ukupnih transakcija");
                Console.WriteLine("3 - Ukupan iznos prihoda i rashoda za odabrani mjesec i godinu");
                Console.WriteLine("4 - Postotak udjela rashoda za odabranu kategoriju");
                Console.WriteLine("5 - Prosječni iznos transakcije za odabrani mjesec i godinu");
                Console.WriteLine("6 - Prosječni iznos transakcije za odabranu kategoriju");
                Console.WriteLine("0 - Vrati se nazad");

                string enteredValue = Console.ReadLine();

                switch (enteredValue)
                {
                    case "1":
                        DisplayAccountBalance(userId, accountType);
                        break;
                    case "2":
                        DisplayTotalTransactionCount(userId, accountType);
                        break;
                    case "3":
                        DisplayTotalIncomeExpenseByMonthYear(userId, accountType);
                        break;
                    case "4":
                        DisplayExpensePercentageForCategory(userId, accountType);
                        break;
                    case "5":
                        DisplayAverageTransactionByMonthYear(userId, accountType);
                        break;
                    case "6":
                        DisplayAverageTransactionByCategory(userId, accountType);
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
        //view transactions
        static void ViewTransactionsByTypeAndCategory(string userId, string accountType)
        {
            string type = GetTypeOfTransaction();
            string category = GetCategory(type);
            Console.Clear();
            var transactionList = transactions[userId][accountType];
            var transactionsTypeAndCat = transactionList.Where(t => t["category"] == category && t["type"] == type).ToList();
            if (transactionsTypeAndCat.Count == 0)
                Console.WriteLine("Korisnik nema transakcija sa ovom kategorijom.");
            else       
                PrintTransactions(transactionsTypeAndCat);

            Console.ReadKey();

        }
        static void ViewTransactionsByCategory(string userId, string accountType)
        {
            var transactionList = transactions[userId][accountType];
            bool isValidCategory = false;
            string enteredCat;
            do
	        {
                Console.Clear();
                Console.Write("Unesite kategoriju koju zelite prikazati: ");
                enteredCat = Console.ReadLine().ToLower();
                if (!income.Contains(enteredCat) && !expense.Contains(enteredCat))
	            {
                    Console.WriteLine("Unesena kategorija ne postoji. Pokusajte ponovno.");
                    Console.ReadKey();
                    continue;
	            }
                isValidCategory = true;
	        } while (!isValidCategory);

            var transactionsWithCategory = transactionList.Where(t => t["category"] == enteredCat).ToList();
            if (transactionsWithCategory.Count == 0)
                Console.WriteLine("Korisnik nema transakcija sa ovom kategorijom.");
            else       
                PrintTransactions(transactionsWithCategory);

            Console.ReadKey();
            
        }
        static void ViewTransactionByType(string userId, string accountType, string type)
        {
            Console.Clear();
            Console.WriteLine($"Ispis svih transakcija tipa '{type}' -->\n");
            var transactionList = transactions[userId][accountType];
            
            if (transactionList.Count == 0)          
                Console.WriteLine("Nema transakcija za ovaj račun.");       
            else
            {                   
                if (type == "prihod")
	            {
                    var incomeTransactions = transactionList.Where(transaction => transaction["type"] == type).ToList();
                    PrintTransactions(incomeTransactions);

	            }
                else if (type == "rashod")
	            {
                    var expenseTransactions = transactionList.Where(transaction => transaction["type"] == type).ToList();                  
                    PrintTransactions(expenseTransactions);
                }    
            }
            Console.ReadKey();
        
        }
        static void ViewTransactionsSortedByDate(string userId, string accountType, string type)
        {
            Console.Clear();
            Console.WriteLine($"Ispis svih transakcija po datumu {type} -->\n");
            var transactionList = transactions[userId][accountType];
            
            if (transactionList.Count == 0)
            {
                Console.WriteLine("Nema transakcija za ovaj račun.");
                
            }
            
            else
            {                   
                if (type == "uzlazno")
	            {
                    var sortedTransactionsAscending = transactionList.OrderBy(t => DateTime.Parse(t["dateTime"])).ToList();
                    PrintTransactions(sortedTransactionsAscending);
	            }
                else if (type == "silazno")
	            {
                    var sortedTransactionsDescending = transactionList.OrderByDescending(t => DateTime.Parse(t["dateTime"])).ToList();	
                    PrintTransactions(sortedTransactionsDescending);
                }
                

            }
            Console.ReadKey();
        }
        static void ViewTransactionsSortedByDescription(string userId, string accountType)
        {
            Console.Clear();
            Console.WriteLine($"Ispis svih transakcija sortirano po opisu -->\n");
            var transactionList = transactions[userId][accountType];
            
            if (transactionList.Count == 0)
            {
                Console.WriteLine("Nema transakcija za ovaj račun.");
                
            }
            else
            {
                var sortedByDescriptionList = transactionList.OrderBy(t => t["description"]).ToList();
                PrintTransactions(sortedByDescriptionList);
            }
            Console.ReadKey();
        }
        static void ViewTransactionsSortedByAmount(string userId, string accountType, string type)
        {
            Console.Clear();
            Console.WriteLine($"Ispis svih transakcija po vrijednosti {type} -->\n");
            var transactionList = transactions[userId][accountType];
            
            if (transactionList.Count == 0)
            {
                Console.WriteLine("Nema transakcija za ovaj račun.");
                
            }
            
            else
            {                   
                if (type == "uzlazno")
	            {
                    var sortedTransactionsAscending = transactionList.OrderBy(t => double.Parse(t["amount"])).ToList();
                    PrintTransactions(sortedTransactionsAscending);
	            }
                else if (type == "silazno")
	            {
                    var sortedTransactionsDescending = transactionList.OrderByDescending(t => double.Parse(t["amount"])).ToList();	
                    PrintTransactions(sortedTransactionsDescending);
                }
                

            }
            Console.ReadKey();
        }
        static void ViewAllTransactions(string userId, string accountType)
        {
            Console.Clear();
            Console.WriteLine($"Ispis svih transakcija za račun: {accountType}:\n");       
            var transactionList = transactions[userId][accountType];
        
            if (transactionList.Count == 0)
            {
                Console.WriteLine("Nema transakcija za ovaj račun.");
            }
            else
            {                   
                PrintTransactions(transactionList);
            }
            
            Console.ReadKey();
        }
        static void PrintTransactions(List<Dictionary<string, string>> transactionList)
        {
            if (transactionList.Count == 0)
	        {
                Console.WriteLine("Ne postoje transakcije za trazeni tip.");
	        }
            foreach (var transaction in transactionList)
                    {
                        
                        Console.WriteLine($"Tip - {transaction["type"]}, Iznos - {transaction["amount"]}$" +
                                          $" Opis - {transaction["description"]}, Kategorija - {transaction["category"]}, Datum - {transaction["dateTime"]}");
                    }
        }
        static void ViewTransactionsMenu(string userId, string accountType)
        {
            bool exit = false;
            while (!exit)
            {
                Console.Clear();
                Console.WriteLine("Pregled transakcija");
                Console.WriteLine("1 - Sve transakcije kako su spremljene");
                Console.WriteLine("2 - Sve transakcije sortirane po iznosu uzlazno");
                Console.WriteLine("3 - Sve transakcije sortirane po iznosu silazno");
                Console.WriteLine("4 - Sve transakcije sortirane po opisu abecedno");
                Console.WriteLine("5 - Sve transakcije sortirane po datumu uzlazno");
                Console.WriteLine("6 - Sve transakcije sortirane po datumu silazno");
                Console.WriteLine("7 - Svi prihodi");
                Console.WriteLine("8 - Svi rashodi");
                Console.WriteLine("9 - Sve transakcije za odabranu kategoriju");
                Console.WriteLine("10 - Sve transakcije za odabrani tip i kategoriju");
                Console.WriteLine("0 - Vrati se nazad");

                string enteredValue = Console.ReadLine();

                switch (enteredValue)
                {
                    case "1":
                        ViewAllTransactions(userId, accountType);
                        break;
                    case "2":
                        ViewTransactionsSortedByAmount(userId, accountType, "uzlazno");
                        break;
                    case "3":
                        ViewTransactionsSortedByAmount(userId, accountType, "silazno");
                        break;
                    case "4":
                        ViewTransactionsSortedByDescription(userId, accountType);
                        break;
                    case "5":
                        ViewTransactionsSortedByDate(userId,accountType,"uzlazno");
                        break;
                    case "6":
                        ViewTransactionsSortedByDate(userId,accountType,"silazno");
                        break;
                    case "7":
                        ViewTransactionByType(userId, accountType, "prihod");
                        break;
                    case "8":
                        ViewTransactionByType(userId, accountType, "rashod");
                        break;
                    case "9":
                        ViewTransactionsByCategory(userId, accountType);
                        break;
                    case "10":
                        ViewTransactionsByTypeAndCategory(userId, accountType);
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
        //edit transactions
        static void EditTransaction(string userId, string accountType)
        {
            var accountTransactions = transactions[userId][accountType];           
            bool isValidId = false;
            string enteredId;
            do
	        {
                Console.Clear();
                Console.Write("Unesite ID transakcije koju zelite urediti (Enter - korak nazad): ");                
                enteredId = Console.ReadLine();
                if (enteredId == "")
                    return;
                isValidId = accountTransactions.Any(transaction => transaction["id"] == enteredId);              
                
                if (!isValidId)
	            {
                    
                    Console.WriteLine("Uneseni ID transakcije ne postoji. Pokusajte ponovno.");
                    Console.ReadKey();
                    continue;
	            }                         
	        } while (!isValidId);
            
            double amountDecimal = GetAmount();
            string type = GetTypeOfTransaction();
            string category = GetCategory(type);
            string description = GetDescription();
            string transactionDate = GetDate();
            bool confirmation = ConfirmationDialog("urediti");
            if (!confirmation)
	        {
                Console.WriteLine("Prekinuli ste proces uređivanja. Ne spremamo promjene.");
                Console.ReadKey();
                return;
	        }

            Dictionary<string,string> foundTransaction = accountTransactions.FirstOrDefault(transaction => transaction["id"] == enteredId);
            foundTransaction["amount"] = amountDecimal.ToString("F2"); 
            foundTransaction["type"] = type;
            foundTransaction["category"] = category;
            foundTransaction["description"] = description;
            foundTransaction["dateTime"] = transactionDate;
            Console.WriteLine("Uspjesno ste uredili transakciju sa ID-om "+enteredId);
            Console.ReadKey();
        }
        //delete transactions
        static void DeleteTransactionMenu(string userId, string accountType)
        {
            bool exit = false;
            while (!exit)
            {
                Console.Clear();
                Console.WriteLine("1 - Brisanje transakcije po ID-u");
                Console.WriteLine("2 - Brisanje transakcija ispod unesenog iznosa");
                Console.WriteLine("3 - Brisanje transakcija iznad unesenog iznosa");
                Console.WriteLine("4 - Brisanje svih prihoda");
                Console.WriteLine("5 - Brisanje svih rashoda");
                Console.WriteLine("6 - Brisanje svih transakcija za odabranu kategoriju");
                Console.WriteLine("0 - Vrati se nazad");

                string enteredValue = Console.ReadLine();

                switch (enteredValue)
                {
                    case "1":
                        
                        DeleteTransactionById(userId, accountType);
                        break;
                    case "2":
                        DeleteTransactionBasedOnAmount(userId, accountType, "ispod");
                        break;
                    case "3":
                        DeleteTransactionBasedOnAmount(userId, accountType, "iznad");
                        break;
                    case "4":
                        DeleteTransactionBasedOnType(userId, accountType, "prihod");
                        break;
                    case "5":
                        DeleteTransactionBasedOnType(userId, accountType, "rashod");
                        break;
                    case "6":
                        DeleteTransactionBasedOnCategory(userId, accountType);
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
        static void DeleteTransactionById(string userId, string accountType)
        {
            bool isValidId = false;
            string enteredId;
            Dictionary<string,string> transactionToDelete = null;
            do
	        {
                Console.Clear();
                Console.Write("Unesite ID transakcije koju želite obisat: ");
                enteredId = Console.ReadLine();
                transactionToDelete = transactions[userId][accountType].FirstOrDefault(t => t["id"] == enteredId);
                if (transactionToDelete == null)
	            {
                    Console.WriteLine("Uneseni ID ne postoji! Pokusajte ponovno.");
                    Console.ReadKey();
                    continue;
	            }
                isValidId = true;
	        } while (!isValidId);
            
           
            bool confirmation = ConfirmationDialog("brisati");
            if (!confirmation)
	        {
                Console.WriteLine("Prekinuli ste proces brisanja. Transakcija se ne briše.");
                Console.ReadKey();
                return;
	        }
            transactions[userId][accountType].Remove(transactionToDelete);
            Console.WriteLine($"Ispravan unos ID-a transakcije {enteredId}.\n Uspješno obrisana transakcija.");
            Console.ReadKey();
        }
        static void DeleteTransactionBasedOnAmount(string userId, string accountType, string condition)
        {
            bool isValidId = false;
            double amount;            
            do
	        {
                Console.Clear();
                Console.Write($"Unesite {condition} koje vrijednosti zelite brisati: ");
                string amountInput = Console.ReadLine();
                if (!double.TryParse(amountInput, out amount))
	            {
                    Console.WriteLine("Nepravilan unos broja. Ponvno pokusajte.");
                    Console.ReadKey();
                    continue;
	            }
                
                isValidId = true;
	        } while (!isValidId);
            //amount je ali treba pretvorit u double u transakcijama "amount", amount.tosting("f2")
            
            bool confirmation = ConfirmationDialog("brisati");
            if (!confirmation)
	        {
                Console.WriteLine("Prekinuli ste proces brisanja. Transakcije se ne brišu.");
                Console.ReadKey();
                return;
	        }
            var accountTransactions = transactions[userId][accountType];
            List<Dictionary<string,string>> transactionsToRemove = new List<Dictionary<string, string>>();

            foreach (var transaction in accountTransactions)
	        {
                double transactionAmount = double.Parse(transaction["amount"]);
                if (condition == "iznad" && transactionAmount > amount)
	            {
                    transactionsToRemove.Add(transaction);
	            }
                else if (condition == "ispod" && transactionAmount < amount)
                {
                    transactionsToRemove.Add(transaction);
                }
	        }
            if (transactionsToRemove.Count != 0)
	        {
                foreach (var transaction in transactionsToRemove)
	            {
                    accountTransactions.Remove(transaction);
	            }
                Console.WriteLine($"Uspjesno ste obrisali transakcije koje su {condition} unesene vrijednosti.");     
                Console.ReadKey();
	        }
            else
            {
                Console.WriteLine($"Korisnik nema transakcija koje su {condition} unesene vrijednosti.");
                Console.ReadKey();
            }
            
        }     
        static void DeleteTransactionBasedOnType(string userId, string accountType, string type)
        {
            bool confirmation = ConfirmationDialog("brisati");
            if (!confirmation)
	        {
                Console.WriteLine("Prekinuli ste proces brisanja. Transakcije se ne brišu.");
                Console.ReadKey();
                return;
	        }
            List<Dictionary<string,string>> transactionsToDelete = new List<Dictionary<string, string>>();
            var accountTransactions = transactions[userId][accountType];
            foreach (var transaction in accountTransactions)
	        {
                if (type == "prihod" && transaction["type"] == type)
	            {
                    transactionsToDelete.Add(transaction);
	            }
                else if (type == "rashod" && transaction["type"] == type)
                {
                    transactionsToDelete.Add(transaction);
                }
	        }
            if (transactionsToDelete.Count != 0)
	        {
                foreach (var transaction in transactionsToDelete)
	            {
                    accountTransactions.Remove(transaction);
	            }
                Console.WriteLine($"Uspjesno ste obrisali transakcije koje su {type}.");     
                Console.ReadKey();
	        }
            else
            {
                Console.WriteLine($"Korisnik nema transakcija koje su {type}.");
                Console.ReadKey();
            }
        }
        static void DeleteTransactionBasedOnCategory(string userId, string accountType)
        {
            var accountTransactions = transactions[userId][accountType];
            bool isValidCategory = false;
            string enteredCat;
            do
	        {
                Console.Clear();
                Console.Write("Unesite kategoriju za koju zelite izbrisati transakcije: ");
                enteredCat = Console.ReadLine().ToLower();
                if (!income.Contains(enteredCat) && !expense.Contains(enteredCat))
	            {
                    Console.WriteLine("Unesena kategorija ne postoji. Pokusajte ponovno.");
                    Console.ReadKey();
                    continue;
	            }
                isValidCategory = true;
	        } while (!isValidCategory);
            bool confirmation = ConfirmationDialog("brisati");
            if (!confirmation)
	        {
                Console.WriteLine("Prekinuli ste proces brisanja. Transakcije se ne brišu.");
                Console.ReadKey();
                return;
	        }
            List<Dictionary<string,string>> transactionsToDelete = new List<Dictionary<string, string>>();
            foreach (var transaction in accountTransactions)
	        {
                if (transaction["category"] == enteredCat)	            
                    transactionsToDelete.Add(transaction);	            
	        }
            if (transactionsToDelete.Count != 0)
	        {
                foreach (var transaction in transactionsToDelete)
	            {
                    accountTransactions.Remove(transaction);
	            }
                Console.WriteLine($"Uspjesno su obrisane sve transakcije categotije ({enteredCat})");
                Console.ReadKey();
	        }
            else
            {
                Console.WriteLine($"Za unesenu kategoriju korisnik nema transakcije.");
                Console.ReadKey();
            }
            
            
        }
        //enter transactions
        static void EnterPastTransaction(string userId,string accountType)
        {
            double amountDecimal = GetAmount();
            string type = GetTypeOfTransaction();
            string category = GetCategory(type);
            string description = GetDescription();
            string transactionDate = GetDate();
            uniqueTransactionId++;
            string transactionId = uniqueTransactionId.ToString();

            Console.WriteLine("Stanje prije transakcije: " + accounts[userId][accountType].ToString());
            if (type == "prihod")               
                accounts[userId][accountType] += amountDecimal;
            else if (type == "rashod")        
                accounts[userId][accountType] -= amountDecimal;
            Console.WriteLine("Stanje nakon transakcije: " + accounts[userId][accountType].ToString());
            
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
            
            Console.ReadKey();
        }
        static void EnterCurrentTransaction(string userId,string accountType)
        {
            double amountDecimal = GetAmount();
            string type = GetTypeOfTransaction();
            string category = GetCategory(type);
            string description = GetDescription();       
            string transactionDate =DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            
            Console.WriteLine("Stanje prije transakcije: " + accounts[userId][accountType].ToString());
            if (type == "prihod")               
                accounts[userId][accountType] += amountDecimal;
            else if (type == "rashod")        
                accounts[userId][accountType] -= amountDecimal;
            Console.WriteLine("Stanje nakon transakcije: " + accounts[userId][accountType].ToString());
            uniqueTransactionId++;
            string transactionId = uniqueTransactionId.ToString();
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
                Console.WriteLine("6 - Interna transakcija");
                Console.WriteLine("7 - Eksterna transakcija");

                Console.WriteLine("0 - Vrati se nazad");

                string enteredValue = Console.ReadLine();

                switch (enteredValue)
                {
                    case "1":
                        EnterTransactionMenu(userId,accountType);
                        break;
                    case "2":
                        DeleteTransactionMenu(userId,accountType);
                        break;
                    case "3":
                        EditTransaction(userId,accountType);
                        break;
                    case "4":
                        ViewTransactionsMenu(userId, accountType);
                        break;
                    case "5":
                        FinancialReportMenu(userId, accountType);
                        break;
                    case "6":
                        InternTransactionMenu(userId, accountType);
                        break;
                    case "7":
                        ExternTransactionMenu(userId, accountType);
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

        static List<string> income = new List<string>{"placa","honorar", "bonus", "penzija","nasljedstvo","povrat poreza","interni prihod","ostalo"};
        static List<string> expense = new List<string>{"hrana","stanarina", "zdravstvo", "obrazovanje","prijevoz","sport i rekreacija","zabava","interni rashod", "ostalo"};    
        static List<Dictionary<string, string>> users = new List<Dictionary<string, string>>();
        static int lastUserId = 0;
        static int uniqueTransactionId = 0;
        static Dictionary<string, Dictionary<string, double>> accounts = new Dictionary<string, Dictionary<string, double>>();
        static Dictionary<string, Dictionary<string, List<Dictionary<string, string>>>> transactions = new Dictionary<string, Dictionary<string, List<Dictionary<string, string>>>>();

        static void Main(string[] args)
        {
            UsersAccountsAndTransactions();

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
