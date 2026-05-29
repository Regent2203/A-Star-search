using ThisProject.Fields.ClickHandlers;
using ThisProject.Nodes;
using ThisProject.ObjectsStorages;
using ThisProject.Views;
using UnityEngine;
using Zenject;

namespace ThisProject.Fields
{
    public abstract class SpatialField<T, V> : VisibleField<T, V, int>
        where T : class, INode<int>
        where V : class, IView<int>
    {
        [SerializeField]
        protected BoxCollider2D _collider;

        protected DictTypeStorage<T> _nodes;
        protected DictTypeStorage<V> _views;

        public override IObjectsStorage<T, int> Nodes => _nodes;
        public override IObjectsStorage<V, int> Views => _views;

        public override BoxCollider2D Box => _collider;


        [Inject]
        public void Construct(DictTypeStorage<T> nodes, DictTypeStorage<V> views, SpatialClickHandler<T, V, int> clickHandler)
        {
            _nodes = nodes;
            _views = views;
        }

        protected void Awake()
        {
            Init();
        }

        protected virtual void Init()
        {
            
        }
    }
}