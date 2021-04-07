using System;
using System.Collections.Generic;

public class PagedList
{
    public int Head { get; set; }
    public int Tail { get; set; }
    public int CurrentPageIndex { get; set; }
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

        Pages.Add(new Page(CasesParPage));
        PageCount++;
    }
    public PagedList(int casesParPage)
    {
        Head = 0;
        Tail = 0;
        PageCount = 0;
        CasesParPage = casesParPage;

        Pages.Add(new Page(CasesParPage));
        PageCount++;
    }
    public void Push(Item item)
    {
        bool added = false;

        added = Pages[Head + Offset].Add(item, CurrentPageIndex);
        if (added)
        {
            Count++;
            CurrentPageIndex++;
        }

        else
        {
            CreatePage();
            Pages[Head + Offset].Add(item, CurrentPageIndex);
            Count++;
            CurrentPageIndex++;
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
    public Item Get(int position)
    {
        int page = position / CasesParPage;

        Item reference = Pages[page].Items[position - (page * CasesParPage)];

        return reference;
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

        Count -= positions.Count;
        Console.WriteLine($"Deleted: {positions.Count} items");
    }

    public void Delete(int position)
    {
        //! OUPS ca delete tous les instances de la valeur
        int page = position / CasesParPage;

        Pages[page].Items[position - (page * CasesParPage)].Value = null;
        Pages[page].Bitmap[position - (page * CasesParPage)] = false;

        Count -= 1;
        Console.WriteLine($"Deleted: 1 items");
    }
    public void Compact()
    {
        Queue<int> open = new Queue<int>();
        Stack<KeyValuePair<int, Item>> filled = new Stack<KeyValuePair<int, Item>>();
        List<Page> emptyPages = new List<Page>();
        
        GetItemsToShift(open, filled, emptyPages);
        ShiftItems(open, filled);
        DeleteEmptyPages(emptyPages);
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
    private void CreatePage()
    {
        Pages.Add(new Page(CasesParPage));
        CurrentPageIndex = 0;
        PageCount++;
        Tail++;
        Offset = PageCount - 1;
    }
    private void GetItemsToShift(Queue<int> open, Stack<KeyValuePair<int, Item>> filled, List<Page> emptyPages)
    {
        for (int i = 0; i < PageCount; i++)
        {
            bool emptyPage = true;
            
            for (int j = 0; j < Pages[i].Bitmap.Length; j++)
            {
                if (Pages[i].Bitmap[j] == false)
                {
                    int position = (i * CasesParPage) + j;
                    open.Enqueue(position);
                }
                else
                {
                    emptyPage = false;
                    int position = (i * CasesParPage) + j;
                    Item item = Pages[i].Items[j];
                    KeyValuePair<int, Item> itemAndPosition = new KeyValuePair<int, Item>(position, item);
                    filled.Push(itemAndPosition);
                }
            }

            if (emptyPage)
                emptyPages.Add(Pages[i]);
        }
    }
    private void ShiftItems(Queue<int> open, Stack<KeyValuePair<int, Item>> filled)
    {
        while (open.Count > 0 && filled.Count > 0)
        {
            int openPosition = open.Dequeue();
            int openPage = openPosition / CasesParPage;

            KeyValuePair<int, Item> item = filled.Pop();
            int movedItemPosition = item.Key;
            int movedItemPage = movedItemPosition / CasesParPage;

            Pages[openPage].Items[openPosition - (openPage * CasesParPage)] = item.Value;
            Pages[openPage].Bitmap[openPosition - (openPage * CasesParPage)] = true;

            Pages[movedItemPage].Items[movedItemPosition - (movedItemPage * CasesParPage)] = null;
            Pages[movedItemPage].Bitmap[movedItemPosition - (movedItemPage * CasesParPage)] = false;
        }
    }
    private void DeleteEmptyPages(List<Page> emptyPages)
    {
        foreach (Page page in emptyPages)
        {
            Pages.Remove(page);
        }
    }

}
