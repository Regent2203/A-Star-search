using ThisProject.Views;
using ThisProject.ObjectsStorages;

namespace ThisProject.Fields
{
    public interface IFieldVisual<V, TId>
        where V : class, IView<TId>
    {
        public IObjectsStorage<V, TId> Views { get; }
    }
}
