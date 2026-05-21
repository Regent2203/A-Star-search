using Core.Inputs;
using System;
using UnityEngine.EventSystems;
using Core.ObjectsStorages;
using Core.Views;
using Core.Nodes;
using UnityEngine;

namespace Core.Fields
{
    public class FieldClickHandler<T, V, TId> where T: class, INode<TId> where V : class, IView<TId>
    {
        private IObjectsStorage<T, TId> _nodes;
        private IObjectsStorage<V, TId> _views;
        private Action<T, PointerEventData.InputButton, InputSnapshot> _nodeClickedCallback;


        private readonly IInputService _inputService;


        public FieldClickHandler(IInputService inputService)
        {
            _inputService = inputService;
        }

        public void SetConfiguration(IObjectsStorage<T, TId> nodes, IObjectsStorage<V, TId> views, Action<T, PointerEventData.InputButton, InputSnapshot> nodeClickedCallback)
        {
            _nodes = nodes;
            _views = views;
            _nodeClickedCallback = nodeClickedCallback;
        }

        
        public bool ProcessClick(PointerEventData eventData)
        {
            var hitObject = eventData.pointerCurrentRaycast.gameObject;

            if (hitObject != null && hitObject.TryGetComponent<V>(out var view)) //todo type
            {
                Debug.Log(view);
                Debug.Log(view.Id);
                var node = _nodes.GetById(view.Id);
                if (node != null)
                {
                    _nodeClickedCallback?.Invoke(node, eventData.button, _inputService.CreateSnapshot());
                    return true;
                }
            }

            return false;
        }
    }
}
