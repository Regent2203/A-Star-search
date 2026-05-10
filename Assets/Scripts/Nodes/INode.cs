using UnityEngine;

namespace Core.Nodes
{
    public interface INode<TId>
    {
        public TId Id { get; }
        public Vector2 NodePosition { get; }
        public bool IsBlocked { get; }
    }
}