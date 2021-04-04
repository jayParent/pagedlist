// Jerome Parent
using System;

namespace pagedlist
{
    class Program
    {
        static void Main(string[] args)
        {
            PagedList pagedList = new PagedList();
            FillWithRandom(pagedList, 100);

            pagedList.PrintInfo();
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