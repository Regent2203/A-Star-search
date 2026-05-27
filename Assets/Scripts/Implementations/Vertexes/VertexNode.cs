using ThisProject.Nodes;
using System;
using UnityEngine;

namespace ThisProject.Implementations.Vertexes
{
    public class VertexNode : IMovableNode<int>
    {
        private readonly int _id;
        private Vector2 _nodePosition;
        private bool _isBlocked;
        private Action<VertexNode, Vector2> _nodeMovedCallback;
        private Action<bool> _nodeBlockedChangedCallback;

        public int Id => _id;
        public Vector2 NodePosition => _nodePosition;
        public bool IsBlocked => _isBlocked;

        public event Action<INode, Vector2> NodePositionChanged
        {
            add => _nodeMovedCallback += value;
            remove => _nodeMovedCallback -= value;
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
            _nodeMovedCallback?.Invoke(this, _nodePosition);
        }
    }
}