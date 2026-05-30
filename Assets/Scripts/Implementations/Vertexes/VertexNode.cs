using System;
using ThisProject.Nodes;
using UnityEngine;

namespace ThisProject.Implementations.Vertexes
{
    public class VertexNode : IMovableNode<int>
    {
        private readonly int _id;
        private Vector2 _nodePosition;
        private bool _isBlocked;

        public int Id => _id;
        public Vector2 NodePosition => _nodePosition;
        public bool IsBlocked => _isBlocked;


        public VertexNode(int id, Vector2 nodePosition)
        {
            _id = id;
            _nodePosition = nodePosition;
        }

        public bool TrySetBlocked(bool blocked)
        {
            if (blocked != _isBlocked)
            {
                _isBlocked = blocked;
                return true;
            }
            return false;
        }

        public bool TryMove(Vector2 position)
        {
            if (position != _nodePosition)
            {
                _nodePosition = position;
                return true;
            }
            return false;
        }
    }
}