namespace DataClass.Interfaces
{
    public interface IDTOWithKey : IDTO
    {
        int Key { get; set; }
    }
}