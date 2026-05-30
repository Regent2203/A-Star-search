using System;
using ThisProject.Inputs;
using ThisProject.Nodes;
using ThisProject.Views;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace ThisProject.Fields.ClickHandlers
{
    public class SpatialClickHandler<T, V, TId> : MonoBehaviour, IClickHandler<T>
        where T : class, INode<TId> 
        where V : class, IView<TId>
    {
        private SpatialField<T, V, TId> _field;
        private IInputService _inputService;

        public event Action<T, PointerEventData.InputButton, InputSnapshot> NodeClicked;
        public event Action<PointerEventData.InputButton, InputSnapshot> FieldClicked;


        [Inject]
        public void Construct(SpatialField<T, V, TId> field, IInputService inputService)
        {
            _field = field;
            _inputService = inputService;
        }

        void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
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

            FieldClicked?.Invoke(eventData.button, _inputService.CreateSnapshot());
        }
    }
}