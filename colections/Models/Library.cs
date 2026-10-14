using System;
using System.Collections.Generic;

namespace colections.Models
{
    internal class Library
    {
        public Dictionary<int, Book> Books { get; set; } = new Dictionary<int, Book>();
        public SortedList<string, List<Book>> bokbyaut { get; set; } = new SortedList<string, List<Book>>();
        public Dictionary<int, Queue<string>> queue { get; set; } = new Dictionary<int, Queue<string>>();
        public Stack<Book> stack { get; set; } = new Stack<Book>();

        
        public void AddBook(Book book)
        {
           
            book.Id = Books.Count > 0 ? GetMaxId() + 1 : 1;

            
            Books.Add(book.Id, book);

            if (!bokbyaut.ContainsKey(book.Author))
            {
                bokbyaut[book.Author] = new List<Book>();
            }
            bokbyaut[book.Author].Add(book);

            queue[book.Id] = new Queue<string>();

            stack.Push(book);

            Console.WriteLine($"wiggni '{book.Title}' daemata (ID: {book.Id})!");
        }

        private int GetMaxId()
        {
            int max = 0;
            foreach (var key in Books.Keys)
            {
                if (key > max) max = key;
            }
            return max;
        }

        public bool SearchById(int id, out Book book)
        {
            return Books.TryGetValue(id, out book);
        }

        public void Undo()
        {
            if (stack.Count > 0)
            {
                Book lastBook = stack.Pop();

                Books.Remove(lastBook.Id);

                if (bokbyaut.ContainsKey(lastBook.Author))
                {
                    bokbyaut[lastBook.Author].RemoveAll(b => b.Id == lastBook.Id);
                    if (bokbyaut[lastBook.Author].Count == 0)
                    {
                        bokbyaut.Remove(lastBook.Author);
                    }
                }

                queue.Remove(lastBook.Id);

                Console.WriteLine($"bolo damate ('{lastBook.Title}') gauqmda (Undo).");
            }
            else
            {
                Console.WriteLine("carielia");
            }
        }

        public List<Book> GetBooksByYearRange(int startYear, int endYear)
        {
            List<Book> filteredBooks = new List<Book>();
            foreach (var book in Books.Values)
            {
                if (book.Year >= startYear && book.Year <= endYear)
                {
                    filteredBooks.Add(book);
                }
            }
            return filteredBooks;
        }

        public void AddToQueue(int bookId, string readerName)
        {
            if (queue.ContainsKey(bookId))
            {
                queue[bookId].Enqueue(readerName);
                Console.WriteLine($"mkitxveli '{readerName}' daemata wignis moledinis rigshi");
            }
            else
            {
                Console.WriteLine("es id ar arsebobs");
            }
        }

        
        public void ProcessReturn(int bookId)
        {
            if (queue.ContainsKey(bookId) && queue[bookId].Count > 0)
            {
                string nextReader = queue[bookId].Dequeue();
                Console.WriteLine($"wigni warmatebit ikittxeba pirveli mkitxvelistvis: {nextReader}");
            }
            else
            {
                Console.WriteLine("wigni ver moizebna");
            }
        }
    }


    }
