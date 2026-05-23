using Core.Inputs;
using Core.Nodes;
using Core.ObjectsStorages;
using Core.Views;
using UnityEngine;
using Zenject;

namespace Core.Fields.Spatials
{
    public class VisualSpatialField<T, V> : VisualField<T, V, int>
        where T : class, INode<int>
        where V : class, IView<int>
    {
        [SerializeField]
        protected BoxCollider2D _collider;

        protected SpatialFieldCore<T> _core;
        protected FieldClickHandler<T, V, int> _clickHandler;
        protected DictTypeStorage<V> _views;

        protected override IField<T, int> Core => _core;
        protected override IClickHandler ClickHandler => _clickHandler;
        public override IObjectsStorage<V, int> Views => _views;


        [Inject]
        public void Construct(DictTypeStorage<V> views, SpatialFieldCore<T> core, FieldClickHandler<T, V, int> clickHandler, V cellViewPrefab)
        {
            _views = views;
            _core = core;
            _clickHandler = clickHandler;
            _viewPrefab = cellViewPrefab;
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