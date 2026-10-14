using colections.Models;

namespace colections
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Library library = new Library();
            bool running = true;

            while (running)
            {
                try
                {
        
                    Console.WriteLine("1. wignis damateba");
                    Console.WriteLine("2. zebna ID (TryGetValue)");
                    Console.WriteLine("3. Undo (bolodamatebis gauqmeba)");
                    Console.WriteLine("4. wlebit zebna");
                    Console.WriteLine("5. mkitxvelis damateba");
                    Console.WriteLine("6. wigni dabruneba");
                    Console.WriteLine("7. gamosvla");
                    Console.Write("aircie (1-7): ");

                    int choice = int.Parse(Console.ReadLine());

                    switch (choice)
                    {
                        case 1:
                            Book newBook = new Book();
                            Console.Write("wignis satauri: ");
                            newBook.Title = Console.ReadLine();
                            Console.Write("avtori: ");
                            newBook.Author = Console.ReadLine();
                            Console.Write("gamocemis weli: ");
                            newBook.Year = int.Parse(Console.ReadLine());

                            library.AddBook(newBook);
                            break;

                        case 2:
                            Console.Write("ipove wigni ID: ");
                            int searchId = int.Parse(Console.ReadLine());
                            if (library.SearchById(searchId, out Book foundBook))
                            {
                                Console.WriteLine($"vipove  satauri: {foundBook.Title}, avtori: {foundBook.Author}, weli: {foundBook.Year}");
                            }
                            else
                            {
                                Console.WriteLine("wign ID-it ar moizebna");
                            }
                            break;

                        case 3:
                            library.Undo();
                            break;

                        case 4:
                            Console.Write("Seiyvane sawyisi weliu ");
                            int start = int.Parse(Console.ReadLine());
                            Console.Write("Seiyvane bolo weli: ");
                            int end = int.Parse(Console.ReadLine());

                            var results = library.GetBooksByYearRange(start, end);
                            Console.WriteLine("Sedegi");
                            foreach (var b in results)
                            {
                                Console.WriteLine($"ID: {b.Id} | {b.Title} - {b.Author} ({b.Year})");
                            }
                            break;

                        case 5:
                            Console.Write("seiyvane wignis id ");
                            int qBookId = int.Parse(Console.ReadLine());
                            Console.Write("mkitxvelis saxeli: ");
                            string readerName = Console.ReadLine();
                            library.AddToQueue(qBookId, readerName);
                            break;

                        case 6:
                            Console.Write("dabrunebuli wignis id");
                            int rBookId = int.Parse(Console.ReadLine());
                            library.ProcessReturn(rBookId);
                            break;

                        case 7:
                            running = false;
                            Console.WriteLine("programa dasrulda");
                            break;

                        default:
                            Console.WriteLine("araswori arcevani");
                            break;
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("seiyvane swori formati");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"secdoma moxda: {ex.Message}");
                }
            }
        }
    }
}
