using UnityEngine;
using Zenject;

namespace ThisProject.Nodes
{
    public abstract class NodeData<TId> : INodeData<TId>, IPoolable<TId, Vector2>
    {
        protected TId _id;
        protected Vector2 _nodePosition;
        protected bool _isBlocked;

        public TId Id => _id;
        public Vector2 NodePosition => _nodePosition;
        public virtual bool IsBlocked => _isBlocked;


        public virtual void OnSpawned(TId id, Vector2 nodePosition)
        {
            _id = id;
            _nodePosition = nodePosition;
            _isBlocked = false;
        }

        public virtual void OnDespawned()
        {
            _id = default;
            _nodePosition = Vector2.zero;
            _isBlocked = false;
        }


        public bool TryChangeNodePosition(Vector2 position)
        {
            if (position != _nodePosition)
            {
                _nodePosition = position;
                return true;
            }
            return false;
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
    }
}