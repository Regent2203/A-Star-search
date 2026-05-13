using Core.Views;
using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Core.Implementations.Vertexes
{
    public class VertexView : MonoBehaviour, IView, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        [SerializeField]
        private GameObject _startMarker;
        [SerializeField]
        private GameObject _finishMarker;
        [SerializeField]
        private SpriteRenderer _spriteRenderer;

        private int _id;
        public int Id => _id;

        private Vector2? _size;
        private Vector3? _center;

        public event Action OnDragBegin;
        public event Action OnDragEnd;


        private void Awake()
        {
            //todo
        }

        public void Init(int id, Vector2 position)
        {
            _id = id;
            name = $"Vertex {id}";
            transform.position = position;
        }

        public void ShowStartMarker(bool show)
        {
            _startMarker.SetActive(show);
        }
        public void ShowFinishMarker(bool show)
        {
            _finishMarker.SetActive(show);
        }
        public void UpdateSprite(Sprite sprite)
        {
            _spriteRenderer.sprite = sprite;
        }


        public Vector2 GetSize()
        {
            _size ??= _spriteRenderer.size;
            return _size.Value;
        }

        public Vector3 GetCenterCoords()
        {
            _center ??= _spriteRenderer.bounds.center;
            return _center.Value;
        }

        private Vector3 _oldPosition;
        private Vector3 _dragOffset;

        public void OnBeginDrag(PointerEventData eventData)
        {
            _oldPosition = transform.position;

            // Вычисляем смещение, чтобы спрайт не "прыгал" центром в курсор
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(eventData.position);
            _dragOffset = transform.position - new Vector3(mousePos.x, mousePos.y, transform.position.z);

            OnDragBegin?.Invoke(); //todo: hide visual links
        }

        public void OnDrag(PointerEventData eventData)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(eventData.position);
            transform.position = new Vector3(mousePos.x, mousePos.y, transform.position.z) + _dragOffset;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            //if not too close to another node and inside field borders - success
            //else use _oldPosition
            OnDragEnd?.Invoke();
        }
    }
}