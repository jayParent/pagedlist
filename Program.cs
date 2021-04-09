// Jerome Parent
using System;

namespace pagedlist
{
    class Program
    {
        static void Main(string[] args)
        {
            PagedList<int> pagedList = new PagedList<int>(8);
            FillWithRandom(pagedList, 48);
            
            pagedList.Delete(13);
            pagedList.Delete(27);
            pagedList.Delete(28);
            pagedList.Delete(11);
            pagedList.Delete(38);
            pagedList.Delete(39);
            pagedList.Delete(25);
            pagedList.Delete(3);
            pagedList.Delete(1);
            // pagedList.Delete(47);
            // pagedList.Delete(41);
            // int count = pagedList.Count;
            // for (int i = 0; i < count; i++)
            // {
            //     pagedList.Delete(i);
            // }
            // pagedList.Push(39);
            // pagedList.Find(39);
            // DeleteRandomItems(pagedList, 26);
            pagedList.Compact();

            // pagedList.Push(item2);
            // System.Console.WriteLine(item2.Value);          
            // pagedList.Delete(1);
            // pagedList.Delete(2);
            // pagedList.Delete(3);
            // pagedList.Delete(5);
            // pagedList.Delete(7);
            // pagedList.Delete(11);
            // pagedList.Delete(14);
            // pagedList.Delete(17);
            // pagedList.Compact();
            // pagedList.PrintInfo();
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
                int n = random.Next(0, pagedList.Count);
                pagedList.Delete(n);
            }
        }
    }
}