using System;

// Jerome Parent

namespace pagedlist
{
    class Program
    {
        static void Main(string[] args)
        {
            PagedList pagedList = new PagedList();

            Item item1 = new Item(29);
            for (var i = 0; i < 100; i++)
            {
                pagedList.Push(item1);
            }

            for (int i = 0; i < pagedList.Pages.Count; i++)
            {
                for (int j = 0; j < pagedList.Pages[i].Items.Length; j++)
                {
                    if (pagedList.Pages[i].Bitmap[j] == true)
                    {
                        Console.WriteLine($"{pagedList.Pages[i].Items[j].Value} - Page: {i}");
                    }
                }

            }
        }
    }
}