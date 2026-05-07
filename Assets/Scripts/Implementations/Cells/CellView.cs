using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Core.Implementations.Cells
{
    public class CellView : MonoBehaviour, IView
    {
        [SerializeField]
        private GameObject _pathMarker;
        [SerializeField]
        private GameObject _startMarker;
        [SerializeField]
        private GameObject _finishMarker;        
        [SerializeField]
        private SpriteRenderer _spriteRenderer;

        private CellNode _node;
        //private Vector2 _viewSize;
        //private Vector3 _viewCenter;

        public CellNode Node => _node;
        


        private void Awake()
        {
            //_viewSize = _spriteRenderer.size;
            //_viewCenter = _spriteRenderer.bounds.center;

            ShowPathMarker(false);
            ShowStartMarker(false);
            ShowFinishMarker(false);
        }

        public void Init(Vector2Int index, Vector2 scale)
        {
            transform.localScale = scale;
            name = $"Cell {index.x},{index.y}";
        }

        public void ShowPathMarker(bool show)
        {
            _pathMarker.SetActive(show);
        }
        public void ShowStartMarker(bool show)
        {
            _startMarker.SetActive(show);
        }
        public void ShowFinishMarker(bool show)
        {
            _finishMarker.SetActive(show);
        }

        public Vector2 GetSize() => _spriteRenderer.size;
        public Vector3 GetCenterCoords() => _spriteRenderer.bounds.center;
    }
}