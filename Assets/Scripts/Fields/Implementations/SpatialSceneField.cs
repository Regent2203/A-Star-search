using ThisProject.Fields.ClickHandlers;
using ThisProject.Nodes;
using ThisProject.ObjectsStorages;
using ThisProject.Views;
using UnityEngine;
using Zenject;

namespace ThisProject.Fields.Implementations
{
    public class SpatialSceneField<T, V> : SceneField<T, V, int>
        where T : class, INode<int>
        where V : class, IView<int>
    {
        [SerializeField]
        protected BoxCollider2D _collider;

        protected DictTypeStorage<T> _nodes;
        protected DictTypeStorage<V> _views;
        protected CommonClickHandler<T, V, int> _clickHandler;

        public override IObjectsStorage<T, int> Nodes => _nodes;
        public override IObjectsStorage<V, int> Views => _views;
        public override IClickHandler ClickHandler => _clickHandler;
        


        [Inject]
        public void Construct(DictTypeStorage<T> nodes, DictTypeStorage<V> views, CommonClickHandler<T, V, int> clickHandler)
        {
            _nodes = nodes;
            _views = views;
            _clickHandler = clickHandler;
        }

        protected void Awake()
        {
            Init();
        }

        protected virtual void Init()
        {
            _clickHandler.SetConfiguration(NotifyNodeClicked, NotifyFieldClicked);
        }
    }
}