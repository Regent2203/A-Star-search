using ThisProject.Nodes;
using ThisProject.Views;

namespace ThisProject.Fields
{
    public interface IVisibleField<T, V, TId> : IField<T, TId>
        where T : class, INode<TId>
        where V : class, IView<TId>
    {
        public IFieldVisual<V, TId> Visual { get; }
        public V GetViewById(TId id);
    }
}
