using Core.Nodes;
using System;
using UnityEngine;

namespace Core.Implementations.Vertexes
{
    public class VertexNode : INode<int>
    {
        private readonly int _id;
        private Vector2 _position;
        private bool _isBlocked;

        public int Id => _id;
        public Vector2 NodePosition => _position;
        public bool IsBlocked => _isBlocked;

        public event Action<Vector2> NodePositionChanged;
        public event Action<bool> NodeBlockedChanged;


        public VertexNode(Vector2 position, int id)
        {
            _position = position;
            _id = id;

            //_field.NotifyNodePositionChanged(this);
            //isDragged
        }

        public void SetBlocked(bool blocked)
        {
            if (blocked == _isBlocked)
                return;

            _isBlocked = blocked;
            NodeBlockedChanged?.Invoke(_isBlocked);
        }

        public void MoveNode(Vector2 position)
        {
            _position = position;
            NodePositionChanged?.Invoke(_position);
        }
    }
}