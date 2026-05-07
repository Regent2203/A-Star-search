using Core.Implementations.Cells;
using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Core.Fields.Grids
{
    public class CellsGridInputHandler : MonoBehaviour, IPointerDownHandler
    {
        [SerializeField]
        private CellsGridField _field;
        [SerializeField] 
        private Grid _grid;

        public event Action<CellView, PointerEventData.InputButton> CellClicked;


        public void OnPointerDown(PointerEventData eventData)
        {
            Vector3 localPos = transform.InverseTransformPoint(eventData.pointerCurrentRaycast.worldPosition);

            int x = Mathf.FloorToInt(localPos.x / _grid.cellSize.x);
            int y = Mathf.FloorToInt(localPos.y / _grid.cellSize.y);

            var view = _field.GetViewByIndex(x, y);
            if (view != null)
            {
                CellClicked?.Invoke(view, eventData.button);
            }
        }
    }
}