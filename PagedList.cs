using System;
using System.Collections.Generic;
using System.Linq;

public class PagedList
{
    public int Head { get; set; }
    public int Tail { get; set; }
    public int Offset { get; set; }
    public List<Page> Pages = new List<Page>();
    public int CasesParPage { get; set; }
    public int Count { get; set; }
    public int PageCount { get; set; }
    private int ItemPosition { get; set; }

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

        // item.Position = ItemPosition;
        // item.PageNumber = Tail;

        added = Pages[Head + Offset].Add(item);
        if (added){
            ItemPosition++;
            Count++;
        }
            
        else // Page est pleine
        {
            Pages.Add(new Page(CasesParPage));
            Tail++;
            PageCount++;
            Offset = PageCount - 1;

            // item.PageNumber = Tail;
            added = Pages[Head + Offset].Add(item);
            ItemPosition++;
            Count++;
        }
    }
    public List<int> Find(Item searchedItem)
    {
        List<int> matches = new List<int>();
        string output = "";

        for (int i = 0; i < PageCount; i++)
        {
            for (int j = 0; j < Pages[i].Items.Length; j++)
            {
                if (Pages[i].Bitmap[j] == false)
                    break;

                if (searchedItem.Value == Pages[i].Items[j].Value)
                {
                    int position = (i * CasesParPage) + j;
                    matches.Add(position);
                }
            }
        }

        if (matches.Count == 0)
            output = "Aucun résultat";
        else
            output = "Found at position: " + String.Join(",", matches);

        Console.WriteLine(output);
        return matches;
    }
    public void Delete(Item searchedItem)
    {
        List<int> positions = Find(searchedItem);
        int position, page;

        for (int i = 0; i < positions.Count; i++)
        {
            position = positions[i];
            page = position / CasesParPage;

            Pages[page].Items[position - (page * CasesParPage)].Value = null;
            Pages[page].Bitmap[position - (page * CasesParPage)] = false;
        }

        Console.WriteLine($"Deleted: {positions.Count} items");
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
                    Console.WriteLine($"{this.Pages[i].Items[j].Value} - Page: {i} Position: {(i * CasesParPage) + j}");
                }
            }

        }
        Console.WriteLine($"PagedList -- Count: {this.Count} Pages: {this.PageCount}");
    }
}
