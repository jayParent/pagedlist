// Jerome Parent
using System;

namespace pagedlist
{
    class Program
    {
        static void Main(string[] args)
        {
            PagedList pagedList = new PagedList();
            FillWithRandom(pagedList, 30);
            Item item2 = new Item(69);

            pagedList.Push(item2);
            // System.Console.WriteLine(item2.Value);          
            DeleteRandomItems(pagedList, 20);
            pagedList.Compact();
            pagedList.PrintInfo();

            // pagedList.Delete(item2);

            // var matches = pagedList.Find(item2);
            // Item r = pagedList.Get(matches[0]);
            // r.Value = 11;
            
            // Console.WriteLine(item2.Value);
            


            // var matches = pagedList.Find(item2);

        }

        private static void FillWithRandom(PagedList pagedList, int qty)
        {
            Random random = new Random();
            for (int i = 0; i < qty; i++)
            {
                Item item = new Item(random.Next());
                pagedList.Push(item);
            }
        }
        private static void DeleteRandomItems(PagedList pagedList, int qty){
            Random random = new Random();

            for (int i = 0; i < qty; i++)
            {
                // int index = random.Next(0, pagedList.Count);
                pagedList.Delete(i);    
            }
        }
    }
}