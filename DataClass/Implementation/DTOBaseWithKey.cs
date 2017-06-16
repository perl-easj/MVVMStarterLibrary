namespace DataClass.Implementation
{
    public abstract class DTOBaseWithKey : DTOBase
    {
        public abstract int Key { get; set; }
    }
}