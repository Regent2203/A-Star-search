using UnityEngine;

namespace Core.Nodes
{
    public interface INode
    {
        public Vector2 NodePosition { get; }
        public bool IsBlocked { get; }
    }

    public interface INode<out TId> : INode
    {
        TId Id { get; }
    }
}