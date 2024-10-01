using System.Text.RegularExpressions;

namespace dt071g_moment3
{
    public class Menu
    {
        public static void MenuPrint()
        {
            Console.WriteLine("");
            Console.WriteLine("M E L V I N ' S  G U E S T B O O K");
            Console.WriteLine("");
            Console.WriteLine("1. Skriv i gästboken");
            Console.WriteLine("2. Ta bort inlägg");
            Console.WriteLine("3. Visa alla inlägg");
            Console.WriteLine("X. Avsluta");

        }
    }

    public class GuestbookEntry : IGuestbookEntry
    {
        public string Name { get; set; }
        public string Message { get; set; }

        public GuestbookEntry(string name, string message)
        {
            Name = name;
            Message = message;
        }
    }


    public class Post
    {
        private static List<GuestbookEntry> guestbookEntries = new List<GuestbookEntry>();
        public static void AddPost()
        {
            Console.Clear();

            while (true)
            {

                Console.Write("Skriv ditt namn: ");
                string? nameInput = Console.ReadLine();

                if (string.IsNullOrEmpty(nameInput))
                {
                    Console.Clear();
                    Console.WriteLine("Du måste ange ett namn");
                    continue;
                }

                /* Använder regex för att kontrollera om namnet innehåller endast bokstäver
                Regex: ^[a-zA-ZåäöÅÄÖ]+$ tillåter endast bokstäver (inklusive svenska bokstäver)*/
                if (!Regex.IsMatch(nameInput, @"^[a-zA-ZåäöÅÄÖ\s]+$"))
                {
                    Console.Clear();
                    Console.WriteLine("Namnet får endast innehålla bokstäver. Försök igen.");
                    continue;
                }

                Console.Write("Skriv ditt inlägg: ");
                string? messageInput = Console.ReadLine();

                if (string.IsNullOrEmpty(messageInput))
                {
                    Console.Clear();
                    Console.WriteLine("Du måste skriva ett inlägg");
                    continue;
                }

                // Skapar en instans av GuestbookEntry och tilldelar name och message
                GuestbookEntry entry = new GuestbookEntry(nameInput, messageInput);

                // Lägg till inlägget i listan
                guestbookEntries.Add(entry);
                Console.WriteLine("Inlägget har lagts till i gästboken");

                break;
            }
            // Rensa konsolen efter varje menyval
            Console.Clear();
        }

        public static void ShowPosts(bool waitForInput = true) // Skickar med en bool ifall man ska vänta på input
        {
            Console.Clear();
             
            Console.WriteLine("Alla nuvarande inlägg: ");
            if (guestbookEntries.Count == 0)// Kollar så att det finns ilägg
            {
                Console.WriteLine("Det finns inga nuvarande inlägg");
            }
            else
            {
                for (int i = 0; i < guestbookEntries.Count; i++) // Skriver ut alla inlägg
                {
                    Console.WriteLine($"{i + 1}. {guestbookEntries[i].Name} - {guestbookEntries[i].Message}");
                }
            }

            if (waitForInput) // Skriver ut input
            {
                Console.Write("Tryck på valfri tangent för att återgå till menyn: ");
                Console.ReadKey();
                Console.Clear();
            }

        }

        public static void DeletePost()
        {
            ShowPosts(false); // Visar inlägg utan att vänta på tangenttryckning
            Console.Write("Skriv numret på det inlägg du vill ta bort: ");
            if (int.TryParse(Console.ReadLine(), out int index) && index > 0 && index <= guestbookEntries.Count) // Kollar på att input är korrekt
            {
                guestbookEntries.RemoveAt(index - 1); // Tar bort ett inlägg
                Console.WriteLine($"Inlägg {index} togs bort");
            }
            else
            {
                Console.WriteLine("Ogiltigt nummer");
            }
            Console.Write("Tryck på valfri tangent för att återgå till menyn: ");
            Console.ReadKey();
            Console.Clear();
        }

    }

}
