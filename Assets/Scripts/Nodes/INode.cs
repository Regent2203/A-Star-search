using UnityEngine;

namespace ThisProject.Nodes
{
    public interface INode
    {
        public Vector2 NodePosition { get; }
        public bool IsBlocked { get; }
    }

    public interface INode<TId> : INode
    {
        public TId Id { get; }
    }
}