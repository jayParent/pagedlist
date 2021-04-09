using System;
using System.Collections.Generic;

public class PagedList<T>
{
    public int Head { get; set; }
    public int Tail { get; set; }
    public int CurrentPageIndex { get; set; }
    public int Offset { get; set; }
    public List<Page<T>> Pages = new List<Page<T>>();
    public int CasesParPage { get; set; }
    public int Count { get; set; }
    public int PageCount { get; set; }

    public PagedList(int casesParPage = 8)
    {
        Head = 0;
        Tail = 0;
        PageCount = 0;
        CasesParPage = casesParPage;

        Pages.Add(new Page<T>(CasesParPage));
        PageCount++;
    }
    public void Push(T item)
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
    public List<int> Find(T searchedItem)
    {
        // checker la valeur total de la bitmap, si pas 8 * size, ya de la place
        List<int> matches = new List<int>();
        string output = "";

        for (int i = 0; i < PageCount; i++)
        {
            for (int j = 0; j < Pages[i].Items.Length; j++)
            {
                if (Pages[i].Bitmap[j] == false)
                    break;

                if (searchedItem.Equals(Pages[i].Items[j]))
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
    public T Get(int position)
    {
        int page = position / CasesParPage;

        T reference = Pages[page].Items[position - (page * CasesParPage)];

        return reference;
    }
    public void Delete(T searchedItem)
    {
        List<int> positions = Find(searchedItem);
        int position, page;

        for (int i = 0; i < positions.Count; i++)
        {
            position = positions[i];
            page = position / CasesParPage;

            Pages[page].Items[position - (page * CasesParPage)] = default(T);
            Pages[page].Bitmap[position - (page * CasesParPage)] = false;
        }

        Count -= positions.Count;
        Console.WriteLine($"Deleted: {positions.Count} items");
    }

    public void Delete(int position)
    {
        int page = position / CasesParPage;

        Pages[page].Items[position - (page * CasesParPage)] = default(T);
        Pages[page].Bitmap[position - (page * CasesParPage)] = false;

        Count -= 1;
        Console.WriteLine($"Deleted: 1 items");
    }
    public void Compact()
    {
        Queue<int> open = new Queue<int>();
        Stack<KeyValuePair<int, T>> filled = new Stack<KeyValuePair<int, T>>();

        GetItemsToShift(open, filled);
        ShiftItems(open, filled);
        DeleteEmptyPages();
    }
    private void GetItemsToShift(Queue<int> open, Stack<KeyValuePair<int, T>> filled)
    {
        for (int i = 0; i < PageCount; i++)
        {
            for (int j = 0; j < Pages[i].Bitmap.Length; j++)
            {
                if (Pages[i].Bitmap[j] == false)
                {
                    int position = (i * CasesParPage) + j;
                    open.Enqueue(position);
                }
                else
                {
                    int position = (i * CasesParPage) + j;
                    T item = Pages[i].Items[j];
                    KeyValuePair<int, T> itemAndPosition = new KeyValuePair<int, T>(position, item);
                    filled.Push(itemAndPosition);
                }
            }
        }
    }
    private void ShiftItems(Queue<int> open, Stack<KeyValuePair<int, T>> filled)
    {
        while (open.Count > 0 && filled.Count > 0)
        {
            int openPosition = open.Dequeue();
            int openPage = openPosition / CasesParPage;

            KeyValuePair<int, T> item = filled.Pop();
            int movedItemPosition = item.Key;
            int movedItemPage = movedItemPosition / CasesParPage;

            Pages[openPage].Items[openPosition - (openPage * CasesParPage)] = item.Value;
            Pages[openPage].Bitmap[openPosition - (openPage * CasesParPage)] = true;

            Pages[movedItemPage].Items[movedItemPosition - (movedItemPage * CasesParPage)] = default(T);
            Pages[movedItemPage].Bitmap[movedItemPosition - (movedItemPage * CasesParPage)] = false;

            this.PrintLayout();
        }
    }
    private void DeleteEmptyPages()
    {
        List<Page<T>> emptyPages = new List<Page<T>>();
        foreach (Page<T> page in Pages)
        {
            bool emptyPage = true;
            foreach (bool bit in page.Bitmap)
            {
                if(bit == true){
                    emptyPage = false;
                    break;
                }
            }

            if(emptyPage)
                emptyPages.Add(page);
        }

        foreach (Page<T> emptyPage in emptyPages)
        {
            Pages.Remove(emptyPage);
        }

        if(emptyPages.Count > 0)
            this.PrintLayout();
    }
    private void CreatePage()
    {
        Pages.Add(new Page<T>(CasesParPage));
        CurrentPageIndex = 0;
        PageCount++;
        Tail++;
        Offset = PageCount - 1;
    }
    public void PrintInfo()
    {
        for (int i = 0; i < Pages.Count; i++)
        {
            for (int j = 0; j < Pages[i].Items.Length; j++)
            {
                if (Pages[i].Bitmap[j] == true)
                {
                    Console.WriteLine($"{Pages[i].Items[j]} - Page: {i} Position: {(i * CasesParPage) + j}");
                }
            }

        }
        Console.WriteLine($"PagedList -- Count: {Count} Pages: {PageCount}");
    }
    public void PrintLayout()
    {
        for (int i = 0; i < Pages.Count; i++)
        {
            string output = "";
            output += "|";

            for (int j = 0; j < Pages[i].Items.Length; j++)
            {
                output += Pages[i].Bitmap[j] == true ? "*" : "-";
            }
            output += "|";
            Console.Write(output);
        }
        Console.Write("\n");
    }
}
