using EasyField.Inputs;
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace EasyField.Fields.ClickHandlers
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class FieldClickHandler : MonoBehaviour, IFieldClickHandler
    {
        protected Camera _mainCamera;
        protected IInputService _inputService;

        public event Action<Vector2, PointerEventData.InputButton, InputSnapshot> FieldClicked;


        [Inject]
        public void Construct(Camera camera, IInputService inputService)
        {
            _mainCamera = camera;
            _inputService = inputService;
        }
        
        public virtual void OnPointerDown(PointerEventData eventData)
        {
            if (eventData.TryGetHitObject(out var hitObject))
            {
                HitField(eventData);
            }
        }

        protected void HitField(PointerEventData eventData)
        {
            FieldClicked?.Invoke(eventData.pointerCurrentRaycast.worldPosition, eventData.button, _inputService.CreateSnapshot());
        }
    }
}