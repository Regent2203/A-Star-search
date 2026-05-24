using ThisProject.Inputs;
using ThisProject.Nodes;
using ThisProject.Views;
using System;
using UnityEngine.EventSystems;

namespace ThisProject.Fields
{
    public class FieldClickHandler<T, V, TId> : IClickHandler
        where T : class, INode<TId> 
        where V : class, IView<TId>
    {
        private Action<T, PointerEventData.InputButton, InputSnapshot> _nodeClickedCallback;
        private Action<PointerEventData.InputButton, InputSnapshot> _fieldClickedCallback;

        private readonly IVisualField<T, V, TId> _field;
        private readonly IInputService _inputService;


        public FieldClickHandler(IInputService inputService)
        {
            _inputService = inputService;
        }

        public void SetConfiguration(Action<T, PointerEventData.InputButton, InputSnapshot> nodeClickedCallback,
            Action<PointerEventData.InputButton, InputSnapshot> fieldClickedCallback)
        {
            _nodeClickedCallback = nodeClickedCallback;
            _fieldClickedCallback = fieldClickedCallback;
        }


        public void ProcessClick(PointerEventData eventData)
        {
            var hitObject = eventData.pointerCurrentRaycast.gameObject;

            if (hitObject != null && hitObject.TryGetComponent<V>(out var view))
            {
                var node = _field.Nodes.GetById(view.Id);
                if (node != null)
                {
                    _nodeClickedCallback?.Invoke(node, eventData.button, _inputService.CreateSnapshot());
                    return;
                }
            }
            
            _fieldClickedCallback.Invoke(eventData.button, _inputService.CreateSnapshot());
        }
    }
}
