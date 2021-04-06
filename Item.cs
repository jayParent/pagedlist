public class Item
{
    public object Value { get; set; }

    public Item(int value)
    {
        Value = value;
    }
    public Item(float value)
    {
        Value = value;
    }
    public Item(double value)
    {
        Value = value;
    }
    public Item(string value)
    {
        Value = value;
    }
    public Item(bool value)
    {
        Value = value;
    }
}