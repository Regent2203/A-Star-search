using ThisProject.Fields.Grids;
using ThisProject.Inputs;
using ThisProject.Nodes;
using ThisProject.ObjectsStorages;
using ThisProject.Views;
using UnityEngine;
using Zenject;

namespace ThisProject.Fields.Spatials
{
    public class VisibleSpatialField<T, V> : VisibleField<T, V, int>
        where T : class, INode<int>
        where V : class, IView<int>
    {
        [SerializeField]
        protected BoxCollider2D _collider;

        protected SpatialFieldCore<T> _core;
        protected SpatialFieldVisual<V> _views;
        protected FieldClickHandler<T, V, int> _clickHandler;

        public override IFieldCore<T, int> Core => _core;
        public override IFieldVisual<V, int> Visual => _views;
        public override IClickHandler ClickHandler => _clickHandler;
        


        [Inject]
        public void Construct(SpatialFieldCore<T> core, SpatialFieldVisual<V> views, FieldClickHandler<T, V, int> clickHandler)
        {
            _views = views;
            _core = core;
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