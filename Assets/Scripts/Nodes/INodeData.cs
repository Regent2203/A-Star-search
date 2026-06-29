using UnityEngine;

namespace ThisProject.Nodes
{
    public interface INodeData
    {
        public Vector2 NodePosition { get; }
        public bool IsBlocked { get; }

        public bool TryChangeNodePosition(Vector2 position);

        public bool TrySetBlocked(bool blocked);
    }

    public interface INodeData<TId> : INodeData
    {
        public TId Id { get; }
    }
}