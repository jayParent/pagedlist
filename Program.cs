// Jerome Parent
using System;
using System.Collections.Generic;

namespace pagedlist
{
    class Program
    {
        static void Main(string[] args)
        {
            PagedList pagedList = new PagedList();
            FillWithRandom(pagedList, 100);

            var matches = pagedList.Find(new Item(69));

            foreach (var dict in matches)
            {
                foreach (var kvp in dict)
                {
                    Console.WriteLine($"{kvp.Key}: {kvp.Value[0]},{kvp.Value[1]}");
                    
                }
            }


            // pagedList.PrintInfo();
        }

        private static void FillWithRandom(PagedList pagedList, int qty)
        {
            Random random = new Random();
            for (int i = 0; i < qty; i++)
            {
                Item item = new Item(random.Next());
                pagedList.Push(item);
                Item item2 = new Item(69);
                pagedList.Push(item2);
            }
        }
    }
}