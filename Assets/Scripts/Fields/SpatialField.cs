using ThisProject.Nodes;
using ThisProject.ObjectsStorages;
using ThisProject.Views;
using UnityEngine;
using Zenject;

namespace ThisProject.Fields
{
    public abstract class SpatialField<T, V, TId> : VisibleField<T, V, TId>
        where T : class, INode<TId>
        where V : class, IView<TId>
    {
        [SerializeField]
        protected BoxCollider2D _collider;

        protected DictTypeStorage<T, TId> _nodes;
        protected DictTypeStorage<V, TId> _views;

        public override IObjectsStorage<T, TId> Nodes => _nodes;
        public override IObjectsStorage<V, TId> Views => _views;

        public override BoxCollider2D Box => _collider;


        [Inject]
        public void Construct(DictTypeStorage<T, TId> nodes, DictTypeStorage<V, TId> views)
        {
            _nodes = nodes;
            _views = views;
        }

        public void AddFieldData(T node, V view)
        {
            _nodes.TryAddData(node.Id, node);
            _views.TryAddData(view.Id, view);
        }

        public void RemoveFieldData(TId id)
        {
            //todo: move into pool

            _nodes.TryRemoveData(id);
            _views.TryRemoveData(id);
        }
    }
}