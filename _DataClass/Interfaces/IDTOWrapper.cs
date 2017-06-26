namespace DataClass.Interfaces
{
    public interface IDTOWrapper<out TDTO>
    {
        TDTO DataObject { get; }
    }
}