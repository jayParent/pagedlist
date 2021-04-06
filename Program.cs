// Jerome Parent
using System;

namespace pagedlist
{
    class Program
    {
        static void Main(string[] args)
        {
            PagedList pagedList = new PagedList();
            FillWithRandom(pagedList, 20);
            Item item2 = new Item(69);
            pagedList.Push(item2);
            pagedList.Push(item2);
            
            // pagedList.PrintInfo();

            pagedList.Delete(item2);

            var matches = pagedList.Find(item2);
            // foreach (var match in matches)
            // {
            //     System.Console.WriteLine(match);
            // }


            // pagedList.PrintInfo();
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
    }
}