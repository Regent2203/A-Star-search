using Core.Links;
using Core.Nodes;
using UnityEngine;

namespace Core.Implementations.VisualLinks
{
    public class VisualLink<T> : MonoBehaviour where T : class, INode
    {
        [SerializeField]
        private LineRenderer _lineRenderer;

        private ILink<T> _link;


        public void Bind(ILink<T> link)
        {
            _link = link;
            _link.From.NodePositionChanged += (_) => UpdatePositions();
            _link.To.NodePositionChanged += (_) => UpdatePositions();
        }

        private void UpdatePositions()
        {
            _lineRenderer.SetPosition(0, _link.From.NodePosition);
            _lineRenderer.SetPosition(1, _link.To.NodePosition);
        }
    }
}