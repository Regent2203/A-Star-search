using Core.Views;
using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Core.Implementations.Vertexes
{
    public class VertexView : MonoBehaviour, IView<int>, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        [SerializeField]
        private GameObject _startMarker;
        [SerializeField]
        private GameObject _finishMarker;
        [SerializeField]
        private SpriteRenderer _spriteRenderer;

        private int _id;
        public int Id => _id;

        private Action<int, Vector2> _onDragBeginCallback;
        private Action<int, Vector2> _onDragEndCallback;


        private void Awake()
        {
            //todo
        }

        public void Init(int id, Vector2 position, Action<int, Vector2> onDragBeginCallback, Action<int, Vector2> onDragEndCallback)
        {
            _id = id;
            name = $"Vertex {id}";
            transform.position = position;

            _onDragBeginCallback = onDragBeginCallback;
            _onDragEndCallback = onDragEndCallback;
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
            return _spriteRenderer.size;
        }

        public Vector3 GetCenterCoords()
        {
            return _spriteRenderer.bounds.center;
        }

        private Vector3? _oldPosition;
        private Vector3? _dragOffset;

        public void OnBeginDrag(PointerEventData eventData)
        {
            _oldPosition = transform.position;

            // Вычисляем смещение, чтобы спрайт не "прыгал" центром в курсор
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(eventData.position);
            _dragOffset = transform.position - new Vector3(mousePos.x, mousePos.y, transform.position.z);

            _onDragBeginCallback?.Invoke(Id, (Vector2)_oldPosition); //todo: hide visual links
        }

        public void OnDrag(PointerEventData eventData)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(eventData.position);
            transform.position = new Vector3(mousePos.x, mousePos.y, transform.position.z) + (Vector3)_dragOffset;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            //if not too close to another node and inside field borders - success
            //else use _oldPosition
            _oldPosition = null;
            _dragOffset = null;

            _onDragEndCallback?.Invoke(Id, transform.position);
        }
    }
}