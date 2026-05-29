using System;
using ThisProject.Inputs;
using ThisProject.Nodes;
using ThisProject.Views;
using UnityEngine.EventSystems;

namespace ThisProject.Fields.ClickHandlers
{
    public class SpatialClickHandler<T, V, TId> : IClickHandler<T>
        where T : class, INode<TId> 
        where V : class, IView<TId>
    {
        private readonly IVisibleField<T, V, TId> _field;
        private readonly IInputService _inputService;

        public event Action<T, PointerEventData.InputButton, InputSnapshot> NodeClicked;
        public event Action<PointerEventData.InputButton, InputSnapshot> FieldClicked;


        public SpatialClickHandler(IInputService inputService)
        {
            _inputService = inputService;
        }

        public void ProcessClick(PointerEventData eventData)
        {
            var hitObject = eventData.pointerCurrentRaycast.gameObject;

            if (hitObject != null && hitObject.TryGetComponent<V>(out var view))
            {
                var node = _field.GetNodeById(view.Id);
                if (node != null)
                {
                    NodeClicked?.Invoke(node, eventData.button, _inputService.CreateSnapshot());
                    return;
                }
            }

            FieldClicked.Invoke(eventData.button, _inputService.CreateSnapshot());
        }
    }
}
