using EasyField.Inputs;
using EasyField.Nodes;
using EasyField.ObjectsStorages;
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace EasyField.Fields.ClickHandlers
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class GridClickHandler<TNodeView> : FieldClickHandler, IFieldClickHandler<TNodeView> 
        where TNodeView : MonoBehaviour, INodeView<Vector2Int>
    {
        private GridField _field;
        private GridTypeStorage<TNodeView> _nodeViews;

        public event Action<TNodeView, PointerEventData.InputButton, InputSnapshot> NodeViewClicked;


        [Inject]
        public void Construct(GridField field, GridTypeStorage<TNodeView> nodeViews)
        {
            _field = field;
            _nodeViews = nodeViews;
        }

        void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
        {
            if (eventData.TryGetHitObject(out var hitObject))
            {
                if (!CheckHitNodeView(hitObject, eventData))
                    HitField(eventData);
            }
        }

        protected bool CheckHitNodeView(GameObject hitObject, PointerEventData eventData)
        {
            var index = _field.PositionToIndex(eventData.pointerCurrentRaycast.worldPosition);
            var nodeView = _nodeViews.GetItem(index);

            if (nodeView != null)
            {
                Debug.Log("GridNodeView clicked");
                NodeViewClicked?.Invoke(nodeView, eventData.button, _inputService.CreateSnapshot());
                return true;
            }

            return false;
        }
    }
}