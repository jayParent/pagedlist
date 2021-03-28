using System;
using System.Collections.Generic;

public class PagedList
{
    public Guid Id { get; set; }
    public int Size { get; set; }
    public int Head { get; set; }
    public int Tail { get; set; }
    public List<Page> Pages = new List<Page>(); // Temporaire

    public PagedList()
    {
        Id = Guid.NewGuid();
    }
}