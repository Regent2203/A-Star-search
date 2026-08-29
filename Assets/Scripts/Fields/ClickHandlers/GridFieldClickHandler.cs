using EasyField.Inputs;
using EasyField.Links;
using EasyField.Nodes;
using EasyField.ObjectsStorages;
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace EasyField.Fields.ClickHandlers
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class GridFieldClickHandler<TNodeView> : FieldClickHandler, IFieldClickHandler<TNodeView> 
        where TNodeView : MonoBehaviour, INodeView<Vector2Int>
    {
        private Grid _grid;
        private IObjectsStorage<TNodeView, Vector2Int> _nodeViews;

        public event Action<TNodeView, PointerEventData.InputButton, InputSnapshot> NodeViewClicked;


        [Inject]
        public void Construct(GridField field, GridTypeStorage<TNodeView> nodeViews)
        {
            _grid = field.Grid;
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
            var index = (Vector2Int)_grid.WorldToCell(eventData.pointerCurrentRaycast.worldPosition);
            CorrectIndexByGridType(ref index);

            if (_nodeViews.TryGetItem(index, out var nodeView))
            {
                NodeViewClicked?.Invoke(nodeView, eventData.button, _inputService.CreateSnapshot());
                return true;
            }

            return false;
        }

        protected virtual void CorrectIndexByGridType(ref Vector2Int index)
        {
        }
    }


    [RequireComponent(typeof(BoxCollider2D))]
    public class GridFieldClickHandler<TNodeView, TLinkView> : GridFieldClickHandler<TNodeView>, IFieldClickHandler<TNodeView, TLinkView>
        where TNodeView : MonoBehaviour, INodeView<Vector2Int>
        where TLinkView : MonoBehaviour, ILinkView
    {
        public event Action<TLinkView, PointerEventData.InputButton, InputSnapshot> LinkViewClicked;


        public override void OnPointerDown(PointerEventData eventData)
        {
            if (eventData.TryGetHitObject(out var hitObject))
            {
                if (!CheckHitLinkView(hitObject, eventData))
                    if (!CheckHitNodeView(hitObject, eventData))
                        HitField(eventData);
            }
        }

        protected bool CheckHitLinkView(GameObject hitObject, PointerEventData eventData)
        {
            var linkView = hitObject.GetComponentInParent<TLinkView>();

            if (linkView != null)
            {
                LinkViewClicked?.Invoke(linkView, eventData.button, _inputService.CreateSnapshot());
                return true;
            }
            return false;
        }
    }
}