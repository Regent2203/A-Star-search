using ThisProject.Nodes;
using ThisProject.ObjectsStorages;

namespace ThisProject.Fields
{
    public interface IFieldCore<T, TId> 
        where T : class, INode<TId>
    {
        public IObjectsStorage<T, TId> Nodes { get; }
    }
}
