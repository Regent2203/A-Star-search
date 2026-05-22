using Core.Fields.Grids;
using Core.Inputs;
using Core.Nodes;
using Core.ObjectsStorages;
using Core.Views;
using System;
using UnityEngine.EventSystems;
using UnityEngine;
using Zenject;
using System.Collections.Generic;

namespace Core.Fields.Spatials
{
    public class SpatialField<T, V> : MonoBehaviour, IPointerDownHandler, IVisualField<T, V, int>
        where T : class, INode<int>
        where V : class, IView<int>
    {
        [SerializeField]
        protected BoxCollider2D _collider;

        protected SpatialFieldCore<T> _core;

        protected V _viewPrefab;

        protected DictTypeStorage<V> _views;
        public IObjectsStorage<V, int> Views => _views;

        protected FieldClickHandler<T, V, int> _clickHandler;

        public IObjectsStorage<T, int> Nodes => _core.Nodes;

        public event Action<T, PointerEventData.InputButton, InputSnapshot> NodeClicked;
        public event Action FieldChanged; //todo _core

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
            
        }

        public void SetFieldData(Dictionary<int, T> nodes, Dictionary<int, V> views)
        {
            _core.SetNodesData(nodes);
            _views.SetData(views);
        }

        public V GetViewById(int id)
        {
            return _views.GetById(id);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (!_clickHandler.ProcessClick(eventData))
            {
                //FieldClicked?.Invoke(this, eventData.button, _inputService.CreateSnapshot()); //todo
            }
        }

        protected void NotifyNodeClicked(T node, PointerEventData.InputButton btn, InputSnapshot input)
        {
            NodeClicked?.Invoke(node, btn, input);
        }

        public T GetNodeById(int id)
        {
            return _core.GetNodeById(id);
        }
    }
}