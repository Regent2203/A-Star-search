using System;
using UnityEngine;

namespace ThisProject.Nodes
{
    public interface IMovableNode : INode
    {
        public void Move(Vector2 position);

        //public event Action<Vector2> NodePositionChanged;
    }

    public interface IMovableNode<TId> : IMovableNode, INode<TId>
    { }
}