namespace Filtering.Interfaces
{
    public interface IFilter<in T>
    {
        string ID { get; }
        bool On { get; set; }
        void Toggle();
        bool Condition(T obj);
    }
}