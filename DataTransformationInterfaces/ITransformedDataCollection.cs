using System.Collections.Generic;

namespace DataTransformation.Interfaces
{
    public interface ITransformedDataCollection
    {
        List<ITransformedData> AllTransformed { get; }
        ITransformedData ReadTransformed(int key);
        void DeleteTransformed(int key);
        void InsertTransformed(ITransformedData obj, bool replaceKey = true);
    }
}