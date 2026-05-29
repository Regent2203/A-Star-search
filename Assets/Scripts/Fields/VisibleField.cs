using ThisProject.Nodes;
using ThisProject.ObjectsStorages;
using ThisProject.Views;
using UnityEngine;

namespace ThisProject.Fields
{
    public abstract class VisibleField<T, V, TId> : MonoBehaviour, IVisibleField<T, V, TId>
        where T : class, INode<TId>
        where V : class, IView<TId>
    {
        public abstract IObjectsStorage<T, TId> Nodes { get; }
        public abstract IObjectsStorage<V, TId> Views { get; }

        public abstract BoxCollider2D Box { get; }

        public T GetNodeById(TId id) => Nodes.GetById(id);
        public V GetViewById(TId id) => Views.GetById(id);
    }
}