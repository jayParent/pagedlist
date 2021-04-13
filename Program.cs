// Jerome Parent
using System;

namespace pagedlist
{
    class Program
    {
        static void Main(string[] args)
        {
            Test1();
        }

        private static void Test1()
        {
            PagedList<int> pagedList = new PagedList<int>();
            FillWithRandom(pagedList, 48);
            Console.WriteLine($"Pages: {pagedList.PageCount}\nCases par page: {pagedList.CasesParPage}\nItems: {pagedList.Count}\n");

            Console.WriteLine("Appuyez sur ENTER pour supprimer des items...");
            Console.ReadLine();

            pagedList.Delete(13);
            pagedList.Delete(27);
            pagedList.Delete(28);
            pagedList.Delete(11);
            pagedList.Delete(38);
            pagedList.Delete(25);
            pagedList.Delete(3);
            pagedList.Delete(16);
            pagedList.Delete(19);
            pagedList.Delete(5);

            Console.WriteLine("Appuyez sur ENTER pour compacter les items...");
            Console.ReadLine();

            pagedList.Compact();

            Console.WriteLine("Appuyez sur ENTER pour ajouter un item...");
            Console.ReadLine();

            pagedList.Push(666);
            pagedList.PrintLayout();

            Console.WriteLine("Appuyez sur ENTER pour rechercher cet item...");
            Console.ReadLine();

            pagedList.Find(666);
        }
        private static void FillWithRandom(PagedList<int> pagedList, int qty)
        {
            Random random = new Random();
            for (int i = 0; i < qty; i++)
            {
                int n = random.Next();
                pagedList.Push(n);
            }

            pagedList.PrintLayout();
        }
        private static void DeleteRandomItems(PagedList<int> pagedList, int qty)
        {
            Random random = new Random();

            for (int i = 0; i < qty; i++)
            {
                int n = random.Next(0, pagedList.Count);
                pagedList.Delete(n);
            }
        }
    }
}