using ThisProject.Implementations.Vertexes;
using ThisProject.Links;
using ThisProject.Nodes;
using UnityEngine;

namespace ThisProject.Implementations.VisualLinks
{
    public class VisualLink : VisualLink<VertexNode>
    {
    }

    public class VisualLink<T> : MonoBehaviour where T : class, INode
    {
        [SerializeField]
        private LineRenderer _lineRenderer;

        private ILink<T> _link;


        public void Bind(ILink<T> link)
        {
            _link = link;
            _link.From.NodePositionChanged += OnNodePositionChanged; //todo
            _link.To.NodePositionChanged += OnNodePositionChanged; //todo

            UpdatePositions();
        }

        private void OnNodePositionChanged(INode node, Vector2 pos)
        {
            UpdatePositions();
        }

        private void UpdatePositions()
        {
            _lineRenderer.SetPosition(0, _link.From.NodePosition);
            _lineRenderer.SetPosition(1, _link.To.NodePosition);
        }
    }
}