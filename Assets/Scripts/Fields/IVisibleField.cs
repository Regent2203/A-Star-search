using ThisProject.Nodes;
using ThisProject.ObjectsStorages;
using ThisProject.Views;

namespace ThisProject.Fields
{
    public interface IVisibleField<T, V, TId> : ILogicField<T, TId>
        where T : class, INode<TId>
        where V : class, IView<TId>
    {
        public IObjectsStorage<V, TId> Views { get; }
        public V GetViewById(TId id);
    }
}
