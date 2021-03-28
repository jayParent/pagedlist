using System;

public class PagedList
{
    public Guid Id { get; set; }
    public int Pages {get;set;}
    public int CasesParPage{get;set;}

    public PagedList(int pages, int casesParPage)
    {
        Id = Guid.NewGuid();
        Pages = pages;
        CasesParPage = casesParPage;
    }
}