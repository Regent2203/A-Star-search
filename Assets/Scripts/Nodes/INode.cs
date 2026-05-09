using UnityEngine;

namespace Core.Nodes
{
    public interface INode<T, TId> where T: class, INode<T, TId>
    {
        public TId Id { get; }
        public Vector2 NodePosition { get; }
        public bool IsBlocked { get; }
    }
}