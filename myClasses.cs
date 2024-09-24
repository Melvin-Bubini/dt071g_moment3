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
                    Console.WriteLine("Du måste ange ett namn");
                    continue;
                }

                /* Använder regex för att kontrollera om namnet innehåller endast bokstäver
                Regex: ^[a-zA-ZåäöÅÄÖ]+$ tillåter endast bokstäver (inklusive svenska bokstäver)*/
                if (!Regex.IsMatch(nameInput, @"^[a-zA-ZåäöÅÄÖ]+$"))
                {
                    Console.WriteLine("Namnet får endast innehålla bokstäver. Försök igen.");
                    continue;
                }

                Console.Write("Skriv ditt inlägg: ");
                string? messageInput = Console.ReadLine();

                if (string.IsNullOrEmpty(messageInput))
                {
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

    }
}
