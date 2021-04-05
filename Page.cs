public class Page
{
    public bool[] Bitmap { get; set; }
    public int Size { get; set; }
    public Item[] Items { get; set; }

    public Page(int size)
    {
        Size = size;
        Items = new Item[Size];
        Bitmap = InitializeBitmap(); 
    }
    public bool Add(Item item)
    {
        bool added = false;

        for (int i = 0; i < Bitmap.Length; i++)
        {
            if (Bitmap[i] == false) // Bitmap indique qu'il n'y a pas d'item a cet endroit dans la page
            {
                Items[i] = item;
                Bitmap[i] = true;
                added = true;
                break;
            }
        }

        return added;
    }
    private bool[] InitializeBitmap()
    {
        bool[] bitmap = new bool[Size];

        for (int i = 0; i < bitmap.Length; i++)
        {
            bitmap[i] = false;
        }

        return bitmap;
    }
}