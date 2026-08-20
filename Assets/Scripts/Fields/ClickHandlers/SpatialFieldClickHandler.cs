using EasyField.Inputs;
using EasyField.Links;
using EasyField.Nodes;
using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace EasyField.Fields.ClickHandlers
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class SpatialFieldClickHandler<TNodeView> : FieldClickHandler, IFieldClickHandler<TNodeView>
        where TNodeView : MonoBehaviour, INodeView
    {
        public event Action<TNodeView, PointerEventData.InputButton, InputSnapshot> NodeViewClicked;
        

        public override void OnPointerDown(PointerEventData eventData)
        {
            if (eventData.TryGetHitObject(out var hitObject))
            {
                if (!CheckHitNodeView(hitObject, eventData))
                    HitField(eventData);
            }
        }

        protected bool CheckHitNodeView(GameObject hitObject, PointerEventData eventData)
        {
            var nodeView = hitObject.GetComponentInParent<TNodeView>();

            if (nodeView != null)
            {
                NodeViewClicked?.Invoke(nodeView, eventData.button, _inputService.CreateSnapshot());
                return true;
            }
            return false;
        }
    }


    [RequireComponent(typeof(BoxCollider2D))]
    public class SpatialClickHandler<TNodeView, TLinkView> : SpatialFieldClickHandler<TNodeView>, IFieldClickHandler<TNodeView, TLinkView>
        where TNodeView : MonoBehaviour, INodeView
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