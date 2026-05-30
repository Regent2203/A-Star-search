using UnityEngine;

namespace ThisProject.Nodes
{
    public interface IMovableNode : INode
    {
        public bool TryMove(Vector2 position);
    }

    public interface IMovableNode<TId> : IMovableNode, INode<TId>
    { }
}