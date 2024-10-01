namespace dt071g_moment3;

class Program
{
    static void Main(string[] args)
    {

        Guestbook guestbook = new Guestbook();

        // Ladda tidigare inlägg från filen
        guestbook.LoadFromFile();

        while (true)
        {
            Menu.MenuPrint();
            Console.Write("Skriv ett av alternativen ovan: ");
            string? input = Console.ReadLine();

            if (string.IsNullOrEmpty(input))
            {
                Console.Clear();
                Console.WriteLine("Du måste ange 1, 2, 3 eller x");
                continue;
            }
            switch (input)
            {
                case "1":
                    Post.AddPost(guestbook); // Skicka med guestbook
                    break;
                case "2":
                    Post.DeletePost(guestbook); // Skicka med guestbook
                    break;
                case "3":
                    Post.ShowPosts(guestbook); // Skicka med guestbook
                    break;
                case "X":
                case "x":
                    Console.WriteLine("Avslutar programmet.");
                    return;  // Avslutar loopen och programmet
                default:
                    Console.WriteLine("Ogiltigt val, försök igen.");
                    break;
            }
            // Rensa konsolen efter varje menyval
            Console.Clear();
        }


    }
}
