using EasyField.Nodes;
using UnityEngine;

namespace EasyField.Implementations.Cells
{
    public class CellView : NodeView<Vector2Int>
    {
        [SerializeField]
        private GameObject _pathMarker;

        protected override string BasicName => "CellView";


        protected override void ClearGraphics()
        {
            base.ClearGraphics();

            ShowPathMarker(false);
        }
        
        public void ShowPathMarker(bool show)
        {
            _pathMarker.SetActive(show);
        }
    }
}