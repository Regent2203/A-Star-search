using Core.Views;
using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Core.Implementations.Vertexes
{
    public class VertexView : MonoBehaviour, IView, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        [SerializeField]
        private SpriteRenderer _spriteRenderer;

        private int _id;
        public int Id => _id;

        private Vector2? _size;
        private Vector3? _center;

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


        private Vector3 offset;

        public void OnBeginDrag(PointerEventData eventData)
        {
            // Вычисляем смещение, чтобы спрайт не "прыгал" центром в курсор
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(eventData.position);
            offset = transform.position - new Vector3(mousePos.x, mousePos.y, transform.position.z);
        }

        public void OnDrag(PointerEventData eventData)
        {
            // Получаем позицию мыши в мировых координатах
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(eventData.position);
            // Обновляем позицию с учетом смещения
            transform.position = new Vector3(mousePos.x, mousePos.y, transform.position.z) + offset;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            OnDragEnd?.Invoke();
        }
    }
}