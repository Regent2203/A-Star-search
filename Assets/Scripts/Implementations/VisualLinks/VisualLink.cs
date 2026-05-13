using Core.Links;
using Core.Nodes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Implementations
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