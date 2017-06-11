using InMemoryStorage.Interfaces;

namespace ModelClass.Interfaces
{
    public interface IDomainClass : IStorable
    {
        IDomainClass Clone();
        void SetDefaultValues();
    }
}