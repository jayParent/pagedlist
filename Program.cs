// Jerome Parent
using System;

namespace pagedlist
{
    class Program
    {
        static void Main(string[] args)
        {
            PagedList<int> pagedList = new PagedList<int>();
            FillWithRandom(pagedList, 50);

            // pagedList.Push(item2);
            // System.Console.WriteLine(item2.Value);          
            // DeleteRandomItems(pagedList, 5);
            // pagedList.Delete(1);
            // pagedList.Delete(2);
            // pagedList.Delete(3);
            // pagedList.Delete(5);
            // pagedList.Delete(7);
            // pagedList.Delete(11);
            // pagedList.Delete(14);
            // pagedList.Delete(17);
            // pagedList.Compact();
            pagedList.PrintInfo();
        }

        private static void FillWithRandom(PagedList<int> pagedList, int qty)
        {
            Random random = new Random();
            for (int i = 0; i < qty; i++)
            {
                int n = random.Next();
                pagedList.Push(n);
            }
        }
        private static void DeleteRandomItems(PagedList<int> pagedList, int qty)
        {
            Random random = new Random();

            for (int i = 0; i < qty; i++)
            {
                pagedList.Delete(i);
            }
        }
    }
}