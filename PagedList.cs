using System.Collections.Generic;

public class PagedList
{
    public int Size { get; set; }
    public int Head { get; set; }
    public int Tail { get; set; }
    public int Offset { get; set; }
    public List<Page> Pages = new List<Page>();

    public PagedList()
    {
        Head = 0;
        Tail = 0;
    }

    public void Push(Item item)
    {
        bool added = false;
        if (Pages.Count == 0)
        {
            Pages.Add(new Page());
        }

        added = Pages[Head + Offset].Add(item);

        if (!added)
        {
            Pages.Add(new Page());
            Offset = Pages.Count - 1;
            added = Pages[Head + Offset].Add(item);
        }
    }
}