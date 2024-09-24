namespace dt071g_moment3;

class Program
{
    static void Main(string[] args)
    {
        while (true)
        {
            Menu.MenuPrint();
            Console.Write("Skriv ett av alternativen ovan: ");
            string? input = Console.ReadLine();

            if (string.IsNullOrEmpty(input))
            {
                Console.WriteLine("Du måste ange 1, 2, 3 eller x");
                continue;
            }
            switch (input)
            {
                case "1":
                    Post.AddPost();
                    break;
                case "2":
                    // Lägg till koden för att visa inlägg
                    // Post.ShowPosts();
                    break;
                case "3":
                    // Lägg till koden för att ta bort inlägg
                    // Post.DeletePost();
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
