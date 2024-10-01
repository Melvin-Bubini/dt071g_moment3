using System.Text.RegularExpressions;
using System.Text.Json;

namespace dt071g_moment3
{
    public class Menu
    {
        public static void MenuPrint()
        {
            Console.WriteLine("M E L V I N ' S  G U E S T B O O K\n\n");

            Console.WriteLine("1. Skriv i gästboken");
            Console.WriteLine("2. Ta bort inlägg");
            Console.WriteLine("3. Visa alla inlägg\n");
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

    public class Guestbook
    {
        private string filename = @"guestbook.json";  // Privat instansvariabel för filnamnet
        private List<GuestbookEntry> guestbookEntries = new List<GuestbookEntry>(); // Privat lista för att lagra inlägg


        // Spara alla inlägg som JSON
        public void SaveToFile()
        {
            string jsonData = JsonSerializer.Serialize(guestbookEntries);
            File.WriteAllText(filename, jsonData);
        }

        // Läs inlägg från JSON-fil
        public void LoadFromFile()
        {
            if (File.Exists(filename))
            {
                string jsonData = File.ReadAllText(filename);
                guestbookEntries = JsonSerializer.Deserialize<List<GuestbookEntry>>(jsonData) ?? new List<GuestbookEntry>();
            }
        }

        // Lägga till ett nytt inlägg i listan
        public void AddEntry(GuestbookEntry entry)
        {
            guestbookEntries.Add(entry);
        }

        // Ta bort ett inlägg från listan
        public void RemoveEntry(int index)
        {
            if (index >= 0 && index < guestbookEntries.Count)
            {
                guestbookEntries.RemoveAt(index);
            }
        }

        // Hämta antal inlägg
        public int GetEntryCount()
        {
            return guestbookEntries.Count;
        }


        //Visa alla inlägg
        public void ShowPosts(bool waitForInput = true)
        {
            Console.Clear();
            Console.WriteLine("Alla nuvarande inlägg: ");
            if (guestbookEntries.Count == 0)
            {
                Console.WriteLine("Det finns inga nuvarande inlägg");
            }
            else
            {
                for (int i = 0; i < guestbookEntries.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {guestbookEntries[i].Name} - {guestbookEntries[i].Message}");
                }
            }

            if (waitForInput)
            {
                Console.Write("Tryck på valfri tangent för att återgå till menyn: ");
                Console.ReadKey();
                Console.Clear();
            }
        }
    }



    public class Post
    {

        public static void AddPost(Guestbook guestbook)
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

                //Lägg till inlägget i listan
                guestbook.AddEntry(entry);
                guestbook.SaveToFile();    // Spara efter att ha lagt till inlägget


                Console.WriteLine("Inlägget har lagts till i gästboken");

                break;
            }
            //Rensa konsolen efter varje menyval
            Console.Clear();
        }

        public static void ShowPosts(Guestbook guestbook, bool waitForInput = true)
        {
            guestbook.ShowPosts(waitForInput); // Använd Guestbook för att visa inlägg
        }

        public static void DeletePost(Guestbook guestbook)
        {
            guestbook.ShowPosts(false); // Visar inlägg utan att vänta på tangenttryckning
            Console.Write("Skriv numret på det inlägg du vill ta bort: ");
            if (int.TryParse(Console.ReadLine(), out int index) && index > 0 && index <= guestbook.GetEntryCount()) // Kollar på att input är korrekt
            {
                guestbook.RemoveEntry(index - 1); // Ta bort inlägget från Guestbook
                guestbook.SaveToFile();           // Spara efter att ha tagit bort inlägget

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
