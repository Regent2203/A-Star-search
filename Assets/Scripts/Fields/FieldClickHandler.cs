using Core.Inputs;
using Core.Nodes;
using System;
using UnityEngine.EventSystems;
using Core.Implementations.Vertexes;

namespace Core.Fields
{
    public class FieldClickHandler<T> where T : class, INode<int>
    {
        private IGraph<T, int> _field;
        private Action<T, PointerEventData.InputButton, InputSnapshot> _nodeClickedCallback;


        private readonly IInputService _inputService;


        public FieldClickHandler(IInputService inputService)
        {
            _inputService = inputService;
        }

        public void SetConfiguration(IGraph<T, int> field, Action<T, PointerEventData.InputButton, InputSnapshot> nodeClickedCallback)
        {
            _field = field;
            _nodeClickedCallback = nodeClickedCallback;
        }

        
        public bool ProcessClick(PointerEventData eventData)
        {
            var hitObject = eventData.pointerCurrentRaycast.gameObject;

            if (hitObject != null && hitObject.TryGetComponent<VertexView>(out var view)) //todo type
            {
                var node = _field.GetNodeById(view.Id);
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
