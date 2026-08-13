using EasyField.Inputs;
using EasyField.Links;
using EasyField.Nodes;
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace EasyField.Fields.ClickHandlers
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class SpatialClickHandler<TNodeView> : MonoBehaviour, IFieldClickHandler<TNodeView>
        where TNodeView : MonoBehaviour, INodeView
    {
        protected Camera _mainCamera;
        protected IInputService _inputService;

        public event Action<TNodeView, PointerEventData.InputButton, InputSnapshot> NodeViewClicked;
        public event Action<Vector2, PointerEventData.InputButton, InputSnapshot> FieldClicked;


        [Inject]
        public void Construct(Camera camera, IInputService inputService)
        {
            _mainCamera = camera;
            _inputService = inputService;
        }

        public virtual void OnPointerDown(PointerEventData eventData)
        {
            var hitObject = eventData.pointerCurrentRaycast.gameObject;

            if (!CheckHitNodeView(hitObject, eventData))
                HitField(eventData);            
        }

        protected bool CheckHitNodeView(GameObject hitObject, PointerEventData eventData)
        {
            if (hitObject != null && hitObject.TryGetComponent<TNodeView>(out var nodeView))
            {
                Debug.Log("NodeView clicked");
                NodeViewClicked?.Invoke(nodeView, eventData.button, _inputService.CreateSnapshot());
                return true;
            }
            return false;
        }

        protected void HitField(PointerEventData eventData)
        {
            Vector2 worldPos = _mainCamera.ScreenToWorldPoint(eventData.position);
            FieldClicked?.Invoke(worldPos, eventData.button, _inputService.CreateSnapshot());
        }
    }

    [RequireComponent(typeof(BoxCollider2D))]
    public class SpatialClickHandler<TNodeView, TLinkView> : SpatialClickHandler<TNodeView>, IFieldClickHandler<TNodeView, TLinkView>
        where TNodeView : MonoBehaviour, INodeView
        where TLinkView : MonoBehaviour, ILinkView
    {
        public event Action<TLinkView, PointerEventData.InputButton, InputSnapshot> LinkViewClicked;


        public override void OnPointerDown(PointerEventData eventData)
        {
            var hitObject = eventData.pointerCurrentRaycast.gameObject;
            
            if (!CheckHitLinkView(hitObject, eventData))
                if (!CheckHitNodeView(hitObject, eventData))
                    HitField(eventData);
        }

        protected bool CheckHitLinkView(GameObject hitObject, PointerEventData eventData)
        {
            if (hitObject != null && hitObject.TryGetComponent<TLinkView>(out var linkView))
            {
                Debug.Log("LinkView clicked");
                LinkViewClicked?.Invoke(linkView, eventData.button, _inputService.CreateSnapshot());
                return true;
            }
            return false;
        }
    }
}