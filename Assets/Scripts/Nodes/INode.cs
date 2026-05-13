using System;
using UnityEngine;

namespace Core.Nodes
{
    public interface INode
    {
        public Vector2 NodePosition { get; }
        public bool IsBlocked { get; }

        public event Action<Vector2> NodePositionChanged;
    }

    public interface INode<out TId> : INode
    {
        TId Id { get; }
    }
}