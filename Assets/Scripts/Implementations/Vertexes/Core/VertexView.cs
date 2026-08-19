using EasyField.Nodes;
using UnityEngine;

namespace EasyField.Implementations.Vertexes
{
    public class VertexView : NodeView<int>
    {
        [SerializeField]
        private GameObject _blockedMarker;

        protected override string BasicName => "VertexView";


        protected override void ClearGraphics()
        {
            base.ClearGraphics();

            ShowBlockedMarker(false);
        }

        public void ShowBlockedMarker(bool show)
        {
            _blockedMarker.SetActive(show);
        }
    }
}