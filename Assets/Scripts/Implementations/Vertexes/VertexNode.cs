using Core.Links;
using Core.Nodes;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Implementations.Vertexes
{
    public class VertexNode : INode<VertexNode, int>
    {
        private readonly int _id;
        private readonly Vector2 _position;
        private bool _isBlocked;
        private List<ILink<VertexNode, int>> _links = new List<ILink<VertexNode, int>>();

        private readonly VertexesField _field;

        public int Id => _id;
        public Vector2 NodePosition => _position;
        public bool IsBlocked => _isBlocked;
        public float MoveCost => 0;


        public VertexNode(Vector2 position, int id, VertexesField field)
        {
            _position = position;
            _id = id;
            _field = field;

            //_field.NotifyNodePosition(this);
            //_field.NotifyNodeWeightChanged(this);
            //isDragged
        }

        public void SetBlocked(bool blocked)
        {
            _isBlocked = blocked;
        }

        public IEnumerable<ILink<VertexNode, int>> GetLinks() => _links;
    }
}