using ThisProject.Nodes;
using ThisProject.ObjectsStorages;
using ThisProject.Views;
using UnityEngine;

namespace ThisProject.Fields
{
    public interface IVisibleField<T, V, TId> : IVisibleField, ILogicField<T, TId>
        where T : class, INode<TId>
        where V : class, IView<TId>
    {
        public IObjectsStorage<V, TId> Views { get; }
        public V GetViewById(TId id) => Views.GetById(id);
    }

    public interface IVisibleField
    {
        public BoxCollider2D Box { get; }

        public bool CheckAndAdjustPoint(ref Vector2 pos);
    }
}
