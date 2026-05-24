using ThisProject.Nodes;
using System;
using UnityEngine;

namespace ThisProject.Implementations.Vertexes
{
    public class VertexNode : INode<int>
    {
        private readonly int _id;
        private Vector2 _nodePosition;
        private bool _isBlocked;
        private Action<Vector2> _nodePositionChangedCallback;
        private Action<bool> _nodeBlockedChangedCallback;

        public int Id => _id;
        public Vector2 NodePosition => _nodePosition;
        public bool IsBlocked => _isBlocked;

        public event Action<Vector2> NodePositionChanged
        {
            add => _nodePositionChangedCallback += value;
            remove => _nodePositionChangedCallback -= value;
        }
        public event Action<bool> NodeBlockedChanged
        {
            add => _nodeBlockedChangedCallback += value;
            remove => _nodeBlockedChangedCallback -= value;
        }


        public VertexNode(int id, Vector2 nodePosition)
        {
            _id = id;
            _nodePosition = nodePosition;
        }

        public void SetBlocked(bool blocked)
        {
            if (blocked == _isBlocked)
                return;

            _isBlocked = blocked;
            _nodeBlockedChangedCallback?.Invoke(_isBlocked);
        }

        public void Move(Vector2 position)
        {
            if (position == _nodePosition)
                return;

            _nodePosition = position;
            _nodePositionChangedCallback?.Invoke(_nodePosition);
        }
    }
}