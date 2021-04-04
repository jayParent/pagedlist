using System;
using System.Collections.Generic;

public class PagedList
{
    public int Head { get; set; }
    public int Tail { get; set; }
    public int Offset { get; set; }
    public List<Page> Pages = new List<Page>();
    public int CasesParPage { get; set; }
    public int Count { get; set; }
    public int PageCount { get; set; }

    public PagedList()
    {
        Head = 0;
        Tail = 0;
        PageCount = 0;
        CasesParPage = 8;
    }

    public PagedList(int casesParPage)
    {
        Head = 0;
        Tail = 0;
        PageCount = 0;
        CasesParPage = casesParPage;
    }
    public void Push(Item item)
    {
        bool added = false;
        if (PageCount == 0)
        {
            Pages.Add(new Page(CasesParPage));
            PageCount++;
        }

        added = Pages[Head + Offset].Add(item);

        if (added)
            Count++;

        else // Page est pleine
        {
            Pages.Add(new Page(CasesParPage));
            PageCount++;
            Offset = PageCount - 1;
            added = Pages[Head + Offset].Add(item);
            Count++;
        }
    }
    public int Find(Item item)
    {
        throw new NotImplementedException();
    }
    public void Delete(int position)
    {
        throw new NotImplementedException();
    }
    public void Compact()
    {
        throw new NotImplementedException();
    }
    public void PrintInfo()
    {
        for (int i = 0; i < this.Pages.Count; i++)
        {
            for (int j = 0; j < this.Pages[i].Items.Length; j++)
            {
                if (this.Pages[i].Bitmap[j] == true)
                {
                    Console.WriteLine($"{this.Pages[i].Items[j].Value} - Page: {i}");
                }
            }

        }
        Console.WriteLine($"PagedList -- Count: {this.Count} Pages: {this.PageCount}");
    }
}
