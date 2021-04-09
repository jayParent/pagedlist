public class Page<T>
{
    public bool[] Bitmap { get; set; }
    public int Size { get; set; }
    public T[] Items { get; set; }

    public Page(int size)
    {
        Size = size;
        Items = new T[Size];
        Bitmap = InitializeBitmap(); 
    }
    public bool Add(T item, int index)
    {
        if(index == Size)
            return false;
        
        Items[index] = item;
        Bitmap[index] = true;

        return true;
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